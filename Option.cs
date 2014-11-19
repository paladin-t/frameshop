using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Frameshop.Data;

namespace Frameshop
{
    internal class Option : PList
    {
        public static readonly string PLUGIN_FOLDER = Path.GetDirectoryName(Application.ExecutablePath) + "\\plugin";
        public static readonly string COMMAND_EXT = ".fsc";

        public static readonly string CMD_ESC_FULL = "$FULL$";
        public static readonly string CMD_ESC_PARENT = "$PARENT$";
        public static readonly string CMD_ESC_FILE = "$FILE$";
        public static readonly string CMD_ESC_EXT = "$EXT$";

        public static string GetCommandFilePath(string name)
        {
            return PLUGIN_FOLDER + "\\" + name + COMMAND_EXT;
        }

        private string SettingsFilePath
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath) + "\\options.plist";
            }
        }

        private Option()
        {
        }

        private static Option self = null;
        public static Option GetInstance()
        {
            if (self == null)
                self = new Option();

            return self;
        }

        public void Init()
        {
            PList winProp = new PList();
            this["win_prop"] = winProp;
            winProp["max"] = true;
            winProp["x"] = 20;
            winProp["y"] = 10;
            winProp["w"] = 800;
            winProp["h"] = 600;

            PList winAnim = new PList();
            this["win_anim"] = winAnim;
            winAnim["max"] = false;
            winAnim["x"] = 50;
            winAnim["y"] = 30;
            winAnim["w"] = 640;
            winAnim["h"] = 480;

            PList fsc = new PList();
            this["commands"] = fsc;
            fsc["folder"] = string.Empty;
            fsc["tex"] = string.Empty;
            fsc["data"] = string.Empty;
            fsc["anim"] = string.Empty;
            fsc["call_anim_at_pub"] = true;

            PList misc = new PList();
            this["misc"] = misc;
            misc["first_run"] = true;
            misc["last_dir"] = Environment.CurrentDirectory;
            misc["ask_before_rem_frame"] = true;
            misc["add_folder_recursively"] = true;
        }

        public void Load()
        {
            if (File.Exists(SettingsFilePath))
                Load(SettingsFilePath);
            else
                Init();
        }

        public void Save()
        {
            Save(SettingsFilePath);
        }
    }
}
