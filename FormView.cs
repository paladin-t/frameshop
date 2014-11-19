using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using Frameshop.Data;

namespace Frameshop
{
    internal partial class FormView : FormDockable<FormView>
    {
        #region Fields

        private bool zooming = false;

        private PageCache pageCache = new PageCache();

        private bool resettingPage = false;

        private PList latestLoadedPList = null;

        #endregion

        #region Properties

        private float ZoomFactor
        {
            get
            {
                return atlasBox.Zoom;
            }
            set
            {
                atlasBox.Zoom = value;
            }
        }
        
        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    atlasBox.PageIndex = value;
                }

                Page p = Package.Current.GetPage(selectedIndex);
                if (FormProperty.GetInstance().SelectedObject != p)
                    FormProperty.GetInstance().SelectedObject = p;
            }
        }

        public string OrignalTitle
        {
            get
            {
                return "View";
            }
        }

        #endregion

        #region Constructor
        
        public FormView()
        {
            InitializeComponent();

            atlasBox.Dock = DockStyle.Fill;
            atlasBox.MouseWheel += atlasBox_MouseWheel;
            atlasBox.MouseClick += atlasBox_MouseClick;

            int[] mfs = new int[] { 25, 50, 75, 100, 150, 200, 400 };
            foreach (int m in mfs)
                cmbMag.Items.Add(Zoom(m));
            cmbMag.SelectedIndex = 3;

            ZoomFactor = 1.0f;

            CloseButton = false;
            CloseButtonVisible = false;

            AllowDrop = true;

            DragEnter += FormView_DragEnter;
            DragDrop += FormView_DragDrop;

            cmbMag.LostFocus += cmbMag_LostFocus;

            treePages.NodeMouseClick += treePages_NodeMouseClick;
            treePages.NodeMouseDoubleClick += treePages_NodeMouseDoubleClick;

            Package.Current.PageAdded += Current_PageAdded;
            Package.Current.PageRemoved += Current_PageRemoved;
            Package.Current.FrameAddedToPage += Current_FrameAddedToPage;
            Package.Current.FrameRemovedFromPage += Current_FrameRemovedFromPage;
            Package.Current.FramePaddingChanged += Current_FramePaddingChanged;
            Package.Current.FrameRotationEnabledChanged += Current_FrameRotationEnabledChanged;
            Package.Current.PageSizeChanged += Current_PageSizeChanged;
        }

        #endregion

        #region WinForm event handlers

        private void FormView_Load(object sender, EventArgs e)
        {
            Package.Current.FrameReloaded += Current_FrameReloaded;

            FormProperty.GetInstance().PropertyValueChanged += (_s, _e) =>
            {
                if (_e.ChangedItem.Label == "Name")
                {
                    TreeNode n = treePages.Nodes.Retrieve((string)_e.OldValue);
                    n.Text = Package.Current.GetPage(SelectedIndex).Name;

                    FormProperty.GetInstance().Reload();
                }
            };
        }

        private void FormView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<Frame>)))
                e.Effect = DragDropEffects.Move;
        }

        private void FormView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<Frame>)))
            {
                List<Frame> frames = e.Data.GetData(typeof(List<Frame>)) as List<Frame>;
                if (frames != null && frames.Count != 0)
                {
                    foreach (Frame frame in frames)
                        AddFrame(frame);
                }
            }
        }

        private void btnAddPage_Click(object sender, EventArgs e)
        {
            using (Util.PackingMessageFilter pmf = new Util.PackingMessageFilter())
            {
                bool ret = Package.Current.AddPage();

                Util.PackingMessageFilter.Poped++;

                if (ret)
                    RefreshView();
            }
        }

        private void btnRemovePage_Click(object sender, EventArgs e)
        {
            if (Util.AskYesNo(this, "Delete current page?") == DialogResult.No)
                return;

            string pn = Package.Current.GetPage(SelectedIndex).Name;
            if (Package.Current.RemovePage(SelectedIndex))
            {
                pageCache[pn] = null;

                if (SelectedIndex - 1 >= 0)
                    SelectedIndex--;
                else if (Package.Current.PageCount == 0)
                    Package.Current.AddPage();
                RefreshView();
                selectedIndex = -1;
            }
        }

        private void numPage_ValueChanged(object sender, EventArgs e)
        {
            if (resettingPage)
                return;

            if (Package.Current.PageCount == 0)
            {
                numPage.Value = 1;
                return;
            }

            int idx = (int)numPage.Value - 1;

            if (idx < 0 || idx >= Package.Current.PageCount)
            {
                numPage.Value = SelectedIndex + 1;
                return;
            }

            SelectedIndex = idx;
            LoadCachedPage();
        }

        private void atlasBox_MouseMove(object sender, MouseEventArgs e)
        {
            Frame frame = atlasBox.MouseOverFrame;

            Bitmap scr = Util.ScreenShootAroundCursor();
            Color cur = Util.GetCenterColor(scr);
        }

        private void atlasBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                int per = (int)(ZoomFactor * 100.0f) + 20;
                if (per > 1000) per = 1000;
                Zoom(per);
            }
            else if (e.Delta < 0)
            {
                int per = (int)(ZoomFactor * 100.0f) - 20;
                if (per < 0) per = 0;
                Zoom(per);
            }
        }

        private void atlasBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                Zoom(100);
        }

        private void treePages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode n = e.Node;
            treePages.SelectedNode = n;
            if (n.Parent == null)
            {
                treePages.ContextMenuStrip = null;

                int idx = int.Parse(n.Name);
                if (idx != SelectedIndex || FormProperty.GetInstance().SelectedObject != Package.Current.GetPage(selectedIndex))
                {
                    SelectedIndex = idx;
                    numPage.Value = SelectedIndex + 1;
                    LoadCachedPage();
                }
            }
            else
            {
                treePages.ContextMenuStrip = contextMenuTree;

                int idx = int.Parse(n.Parent.Name);
                if (idx != SelectedIndex)
                {
                    SelectedIndex = idx;
                    numPage.Value = SelectedIndex + 1;
                    LoadCachedPage();
                }

                int fidx = n.Parent.Nodes.IndexOf(n);
                atlasBox.MouseOver = fidx;
                atlasBox.Invalidate(true);
            }
        }

        private void treePages_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode n = e.Node;
            if (n.Parent == null)
            {
                int idx = int.Parse(n.Name);
                if (idx != SelectedIndex)
                {
                    SelectedIndex = idx;
                    numPage.Value = SelectedIndex + 1;
                    LoadCachedPage();
                }
            }
            else
            {
                int idx = int.Parse(n.Parent.Name);
                if (idx != SelectedIndex)
                {
                    SelectedIndex = idx;
                    numPage.Value = SelectedIndex + 1;
                    RefreshView();
                }

                Frame frame = Package.Current[n.Name];
                atlasBox.Remove(frame);
            }
        }

        private void cmbMag_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbMag_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnZoomChanged();
        }

        private void cmbMag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OnZoomChanged();
        }

        private void cmbMag_LostFocus(object sender, EventArgs e)
        {
            OnZoomChanged();
        }

        private void menuRemoveFrame_Click(object sender, EventArgs e)
        {
            Frame frame = atlasBox.MouseOverFrame;
            atlasBox.Remove(frame);
        }

        private void menuRemoveFrame_Tree_Click(object sender, EventArgs e)
        {
            TreeNode n = treePages.SelectedNode;
            if (n == null || n.Parent == null)
                return;

            Frame frame = Package.Current[n.Name];
            atlasBox.Remove(frame);
        }

        #endregion

        #region Event handlers

        private void Current_FrameReloaded(object sender, EventArgs e)
        {
            Reload(false);
        }

        private void Current_PageAdded(object sender, PageEventArgs e)
        {
            PageAdded(e.PageIndex);
        }

        private void Current_PageRemoved(object sender, PageEventArgs e)
        {
            PageRemoved(e.PageIndex);
        }

        private void Current_FrameAddedToPage(object sender, FrameEventArgs e)
        {
            FrameAddedToPage(e.PageIndex, e.FrameName);
        }

        private void Current_FrameRemovedFromPage(object sender, FrameEventArgs e)
        {
            FrameRemovedFromPage(e.PageIndex, e.FrameName);
        }

        private bool Current_FramePaddingChanged(object sender, FramePaddingChangedEventArgs e)
        {
            return RefreshView(false, e.CannotHold, e.Rotated);
        }

        private bool Current_FrameRotationEnabledChanged(object sender, FrameRotationEnabledChangedEventArgs e)
        {
            return RefreshView(false, e.CannotHold, e.Rotated);
        }

        private bool Current_PageSizeChanged(object sender, PageSizeChangedEventArgs e)
        {
            return RefreshView(false, e.CannotHold, e.Rotated);
        }

        #endregion

        #region Common

        private void OnZoomChanged()
        {
            if (zooming)
                return;

            string str = cmbMag.Text.Replace("%", string.Empty);
            int per = 100;
            if (!int.TryParse(str, out per))
                per = 100;
            else if (per < 0 || per > 1000)
                per = 100;
            Zoom(per);
        }

        private string Zoom(int z)
        {
            zooming = true;
            {
                ZoomFactor = (float)z / 100.0f;

                cmbMag.Text = z.ToString() + " %";
            }
            zooming = false;

            return cmbMag.Text;
        }

        private void PageAdded(int idx)
        {
            Page p = Package.Current.GetPage(idx);
            treePages.Nodes.Add
            (
                idx.ToString(),
                p.Name,
                "tree_page_node.png",
                "tree_page_node_sel.png"
            );
        }

        private void PageRemoved(int idx)
        {
            treePages.Nodes.RemoveByKey(idx.ToString());
            for (int i = idx; i < Package.Current.PageCount; i++)
            {
                Page p = Package.Current.GetPage(i);
                int j = i;
                TreeNode node = treePages.Nodes[i];
                node.Name = j.ToString();
                node.Text = p.Name;
            }
        }

        private void FrameAddedToPage(int p, string f)
        {
            TreeNode pageNode = treePages.Nodes[p.ToString()];
            Debug.Assert(!pageNode.Nodes.ContainsKey(f));
            pageNode.Nodes.Add
            (
                f,
                f,
                "tree_frame_node.png",
                "tree_frame_node_sel.png"
            );
        }

        private void FrameRemovedFromPage(int p, string f)
        {
            TreeNode pageNode = treePages.Nodes[p.ToString()];
            pageNode.Nodes.RemoveByKey(f);
        }

        public void Clear()
        {
            _Clear(true);
            treePages.Nodes.Clear();
        }

        public void ResetPage()
        {
            latestLoadedPList = null;

            resettingPage = true;

            numPage.Value = 0;

            resettingPage = false;

            pageCache.Clear();
        }

        private void _Clear(bool toFirstPage, bool setLatestLoadedPListToNull = false)
        {
            if (setLatestLoadedPListToNull)
                latestLoadedPList = null;

            atlasBox.Dock = DockStyle.None;
            atlasBox.Image = null;
            if (toFirstPage)
                numPage.Value = 1;
            lblFilePath.Text = string.Empty;
            treePages.SelectedNode = null;

            pageCache.Clear();
        }

        public void ReloadTreeView()
        {
            treePages.Nodes.Clear();

            for (int i = 0; i < Package.Current.PageCount; i++)
            {
                PageAdded(i);

                var frames = Package.Current.GetFrameNamesInPage(i);
                foreach (string f in frames)
                    FrameAddedToPage(i, f);
            }

            Page p = Package.Current.GetPage(selectedIndex);
            FormProperty.GetInstance().SelectedObject = p;
        }

        public void Reload(bool toFirstPage, string justRemoved = null)
        {
            using (Util.PackingMessageFilter pmf = new Util.PackingMessageFilter())
            {
                Image img = atlasBox.Image;
                {
                    _Clear(toFirstPage, justRemoved == null);
                }
                if (img != null) atlasBox.Image = img;

                Util.PackingMessageFilter.Poped++;

                if (Package.Current.PageCount != 0)
                {
                    if (toFirstPage)
                        SelectedIndex = 0;
                    RefreshView(true, null, null, justRemoved);
                }
            }
        }

        public bool RefreshView(bool showMsg = true, List<Frame> cannotHold = null, List<Frame> rotated = null, string justRemoved = null)
        {
            bool forceRefresh = false;
            if (latestLoadedPList != null)
                forceRefresh = ContainsFrame(latestLoadedPList, justRemoved);

            Size s = (SelectedIndex == -1) ?
                Package.Current.ProjectData.TextureSize :
                Package.Current.GetPage(SelectedIndex).TextureSize;
            PList plist = null;
            Bitmap bmp = new Bitmap(s.Width, s.Height);
            Util.PackResult ret = Util.Pack(SelectedIndex, bmp, ref plist, Util.GrabColored, Util.FillColored, Util.InformIndexed, cannotHold, rotated);
            string errMsg = "Failed to pack current page, did you add too many images or large ones?";
            if (ret == Util.PackResult.OK || ret == Util.PackResult.Missing || Util.RAII.Has("OPENING_PROJECT") || forceRefresh)
            {
                LoadPList(plist);

                string pn = Package.Current.GetPage(SelectedIndex).Name;
                pageCache[pn] = new PageShadow(plist, bmp);

                if (ret == Util.PackResult.NoSpace)
                {
                    if (showMsg && Util.PackingMessageFilter.Poped == 0)
                        Util.Error(this, errMsg);
                }
            }
            else
            {
                if (showMsg && Util.PackingMessageFilter.Poped == 0)
                    Util.Error(this, errMsg);

                return false;
            }

            atlasBox.Image = bmp;

            lblFilePath.Text = "Content: " + SelectedIndex.ToString();
            lblTotal.Text = "Total:" + Package.Current.PageCount.ToString();

            if (selectedIndex == -1)
                selectedIndex = 0;
            numPage.Value = selectedIndex + 1;

            return true;
        }

        public bool LoadCachedPage()
        {
            Size s = (SelectedIndex == -1) ?
                   Package.Current.ProjectData.TextureSize :
                   Package.Current.GetPage(SelectedIndex).TextureSize;

            string pn = Package.Current.GetPage(SelectedIndex).Name;

            if (pageCache[pn] == null)
                return RefreshView();

            PList plist = pageCache[pn].PList;
            Bitmap bmp = pageCache[pn].Bitmap;
            LoadPList(plist);
            atlasBox.Image = bmp;

            lblFilePath.Text = "Content: " + SelectedIndex.ToString();
            lblTotal.Text = "Total:" + Package.Current.PageCount.ToString();

            if (selectedIndex == -1)
                selectedIndex = 0;
            numPage.Value = selectedIndex + 1;

            return true;
        }

        private bool ContainsFrame(PList plist, string frameName)
        {
            if (frameName == null)
                return false;

            if (plist.Count == 0)
                return false;

            dynamic frames = plist["frames"];
            dynamic metadata = plist["metadata"];

            foreach (dynamic frameKV in frames)
            {
                dynamic frameKey = frameKV.Key;

                if ((frameKey as string) == frameName)
                    return true;
            }

            return false;
        }

        private void LoadPList(PList plist)
        {
            latestLoadedPList = plist;

            if (plist.Count == 0)
                return;

            dynamic frames = plist["frames"];
            dynamic metadata = plist["metadata"];

            foreach (dynamic frameKV in frames)
            {
                dynamic frameKey = frameKV.Key;
                dynamic frameVal = frameKV.Value;

                dynamic _frame = frameVal["frame"];
                dynamic _offset = frameVal["offset"];
                dynamic _rotated = frameVal["rotated"];
                dynamic _sourceColorRect = frameVal["sourceColorRect"];
                dynamic _sourceSize = frameVal["sourceSize"];

                if (!Package.Current.FrameNames.Contains(frameKey as string))
                {
                    Util.Error(this, "Cannot find a frame named " + frameKey as string + ".");

                    return;
                }

                Frame frame = Package.Current[frameKey as string];
                frame.Area = Util.ParseRect(_frame as string);
                frame.Offset = Util.ParsePoint(_offset as string);
                frame.Rotated = _rotated;
                frame.SourceColorRect = Util.ParseRect(_sourceColorRect as string);
                frame.SourceSize = Util.ParseSize(_sourceSize as string);
                if (frame.Rotated)
                    frame.Area = new Rectangle(frame.Area.Location, new Size(frame.Area.Size.Height, frame.Area.Size.Width));
            }
        }

        public void AddFrame(Frame frame)
        {
            Size s = Package.Current.GetPage(SelectedIndex).TextureSize;
            if (frame.Image != null)
            {
                if ((frame.Image.Width > s.Width || frame.Image.Height > s.Height) &&
                    (frame.Image.Width > s.Height || frame.Image.Height > s.Width))
                {
                    Util.Error(this, "This texture is too large for current page.");

                    return;
                }
            }
            Package.Current.AddFrameToPage(SelectedIndex, frame.Name);
            if (!RefreshView())
                Package.Current.RemoveFrameFromPage(SelectedIndex, frame.Name);
        }

        #endregion
    }
}
