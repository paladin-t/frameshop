using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Frameshop.Data;

namespace Frameshop
{
    internal partial class FormProperty : FormDockable<FormProperty>
    {
        public object SelectedObject
        {
            get { return propGrid.SelectedObject; }
            set { propGrid.SelectedObject = value; }
        }

        public event PropertyValueChangedEventHandler PropertyValueChanged;

        public FormProperty()
        {
            InitializeComponent();

            Package.Current.PageRemoved += Current_PageRemoved;

            CloseButton = false;
            CloseButtonVisible = false;

            propGrid.PropertyValueChanged += propGrid_PropertyValueChanged;
        }

        private void Current_PageRemoved(object sender, PageEventArgs e)
        {
            propGrid.SelectedObject = null;
        }

        private void propGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (PropertyValueChanged != null)
                PropertyValueChanged(s, e);
        }

        public void Clear()
        {
            propGrid.SelectedObject = null;
        }

        public void Reload()
        {
            propGrid.Refresh();
        }
    }
}
