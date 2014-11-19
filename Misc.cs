using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Frameshop.Data;
using Frameshop.Anim;

namespace Frameshop
{
    #region Delegates
    
    internal delegate bool FramePaddingChanged(object sender, FramePaddingChangedEventArgs e);

    internal delegate bool FrameRotationEnabledChanged(object sender, FrameRotationEnabledChangedEventArgs e);

    internal delegate bool PageSizeChanged(object sender, PageSizeChangedEventArgs e);

    internal delegate bool NameChanged(object sender, EventArgs e);

    #endregion

    #region Event args
    
    internal class PageEventBaseWithCannotHoldEventArgs : EventArgs
    {
        public List<Frame> CannotHold
        {
            get;
            private set;
        }

        public List<Frame> Rotated
        {
            get;
            private set;
        }

        public int Index
        {
            get;
            private set;
        }

        public PageEventBaseWithCannotHoldEventArgs(int idx)
        {
            CannotHold = new List<Frame>();
            Rotated = new List<Frame>();
            Index = idx;
        }
    }

    internal class FramePaddingChangedEventArgs : PageEventBaseWithCannotHoldEventArgs
    {
        public int PixelCount
        {
            get;
            private set;
        }

        public FramePaddingChangedEventArgs(int pix, int idx)
            : base(idx)
        {
            PixelCount = pix;
        }
    }

    internal class FrameRotationEnabledChangedEventArgs : PageEventBaseWithCannotHoldEventArgs
    {
        public bool Enabled
        {
            get;
            private set;
        }

        public FrameRotationEnabledChangedEventArgs(bool e, int idx)
            : base(idx)
        {
            Enabled = e;
        }
    }

    internal class PageSizeChangedEventArgs : PageEventBaseWithCannotHoldEventArgs
    {
        public Size Size
        {
            get;
            private set;
        }

        public PageSizeChangedEventArgs(Size s, int idx)
            : base(idx)
        {
            Size = s;
        }
    }

    internal class PageEventArgs : EventArgs
    {
        public int PageIndex
        {
            get;
            set;
        }

        public PageEventArgs(int _page)
        {
            PageIndex = _page;
        }
    }

    internal class FrameEventArgs : EventArgs
    {
        public int PageIndex
        {
            get;
            set;
        }

        public string FrameName
        {
            get;
            set;
        }

        public FrameEventArgs(int _page, string _frame)
        {
            PageIndex = _page;
            FrameName = _frame;
        }
    }

    internal class ColorChangedEventArgs : EventArgs
    {
        public string Name
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public ColorChangedEventArgs(string name, int index, Color color)
        {
            Name = name;
            Index = index;
            Color = color;
        }
    }

    internal class FrameNameChangedEventArgs : EventArgs
    {
        public string Name
        {
            get;
            private set;
        }

        public AnimFrame Anim
        {
            get;
            private set;
        }

        public FrameNameChangedEventArgs(string fn, AnimFrame a)
        {
            Name = fn;
            Anim = a;
        }
    }

    internal class FrameOffsetChangedEventArgs : EventArgs
    {
        public Point Offset
        {
            get;
            private set;
        }

        public AnimFrame Anim
        {
            get;
            private set;
        }

        public FrameOffsetChangedEventArgs(Point offset, AnimFrame a)
        {
            Offset = offset;
            Anim = a;
        }
    }

    internal class FrameAlphaChangedEventArgs : EventArgs
    {
        public byte Alpha
        {
            get;
            private set;
        }

        public AnimFrame Anim
        {
            get;
            private set;
        }

        public FrameAlphaChangedEventArgs(byte alpha, AnimFrame a)
        {
            Alpha = alpha;
            Anim = a;
        }
    }

    internal class AnimBoxMoveEventArgs : EventArgs
    {
        public Point Offset
        {
            get;
            private set;
        }

        public AnimBoxMoveEventArgs(Point offset)
        {
            Offset = offset;
        }
    }

    internal class AnimBoxQuadEventArgs : EventArgs
    {
        public Rectangle Area
        {
            get;
            private set;
        }

        public AnimBoxQuadEventArgs(Rectangle area)
        {
            Area = area;
        }
    }

    #endregion

    #region Exceptions
    
    internal class CancelAddingWhenTooManyColor : Exception
    {
    }

    #endregion

    #region Page cache

    internal sealed class PageShadow
    {
        public PList PList { get; set; }

        public Bitmap Bitmap { get; set; }

        public PageShadow(PList pl, Bitmap bmp)
        {
            PList = pl;
            Bitmap = bmp;
        }
    }

    internal sealed class PageCache : Dictionary<string, PageShadow>
    {
        public new PageShadow this[string name]
        {
            get
            {
                if (!ContainsKey(name))
                    return null;

                return base[name];
            }
            set
            {
                if (ContainsKey(name) && value == null)
                    Remove(name);
                else
                    base[name] = value;
            }
        }
    }

    #endregion

    #region Misc

    internal static class Misc
    {
        public static readonly int BINARY_PACKAGE_VERSION = 0x00010001;

        public static readonly string PRJ_FILE_EXT = ".fsp";
        public static readonly string PRJ_FILE_TYPE = "Frameshop Project";
        public static readonly string PRJ_FILE_DESC = "Frameshop Project";
        public static readonly string PRJ_FILE_ICON = "0";

        public static readonly string ANIM_FILE_EXT = ".ani";
    }

    #endregion
}
