using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Threading;
using WeifenLuo.WinFormsUI.Docking;
using Frameshop.Data;

namespace Frameshop
{
    internal partial class FormMain : Form
    {
        #region Fields

        public static readonly string PROJECT_FILTER = "Project files(*" + Misc.PRJ_FILE_EXT + ")|*" + Misc.PRJ_FILE_EXT + "|All files(*.*)|*.*";

        public static readonly string IMAGE_FILTER = "Texture files(*.png, *.bmp, *.jpg, *.svg, *.tga)|*.png;*.bmp;*.jpg;*.svg;*.tga|All files(*.*)|*.*";

        private FormProperty propertyPanel = null;

        private FormFrames framesPanel = null;

        private FormView viewPanel = null;

        private FormOutput outputPanel = null;

        private Package package = null;

        private string postOpen = null;

        private string lastDir = null;

        #endregion

        #region Methods

        #region Constructor

        public FormMain()
        {
            Construct();
        }

        public FormMain(string file)
        {
            Construct();

            postOpen = file;
        }

        private void Construct()
        {
            self = this;

            InitializeComponent();
#if DEBUG
            Text = "Frameshop (Debug)";
#else
            Text = "Frameshop";
#endif

            Util.ScreenShotSize = new Size(50, 50);

            package = new Package();
            package.EditStateChanged += package_EditStateChanged;
            package.FrameRemovedFromPage += package_FrameRemovedFromPage;

            propertyPanel = FormProperty.GetInstance();
            framesPanel = new FormFrames();
            viewPanel = FormView.GetInstance();
            outputPanel = new FormOutput();

            framesPanel.OriginalImageRemoved += framesPanel_OriginalImageRemoved;

            propertyPanel.Enabled = false;
            viewPanel.Enabled = false;
            framesPanel.Enabled = false;
            outputPanel.Enabled = false;
        }

        #endregion

        #region Singleton

        private static FormMain self = null;

        public static FormMain GetInstance()
        {
            return self;
        }

        #endregion

        #region WinForm event handlers

