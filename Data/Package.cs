using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;

namespace Frameshop.Data
{
    internal class Package : IComparer<Color>
    {
        #region Delegates

        internal delegate string PaletteNameGeneratorFunc();

        #endregion

        #region Events

        public event EventHandler EditStateChanged;

        public event FramePaddingChanged FramePaddingChanged;

        public event FrameRotationEnabledChanged FrameRotationEnabledChanged;

        public event PageSizeChanged PageSizeChanged;

        public event EventHandler<PageEventArgs> PageAdded;

        public event EventHandler<PageEventArgs> PageRemoved;

        public event EventHandler<FrameEventArgs> FrameAddedToPage;

        public event EventHandler<FrameEventArgs> FrameRemovedFromPage;

        public event EventHandler FrameReloaded;

        #endregion

        #region Fields

        private Serializer serializer = null;

        private ProjectData projectData = null;

        private List<Frame> frames = null;

        private List<Page> pages = null;

        #endregion

        #region Properties

        public static Package Current
        {
            get;
            private set;
        }

        public ProjectData ProjectData
        {
            get
            {
                return projectData;
            }
        }

        public IEnumerable<Frame> FrameCollection
        {
            get
            {
                return frames;
            }
        }

        public int FrameCount
        {
            get
            {
                return frames.Count;
            }
        }

        public Frame this[int index]
        {
            get
            {
                return frames[index];
            }
        }

        public Frame this[string name]
        {
            get
            {
                return frames.SingleOrDefault(_f => _f.Name == name);
            }
        }

        public IEnumerable<Frame> Frames
        {
            get
            {
                return frames;
            }
        }

        public IEnumerable<string> FrameNames
        {
            get
            {
                return from f in frames select f.Name;
            }
        }

        public int PageCount
        {
            get
            {
                return pages.Count;
            }
        }

        public string SavePath
        {
            get;
            set;
        }

        private EditStates editState = EditStates.Closed;
        public EditStates EditState
        {
            get { return editState; }
            private set
            {
                //if (editState == EditStates.New && value == EditStates.OpenedNotSaved)
                    //return;
                if (editState != value)
                {
                    editState = value;
                    OnEditStateChanged();
                }
            }
        }

        #endregion

        #region Methods

        #region Constructor

        public Package()
        {
            serializer = new Serializer();

            Current = this;

            projectData = new Data.ProjectData();

            frames = new List<Frame>();

            pages = new List<Page>();

            SavePath = null;

            AddPage();

            EditState = EditStates.Closed;
        }

        #endregion

        #region File operations

        public void New()
        {
            frames.Clear();
            pages.Clear();
            AddPage();
            SavePath = null;
            EditState = EditStates.New;
        }

        public bool Open(string path)
        {
            try
            {
                string projectText = File.ReadAllText(path);
                var fileContents = from _c in projectText.Split(new string[] { "<?xml version=\"1.0\"?>" }, StringSplitOptions.None)
                                   where !string.IsNullOrEmpty(_c)
                                   select _c;

                string metaPath = Path.ChangeExtension(path, ".pt");
                File.WriteAllText(metaPath, fileContents.ElementAt(0));
                projectData = serializer.Deserialize<ProjectData>(metaPath);

                string projectDataPath = Path.ChangeExtension(path, ".pp");
                File.WriteAllText(projectDataPath, fileContents.ElementAt(1));
                frames = serializer.Deserialize<List<Frame>>(projectDataPath);

                string pagesPath = Path.ChangeExtension(path, ".pg");
                File.WriteAllText(pagesPath, fileContents.ElementAt(2));
                pages.Clear();
                pages = serializer.Deserialize<List<Page>>(pagesPath);

                File.Delete(metaPath);
                File.Delete(projectDataPath);
                File.Delete(pagesPath);

                SavePath = path;

                EditState = EditStates.Saved;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + Environment.NewLine + "Cannot read a bad or old formatted file.";
                Util.Output("Failed to open project file: " + path);
                Util.Notice(null, msg);

                return false;
            }

            return true;
        }

