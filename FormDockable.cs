using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace Frameshop
{
    internal class FormDockable<T> : DockContent where T : DockContent, new()
    {
        private static T self = null;

        public static T GetInstance()
        {
            if (self == null)
                self = new T();

            return self;
        }
    }
}
