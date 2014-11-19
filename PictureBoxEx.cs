using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frameshop
{
    internal partial class PictureBoxEx : UserControl, IMessageFilter
    {
        public event EventHandler CloseButtonPressed;

        public event EventHandler RefreshButtonPressed;

        public bool Selected
        {
            get
            {
                return BorderStyle == BorderStyle.Fixed3D;
            }
            set
            {
                BorderStyle = value ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            }
        }

        public string Title
        {
            get
            {
                return labelTitle.Text;
            }
            set
            {
                labelTitle.Text = value;
            }
        }

        public PictureBoxSizeMode SizeMode
        {
            get
            {
                return pictureBox.SizeMode;
            }
            set
            {
                pictureBox.SizeMode = value;
            }
        }

        public Image Image
        {
            get
            {
                return pictureBox.Image;
            }
            set
            {
                pictureBox.Image = value;

                if (value != null)
                {
                    string tip = "Size: " + value.Width.ToString() + "x" + value.Height.ToString();
                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pictureBox, tip);
                }
            }
        }

        public PictureBoxEx()
        {
            InitializeComponent();

            Application.AddMessageFilter(this);

            Title = string.Empty;

            ToolTip tipRefresh = new ToolTip();
            tipRefresh.SetToolTip(btnRefresh, "Refresh");

            ToolTip tipClose = new ToolTip();
            tipClose.SetToolTip(btnClose, "Remove");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Application.AddMessageFilter(this);

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool PreFilterMessage(ref Message msg)
        {
            if (msg.Msg == 0x0200)
            {
                Point pos = PointToClient(Cursor.Position);
                bool entered = ClientRectangle.Contains(pos);
                //btnRefresh.Visible = entered;
                btnClose.Visible = entered;

                if (entered)
                    OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pos.X, pos.Y, 0));
            }

            return false;
        }

        private void OnCloseButtonPressed()
        {
            if (CloseButtonPressed != null)
                CloseButtonPressed(this, EventArgs.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseButtonPressed();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (RefreshButtonPressed != null)
                RefreshButtonPressed(this, EventArgs.Empty);
        }

        private void ctrl_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void ctrl_MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void ctrl_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }
    }
}
