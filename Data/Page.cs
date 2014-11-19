using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace Frameshop.Data
{
    public class Page
    {
        private static int nameSeed = 0;

        private List<string> frames = null;

        private bool constructing = false;

        private string name = null;
        [Category("Designing")]
        [Description("Page name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Util.Output("Invalid name.");

                    return;
                }

                const string invalidChars = ",.!?*:|<>\"/\\";
                foreach (char c in invalidChars)
                {
                    if (value.Contains(c))
                    {
                        Util.Output("Invalid name, a page name cannot contains any of these charactors: , . ! ? * : | < > \" / \\.");

                        return;
                    }
                }

                int idx = FormView.GetInstance().SelectedIndex;
                int fnd = Package.Current.GetPage(value);
                if (fnd != -1 && idx != fnd)
                {
                    Util.Output("A page named \"" + value + "\" already exists.");

                    return;
                }

                if (name != null)
                    Util.Output("Renamed page \"" + name + "\" to \"" + value + "\".");

                Package.Current.ChangePageName();

                name = value;
            }
        }

        [Browsable(false)]
        public int ColorDepth
        {
            get;
            set;
        }

        private bool allowRotation = true;
        [Category("Frame Layout")]
        [Description("Allow frame rotation")]
        public bool AllowRotation
        {
            get
            {
                return allowRotation;
            }
            set
            {
                bool old = allowRotation;
                allowRotation = value;

                if (Package.Current.PageCount == 0)
                    return;
                int idx = FormView.GetInstance().SelectedIndex;
                if (!Package.Current.TryChangeFrameRotationEnabled(new FrameRotationEnabledChangedEventArgs(allowRotation, idx)))
                {
                    Util.Output("This page is too small to hold its contents.");

                    allowRotation = old;

                    FormProperty.GetInstance().Reload();
                }
            }
        }

        private int padding = 0;
        [Category("Frame Layout")]
        [Description("Border padding")]
        public int Padding
        {
            get
            {
                return padding;
            }
            set
            {
                int old = padding;
                padding = value;

                if (Package.Current.PageCount == 0)
                    return;
                int idx = FormView.GetInstance().SelectedIndex;
                if (!Package.Current.TryChangeFramePadding(new FramePaddingChangedEventArgs(padding, idx)))
                {
                    Util.Output("This page is too small to hold its contents.");

                    padding = old;

                    FormProperty.GetInstance().Reload();
                }
            }
        }

        private Size textureSize;
        [Category("Geometry")]
        [Description("Texture size of a page")]
        public Size TextureSize
        {
            get
            {
                return textureSize;
            }
            set
            {
                Size s = value;
                if (s == textureSize)
                    return;

                Size old = textureSize;
                textureSize = s;

                if (Package.Current.PageCount == 0)
                    return;
                if (!constructing)
                {
                    int idx = FormView.GetInstance().SelectedIndex;
                    if (!Package.Current.TryChangePageSize(new PageSizeChangedEventArgs(s, idx)))
                    {
                        Util.Output
                        (
                            "This page is too small to hold its contents." + Environment.NewLine +
                            "The proerty won't be changed, please remove big images if you'd like to narrow a page."
                        );

                        textureSize = old;

                        FormProperty.GetInstance().Reload();
                    }
                }
            }
        }

        [Category("Geometry")]
        [Description("Memory occupation estimate")]
        [ReadOnly(true)]
        public string MemoryOccupy
        {
            get
            {
                float b = TextureSize.Width * TextureSize.Height * ColorDepth / 1024.0f;
                string s = string.Format("{0:n} KB", b);

                return s;
            }
        }

        #region List wrapper
        
        //[Browsable(false)]
        //public new int Capacity
        //{
        //    get
        //    {
        //        return base.Capacity;
        //    }
        //    set
        //    {
        //        base.Capacity = value;
        //    }
        //}

        [Browsable(false)]
        public string this[int index]
        {
            get
            {
                return frames[index];
            }
        }

        [Browsable(false)]
        public List<string> Data
        {
            get
            {
                return frames;
            }
        }

        #endregion

        public Page()
        {
            frames = new List<string>();
        }

        public Page(Size ts)
        {
            constructing = true;

            string n = "page_" + nameSeed.ToString();
            while (Package.Current.GetPage(n) != -1)
            {
                nameSeed++;
                n = "page_" + nameSeed.ToString();
            }
            Name = n;

            frames = new List<string>();

            TextureSize = ts;

            ColorDepth = 4;

            constructing = false;
        }
    }
}
