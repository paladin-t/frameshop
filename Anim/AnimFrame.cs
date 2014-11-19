using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Frameshop.Data;

namespace Frameshop.Anim
{
    internal class AnimFrame
    {
        public enum InterpolateAlgorithm
        {
            None,
            Linear
        }

        public static string ToString(InterpolateAlgorithm i)
        {
            if (i == InterpolateAlgorithm.None)
                return "none";
            else if (i == InterpolateAlgorithm.Linear)
                return "linear";

            throw new Exception("Invalid");
        }

        public static InterpolateAlgorithm ParseInterpolation(string s)
        {
            if (s == "none")
                return InterpolateAlgorithm.None;
            else if (s == "linear")
                return InterpolateAlgorithm.Linear;

            return InterpolateAlgorithm.None;
        }

        public event EventHandler<FrameNameChangedEventArgs> FrameNameChanged;

        public event EventHandler<FrameOffsetChangedEventArgs> FrameOffsetChanged;

        public event EventHandler<FrameAlphaChangedEventArgs> FrameAlphaChanged;

        private Sequence Owner
        {
            get;
            set;
        }

        [Category("Designing")]
        [Description("Source page name")]
        public virtual string PageName { get; set; }

        private string name = null;
        [Category("Designing")]
        [Description("Frame name alias")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                    Util.Error(null, "Cannot use a blank name");
                else if (Owner.Frames.Count((f) => { return f.Name == value.Trim(); }) != 0)
                    Util.Error(null, "Name conflict");
                else
                    name = value.Trim();
            }
        }

        private string frameName = null;
        [Category("Designing")]
        [Description("Source frame name")]
        public virtual string FrameName
        {
            get
            {
                return frameName;
            }
            set
            {
                if (!Package.Current.FrameNames.Contains(value))
                {
                    Util.Error(null, "Invalid frame: \"" + value + "\".");
                }
                else
                {
                    frameName = value;

                    OnFrameNameChanged(frameName);
                }
            }
        }

        [Category("Animation")]
        [Description("Retain time")]
        public float Time
        {
            get;
            set;
        }

        [Category("Animation")]
        [Description("Interpolate algorithm")]
        public InterpolateAlgorithm Interpolation
        {
            get;
            set;
        }

        private byte alpha = byte.MaxValue;
        [Category("Animation")]
        [Description("Frame alpha value")]
        public byte Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;

                OnFrameAlphaChanged();
            }
        }

        private Point offset = Point.Empty;
        [Category("Frame Layout")]
        [Description("Offset from center")]
        public virtual Point Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;

                OnFrameOffsetChanged();
            }
        }

        [Category("Geometry")]
        [Description("Rectangle 0")]
        public Rectangle Rect0
        {
            get;
            set;
        }

        [Category("Geometry")]
        [Description("Rectangle 1")]
        public Rectangle Rect1
        {
            get;
            set;
        }

        [Category("Geometry")]
        [Description("Rectangle 2")]
        public Rectangle Rect2
        {
            get;
            set;
        }

        [Category("Geometry")]
        [Description("Rectangle 3")]
        public Rectangle Rect3
        {
            get;
            set;
        }

        [Category("Geometry")]
        [Description("Rectangle 4")]
        public Rectangle Rect4
        {
            get;
            set;
        }

        [Category("Misc")]
        [Description("Tag string")]
        public string Tag
        {
            get;
            set;
        }

        public AnimFrame(Sequence seq)
        {
            Owner = seq;
            Time = 0.1f;
            Interpolation = InterpolateAlgorithm.Linear;
            Alpha = 255;
        }

        public AnimFrame(Sequence seq, string page, string frame)
        {
            Owner = seq;
            PageName = page;
            FrameName = frame;
            Time = 0.1f;
            Interpolation = InterpolateAlgorithm.Linear;
            Alpha = 255;
        }

        private void OnFrameNameChanged(string fn)
        {
            if (FrameNameChanged != null)
                FrameNameChanged(Owner, new FrameNameChangedEventArgs(fn, this));
        }

        private void OnFrameOffsetChanged()
        {
            if (FrameOffsetChanged != null)
                FrameOffsetChanged(Owner, new FrameOffsetChangedEventArgs(Offset, this));
        }

        private void OnFrameAlphaChanged()
        {
            if (FrameAlphaChanged != null)
                FrameAlphaChanged(Owner, new FrameAlphaChangedEventArgs(Alpha, this));
        }
    }

    internal class BlankAnimFrame : AnimFrame
    {
        [Category("Designing")]
        [Description("Source page name")]
        [ReadOnly(true)]
        public override string PageName
        {
            get { return string.Empty; }
            set { }
        }

        [Category("Designing")]
        [Description("Source frame name")]
        [ReadOnly(true)]
        public override string FrameName
        {
            get { return string.Empty; }
            set { }
        }

        [Category("Frame Layout")]
        [Description("Offset from center")]
        [ReadOnly(true)]
        public override Point Offset
        {
            get { return Point.Empty; }
            set { }
        }

        public BlankAnimFrame(Sequence seq)
            : base(seq)
        {
        }
    }
}
