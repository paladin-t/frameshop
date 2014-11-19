using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Frameshop
{
    [ToolboxBitmap(typeof(ComboBox))]
    internal class ComboBoxEx : ComboBox
    {
        private bool _internal = false;

        private string text = null;

        private string blankText = null;
        public string BlankText
        {
            get
            {
                return blankText;
            }
            set
            {
                blankText = value;

                OnGotFocus(EventArgs.Empty);
                OnLostFocus(EventArgs.Empty);
            }
        }

        public ComboBoxEx()
        {
            BlankText = "Blank";
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (_internal)
            {
                // Do nothing
            }
            else
            {
                base.OnTextChanged(e);
                text = Text;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            _internal = true;
            base.Text = text;
            _internal = false;
            Font = new Font(Font, FontStyle.Regular);
            ForeColor = Color.Black;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (string.IsNullOrEmpty(text))
            {
                _internal = true;
                base.Text = BlankText;
                _internal = false;
                Font = new Font(Font, FontStyle.Italic);
                ForeColor = Color.Gray;
            }
        }
    }
}
