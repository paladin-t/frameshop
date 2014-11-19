using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Frameshop.Data;

namespace Frameshop
{
    internal partial class FormFrames : DockContent
    {
        public event EventHandler OriginalImageRemoved;

        private string lastFilter = string.Empty;

        public FormFrames()
        {
            InitializeComponent();

            CloseButton = false;
            CloseButtonVisible = false;

            cmbFilter.Items.Add("-- ALL --");

            cmbFilter.BlankText = "Type and press enter to match approximately";
        }

        private void FormFrames_Load(object sender, EventArgs e)
        {
            Package.Current.FrameReloaded += Current_FrameReloaded;
        }

        private void flowPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void flowPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<string> files = new List<string>();
                List<string> dirs = new List<string>();
                foreach (string path in paths)
                {
                    FileInfo fi = new FileInfo(path);
                    DirectoryInfo di = new DirectoryInfo(path);
                    if (fi.Exists)
                        files.Add(path);
                    else if (di.Exists)
                        dirs.Add(path);
                }
                FormMain.GetInstance().AddImage(files.ToArray());
                FormMain.GetInstance().AddFolder(dirs.ToArray());
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (lastFilter == cmb.Text) return;
            Active(new string[] { cmb.Text });
        }

        private void cmbFilter_TextChanged(object sender, EventArgs e)
        {
            List<string> actived = new List<string>();
            if (string.IsNullOrEmpty(cmbFilter.Text))
            {
                foreach (var it in cmbFilter.Items)
                {
                    string txt = it as string;
                    actived.Add(txt);
                }

                Active(actived.ToArray());
            }
        }

