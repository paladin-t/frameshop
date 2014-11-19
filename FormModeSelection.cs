using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Frameshop.Data;

namespace Frameshop
{
    internal partial class FormModeSelection : Form
    {
        public FormModeSelection()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormModeSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
