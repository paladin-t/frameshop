using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Frameshop.Data;

namespace Frameshop
{
    internal partial class AtlasBox : PictureBox
    {
        private float zoom = 1.0f;
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;

                if (Image != null)
                {
                    Size s = new Size((int)(Image.Width * zoom), (int)(Image.Height * zoom));
                    Size = s;
                    SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        public new Image Image
        {
            get { return base.Image; }
            set
            {
                if (value != null)
                {
                    Size s = new Size((int)(value.Width * zoom), (int)(value.Height * zoom));
                    Size = s;
                    SizeMode = PictureBoxSizeMode.StretchImage;
                }

                base.Image = value;
            }
        }

        private Pen commonPen = null;
        private Pen CommonPen
        {
            get
            {
                if (commonPen == null)
                {
                    commonPen = new Pen(Color.Black);
                    commonPen.DashStyle = DashStyle.Dot;
                }

                return commonPen;
            }
        }

        private Pen hoverPen = null;
        private Pen HoverPen
        {
            get
            {
                if (hoverPen == null)
                {
                    hoverPen = new Pen(Color.Red);
                    hoverPen.DashStyle = DashStyle.Dot;
                }

                return hoverPen;
            }
        }

        private int mouseOver = -1;
        public int MouseOver
        {
            get
            {
                return mouseOver;
            }
            set
            {
                mouseOver = value;
            }
        }

        public int PageIndex
        {
            get;
            set;
        }

        public Frame MouseOverFrame
        {
            get;
            private set;
        }

        public AtlasBox()
        {
            InitializeComponent();

            BorderStyle = BorderStyle.FixedSingle;

            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.InterpolationMode = InterpolationMode.Low;

            const int n = 8;
            for (int j = 0; j < Height; j += n)
            {
                bool f = (j / n) % 2 == 0;
                for (int i = 0; i < Width; i += n)
                {
                    if (f)
                        g.FillRectangle(Brushes.LightGray, i, j, n, n);

                    f = !f;
                }
            }

            base.OnPaint(pe);

            if (Package.Current == null || Package.Current.Frames == null || Package.Current.Frames.Count() == 0)
                return;

            var frames = Package.Current.GetFramesInPage(PageIndex);
            if (frames != null)
            {
                for (int i = 0; i < frames.Count(); i++)
                {
                    Frame frame = frames.ElementAt(i);
                    Rectangle rect = frame.FinalArea.Zoom(Zoom);
                    rect.Width--;
                    rect.Height--;
                    if (mouseOver != i)
                        g.DrawRectangle(CommonPen, rect);
                    else
                        g.DrawRectangle(HoverPen, rect);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mouseOver = -1;

            int idx;
            Frame frame = Select(e.Location, out idx);
            if (frame != null)
                mouseOver = idx;

            MouseOverFrame = frame;

            Invalidate(true);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (e.Button == MouseButtons.Left)
            {
                int idx;
                Frame frame = Select(e.Location, out idx);

                Remove(frame);
            }
        }

        private Frame Select(Point mouse, out int idx)
        {
            idx = 0;
            var frames = Package.Current.GetFramesInPage(PageIndex);
            for (int i = 0; i < frames.Count(); i++)
            {
                Frame frame = frames.ElementAt(i);
                Rectangle rect = frame.FinalArea.Zoom(Zoom);
                if (rect.Contains(mouse))
                {
                    idx = i;

                    return frame;
                }
            }

            return null;
        }

        public void Remove(Frame f)
        {
            if (f != null)
            {
                if (Util.AskYesNo(this, "Remove frame: " + f.Name + " from current page?") == DialogResult.Yes)
                {
                    Package.Current.RemoveFrameFromPage(PageIndex, f.Name);

                    Invalidate(true);
                }
            }
        }
    }
}