        private void cmbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(cmbFilter.Text))
                {
                    return;
                }
                else
                {
                    List<Util.ApproximateStringMatchResult> actived = new List<Util.ApproximateStringMatchResult>();
                    if (!string.IsNullOrEmpty(cmbFilter.Text))
                    {
                        foreach (var it in cmbFilter.Items)
                        {
                            string txt = it as string;
                            float score = Util.ApproximateStringMatch(txt.ToLower(), cmbFilter.Text.ToLower());
                            if (score >= 0)
                                actived.Add(new Util.ApproximateStringMatchResult(txt, score));
                        }
                    }

                    var ad = from a in actived
                             orderby -a.Score
                             select a.Content;

                    Active(ad.ToArray());
                }
            }
        }

        private void pic_CloseButtonPressed(object sender, EventArgs e)
        {
            PictureBoxEx pic = sender as PictureBoxEx;
            Frame frame = pic.Tag as Frame;

            if (Option.GetInstance()["misc"]["ask_before_rem_frame"])
            {
                if (Util.AskYesNo(this, "Are you sure to remove frame: " + frame.Name + "?" + Environment.NewLine + "All frames in pages referenced to it will be removed as well.") == DialogResult.No)
                    return;
            }

            flowPanel.Controls.Remove(pic);
            ReloadSearchBox();
            pic.MouseDown -= pic_MouseDown;
            pic.MouseUp -= pic_MouseUp;
            pic.MouseMove -= pic_MouseMove;
            pic.MouseClick -= pic_MouseClick;
            pic.MouseDoubleClick -= pic_MouseDoubleClick;

            Package.Current.RemoveImage(frame.FilePath);

            if (OriginalImageRemoved != null)
                OriginalImageRemoved(this, EventArgs.Empty);
        }

        private void pic_RefreshButtonPressed(object sender, EventArgs e)
        {
            PictureBoxEx pic = sender as PictureBoxEx;
            Frame frame = pic.Tag as Frame;
            Package.Current.ReloadImage(frame);
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBoxEx pic = sender as PictureBoxEx;
            Frame frame = pic.Tag as Frame;

            Bitmap scr = Util.ScreenShootAroundCursor();
            Color cur = Util.GetCenterColor(scr);

            if (e.Button == MouseButtons.Left)
            {
                List<Frame> selected = new List<Frame>();
                if (ModifierKeys == Keys.Control)
                {
                    foreach (Control c in flowPanel.Controls)
                    {
                        PictureBoxEx _pic = c as PictureBoxEx;
                        if (_pic != null && _pic.Selected)
                        {
                            Frame _frame = _pic.Tag as Frame;
                            selected.Add(_frame);
                        }
                    }
                }
                else
                {
                    foreach (Control c in flowPanel.Controls)
                    {
                        PictureBoxEx _pic = c as PictureBoxEx;
                        if (_pic != null)
                            _pic.Selected = false;
                    }
                }
                pic.Selected = !pic.Selected;
                selected.Add(frame);

                DoDragDrop(selected, DragDropEffects.Move);
            }
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBoxEx pic = sender as PictureBoxEx;
            Frame frame = pic.Tag as Frame;
            Bitmap scr = Util.ScreenShootAroundCursor();
            Color cur = Util.GetCenterColor(scr);
            if (e.Button == MouseButtons.Left)
            {
                if (ModifierKeys != Keys.Control)
                {
                    foreach (Control c in flowPanel.Controls)
                    {
                        PictureBoxEx _pic = c as PictureBoxEx;
                        if (_pic != null)
                            _pic.Selected = false;
                    }
                }

                pic.Selected = !pic.Selected;
            }
        }

        private void pic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBoxEx pic = sender as PictureBoxEx;
            Frame frame = pic.Tag as Frame;
            if (e.Button == MouseButtons.Left)
            {
                foreach (Control c in flowPanel.Controls)
                {
                    PictureBoxEx _pic = c as PictureBoxEx;
                    if (_pic != null)
                        _pic.Selected = false;
                }
                pic.Selected = !pic.Selected;

                FormView.GetInstance().AddFrame(frame);
            }
        }

        private void Current_FrameReloaded(object sender, EventArgs e)
        {
            Reload();
        }

        public void Clear()
        {
            flowPanel.Controls.Clear();

            ReloadSearchBox();
        }

        public void Reload()
        {
            Clear();

            IEnumerable<PictureBoxEx> picBoxes = new List<PictureBoxEx>();
            foreach (Frame frame in Package.Current.Frames)
            {
                if (frame.Image == null)
                    continue;

                PictureBoxEx pic = new PictureBoxEx();
                pic.BorderStyle = BorderStyle.FixedSingle;
                pic.Image = frame.Image;
                if (pic.Image.Width < pic.Width && pic.Image.Height < pic.Height)
                    pic.SizeMode = PictureBoxSizeMode.CenterImage;
                else
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.AutoSize = false;
                FileInfo fi = new FileInfo(frame.FilePath);
                pic.Title = fi.Name;
                pic.Name = fi.Name;
                pic.CloseButtonPressed += pic_CloseButtonPressed;
                pic.RefreshButtonPressed += pic_RefreshButtonPressed;
                pic.Tag = frame;
                (picBoxes as List<PictureBoxEx>).Add(pic);
                pic.MouseDown += pic_MouseDown;
                pic.MouseUp += pic_MouseUp;
                pic.MouseMove += pic_MouseMove;
                pic.MouseClick += pic_MouseClick;
                pic.MouseDoubleClick += pic_MouseDoubleClick;

                Application.DoEvents();
            }

            picBoxes = picBoxes.OrderBy((_p) => { return _p.Image.Height; });

            flowPanel.Controls.AddRange(picBoxes.ToArray());
            ReloadSearchBox();
        }

        private void ReloadSearchBox()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("-- ALL --");
            foreach (PictureBoxEx pic in flowPanel.Controls)
                cmbFilter.Items.Add(pic.Title);
        }

        private void Active(string[] paletteNames)
        {
            bool showAll = cmbFilter.SelectedIndex == 0;
            if (showAll)
            {
                foreach (Control c in flowPanel.Controls)
                {
                    c.Visible = true;
                }
            }
            else
            {
                foreach (Control c in flowPanel.Controls)
                    c.Visible = false;
            }

            if (!showAll)
            {
                foreach (string pn in paletteNames)
                {
                    PictureBoxEx pb = flowPanel.Controls[pn] as PictureBoxEx;
                    if (pb != null)
                    {
                        pb.Visible = true;
                        pb.SendToBack();
                    }
                }
            }
        }
    }
}
