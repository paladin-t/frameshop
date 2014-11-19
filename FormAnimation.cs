using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using Frameshop.Data;
using Frameshop.Anim;

namespace Frameshop
{
    public partial class FormAnimation : Form
    {
        private static readonly string ANIM_FILTER = "Animation files(*" + Misc.ANIM_FILE_EXT + ")|*" + Misc.ANIM_FILE_EXT + "|All files(*.*)|*.*";

        private Sequence sequence = null;

        private string SavePath
        {
            get;
            set;
        }

        private EditStates _editState = EditStates.Closed;
        private EditStates EditState
        {
            get { return _editState; }
            set
            {
                _editState = value;

                menuNew.Enabled = false;
                menuOpen.Enabled = false;
                menuSave.Enabled = false;
                menuPreview.Enabled = false;
                splitMain.Enabled = false;

                switch (_editState)
                {
                    case EditStates.Closed:
                        menuNew.Enabled = true;
                        menuOpen.Enabled = true;
                        break;
                    case EditStates.New:
                        menuNew.Enabled = true;
                        menuOpen.Enabled = true;
                        menuSave.Enabled = true;
                        menuPreview.Enabled = true;
                        splitMain.Enabled = true;
                        break;
                    case EditStates.OpenedNotSaved:
                        menuNew.Enabled = true;
                        menuOpen.Enabled = true;
                        menuSave.Enabled = true;
                        menuPreview.Enabled = true;
                        splitMain.Enabled = true;
                        break;
                    case EditStates.Saved:
                        menuNew.Enabled = true;
                        menuOpen.Enabled = true;
                        menuPreview.Enabled = true;
                        splitMain.Enabled = true;
                        break;
                }
            }
        }

        public FormAnimation()
        {
            InitializeComponent();

            sequence = new Sequence();
            sequence.FrameNameChanged += sequence_FrameNameChanged;
            sequence.FrameOffsetChanged += sequence_FrameOffsetChanged;
            sequence.FrameAlphaChanged += sequence_FrameAlphaChanged;

            SavePath = null;
            EditState = EditStates.Closed;

            treePages.NodeMouseClick += treePages_NodeMouseClick;
            treePages.NodeMouseDoubleClick += treePages_NodeMouseDoubleClick;
            treePages.ItemDrag += treePages_ItemDrag;
            treePages.MouseDown += treePages_MouseDown;

            treeSeq.NodeMouseClick += treeSeq_NodeMouseClick;
            treeSeq.NodeMouseDoubleClick += treeSeq_NodeMouseDoubleClick;
            treeSeq.DragEnter += treeSeq_DragEnter;
            treeSeq.DragDrop += treeSeq_DragDrop;

            picView.WorkingSequence = sequence;
            picView.AnimBoxMoving += picView_AnimBoxMoving;
            picView.AnimBoxMoved += picView_AnimBoxMoved;
            picView.AnimBoxQuading += picView_AnimBoxQuading;
            picView.AnimBoxQuaded += picView_AnimBoxQuaded;
        }

        ~FormAnimation()
        {
            sequence.FrameNameChanged -= sequence_FrameNameChanged;
            sequence.FrameOffsetChanged -= sequence_FrameOffsetChanged;
            sequence.FrameAlphaChanged -= sequence_FrameAlphaChanged;
        }

        #region WinForm event handlers
        
        private void FormAnimation_Load(object sender, EventArgs e)
        {
            try
            {
                Location = new Point
                (
                    Option.GetInstance()["win_anim"]["x"],
                    Option.GetInstance()["win_anim"]["y"]
                );
                Size = new Size
                (
                    Option.GetInstance()["win_anim"]["w"],
                    Option.GetInstance()["win_anim"]["h"]
                );
                if (Option.GetInstance()["win_anim"]["max"])
                    WindowState = FormWindowState.Maximized;
            }
            catch
            {
                Option.GetInstance().Init();
            }

            for (int i = 0; i < Package.Current.PageCount; i++)
            {
                Page p = Package.Current.GetPage(i);
                treePages.Nodes.Add
                (
                    i.ToString(),
                    p.Name,
                    "tree_page_node.png",
                    "tree_page_node_sel.png"
                );

                TreeNode pageNode = treePages.Nodes[i.ToString()];

                var fns = Package.Current.GetFrameNamesInPage(i);
                foreach (string f in fns)
                {
                    Debug.Assert(!pageNode.Nodes.ContainsKey(f));
                    pageNode.Nodes.Add
                    (
                        f,
                        f,
                        "tree_frame_node.png",
                        "tree_frame_node_sel.png"
                    );
                }
            }
        }

