using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Frameshop
{
    internal partial class FormOutput : DockContent
    {
        private static FormOutput self = null;

        public static FormOutput GetInstance()
        {
            return self;
        }

        public FormOutput()
        {
            self = this;

            InitializeComponent();

            CloseButton = false;
            CloseButtonVisible = false;
        }

        public void Clear()
        {
            txtOut.Clear();
        }

        public void Append(string txt)
        {
            BeginInvoke
            (
                new Action
                (
                    () =>
                    {
                        txtOut.AppendText(txt);
                        txtOut.AppendText(Environment.NewLine);
                    }
                )
            );
        }

        private void btnNewline_Click(object sender, EventArgs e)
        {
            btnNewline.Checked = !btnNewline.Checked;
            if (btnNewline.Checked)
                txtOut.ScrollBars = ScrollBars.Vertical;
            else
                txtOut.ScrollBars = ScrollBars.Both;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
