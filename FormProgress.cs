using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Frameshop
{
    internal partial class FormProgress : Form
    {
        public float Percentage
        {
            set
            {
                progBar.Value = (int)(value * 1000.0f);
                Application.DoEvents();
                if (value >= 1.0f)
                    Close();
            }
        }

        public FormProgress()
        {
            InitializeComponent();
            progBar.Maximum = 1000;
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            Size rect = new Size(284, 46);
            int dw = rect.Width - ClientSize.Width;
            int dh = rect.Height - ClientSize.Height;
            Size = new Size(Size.Width + dw, Size.Height + dh);
        }
    }
}