        private void FormMain_Load(object sender, EventArgs e)
        {
            Option.GetInstance().Load();
            try
            {
                Size = new Size
                (
                    Option.GetInstance()["win_prop"]["w"],
                    Option.GetInstance()["win_prop"]["h"]
                );
                Location = new Point
                (
                    Option.GetInstance()["win_prop"]["x"],
                    Option.GetInstance()["win_prop"]["y"]
                );
                if (Screen.AllScreens.Length == 1)
                {
                    int l = Location.X;
                    int t = Location.Y;
                    if (l < Screen.PrimaryScreen.WorkingArea.Left)
                        l = Screen.PrimaryScreen.WorkingArea.Left;
                    if (t < Screen.PrimaryScreen.WorkingArea.Top)
                        t = Screen.PrimaryScreen.WorkingArea.Top;
                    if (l + Size.Width >= Screen.PrimaryScreen.WorkingArea.Width)
                        l = Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 1;
                    if (t + Size.Height >= Screen.PrimaryScreen.WorkingArea.Height)
                        t = Screen.PrimaryScreen.WorkingArea.Height - Size.Height - 1;
                    Location = new Point(l, t);
                }
                if (Option.GetInstance()["win_prop"]["max"])
                    WindowState = FormWindowState.Maximized;

                if (Option.GetInstance()["misc"]["first_run"])
                {
                    Util.RelateFileType(Misc.PRJ_FILE_EXT, Misc.PRJ_FILE_TYPE, Misc.PRJ_FILE_DESC, Misc.PRJ_FILE_ICON);

                    Option.GetInstance()["misc"]["first_run"] = false;
                }

                lastDir = Option.GetInstance()["misc"]["last_dir"];

                // Not now
                Action<object> check = (_o) => { if (_o == null) throw new Exception(); };
                check(Option.GetInstance()["win_anim"]["max"]);
                check(Option.GetInstance()["win_anim"]["x"]);
                check(Option.GetInstance()["win_anim"]["y"]);
                check(Option.GetInstance()["win_anim"]["w"]);
                check(Option.GetInstance()["win_anim"]["h"]);
                check(Option.GetInstance()["commands"]["folder"]);
                check(Option.GetInstance()["commands"]["tex"]);
                check(Option.GetInstance()["commands"]["data"]);
                check(Option.GetInstance()["commands"]["anim"]);
                check(Option.GetInstance()["commands"]["call_anim_at_pub"]);
                check(Option.GetInstance()["misc"]["ask_before_rem_frame"]);
                check(Option.GetInstance()["misc"]["add_folder_recursively"]);
            }
            catch
            {
                Option.GetInstance().Init();
            }

            propertyPanel.Show(dockPanel, DockState.DockLeft);
            //magnifierPanel.Show(dockPanel, DockState.DockRight);
            //palletPanel.Show(dockPanel, DockState.DockRight);
            framesPanel.Show(dockPanel, DockState.DockRight);
            viewPanel.Show(dockPanel, DockState.Document);
            viewPanel.Show(dockPanel, DockState.Document);
            outputPanel.Show(dockPanel, DockState.DockBottom);

            if (!string.IsNullOrEmpty(postOpen))
            {
                try
                {
                    Enabled = false;

                    int oldsi = viewPanel.SelectedIndex;
                    viewPanel.SelectedIndex = 0;
                    if (!Open(postOpen))
                    {
                        viewPanel.SelectedIndex = oldsi;

                        return;
                    }
                }
#if !DEBUG
                catch (Exception ex)
                {
                    throw ex;
                }
#endif
                finally
                {
                    postOpen = null;
                    Enabled = true;
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ConfirmSaving())
            {
                e.Cancel = true;
            }
            else
            {
                Option.GetInstance()["win_prop"]["max"] = WindowState == FormWindowState.Maximized;
                Option.GetInstance()["win_prop"]["x"] = Left;
                Option.GetInstance()["win_prop"]["y"] = Top;
                Option.GetInstance()["win_prop"]["w"] = Width;
                Option.GetInstance()["win_prop"]["h"] = Height;

                Option.GetInstance()["misc"]["last_dir"] = lastDir;

                Option.GetInstance().Save();
            }
        }

        private void toolStripContainer_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void toolStripContainer_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<string> files = new List<string>();
                foreach (string path in paths)
                {
                    FileInfo fi = new FileInfo(path);
                    if (fi.Exists)
                        files.Add(path);
                }

                if (files.Count > 1)
                {
                    var left = files.Skip(1);

                    Thread t = new Thread(new ThreadStart(() => { foreach (string l in left)Process.Start(Application.ExecutablePath, l); }));
                    t.Priority = ThreadPriority.Highest;
                    t.Start();
                    Thread.Sleep(0);
                }

                Open(() => files.First());
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (ConfirmSaving())
            {
                framesPanel.Clear();
                viewPanel.Clear();
                outputPanel.Clear();
                propertyPanel.Clear();

                outputPanel.Clear();

                FormProjectConfiguration fpss = new FormProjectConfiguration();
                fpss.ShowDialog(this);
                Package.Current.ProjectData.TextureSize = new Size(fpss.WidthData, fpss.HeightData);
                Package.Current.ProjectData.VectorTextureScale = fpss.VectorTextureScale;

                Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                Package.Current.New();

                viewPanel.Reload(false);

                outputPanel.Append("Project created.");
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Open
            (
                () =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = PROJECT_FILTER;
                    if (ofd.ShowDialog(this) != DialogResult.OK)
                        return null;

                    return ofd.FileName;
                }
            );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            outputPanel.Clear();

            if (Save())
                outputPanel.Append("Project \"" + Package.Current.SavePath + "\" saved.");
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            outputPanel.Clear();

            if (SaveAs())
                outputPanel.Append("Project \"" + Package.Current.SavePath + "\" saved.");
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = IMAGE_FILTER;
            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;

            AddImage(ofd.FileNames);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(lastDir))
                fbd.SelectedPath = lastDir;
            if (fbd.ShowDialog(this) != DialogResult.OK)
                return;