        public bool Save()
        {
            if (SavePath == null)
                return false;

            string metaPath = Path.ChangeExtension(SavePath, ".pt");
            serializer.Serialize(metaPath, projectData);

            string projectDataPath = Path.ChangeExtension(SavePath, ".pp");
            serializer.Serialize(projectDataPath, frames);

            string pagesPath = Path.ChangeExtension(SavePath, ".pg");
            serializer.Serialize(pagesPath, pages);

            string prj = string.Empty;
            prj += File.ReadAllText(metaPath) + Environment.NewLine;
            prj += File.ReadAllText(projectDataPath) + Environment.NewLine;
            prj += File.ReadAllText(pagesPath) + Environment.NewLine;
            File.WriteAllText(SavePath, prj);
            File.Delete(metaPath);
            File.Delete(projectDataPath);
            File.Delete(pagesPath);

            EditState = EditStates.Saved;

            return true;
        }

        #endregion

        #region Image operations

        public bool AddImage(IWin32Window wnd, string[] filePaths)
        {
            if (filePaths.Length == 0)
                return false;

            FormProgress fp = new FormProgress();
            fp.Show(wnd);

            return AddImage(filePaths, _f => fp.Percentage = _f);
        }

        private bool AddImage(string[] filePaths, Action<float> updateProgress)
        {
            if (filePaths.Length == 0)
                return false;

            float total = (float)filePaths.Length;
            float curr = 0.0f;
            updateProgress(0.0f);
            List<string> exists = new List<string>();
            foreach (string filePath in filePaths)
            {
                do
                {
                    int existsCount = frames.Count
                    (
                        (_f) =>
                        {
                            string _fp = Path.GetFileName(filePath);

                            return _f.Name == _fp && !_f.Invalid;
                        }
                    );
                    if (existsCount != 0)
                    {
                        exists.Add(Path.GetFileName(filePath));
                    }
                    else
                    {
                        try
                        {
                            Frame f = new Frame(filePath);
                            for (int i = 0; i < frames.Count; i++)
                            {
                                if (frames[i].Name == f.Name)
                                {
                                    frames.RemoveAt(i);

                                    break;
                                }
                            }
                            frames.Add(f);

                            Util.Output("Texture file \"" + filePath + "\" added.");
                        }
                        catch (CancelAddingWhenTooManyColor)
                        {
                            break;
                        }
                        catch
                        {
                            Util.Output("Failed to add texture file \"" + filePath + "\".");
                        }

                        frames.Sort(new Frame());

                        //AddFrameToPage(frames.Last().Name);
                    }
                } while (false);

                curr += 1.0f;
                float per = curr / total;
                updateProgress(per);
            }

            if (exists.Count == 1)
            {
                string msg = " There is alteady a texture named:" + Environment.NewLine + exists.First();
                Util.Notice(null, msg);
            }
            else if (exists.Count > 1)
            {
                string msg = "In the pack there are already some textures named:" + Environment.NewLine;
                foreach (string e in exists)
                    msg += e + Environment.NewLine;
                Util.Notice(null, msg);
            }

            EditState = EditStates.OpenedNotSaved;

            return true;
        }

        public bool AddFolder(IWin32Window wnd, string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if (!di.Exists)
                return false;

            bool ar = Option.GetInstance()["misc"]["add_folder_recursively"];
            string[] exts = new string[] { ".png", ".bmp", ".jpg", ".svg", ".tga" };
            var files = ar ? di.GetFilesByExtensionsRecursively(exts) : di.GetFilesByExtensions(exts);
            var fileNames = from fi in files
                            select fi.FullName;
            if (!AddImage(wnd, fileNames.ToArray()))
                return false;

            EditState = EditStates.OpenedNotSaved;

            return true;
        }

        public bool RemoveImage(string filePath)
        {
            Frame f = frames.SingleOrDefault((_f) => { return _f.FilePath == filePath; });
            if (f == null)
                return false;

            frames.Remove(f);

            EditState = EditStates.OpenedNotSaved;

            for (int p = 0; p < pages.Count; p++)
            {
                Page page = pages[p];
                for (int i = page.Data.Count - 1; i >= 0; i--)
                {
                    if (page[i] == f.Name)
                    {
                        page.Data.RemoveAt(i);

                        OnFrameRemovedFromPage(new FrameEventArgs(p, f.Name));
                    }
                }
            }

            return true;
        }

        public bool ReloadImage(Frame frame)
        {
            if (!File.Exists(frame.AbsoluteFilePath))
                return false;
            frame.FilePath = frame.AbsoluteFilePath;

            if (FrameReloaded != null)
                FrameReloaded(this, EventArgs.Empty);

            return true;
        }

