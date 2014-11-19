using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Frameshop.Data;
using Frameshop.Anim;

namespace Frameshop
{
    internal partial class AnimPreviewBox : PictureBox
    {
        private bool settingFrame = false;

        private Point offset = Point.Empty;
        public Point Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;

                Image = originalImage;
            }
        }

        private byte alpha = byte.MaxValue;
        public byte Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                if (alpha == value)
                    return;

                alpha = value;

                Image = originalImage;
            }
        }

        private Image originalImage = null;
        public new Image Image
        {
            get
            {
                return originalImage;
            }
            set
            {
                if (!settingFrame)
                    frameName = null;

                originalImage = value;
                if (originalImage == null)
                {
                    if (base.Image != null)
                    {
                        Graphics g = Graphics.FromImage(base.Image);
                        g.Clear(Color.Transparent);
                    }
                }
                else
                {
                    Graphics g = Graphics.FromImage(base.Image);
                    g.Clear(Color.Transparent);

                    if (Alpha == byte.MaxValue)
                    {
                        g.DrawImage(
                        originalImage,
                        Offset.X, Offset.Y,
                        originalImage.Width, originalImage.Height
                    );
                    }
                    else
                    {
                        using (Bitmap bmp = new Bitmap(originalImage))
                        {
                            for (int j = 0; j < bmp.Height; j++)
                            {
                                for (int i = 0; i < bmp.Width; i++)
                                {
                                    Color oc = bmp.GetPixel(i, j);
                                    Color c = oc.A > 128 ? Color.FromArgb(Alpha, oc) : oc;
                                    g.DrawRectangle(
                                        new Pen(c),
                                        Offset.X + i,
                                        Offset.Y + j,
                                        1, 1
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }

        private string frameName = null;
        public string FrameName
        {
            get
            {
                return frameName;
            }
        }

        public object Frame
        {
            get
            {
                return originalImage;
            }
            set
            {
                if (value == null)
                {
                    Image = null;
                    frameName = null;
                }
                else if (value is Image)
                {
                    Image = value as Image;
                    frameName = null;
                }
                else if (value is string)
                {
                    frameName = value as string;
                    Frame f = Package.Current[frameName];
                    settingFrame = true;
                    if (f != null)
                    {
                        Image = f.Image;
                    }
                    else
                    {
                        Image = null;

                        Invalidate(true);
                    }
                    settingFrame = false;
                }
                else
                {
                    throw new Exception("Unsupported type");
                }
            }
        }

        public Sequence WorkingSequence { get; set; }

        public int IndexInSequence { get; set; }

        public AnimPreviewBox()
        {
            InitializeComponent();

            BorderStyle = BorderStyle.FixedSingle;

            BackColor = Color.White;

            IndexInSequence = -1;
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

            if (IndexInSequence != -1)
            {
                AnimFrame af = WorkingSequence[IndexInSequence];
                g.DrawRectangle(Pens.Red, Util.GameToWindow(this, af.Rect0));
                g.DrawRectangle(Pens.Green, Util.GameToWindow(this, af.Rect1));
                g.DrawRectangle(Pens.Blue, Util.GameToWindow(this, af.Rect2));
                g.DrawRectangle(Pens.Yellow, Util.GameToWindow(this, af.Rect3));
                g.DrawRectangle(Pens.Pink, Util.GameToWindow(this, af.Rect4));
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Width != 0 && Height != 0)
                base.Image = new Bitmap(Width, Height);
        }
    }
}
