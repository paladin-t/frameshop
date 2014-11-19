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
    internal enum AnimBoxState
    {
        Common,
        Move,
        Quad
    }

    internal partial class AnimBox : PictureBox
    {
        private static readonly int BORDER_SIZE = 64;

        private bool settingFrame = false;

        private Point mouseDownLocation;

        private AnimBoxState state = AnimBoxState.Move;
        public AnimBoxState State
        {
            get { return state; }
            set { state = value; }
        }

        private bool showCenterPoint = true;

        public bool ShowCenterPoint
        {
            get { return showCenterPoint; }
            set
            {
                showCenterPoint = value;

                Invalidate(true);
            }
        }

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
                bool contentSizeChanged = false;

                if (!settingFrame)
                    frameName = null;

                originalImage = value;
                if (originalImage == null)
                {
                    int w = BORDER_SIZE;
                    int h = BORDER_SIZE;
                    if (Parent != null)
                    {
                        w = Parent.Width - 30;
                        h = Parent.Height - 30;

                        if (w < 0) w = 8;
                        if (h < 0) h = 8;
                    }

                    if (base.Image != null)
                        contentSizeChanged = w != base.Image.Width || h != base.Image.Height;

                    base.Image = new Bitmap(w, h);
                }
                else
                {
                    int w = originalImage.Width;
                    int h = originalImage.Height;
                    int bw = w;
                    int bh = h;
                    if (w > BORDER_SIZE || h > BORDER_SIZE)
                    {
                        bw = BORDER_SIZE;
                        bh = BORDER_SIZE;
                    }
                    w += bw * 4;
                    h += bh * 4;
                    if (base.Image != null)
                        contentSizeChanged = w != base.Image.Width || h != base.Image.Height;
                    base.Image = new Bitmap(w, h);
                    Graphics g = Graphics.FromImage(base.Image);
                    if (Alpha == byte.MaxValue)
                    {
                        g.DrawImage(
                            originalImage,
                            bw * 2 + Offset.X, bh * 2 + Offset.Y,
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
                                        bw * 2 + Offset.X + i,
                                        bh * 2 + Offset.Y + j,
                                        1, 1
                                    );
                                }
                            }
                        }
                    }
                }

                if (contentSizeChanged)
                {
                    if (AnimBoxContentSizeChanged != null)
                        AnimBoxContentSizeChanged(this, null);
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
                        Image = f.Image;
                    else
                        Image = null;
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

        public event EventHandler<AnimBoxMoveEventArgs> AnimBoxMoving;

        public event EventHandler<AnimBoxMoveEventArgs> AnimBoxMoved;

        public event EventHandler<AnimBoxQuadEventArgs> AnimBoxQuading;

        public event EventHandler<AnimBoxQuadEventArgs> AnimBoxQuaded;

        public event EventHandler AnimBoxContentSizeChanged;

        public AnimBox()
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

            if (ShowCenterPoint)
            {
                Pen p = new Pen(new HatchBrush(HatchStyle.Cross, Color.Black));
                g.DrawLine(
                    p,
                    new Point(Width / 2, Height / 2 - 10),
                    new Point(Width / 2, Height / 2 + 10)
                );
                g.DrawLine(
                    p,
                    new Point(Width / 2 - 10, Height / 2),
                    new Point(Width / 2 + 10, Height / 2)
                );
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (state)
                {
                    case AnimBoxState.Common:
                        break;
                    case AnimBoxState.Move:
                        if (IndexInSequence != -1)
                        {
                            Point offset = new Point(e.Location.X - mouseDownLocation.X, e.Location.Y - mouseDownLocation.Y);
                            mouseDownLocation = e.Location;
                            OnAnimBoxMoving(offset);
                        }
                        break;
                    case AnimBoxState.Quad:
                        int l = Math.Min(mouseDownLocation.X, e.Location.X);
                        int t = Math.Min(mouseDownLocation.Y, e.Location.Y);
                        int r = Math.Max(mouseDownLocation.X, e.Location.X);
                        int b = Math.Max(mouseDownLocation.Y, e.Location.Y);
                        Rectangle area = new Rectangle(l, t, r - l + 1, b - t + 1);
                        OnAnimBoxQuading(area);
                        break;
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (state)
                {
                    case AnimBoxState.Common:
                        break;
                    case AnimBoxState.Move:
                        if (IndexInSequence != -1)
                        {
                            Point offset = new Point(e.Location.X - mouseDownLocation.X, e.Location.Y - mouseDownLocation.Y);
                            mouseDownLocation = e.Location;
                            OnAnimBoxMoved(offset);
                        }
                        break;
                    case AnimBoxState.Quad:
                        int l = Math.Min(mouseDownLocation.X, e.Location.X);
                        int t = Math.Min(mouseDownLocation.Y, e.Location.Y);
                        int r = Math.Max(mouseDownLocation.X, e.Location.X);
                        int b = Math.Max(mouseDownLocation.Y, e.Location.Y);
                        Rectangle area = new Rectangle(l, t, r - l + 1, b - t + 1);
                        OnAnimBoxQuaded(area);
                        break;
                }
            }

            base.OnMouseUp(e);
        }

        private void OnAnimBoxMoving(Point offset)
        {
            if (AnimBoxMoving != null)
                AnimBoxMoving(this, new AnimBoxMoveEventArgs(offset));
        }

        private void OnAnimBoxMoved(Point offset)
        {
            if (AnimBoxMoved != null)
                AnimBoxMoved(this, new AnimBoxMoveEventArgs(offset));
        }

        private void OnAnimBoxQuading(Rectangle area)
        {
            if (AnimBoxQuading != null)
                AnimBoxQuading(this, new AnimBoxQuadEventArgs(area));
        }

        private void OnAnimBoxQuaded(Rectangle area)
        {
            if (AnimBoxQuaded != null)
                AnimBoxQuaded(this, new AnimBoxQuadEventArgs(area));
        }

        public void Clear()
        {
            IndexInSequence = -1;

            Image = null;
        }
    }
}