        #endregion

        #region Page operations

        public int GetPage(string name)
        {
            if (pages == null)
                return -1;

            for (int i = 0; i < pages.Count; i++)
            {
                Page p = pages[i];
                if (p.Name == name)
                    return i;
            }

            return -1;
        }

        public Page GetPage(int index)
        {
            if (index < 0 || index >= pages.Count)
                return null;

            return pages[index];
        }

        public bool AddPage()
        {
            pages.Add(new Page(ProjectData.TextureSize));

            EditState = EditStates.OpenedNotSaved;

            OnPageAdded(new PageEventArgs(pages.Count - 1));

            return true;
        }

        public bool RemovePage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= pages.Count)
                return false;

            pages.RemoveAt(pageIndex);

            EditState = EditStates.OpenedNotSaved;

            OnPageRemoved(new PageEventArgs(pageIndex));

            return true;
        }

        public bool AddFrameToPage(int pageIndex, string frameName)
        {
            if (pageIndex < 0 || pageIndex >= pages.Count)
                return false;

            if (pages[pageIndex].Data.Contains(frameName))
                return false;

            pages[pageIndex].Data.Add(frameName);
            this[frameName].OwnerPageIndex = pageIndex;

            EditState = EditStates.OpenedNotSaved;

            OnFrameAddedToPage(new FrameEventArgs(pageIndex, frameName));

            return true;
        }

        public bool AddFrameToPage(string frameName)
        {
            Func<int, string, bool> tryAdd = (_p, _f) =>
            {
                AddFrameToPage(_p, frameName);

                Size s = Package.Current.GetPage(_p).TextureSize;
                PList plist = null;
                Bitmap bmp = new Bitmap(s.Width, s.Height);
                Util.PackResult ret = Util.Pack(_p, bmp, ref plist, Util.GrabColored, Util.FillColored, Util.InformIndexed, null, null);
                if (ret == Util.PackResult.OK || ret == Util.PackResult.Missing)
                {
                    EditState = EditStates.OpenedNotSaved;

                    return true;
                }
                else
                {
                    RemoveFrameFromPage(_p, frameName);

                    return false;
                }
            };

            for (int i = 0; i < pages.Count; i++)
            {
                if (tryAdd(i, frameName))
                {
                    EditState = EditStates.OpenedNotSaved;

                    return true;
                }
            }

            //AddPage();
            //if (tryAdd(PageCount - 1, frameName))
            //{
            //    EditState = EditStates.OpenedNotSaved;

            //    return true;
            //}
            //else
            //{
            //    RemovePage(PageCount - 1);
            //}

            return false;
        }

        public bool RemoveFrameFromPage(int pageIndex, string frameName)
        {
            if (pageIndex < 0 || pageIndex >= pages.Count)
                return false;

            if (!pages[pageIndex].Data.Contains(frameName))
                return false;

            pages[pageIndex].Data.Remove(frameName);
            this[frameName].OwnerPageIndex = -1;

            EditState = EditStates.OpenedNotSaved;

            OnFrameRemovedFromPage(new FrameEventArgs(pageIndex, frameName));

            return true;
        }

        public IEnumerable<string> GetFrameNamesInPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= pages.Count)
                return null;

            return pages[pageIndex].Data;
        }

        public IEnumerable<Frame> GetFramesInPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= pages.Count)
                return null;

            var frames = from n in pages[pageIndex].Data
                         where this[n] != null
                         select this[n];

            return frames;
        }

        #endregion

        #region Event handlers

        public void GlobalPropertyChanged()
        {
            EditState = EditStates.OpenedNotSaved;
        }

        public bool TryOperation(MulticastDelegate evt, dynamic e, string msg)
        {
            if (evt != null)
            {
                bool result = (bool)evt.DynamicInvoke(this, e);
                if (result)
                {
                    EditState = EditStates.OpenedNotSaved;

                    return true;
                }
                else
                {
                    string dups = string.Empty;
                    foreach (Frame dup in e.CannotHold)
                        dups += "  " + dup.Name + Environment.NewLine;

                    string q = "Some frames cannot be held in current page:" + Environment.NewLine + dups +
                        msg;
                    if (Util.AskYesNo(FormMain.GetInstance(), q) == DialogResult.Yes)
                    {
                        foreach (Frame dup in e.CannotHold)
                            RemoveFrameFromPage(e.Index, dup.Name);

                        EditState = EditStates.OpenedNotSaved;

                        return true;
                    }
                    else
                    {
                        foreach (Frame rf in e.Rotated)
                        {
                            rf.Rotated = false;
                        }

                        return false;
                    }
                }
            }

            throw new Exception("This event must be observed");
        }

        public bool TryChangeFramePadding(FramePaddingChangedEventArgs e)
        {
            return TryOperation(FramePaddingChanged, e,
                "Change the padding anyway?" + Environment.NewLine +
                "Click \"Yes\" to change padding and remove these frames, or click \"No\" to revert padding."
            );
        }

        public bool TryChangeFrameRotationEnabled(FrameRotationEnabledChangedEventArgs e)
        {
            return TryOperation(FrameRotationEnabledChanged, e,
                "Disable rotation anyway?" + Environment.NewLine +
                "Click \"Yes\" to disable rotation and remove these frames, or click \"No\" to enable rotation again."
            );
        }

        public bool TryChangePageSize(PageSizeChangedEventArgs e)
        {
            return TryOperation(PageSizeChanged, e,
                "Resize this page anyway?" + Environment.NewLine +
                "Click \"Yes\" to resize and remove these frames, or click \"No\" to revert resizing."
            );
        }

        public void ChangePageName()
        {
            EditState = EditStates.OpenedNotSaved;
        }

        private void OnPageAdded(PageEventArgs e)
        {
            if (PageAdded != null)
                PageAdded(this, e);
        }

        private void OnPageRemoved(PageEventArgs e)
        {
            if (PageRemoved != null)
                PageRemoved(this, e);
        }

        private void OnFrameAddedToPage(FrameEventArgs e)
        {
            if (FrameAddedToPage != null)
                FrameAddedToPage(this, e);
        }

        private void OnFrameRemovedFromPage(FrameEventArgs e)
        {
            if (FrameRemovedFromPage != null)
                FrameRemovedFromPage(this, e);
        }

        private void OnEditStateChanged()
        {
            if (EditStateChanged != null)
                EditStateChanged(this, EventArgs.Empty);
        }

        public void ClearFrameRemovedFromPageEventHandlers()
        {
            FrameRemovedFromPage = null;
        }

        public MulticastDelegate BackupFrameRemovedFromPageEventHandlers()
        {
            return FrameRemovedFromPage;
        }

        public void RestoreFrameRemovedFromPageEventHandlers(MulticastDelegate d)
        {
            FrameRemovedFromPage = (EventHandler<FrameEventArgs>)d;
        }

        #endregion

        #region Comparison methods

        public int Compare(Color x, Color y)
        {
            if (x.IsEmpty && !y.IsEmpty)
                return 1;
            else if (!x.IsEmpty && y.IsEmpty)
                return -1;

            int xx = -(x.A * 125 + x.R * 25 + x.G * 5 + x.B);
            int yy = -(y.A * 125 + y.R * 25 + y.G * 5 + y.B);

            return xx.CompareTo(yy);
        }

        #endregion

        #region Command line packaging

        public void PackPage(string name, int width, int height, string outPath, params string[] files)
        {
            Current.GetPage(0).Name = name;

            Current.ProjectData.VectorTextureScale = 0.25f;
            Current.AddImage(files, _ => { });
            foreach (string fn in Current.FrameNames)
            {
                Current.AddFrameToPage(0, fn);
            }

            PList plist = null;
            Bitmap bmp = new Bitmap(width, height);
            Util.PackResult ret = Util.Pack(0, bmp, ref plist, Util.GrabColored, Util.FillColored, Util.InformColored, null, null);
            string errMsg = "Failed to pack current page, did you add too many images or large ones?";
            if (ret == Util.PackResult.OK || ret == Util.PackResult.Missing)
            {
                if (ret == Util.PackResult.NoSpace)
                {
                    Util.Error(null, errMsg);
                }
            }
            else
            {
                Util.Error(null, errMsg);

                //return;
            }

            bmp.Save(outPath + ".png");
            plist.Save(outPath + ".plist");
        }

        #endregion

        #endregion
    }
}
