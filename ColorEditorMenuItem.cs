using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;

namespace Frameshop
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
        ToolStripItemDesignerAvailability.ContextMenuStrip)]
    internal class ColorEditorMenuItem : ToolStripControlHost
    {
        private ColorBox colorBox = null;

        public Color Color
        {
            get
            {
                return Color.FromArgb(colorBox.A, colorBox.R, colorBox.G, colorBox.B);
            }
            set
            {
                colorBox.A = value.A;
                colorBox.R = value.R;
                colorBox.G = value.G;
                colorBox.B = value.B;
            }
        }

        public ColorEditorMenuItem()
            : base(new ColorBox())
        {
            colorBox = Control as ColorBox;
        }
    }
}