            lastDir = fbd.SelectedPath;

            AddFolder(fbd.SelectedPath);
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Package.Current.SavePath))
            {
                Util.Notice(this, "Please save the project before publishing it.");

                if (!Save())
                    return;
            }

            Dictionary<string, PList> colorPacks = new Dictionary<string, PList>();

            List<string> texs = new List<string>();
            List<string> plists = new List<string>();

            string publishFolder = Path.GetDirectoryName(Package.Current.SavePath);
            int s = 0;
            string binPath = Path.GetFileNameWithoutExtension(Package.Current.SavePath) + ".bin";

            for (int i = 0; i < Package.Current.PageCount; i++)
            {
                Page p = Package.Current.GetPage(i);
                string pubPageName = p.Name;
                string publishFile = publishFolder + "\\" + pubPageName;
                if (Util.Pack(i, publishFile, null/*bfw*/, texs, plists, null, null))
                {
                    s++;

                    PList plist = new PList(publishFile + ".plist");
                    colorPacks[publishFile] = plist;
                }
                else
                {
                    Util.Error(this, "Failed to pack page " + i.ToString() + ".");
                }
            }

            Enabled = false;
            Thread t = new Thread
            (
                new ThreadStart
                (
                    () =>
                    {
                        PostEvent(publishFolder, texs, plists);

                        if (Option.GetInstance()["commands"]["call_anim_at_pub"])
                        {
                            DirectoryInfo pubDirInfo = new DirectoryInfo(publishFolder);
                            FileInfo[] aniFiles = pubDirInfo.GetFiles("*.ani");
                            foreach (FileInfo aniFile in aniFiles)
                                FormMain.GetInstance().PostAnimEvent(aniFile.FullName);
                        }

                        BeginInvoke
                        (
                            new Action
                            (
                                () =>
                                {
                                    Enabled = true;

                                    if (Util.AskYesNo(this, Util.Count(s, "page") + " published.\nOpen output folder?") == DialogResult.Yes)
                                        Process.Start(publishFolder);
                                }
                            )
                        );
                    }
                )
            );
            t.Priority = ThreadPriority.Highest;
            t.Start();
            Thread.Sleep(0);
        }

        private void btnAnim_Click(object sender, EventArgs e)
        {
            FormAnimation fa = new FormAnimation();
            fa.ShowDialog(this);
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            FormOption fo = new FormOption();
            fo.ShowDialog(this);
        }

        #region Help

        private void menuAbout_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog(this);
        }

        private void menuWeb_Click(object sender, EventArgs e)
        {
            Util.OpenWebsite("https://github.com/paladin-t/frameshop");
        }
        
        private void menuHelp_Click(object sender, EventArgs e)
        {
            Util.OpenWebsite((new FileInfo(Application.ExecutablePath)).Directory.FullName + "/manual.htm");
        }

        private void btnHelp_ButtonClick(object sender, EventArgs e)
        {
            Util.OpenWebsite((new FileInfo(Application.ExecutablePath)).Directory.FullName + "/manual.htm");
        }

        private void buy_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #endregion

        #region Logic event handlers

        private void framesPanel_OriginalImageRemoved(object sender, EventArgs e)
        {
            viewPanel.Reload(false);
        }

        private void package_FrameRemovedFromPage(object sender, EventArgs e)
        {
            if (e is FrameEventArgs)
                viewPanel.Reload(false, ((FrameEventArgs)e).FrameName);
            else
                viewPanel.Reload(false);
        }

        private void package_PaletteRemoved(object sender, EventArgs e)
        {
            viewPanel.Reload(false);
        }

        private void package_EditStateChanged(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            btnOpen.Enabled = false;
            btnSave.Enabled = false;
            btnSaveAs.Enabled = false;
            btnAddImage.Enabled = false;
            btnAddFolder.Enabled = false;
            btnPublish.Enabled = false;
            btnAnim.Enabled = false;
            viewPanel.Text = viewPanel.OrignalTitle;
            propertyPanel.Enabled = false;
            viewPanel.Enabled = false;
            framesPanel.Enabled = false;
            outputPanel.Enabled = false;

            switch (Package.Current.EditState)
            {
                case EditStates.Closed:
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
                    break;
                case EditStates.New:
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
                    btnSave.Enabled = true;
                    btnSaveAs.Enabled = true;
                    btnAddImage.Enabled = true;
                    btnAddFolder.Enabled = true;
                    btnPublish.Enabled = true;
                    btnAnim.Enabled = true;
                    viewPanel.Text += " *";
                    propertyPanel.Enabled = true;
                    viewPanel.Enabled = true;
                    framesPanel.Enabled = true;
                    outputPanel.Enabled = true;
                    break;
                case EditStates.OpenedNotSaved:
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
                    btnSave.Enabled = true;
                    btnSaveAs.Enabled = true;
                    btnAddImage.Enabled = true;
                    btnAddFolder.Enabled = true;
                    btnPublish.Enabled = true;
                    btnAnim.Enabled = true;
                    viewPanel.Text += " *";
                    propertyPanel.Enabled = true;
                    viewPanel.Enabled = true;
                    framesPanel.Enabled = true;
                    outputPanel.Enabled = true;
                    break;
                case EditStates.Saved:
                    btnNew.Enabled = true;
                    btnOpen.Enabled = true;
                    btnSaveAs.Enabled = true;
                    btnAddImage.Enabled = true;
                    btnAddFolder.Enabled = true;
                    btnPublish.Enabled = true;
                    btnAnim.Enabled = true;
                    propertyPanel.Enabled = true;
                    viewPanel.Enabled = true;
                    framesPanel.Enabled = true;
                    outputPanel.Enabled = true;
                    break;
            }
        }

        #endregion

        #region Common methods

        public void AddImage(string[] paths)
        {
            if (paths.Length == 0)
                return;

            using (Util.Disabler d = new Util.Disabler(this))
            {
                if (!Package.Current.AddImage(this, paths))
                    return;
            }

            framesPanel.Reload();
            viewPanel.Reload(false);
        }

        public void AddFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            using (Util.Disabler d = new Util.Disabler(this))
            {
                if (!Package.Current.AddFolder(this, path))
                    return;
            }

            framesPanel.Reload();
            viewPanel.Reload(false);
        }

        public void AddFolder(string[] paths)
        {
            if (paths.Length == 0)
                return;

            using (Util.Disabler d = new Util.Disabler(this))
            {
                foreach (string path in paths)
                    AddFolder(path);
            }
        }

        #region TMP
        
        /*
        private void Publish(Dictionary<string, PList> plists, string outputFile)
        {
            string imgMode = "Color";
            if (Package.Current.ProjectData.ColorMode == ColorModes._16Colors)
                imgMode = "Palette16";
            else if (Package.Current.ProjectData.ColorMode == ColorModes._256Colors)
                imgMode = "Palette256";

            XDocument doc = new XDocument
            (
                new XElement
                (
                    "ExtendedImage",
                    new XAttribute("Mode", imgMode),
                    new XElement("Images"),
                    new XElement("Frames")
                )
            );

            bool paletteMode = Package.Current.ProjectData.ColorMode != ColorModes._32Bit;
            XElement paletteElems = null;
            if (paletteMode)
            {
                paletteElems = new XElement("Palettes");
                doc.Element("ExtendedImage").Add(paletteElems);
            }
            XElement imageElems = doc.Element("ExtendedImage").Element("Images");
            XElement frameElems = doc.Element("ExtendedImage").Element("Frames");

            foreach (var plist in plists)
            {
                string path = Util.GetRelativePathFromWorkingDirectory(plist.Key + ".png");
                string imgPath = Path.GetFileName(plist.Key);

                imageElems.Add
                (
                    new XElement
                    (
                        paletteMode ? "IndexedImage" : "ColorImage",
                        new XAttribute("Image", imgPath),
                        new XAttribute("Path", path)
                    )
                );

                if (paletteMode)
                {
                    foreach (Palette p in Package.Current.AllPalettes)
                    {
                        paletteElems.Add
                        (
                            new XElement
                            (
                                "Palette",
                                new XAttribute("Name", p.Name),
                                new XAttribute("Path", p.Name + Palette.PALETTE_EXTENTION_NAME)
                            )
                        );
                    }
                }

                if (plist.Value.Count > 0)
                {
                    foreach (dynamic frame in plist.Value["frames"])
                    {
                        Rectangle rect = Util.ParseRect(frame.Value["frame"]);

                        dynamic frameInfo = frame.Value;
                        XElement fe = new XElement
                        (
                            "Frame",
                            new XAttribute("Name", frame.Key),
                            new XAttribute("Image", imgPath),
                            new XAttribute("X", rect.X),
                            new XAttribute("Y", rect.Y),
                            new XAttribute("Width", rect.Width),
                            new XAttribute("Height", rect.Height)
                        );
                        if (paletteMode)
                        {
                            string pal = frame.Value["palette"];
                            fe.Add(new XAttribute("DefaultPalette", pal));
                        }
                        frameElems.Add(fe);
                    }
                }
            }

            doc.Save(outputFile);
        }
        */

        #endregion

        public void PostAnimEvent(string anim)
        {
            string ai = Option.GetInstance()["commands"]["anim"];

            FileInfo file = new FileInfo(anim);
            PostEvent
            (
                ai,
                file.FullName,
                file.Directory.FullName,
                Path.GetFileNameWithoutExtension(anim),
                file.Extension
            );
        }

        private void PostEvent(string folder, IEnumerable<string> texs, IEnumerable<string> plists)
        {
            string fi = Option.GetInstance()["commands"]["folder"];
            string ti = Option.GetInstance()["commands"]["tex"];
            string di = Option.GetInstance()["commands"]["data"];

            DirectoryInfo dir = new DirectoryInfo(folder);
            PostEvent(fi, dir.FullName, dir.Parent.FullName, dir.Name, dir.Extension);

            foreach (string tex in texs)
            {
                FileInfo file = new FileInfo(tex);
                if (file.FullName.ContainsWideChar())
                {
                    string newfile = file.FullName;

                    using (TemporaryFile tmp = new TemporaryFile(file))
                    {
                        file = tmp.FileInfo;
                        PostEvent
                        (
                            ti,
                            file.FullName,
                            file.Directory.FullName,
                            Path.GetFileNameWithoutExtension(tex),
                            file.Extension
                        );
                    }

                    FileInfo[] bros = file.Directory.GetFiles(Path.GetFileNameWithoutExtension(file.FullName) + ".*");
                    foreach (FileInfo bro in bros)
                    {
                        newfile = Path.ChangeExtension(newfile, bro.Extension);
                        bro.MoveTo(newfile);

                        outputPanel.Append("File moved to \"" + newfile + "\".");
                    }
                }
                else
                {
                    PostEvent
                    (
                        ti,
                        file.FullName,
                        file.Directory.FullName,
                        Path.GetFileNameWithoutExtension(tex),
                        file.Extension
                    );
                }
            }

            foreach (string plist in plists)
            {
                FileInfo file = new FileInfo(plist);
                PostEvent
                (
                    di,
                    file.FullName,
                    file.Directory.FullName,
                    Path.GetFileNameWithoutExtension(plist),
                    file.Extension
                );
            }
        }

        private void PostEvent(string cmdName, string full, string parent, string file, string ext)
        {
            if (string.IsNullOrEmpty(cmdName))
                return;

            string cmdFile = Option.GetCommandFilePath(cmdName);
            if (!File.Exists(cmdFile))
                return;

            string cmd = File.ReadAllText(cmdFile);
            cmd = cmd.Replace(Option.CMD_ESC_FULL, full);
            cmd = cmd.Replace(Option.CMD_ESC_PARENT, parent);
            cmd = cmd.Replace(Option.CMD_ESC_FILE, file);
            cmd = cmd.Replace(Option.CMD_ESC_EXT, ext);

            int spl = cmd.IndexOf(' ');
            string exe = cmd;
            string arg = string.Empty;
            if (spl != -1)
            {
                exe = cmd.Substring(0, spl);
                arg = cmd.Substring(spl + 1, cmd.Length - spl - 1);
            }

            Process p = new Process();
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = arg;
            p.StartInfo.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();

            while (!p.HasExited)
            {
                while (!p.StandardError.EndOfStream)
                {
                    string msg = p.StandardError.ReadToEnd();
                    outputPanel.Append(msg);
                }
                while (!p.StandardOutput.EndOfStream)
                {
                    string msg = p.StandardOutput.ReadToEnd();
                    outputPanel.Append(msg);
                }
            }

            p.WaitForExit();
        }

        private bool ConfirmSaving()
        {
            if (/*Package.Current.EditState == EditStates.New || */Package.Current.EditState == EditStates.OpenedNotSaved)
            {
                DialogResult dr = Util.AskYesNoCancel(this, "Current project not saved, save it before other operations?");
                if (dr == DialogResult.Yes)
                    return Save();
                else if (dr == DialogResult.No)
                    return true;
                else
                    return false;
            }

            return true;
        }

        private bool Save()
        {
            if (Package.Current.SavePath == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = PROJECT_FILTER;
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return false;

                Environment.CurrentDirectory = Path.GetDirectoryName(sfd.FileName);
                Package.Current.SavePath = sfd.FileName;
            }

            Package.Current.Save();

            return true;
        }

        private bool SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = PROJECT_FILTER;
            if (sfd.ShowDialog(this) != DialogResult.OK)
                return false;

            Environment.CurrentDirectory = Path.GetDirectoryName(sfd.FileName);
            Package.Current.SavePath = sfd.FileName;

            Package.Current.Save();

            return true;
        }

        private bool Open(string file)
        {
            using
            (
                PListKeyNotFoundExceptionEventHolder holder = new PListKeyNotFoundExceptionEventHolder
                ((_s, _e) => { Util.Output("Corrupt data around: " + _e.Key); })
            )
            {
                using (Util.RAII raii = new Util.RAII("OPENING_PROJECT"))
                {
                    Environment.CurrentDirectory = Path.GetDirectoryName(file);
                    using (Util.FrameOutputLoadInformation foli = new Util.FrameOutputLoadInformation())
                    {
                        if (!Package.Current.Open(file))
                            return false;
                    }

                    var invalids = from f in Package.Current.Frames
                                   where f.Invalid
                                   select f;
                    if (invalids.Count() != 0)
                    {
                        if (Util.AskYesNo(this, "Some frame assets missing, remove them?") == DialogResult.Yes)
                        {
                            //var evt = Package.Current.BackupFrameRemovedFromPageEventHandlers();
                            //Package.Current.ClearFrameRemovedFromPageEventHandlers();

                            var invalidNames = (from f in invalids
                                                select f.FilePath).ToArray();
                            foreach (string i in invalidNames)
                            {
                                Package.Current.RemoveImage(i);
                            }

                            //Package.Current.RestoreFrameRemovedFromPageEventHandlers(evt);
                        }
                    }

                    framesPanel.Reload();
                    viewPanel.Reload(true);

                    viewPanel.ReloadTreeView();

                    return true;
                }
            }
        }

        private bool Open(Func<string> filer)
        {
            if (ConfirmSaving())
            {
                string file = filer();
                if (string.IsNullOrEmpty(file))
                    return false;

                viewPanel.ResetPage();

                propertyPanel.Clear();

                outputPanel.Clear();

                int oldsi = viewPanel.SelectedIndex;
                viewPanel.SelectedIndex = 0;
                using (Util.Disabler d = new Util.Disabler(this))
                {
                    if (!Open(file))
                    {
                        viewPanel.SelectedIndex = oldsi;

                        return false;
                    }
                }

                outputPanel.Append("Project \"" + file + "\" opened.");

                return true;
            }

            return false;
        }

        #endregion

        #endregion
    }
}
