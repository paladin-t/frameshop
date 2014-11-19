using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Frameshop.Data
{
    public class ProjectData
    {
        [Category("Geometry")]
        [Description("Texture size of each pack")]
        public Size TextureSize
        {
            get;
            set;
        }

        [Category("Geometry")]
        [Description("Vector texture size scale")]
        public float VectorTextureScale
        {
            get;
            set;
        }

        public ProjectData()
        {
            TextureSize = new Size(512, 512);

            VectorTextureScale = 1.0f;
        }
    }
}
