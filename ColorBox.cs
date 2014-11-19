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
    internal partial class ColorBox : UserControl
    {
        private byte a;
        public byte A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
                trackA.Value = value;
                numA.Value = value;
                Preview();
            }
        }

        private byte r;
        public byte R
        {
            get
            {
                return r;
            }
            set
            {
                r = value;
                trackR.Value = value;
                numR.Value = value;
                Preview();
            }
        }

        private byte g;
        public byte G
        {
            get
            {
                return g;
            }
            set
            {
                g = value;
                trackG.Value = value;
                numG.Value = value;
                Preview();
            }
        }

        private byte b;
        public byte B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                trackB.Value = value;
                numB.Value = value;
                Preview();
            }
        }

        public ColorBox()
        {
            InitializeComponent();

            Preview();
        }

        private void trackA_Scroll(object sender, EventArgs e)
        {
            a = (byte)trackA.Value;
            numA.Value = trackA.Value;
            Preview();
        }

        private void trackR_Scroll(object sender, EventArgs e)
        {
            r = (byte)trackR.Value;
            numR.Value = trackR.Value;
            Preview();
        }

        private void trackG_Scroll(object sender, EventArgs e)
        {
            g = (byte)trackG.Value;
            numG.Value = trackG.Value;
            Preview();
        }

        private void trackB_Scroll(object sender, EventArgs e)
        {
            b = (byte)trackB.Value;
            numB.Value = trackB.Value;
            Preview();
        }

        private void numA_ValueChanged(object sender, EventArgs e)
        {
            a = (byte)numA.Value;
            trackA.Value = (int)numA.Value;
            Preview();
        }

        private void numR_ValueChanged(object sender, EventArgs e)
        {
            r = (byte)numR.Value;
            trackR.Value = (int)numR.Value;
            Preview();
        }

        private void numG_ValueChanged(object sender, EventArgs e)
        {
            g = (byte)numG.Value;
            trackG.Value = (int)numG.Value;
            Preview();
        }

        private void numB_ValueChanged(object sender, EventArgs e)
        {
            b = (byte)numB.Value;
            trackB.Value = (int)numB.Value;
            Preview();
        }

        private void Preview()
        {
            pnlPreview.BackColor = Color.FromArgb(a, r, g, b);
        }
    }
}
