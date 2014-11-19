using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using BalloonCS;

namespace Frameshop
{
    public partial class FormOption : Form
    {
        private static readonly string CMD_TIP =
            "  $FULL$   : full path;" + Environment.NewLine +
            "  $PARENT$ : parent folder;" + Environment.NewLine +
            "  $FILE$   : file name;" + Environment.NewLine +
            "  $EXT$    : file extention;" + Environment.NewLine +
            "  use these escape to represent published files/folders.";

        private HoverBalloon balloon = new HoverBalloon();

        private bool changed = false;
        private bool Changed
        {
            get
            {
                return changed;
            }
            set
            {
                changed = value;
                btnReset.Enabled = changed;
            }
        }

        public FormOption()
        {
            InitializeComponent();
        }

        private void FormOption_Load(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Command
            Option.GetInstance()["commands"]["folder"] = cmbFolder.SelectedItem as string ?? string.Empty;
            Option.GetInstance()["commands"]["tex"] = cmbTexture.SelectedItem as string ?? string.Empty;
            Option.GetInstance()["commands"]["data"] = cmbData.SelectedItem as string ?? string.Empty;
            Option.GetInstance()["commands"]["anim"] = cmbAnim.SelectedItem as string ?? string.Empty;
            Option.GetInstance()["commands"]["call_anim_at_pub"] = ckbConvertAnim.Checked;
            Option.GetInstance()["misc"]["ask_before_rem_frame"] = ckbAskBeforeRemoveFrame.Checked;
            Option.GetInstance()["misc"]["add_folder_recursively"] = ckbAddFolderRecursively.Checked;

            // Misc
            if (ckbFileRel.Checked)
                Util.RelateFileType(Misc.PRJ_FILE_EXT, Misc.PRJ_FILE_TYPE, Misc.PRJ_FILE_DESC, Misc.PRJ_FILE_ICON);
            else
                Util.UnrelateFileType(Misc.PRJ_FILE_TYPE);

            // Close
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEditFolder_Click(object sender, EventArgs e)
        {
            Edit(cmbFolder);
        }

        private void btnEditTexture_Click(object sender, EventArgs e)
        {
            Edit(cmbTexture);
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            Edit(cmbData);
        }

        private void btnEditAnim_Click(object sender, EventArgs e)
        {
            Edit(cmbAnim);
        }

        private void btnWhatFolder_Click(object sender, EventArgs e)
        {
            string msg = "This command will be invoked with target folder as an argument." + Environment.NewLine + CMD_TIP;

            if (balloon != null)
                balloon.Dispose();
            balloon = new HoverBalloon();
            balloon.Title = "Frameshop";
            balloon.TitleIcon = TooltipIcon.Info;
            balloon.SetToolTip(btnWhatFolder, msg);
        }

        private void btnWhatTex_Click(object sender, EventArgs e)
        {
            string msg = "This command will be invoked with target textures one by one as arguments." + Environment.NewLine + CMD_TIP;
            if (balloon != null)
                balloon.Dispose();
            balloon = new HoverBalloon();
            balloon.Title = "Frameshop";
            balloon.TitleIcon = TooltipIcon.Info;
            balloon.SetToolTip(btnWhatTex, msg);
        }

        private void btnWhatData_Click(object sender, EventArgs e)
        {
            string msg = "This command will be invoked with target data files one by one as arguments." + Environment.NewLine + CMD_TIP;
            if (balloon != null)
                balloon.Dispose();
            balloon = new HoverBalloon();
            balloon.Title = "Frameshop";
            balloon.TitleIcon = TooltipIcon.Info;
            balloon.SetToolTip(btnWhatData, msg);
        }

        private void btnWhatConvertAnim_Click(object sender, EventArgs e)
        {
            string msg = "Animation command will be only called automatically when saving an animation sequence as default;" +
                "enable this to call it with files at the project folder when publishing as well.";
            if (balloon != null)
                balloon.Dispose();
            balloon = new HoverBalloon();
            balloon.Title = "Frameshop";
            balloon.TitleIcon = TooltipIcon.Info;
            balloon.SetToolTip(btnWhatConvertAnim, msg);
        }

        private void btnWhatAnim_Click(object sender, EventArgs e)
        {
            string msg = "This command will be invoked with animation files one by one as arguments." + Environment.NewLine + CMD_TIP;
            if (balloon != null)
                balloon.Dispose();
            balloon = new HoverBalloon();
            balloon.Title = "Frameshop";
            balloon.TitleIcon = TooltipIcon.Info;
            balloon.SetToolTip(btnWhatAnim, msg);
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void ckb_CheckedChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void Edit(ComboBox cmb)
        {
            string name = cmb.SelectedItem as string ?? cmb.Text;

            if (string.IsNullOrEmpty(name))
                return;

            string file = Option.GetCommandFilePath(name);
            if (!File.Exists(file))
            {
                if (Util.AskYesNo(this, "File doesn't exist, create it?") == DialogResult.Yes)
                {
                    using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                    }

                    AddValidPlugin(Path.GetFileNameWithoutExtension(file));
                    cmb.SelectedIndex = cmb.Items.Count - 1;
                }
                else
                {
                    return;
                }
            }

            string prompt = "Input your command."+ Environment.NewLine + "Escape:" + Environment.NewLine + CMD_TIP;
            string cmd = File.ReadAllText(file);
            string t = Util.InputBox(this, prompt, cmd);
            if (t != null)
                File.WriteAllText(file, t);
        }

        private void Select(ComboBox cmb, string txt)
        {
            cmb.SelectedItem = txt;
        }

        private void Reset()
        {
            // Command
            cmbFolder.Text = string.Empty;
            cmbTexture.Text = string.Empty;
            cmbData.Text = string.Empty;
            cmbAnim.Text = string.Empty;

            cmbFolder.Items.Clear();
            cmbTexture.Items.Clear();
            cmbData.Items.Clear();
            cmbAnim.Items.Clear();

            cmbFolder.Items.Add(string.Empty);
            cmbTexture.Items.Add(string.Empty);
            cmbData.Items.Add(string.Empty);
            cmbAnim.Items.Add(string.Empty);

            DirectoryInfo diri = new DirectoryInfo(Option.PLUGIN_FOLDER);
            FileInfo[] fscs = diri.GetFiles("*" + Option.COMMAND_EXT);
            foreach (FileInfo fsc in fscs)
            {
                string name = fsc.Name.Substring(0, (int)(fsc.Name.Length - Option.COMMAND_EXT.Length));
                AddValidPlugin(name);
            }

            string fi = Option.GetInstance()["commands"]["folder"];
            string ti = Option.GetInstance()["commands"]["tex"];
            string di = Option.GetInstance()["commands"]["data"];
            string ai = Option.GetInstance()["commands"]["anim"];
            if (!string.IsNullOrEmpty(fi))
                Select(cmbFolder, fi);
            if (!string.IsNullOrEmpty(ti))
                Select(cmbTexture, ti);
            if (!string.IsNullOrEmpty(di))
                Select(cmbData, di);
            if (!string.IsNullOrEmpty(ai))
                Select(cmbAnim, ai);

            bool ca = Option.GetInstance()["commands"]["call_anim_at_pub"];
            ckbConvertAnim.Checked = ca;

            // Misc
            ckbFileRel.Checked = Util.IsFileTypeRelated(Misc.PRJ_FILE_TYPE);

            bool ab = Option.GetInstance()["misc"]["ask_before_rem_frame"];
            ckbAskBeforeRemoveFrame.Checked = ab;

            bool ar = Option.GetInstance()["misc"]["add_folder_recursively"];
            ckbAddFolderRecursively.Checked = ar;

            Changed = false;
        }

        private void AddValidPlugin(string name)
        {
            cmbFolder.Items.Add(name);
            cmbTexture.Items.Add(name);
            cmbData.Items.Add(name);
            cmbAnim.Items.Add(name);
        }
    }
}