        private void FormAnimation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ConfirmSaving())
            {
                e.Cancel = true;
            }
            else
            {
                Option.GetInstance()["win_anim"]["max"] = WindowState == FormWindowState.Maximized;
                Option.GetInstance()["win_anim"]["x"] = Left;
                Option.GetInstance()["win_anim"]["y"] = Top;
                Option.GetInstance()["win_anim"]["w"] = Width;
                Option.GetInstance()["win_anim"]["h"] = Height;
            }
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            if (ConfirmSaving())
            {
                SavePath = null;

                picView.Clear();

                treeSeq.Nodes.Clear();

                sequence.Clear();

                propSeq.SelectedObject = null;

                EditState = EditStates.New;
            }
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            if (ConfirmSaving())
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.RestoreDirectory = true;
                ofd.Filter = ANIM_FILTER;
                if (ofd.ShowDialog(this) != DialogResult.OK)
                    return;

                sequence.Clear();
                propSeq.SelectedObject = null;

                picView.Clear();
                Open(ofd.FileName);

                treeSeq.Nodes.Clear();
                foreach (AnimFrame af in sequence.Frames)
                {
                    Frame f = Package.Current[af.FrameName];

                    if (f == null)
                        treeSeq.Nodes.Add("BLANK");
                    else
                        treeSeq.Nodes.Add(f.Name, af.Name + " : " + f.Name);
                }
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panelCenter_SizeChanged(object sender, EventArgs e)
        {
            int x = (panelCenter.Width - picView.Width) / 2;
            int y = (panelCenter.Height - picView.Height) / 2;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            picView.Location = new Point(x, y);
        }

        private void picView_AnimBoxContentSizeChanged(object sender, EventArgs e)
        {
            panelCenter_SizeChanged(sender, e);
        }

