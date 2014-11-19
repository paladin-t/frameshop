using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace Frameshop.Data
{
    public class Frame : IComparer<Frame>
    {
        #region Fields

        public static bool OutputLoadInformation = false;

        #endregion

        #region Properties

        [XmlIgnore]
        public bool Invalid
        {
            get
            {
                return coloredImage == null;
            }
        }

        public int OwnerPageIndex
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        private string paletteName = null;
        public string PaletteName
        {
            get
            {
                return paletteName;
            }
            set
            {
                paletteName = value;
            }
        }

        private string filePath = null;
        /**
         * NOTE: The setter can receive both absolute and relative paths.
         *       The getter returns a relative path, if the file is on a different
         *       disk from working folder, then returns a absolute path.
         */
        public string FilePath
        {
            get
            {
                string rel = Util.GetRelativePathFromWorkingDirectory(filePath);

                return rel;
            }
            set
            {
                filePath = value;

                if (string.IsNullOrEmpty(filePath))
                {
                    coloredImage = null;

                    Name = null;
                }
                else
                {
                    Uri file = new Uri(filePath, UriKind.RelativeOrAbsolute);
                    if (!file.IsAbsoluteUri)
                        filePath = Environment.CurrentDirectory + "\\" + filePath;

                    coloredImage = Util.LoadImageFile(FilePath);

                    Name = Path.GetFileName(FilePath);

                    if (OutputLoadInformation)
                    {
                        if (Invalid)
                            Util.Output("Failed to load texture file \"" + filePath + "\".");
                        else
                            Util.Output("Texture file \"" + filePath + "\" loaded.");

                        Application.DoEvents();
                    }
                }
            }
        }

        /**
         * NOTE: Since most of the time we can only get a relative path via FilePath property,
         *       use this one in some cases such as packing.
         */
        [XmlIgnore]
        public string AbsoluteFilePath
        {
            get
            {
                return filePath;
            }
        }

        private Image coloredImage = null;
        [XmlIgnore]
        public Image Image
        {
            get
            {
                return coloredImage;
            }
        }

        #region Helper properties

        [XmlIgnore]
        public Rectangle Area
        {
            get;
            set;
        }

        [XmlIgnore]
        public Rectangle FinalArea
        {
            get
            {
                Rectangle ret = Area;
                if (Rotated)
                {
                    int t = ret.Width;
                    ret.Width = ret.Height;
                    ret.Height = t;
                }

                return ret;
            }
        }

        [XmlIgnore]
        public Point Offset
        {
            get;
            set;
        }

        private bool rotated = false;
        [XmlIgnore]
        public bool Rotated
        {
            get
            {
                Page p = Package.Current.GetPage(OwnerPageIndex);
                if (p != null && !p.AllowRotation)
                    return false;

                return rotated;
            }
            set
            {
                rotated = value;
            }
        }

        [XmlIgnore]
        public Rectangle SourceColorRect
        {
            get;
            set;
        }

        [XmlIgnore]
        public Size SourceSize
        {
            get;
            set;
        }

        [XmlIgnore]
        public int OrderScore
        {
            get
            {
                if (Image != null)
                    return Image.Width * Image.Height + Image.Height * Image.Height;
                else
                    return 0;
            }
        }

        #endregion

        #endregion

        #region Methods

        public Frame()
        {
            FilePath = null;
        }

        public Frame(string filePath)
        {
            FilePath = filePath;
        }

        public override string ToString()
        {
            return "Frame: " + Name;
        }

        public int Compare(Frame x, Frame y)
        {
            return x.FilePath.CompareTo(y.FilePath);
        }

        #endregion
    }
}
