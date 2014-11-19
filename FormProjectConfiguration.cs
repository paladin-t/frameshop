using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frameshop
{
    internal partial class FormProjectConfiguration : Form
    {
        public int WidthData
        {
            get;
            private set;
        }

        public int HeightData
        {
            get;
            private set;
        }

        public float VectorTextureScale
        {
            get;
            private set;
        }

        public FormProjectConfiguration()
        {
            InitializeComponent();

            cmbWidth.Items.Add("32");
            cmbWidth.Items.Add("64");
            cmbWidth.Items.Add("128");
            cmbWidth.Items.Add("256");
            cmbWidth.Items.Add("512");
            cmbWidth.Items.Add("1024");
            cmbWidth.Items.Add("2048");
            cmbWidth.Items.Add("4096");
            cmbWidth.Items.Add("8192");

            cmbHeight.Items.Add("32");
            cmbHeight.Items.Add("64");
            cmbHeight.Items.Add("128");
            cmbHeight.Items.Add("256");
            cmbHeight.Items.Add("512");
            cmbHeight.Items.Add("1024");
            cmbHeight.Items.Add("2048");
            cmbHeight.Items.Add("4096");
            cmbHeight.Items.Add("8192");

            cmbWidth.SelectedIndex = 4;
            cmbHeight.SelectedIndex = 4;

            txtVecTexScale.Text = (1.0f).ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormProjectConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            int widthData = 64;
            int heightData = 64;

            int.TryParse(cmbWidth.Text, out widthData);
            int.TryParse(cmbHeight.Text, out heightData);

            WidthData = widthData;
            HeightData = heightData;

            float vectorTextureScale = 1.0f;

            float.TryParse(txtVecTexScale.Text, out vectorTextureScale);
            if (vectorTextureScale <= 0.0f || vectorTextureScale == float.NaN)
                vectorTextureScale = 1.0f;
            VectorTextureScale = vectorTextureScale;
        }
    }
}
