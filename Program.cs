using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Frameshop.Data;

namespace Frameshop
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
            try
#endif
            {
                if (args.Length == 0)
                {
                    Application.Run(new FormMain());
                }
                else
                {
                    if (args.Length == 1 && args.First().ToLower() == "-e")
                    {
                        Application.Run(new FormMain());
                    }
                    else
                    {
                        if (args.Length >= 5 && args.First().ToLower() == "-p")
                        {
                            Util.OpenConsole();

                            string pageName = args[1];
                            int width, height;
                            if (!int.TryParse(args[2], out width))
                                throw new Exception("Invalid width " + args[2] + ".");
                            if (!int.TryParse(args[3], out height))
                                throw new Exception("Invalid height " + args[3] + ".");
                            string outPath = args[4];
                            List<string> files = new List<string>();
                            for (int i = 5; i < args.Length; i++)
                            {
                                FileInfo fi = new FileInfo(args[i]);
                                files.Add(fi.FullName);
                            }
                            if (files.Count != 0)
                                (new Package()).PackPage(pageName, width, height, outPath, files.ToArray());

                            Util.CloseConsole();
                        }
                        else
                        {
                            string file = string.Empty;
                            foreach (string arg in args)
                                file += arg + " ";
                            Application.Run(new FormMain(file));
                        }
                    }
                }
            }
#if !DEBUG
            catch (Exception ex)
            {
                string msg = string.Empty;
                if (ex.InnerException != null)
                    msg += "Inner: " + ex.InnerException.Message + Environment.NewLine;
                if (ex.Message != null)
                    msg += "Message: " + ex.Message;

                Util.Error(null, msg);
            }
#endif
        }
    }
}