        private void treePages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (e.Node.Parent != null)
                {
                    Frame f = Package.Current[e.Node.Name];
                    picView.Alpha = byte.MaxValue;
                    picView.Image = f.Image;
                    picView.Offset = Point.Empty;
                    picView.IndexInSequence = -1;

                    propSeq.SelectedObject = null;
                }
            }

            treePages.SelectedNode = e.Node;
        }

        private void treePages_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                AddToSeq(e.Node);
            }
        }

        private void treePages_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (treePages.SelectedNode != null)
                DoDragDrop(treePages.SelectedNode, DragDropEffects.Move);
        }

        private void treePages_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = treeSeq.GetNodeAt(e.Location);
                if (node != null)
                    treePages.SelectedNode = node;
            }
        }

        private void treeSeq_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                AnimFrame af = sequence[e.Node.Index];
                picView.Alpha = af.Alpha;
                picView.Frame = e.Node.Name;
                picView.Offset = af.Offset;
                picView.IndexInSequence = e.Node.Index;

                propSeq.SelectedObject = sequence[e.Node.Index];
            }

            treeSeq.SelectedNode = e.Node;
        }

        private void treeSeq_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                picView.Image = null;
                picView.IndexInSequence = -1;

                RemoveFromSeq(e.Node);
            }
        }

        private void treeSeq_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
        }

        private void treeSeq_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode node = e.Data.GetData(typeof(TreeNode)) as TreeNode;
                if (node != null)
                {
                    AddToSeq(node);
                }
            }
        }

        private void propSeq_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            picView.Invalidate(true);

            EditState = EditStates.OpenedNotSaved;
        }

        private void menuAddToSeq_Click(object sender, EventArgs e)
        {
            AddToSeq(treePages.SelectedNode);
        }

        private void menuRemoveFromSeq_Click(object sender, EventArgs e)
        {
            RemoveFromSeq(treeSeq.SelectedNode);
        }

        private void menuInsertBlankFrame_Click(object sender, EventArgs e)
        {
            AddToSeq(null);
        }

        private void menuMoveMode_Click(object sender, EventArgs e)
        {
            menuMoveMode.Checked = true;
            menuQuadMode1.Checked = false;
            menuQuadMode2.Checked = false;
            menuQuadMode3.Checked = false;
            menuQuadMode4.Checked = false;
            menuQuadMode5.Checked = false;
            picView.State = AnimBoxState.Move;
        }

        private void menuQuadMode1_Click(object sender, EventArgs e)
        {
            menuMoveMode.Checked = false;
            menuQuadMode1.Checked = true;
            menuQuadMode2.Checked = false;
            menuQuadMode3.Checked = false;
            menuQuadMode4.Checked = false;
            menuQuadMode5.Checked = false;
            picView.State = AnimBoxState.Quad;
        }

        private void menuQuadMode2_Click(object sender, EventArgs e)
        {
            menuMoveMode.Checked = false;
            menuQuadMode1.Checked = false;
            menuQuadMode2.Checked = true;
            menuQuadMode3.Checked = false;
            menuQuadMode4.Checked = false;
            menuQuadMode5.Checked = false;
            picView.State = AnimBoxState.Quad;
        }

        private void menuQuadMode3_Click(object sender, EventArgs e)
        {
            menuMoveMode.Checked = false;
            menuQuadMode1.Checked = false;
            menuQuadMode2.Checked = false;
            menuQuadMode3.Checked = true;
            menuQuadMode4.Checked = false;
            menuQuadMode5.Checked = false;
            picView.State = AnimBoxState.Quad;
        }

        private void menuQuadMode4_Click(object sender, EventArgs e)
        {
            menuMoveMode.Checked = false;
            menuQuadMode1.Checked = false;
            menuQuadMode2.Checked = false;
            menuQuadMode3.Checked = false;
            menuQuadMode4.Checked = true;
            menuQuadMode5.Checked = false;
            picView.State = AnimBoxState.Quad;
        }

        private void menuQuadMode5_Click(object sender, EventArgs e)
        {
            menuMoveMode.Checked = false;
            menuQuadMode1.Checked = false;
            menuQuadMode2.Checked = false;
            menuQuadMode3.Checked = false;
            menuQuadMode4.Checked = false;
            menuQuadMode5.Checked = true;
            picView.State = AnimBoxState.Quad;
        }

        private void menuShowCenterPoint_Click(object sender, EventArgs e)
        {
            menuShowCenterPoint.Checked = !menuShowCenterPoint.Checked;

            picView.ShowCenterPoint = menuShowCenterPoint.Checked;
        }

        private void menuPreview_Click(object sender, EventArgs e)
        {
            FormAnimationPreview fap = new FormAnimationPreview(sequence);
            fap.ShowDialog(this);
        }

        private void picView_AnimBoxMoving(object sender, AnimBoxMoveEventArgs e)
        {
            if (propSeq.SelectedObject is AnimFrame)
            {
                AnimFrame af = (AnimFrame)propSeq.SelectedObject;
                af.Offset = new Point(af.Offset.X + e.Offset.X, af.Offset.Y + e.Offset.Y);
            }
        }

        private void picView_AnimBoxMoved(object sender, AnimBoxMoveEventArgs e)
        {
            if (propSeq.SelectedObject is AnimFrame)
            {
                AnimFrame af = (AnimFrame)propSeq.SelectedObject;
                af.Offset = new Point(af.Offset.X + e.Offset.X, af.Offset.Y + e.Offset.Y);
                propSeq.SelectedObject = af;

                EditState = EditStates.OpenedNotSaved;
            }
        }

        private void picView_AnimBoxQuading(object sender, AnimBoxQuadEventArgs e)
        {
            if (propSeq.SelectedObject is AnimFrame)
            {
                AnimFrame af = (AnimFrame)propSeq.SelectedObject;
                if (menuQuadMode1.Checked)
                    af.Rect0 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode2.Checked)
                    af.Rect1 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode3.Checked)
                    af.Rect2 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode4.Checked)
                    af.Rect3 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode5.Checked)
                    af.Rect4 = Util.WindowToGame(picView, e.Area);
                picView.Invalidate(true);
                picView.Refresh();
                propSeq.SelectedObject = af;
            }
        }

        private void picView_AnimBoxQuaded(object sender, AnimBoxQuadEventArgs e)
        {
            if (propSeq.SelectedObject is AnimFrame)
            {
                AnimFrame af = (AnimFrame)propSeq.SelectedObject;
                if (menuQuadMode1.Checked)
                    af.Rect0 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode2.Checked)
                    af.Rect1 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode3.Checked)
                    af.Rect2 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode4.Checked)
                    af.Rect3 = Util.WindowToGame(picView, e.Area);
                else if (menuQuadMode5.Checked)
                    af.Rect4 = Util.WindowToGame(picView, e.Area);
                picView.Invalidate(true);
                picView.Refresh();
                propSeq.SelectedObject = af;

                EditState = EditStates.OpenedNotSaved;
            }
        }

        private void menuExportGif_Click(object sender, EventArgs e)
        {
            if (sequence.Frames.Count() == 0)
            {
                Util.Notice(this, "No frame to export.");

                return;
            }

            int maxw = (int)(sequence.Frames.Max(_f => Package.Current[_f.FrameName].Image.Width) * 1.5f);
            int maxh = (int)(sequence.Frames.Max(_f => Package.Current[_f.FrameName].Image.Height) * 1.5f);
            var func =
                new Func<AnimFrame, Image>
                (
                    (_f) =>
                    {
                        Image ori = Package.Current[_f.FrameName].Image;
                        Point offset = _f.Offset;
                        offset.X += (maxw - ori.Width) / 2;
                        offset.Y += (maxh - ori.Height) / 2;
                        Bitmap bmp = new Bitmap(maxw, maxh);
                        Graphics g = Graphics.FromImage(bmp);
                        g.DrawImage(ori, offset);

                        return bmp;
                    }
                );
            var _frames = from f in sequence.Frames
                          select func(f);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "GIF files(*.gif)|*.gif|All files(*.*)|*.*";
            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;
            string outPath = sfd.FileName;
            Util.ExportGif(outPath, 200, _frames.ToArray());
        }

        #endregion

        #region Event handlers

        private void sequence_FrameNameChanged(object sender, FrameNameChangedEventArgs e)
        {
            if (propSeq.SelectedObject == e.Anim)
                picView.Frame = e.Name;
        }

        private void sequence_FrameOffsetChanged(object sender, FrameOffsetChangedEventArgs e)
        {
            if (propSeq.SelectedObject == e.Anim)
                picView.Offset = e.Offset;
        }

        private void sequence_FrameAlphaChanged(object sender, FrameAlphaChangedEventArgs e)
        {
            if (propSeq.SelectedObject == e.Anim)
                picView.Alpha = e.Alpha;
        }

        #endregion

        #region Common methods

        private bool ConfirmSaving()
        {
            if (EditState == EditStates.New || EditState == EditStates.OpenedNotSaved)
            {
                DialogResult dr = Util.AskYesNoCancel(this, "Current animation not saved, save it before other operations?");
                if (dr == DialogResult.Yes)
                    return Save();
                else if (dr == DialogResult.No)
                    return true;
                else
                    return false;
            }

            return true;
        }

        private bool Save()
        {
            if (SavePath == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.RestoreDirectory = true;
                sfd.Filter = ANIM_FILTER;
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return false;

                SavePath = sfd.FileName;
            }

            bool ret = sequence.Save(SavePath);
            if (ret)
                EditState = EditStates.Saved;

            FormMain.GetInstance().PostAnimEvent(SavePath);

            return ret;
        }

        private bool Open(string file)
        {
            bool ret = sequence.Open(file);
            if (ret)
            {
                SavePath = file;

                EditState = EditStates.Saved;
            }

            return ret;
        }

        private void AddToSeq(TreeNode node)
        {
            if (node == null)
            {
                AnimFrame a = sequence.Add(null, null);

                treeSeq.Nodes.Add("BLANK");

                EditState = EditStates.OpenedNotSaved;
            }
            else if (node.Parent != null)
            {
                Frame f = Package.Current[node.Name];
                AnimFrame a = sequence.Add(node.Parent.Text, f.Name);

                treeSeq.Nodes.Add(f.Name, a.Name + " : " + f.Name);

                EditState = EditStates.OpenedNotSaved;
            }
        }

        private void RemoveFromSeq(TreeNode node)
        {
            Frame f = Package.Current[node.Name];
            AnimFrame a = sequence[node.Index];
            sequence.RemoveAt(node.Index);

            treeSeq.Nodes.RemoveAt(node.Index);

            if (picView.FrameName == node.Name)
                picView.Frame = null;

            if (propSeq.SelectedObject == a)
                propSeq.SelectedObject = null;

            EditState = EditStates.OpenedNotSaved;
        }

        #endregion
    }
}
