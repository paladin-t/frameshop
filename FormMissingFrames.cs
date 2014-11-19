using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Frameshop.Data;

namespace Frameshop
{
    public partial class FormMissingFrames : Form
    {
        private DialogResult dialogResult = DialogResult.Cancel;

        private List<Frame> frames = null;

        private List<string> pathes = null;
        public ReadOnlyCollection<string> Pathes
        {
            get
            {
                return pathes.AsReadOnly();
            }
        }

        public FormMissingFrames(List<Frame> fs)
        {
            InitializeComponent();

            frames = fs;
            pathes = new List<string>();
            foreach (Frame f in frames)
            {
                lstMissing.Items.Add(f.Name);

                pathes.Add(null);
            }
        }

        private void lstMissing_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void lstMissing_DoubleClick(object sender, EventArgs e)
        {
            if (lstMissing.SelectedIndex == -1)
                return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FormMain.IMAGE_FILTER;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                pathes[lstMissing.SelectedIndex] = ofd.FileName;

                if (((string)lstMissing.SelectedItem).Last() != '*')
                    lstMissing.Items[lstMissing.SelectedIndex] = (string)lstMissing.SelectedItem + " *";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.OK;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;

            Close();
        }

        public new DialogResult ShowDialog(IWin32Window wnd)
        {
            base.ShowDialog(wnd);

            return dialogResult;
        }
    }
}
