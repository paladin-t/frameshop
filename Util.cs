using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using Svg;
using Svg.Transforms;
using Paloma;
using Gif.Components;
using Frameshop.Data;

namespace Frameshop
{
    internal static class Util
    {
        #region DLL imports

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        #endregion

        #region Reflection methods

        public static Delegate BuildDynamicDelegate(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var paramExpressions = methodInfo.GetParameters().Select
            (
                (p, i) =>
                {
                    string name = "param" + (i + 1).ToString(CultureInfo.InvariantCulture);
                    return Expression.Parameter(p.ParameterType, name);
                }
            ).ToList();

            MethodCallExpression callExpression;
            if (methodInfo.IsStatic)
            {
                callExpression = Expression.Call(methodInfo, paramExpressions);
            }
            else
            {
                ParameterExpression instanceExpression = Expression.Parameter(methodInfo.ReflectedType, "instance");
                callExpression = Expression.Call(instanceExpression, methodInfo, paramExpressions);
                paramExpressions.Insert(0, instanceExpression);
            }
            LambdaExpression lambdaExpression = Expression.Lambda(callExpression, paramExpressions);

            return lambdaExpression.Compile();
        }

        public static Action<TInstance, TProperty> BuildSetPropertyAction<TInstance, TProperty>(PropertyInfo propertyInfo)
        {
            ParameterExpression instanceParam = Expression.Parameter(typeof(TInstance), "instance");
            ParameterExpression valueParam = Expression.Parameter(typeof(TProperty), "value");
            MemberExpression propertyProperty = Expression.Property(instanceParam, propertyInfo);
            BinaryExpression assignExpression = Expression.Assign(propertyProperty, valueParam);
            var lambdaExpression = Expression.Lambda<Action<TInstance, TProperty>>(assignExpression, instanceParam, valueParam);

            return lambdaExpression.Compile();
        }

        public static Action BuildAction(MethodInfo methodInfo) { return (Action)BuildDynamicDelegate(methodInfo); }

        public static Action<T0> BuildAction<T0>(MethodInfo methodInfo) { return (Action<T0>)BuildDynamicDelegate(methodInfo); }

        public static Action<T0, T1> BuildAction<T0, T1>(MethodInfo methodInfo) { return (Action<T0, T1>)BuildDynamicDelegate(methodInfo); }

        public static Action<T0, T1, T2> BuildAction<T0, T1, T2>(MethodInfo methodInfo) { return (Action<T0, T1, T2>)BuildDynamicDelegate(methodInfo); }

        public static Action<T0, T1, T2, T3> BuildAction<T0, T1, T2, T3>(MethodInfo methodInfo) { return (Action<T0, T1, T2, T3>)BuildDynamicDelegate(methodInfo); }

        public static Func<R> BuildFunc<R>(MethodInfo methodInfo) { return (Func<R>)BuildDynamicDelegate(methodInfo); }

        public static Func<T0, R> BuildFunc<T0, R>(MethodInfo methodInfo) { return (Func<T0, R>)BuildDynamicDelegate(methodInfo); }

        public static Func<T0, T1, R> BuildFunc<T0, T1, R>(MethodInfo methodInfo) { return (Func<T0, T1, R>)BuildDynamicDelegate(methodInfo); }

        public static Func<T0, T1, T2, R> BuildFunc<T0, T1, T2, R>(MethodInfo methodInfo) { return (Func<T0, T1, T2, R>)BuildDynamicDelegate(methodInfo); }

        public static Func<T0, T1, T2, T3, R> BuildFunc<T0, T1, T2, T3, R>(MethodInfo methodInfo) { return (Func<T0, T1, T2, T3, R>)BuildDynamicDelegate(methodInfo); }

        #endregion

        #region Calculation methods

        public static Point Lerp(Point p1, Point p2, float p)
        {
            return new Point((int)(p1.X + (p2.X - p1.X) * p), (int)(p1.Y + (p2.Y - p1.Y) * p));
        }

        #endregion

        #region Extention methods

        public static void Save(this Bitmap bmp, BinaryWriter bfw)
        {
            bfw.WriteInt(0);
            bfw.WriteShort((short)bmp.Width);
            bfw.WriteShort((short)bmp.Height);
            for (int j = 0; j < bmp.Height; j++)
            {
                for (int i = 0; i < bmp.Width; i++)
                {
                    bfw.WriteInt(bmp.GetPixel(i, j).ToArgb());
                }
            }
        }

        public static void WriteInt(this BinaryWriter binWritter, int val)
        {
            binWritter.Write(val);
        }

        public static void WriteShort(this BinaryWriter binWritter, short val)
        {
            binWritter.Write(val);
        }

        public static void WriteByte(this BinaryWriter binWritter, byte val)
        {
            binWritter.Write(val);
        }

        public static void WriteBoolean(this BinaryWriter binWritter, bool val)
        {
            binWritter.Write(val);
        }

        public static void WriteUtfString(this BinaryWriter binWritter, string val, bool len)
        {
            if (len)
                binWritter.Write(val.Length);
            binWritter.Write(val.ToCharArray());
        }

        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                return null;

            var exts = from e in extensions
                       select e.ToLower();
            var files = dir.EnumerateFiles();

            return files.Where((f) => exts.Contains(f.Extension.ToLower()));
        }

        public static IEnumerable<FileInfo> GetFilesByExtensionsRecursively(this DirectoryInfo dir, params string[] extensions)
        {
            var files = (dir.GetFilesByExtensions(extensions)).ToList();
            DirectoryInfo[] subDirs = dir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirs)
                files.AddRange(subDir.GetFilesByExtensionsRecursively(extensions));

            return files;
        }

        public static List<Color> GetAllColors(this Bitmap bitmap)
        {
            HashSet<Color> colors = new HashSet<Color>();
            for (int j = 0; j < bitmap.Height; j++)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    Color c = bitmap.GetPixel(i, j);
                    if (!colors.Contains(c))
                        colors.Add(c);
                }
            }

            return colors.ToList();
        }

        public static int HashColorList(this List<Color> colors)
        {
            int result = 0;
            foreach (Color c in colors)
                result += c.ToArgb();

            return result;
        }

        public static bool CollectionEqual(this IEnumerable<Color> left, IEnumerable<Color> right)
        {
            if (left.Count() != right.Count())
                return false;

            for (int i = 0; i < left.Count(); i++)
            {
                if (left.ElementAt(i) != right.ElementAt(i))
                    return false;
            }

            return true;
        }

        public static Rectangle Zoom(this Rectangle r, float z)
        {
            return new Rectangle
            (
                (int)(r.Left * z),
                (int)(r.Top * z),
                (int)(r.Width * z),
                (int)(r.Height * z)
            );
        }

        public static PList ToPList(this Point p)
        {
            PList result = new PList();
            result["x"] = p.X;
            result["y"] = p.Y;

            return result;
        }

        public static Point ToPoint(PList pl)
        {
            int x = pl["x"];
            int y = pl["y"];

            return new Point(x, y);
        }

        public static PList ToPList(this Rectangle r)
        {
            PList result = new PList();
            result["l"] = r.Left;
            result["t"] = r.Top;
            result["r"] = r.Right;
            result["b"] = r.Bottom;

            return result;
        }

        public static Rectangle ToRectangle(PList pl)
        {
            int l = pl["l"];
            int t = pl["t"];
            int r = pl["r"];
            int b = pl["b"];

            return new Rectangle(l, t, r - l, b - t);
        }

        public static TreeNode Retrieve(this TreeNodeCollection c, string name)
        {
            foreach (TreeNode n in c)
            {
                if (n.Text == name)
                    return n;
            }

            return null;
        }

        public static bool ContainsWideChar(this string str)
        {
            return str.Length <
                Encoding.UTF8.GetBytes(str).Length;
        }

        #endregion

        #region String methods

        public static Point ParsePoint(string str)
        {
            Point point = new Point();
            str = str.Trim('{', '}');
            string[] parts = str.Split(',');
            point.X = int.Parse(parts[0]);
            point.Y = int.Parse(parts[1]);

            return point;
        }

        public static Size ParseSize(string str)
        {
            Size size = new Size();
            str = str.Trim('{', '}');
            string[] parts = str.Split(',');
            size.Width = int.Parse(parts[0]);
            size.Height = int.Parse(parts[1]);

            return size;
        }

        public static Rectangle ParseRect(string str)
        {
            Rectangle rect;
            str = str.Trim('{', '}');
            string[] parts = str.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
            Point p = new Point();
            Size s = new Size();
            p = ParsePoint(parts[0]);
            s = ParseSize(parts[1]);
            rect = new Rectangle(p, s);

            return rect;
        }

        public static string ToString(Point p)
        {
            return "{" + p.X.ToString() + "," + p.Y.ToString() + "}";
        }

        public static string ToString(Size s)
        {
            return "{" + s.Width.ToString() + "," + s.Height.ToString() + "}";
        }

        public static string ToString(Rectangle r)
        {
            return "{" + ToString(r.Location) + "," + ToString(r.Size) + "}";
        }

        public static T ParseEnum<T>(string enumStr)
        {
            Type t = typeof(T);
            foreach (T en in Enum.GetValues(t))
            {
                MemberInfo[] ms = t.GetMember(en.ToString());
                if (ms != null && ms.Length > 0)
                {
                    object[] attribs = ms.First().GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attribs != null && attribs.Length > 0)
                    {
                        if (((DescriptionAttribute)attribs.First()).Description == enumStr)
                            return en;
                    }
                }
            }

            return default(T);
        }

        public static string ToString(Enum en)
        {
            Type t = en.GetType();
            MemberInfo[] ms = t.GetMember(en.ToString());
            if (ms != null && ms.Length > 0)
            {
                object[] attribs = ms.First().GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribs != null && attribs.Length > 0)
                    return ((DescriptionAttribute)attribs.First()).Description;
            }

            return en.ToString();
        }

        public class ApproximateStringMatchResult
        {
            public string Content { get; private set; }

            public float Score { get; private set; }

            public ApproximateStringMatchResult(string ctt, float scr)
            {
                Content = ctt;
                Score = scr;
            }

            public override string ToString()
            {
                return "{ " + Content + ", " + Score.ToString() + " }";
            }
        }

        public static float ApproximateStringMatch(string t, string p)
        {
            int m = p.Length;
            int n = t.Length;
            int k1 = -1, km;
            int i = 0, j = 0;
            float md = -1;

            while (i < n && j < m)
            {
                if (t[i] == p[j])
                {
                    if (k1 == -1)
                        k1 = i;
                    i++;
                    j++;
                }
                else
                {
                    i++;
                }
            }
            km = i;
            if (j == m)
                md = (2.0f * (float)m - ((float)km - (float)k1 - (float)m + 1.0f) / (float)n) / ((float)m + (float)n);

            return md;
        }

        public static string Count(int c, string one, string more = null)
        {
            if (more == null)
                more = one + "s";
            string s = (c == 1) ? one : more;

            return c.ToString() + " " + s;
        }

        #endregion

        #region IO methods

        public static FileStream TruncateFileStream(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
            }
            FileStream result = new FileStream(path, FileMode.Truncate, FileAccess.Write);

            return result;
        }

        public static string GetRelativePathFromWorkingDirectory(string path)
        {
            Uri file = new Uri(path);
            Uri work = new Uri(Environment.CurrentDirectory + "\\");
            Uri relative = work.MakeRelativeUri(file);
            string rel = Uri.UnescapeDataString(relative.ToString());

            return rel;
        }

        #endregion

        #region Console methods

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static void OpenConsole()
        {
            IntPtr ptr = GetForegroundWindow();
            int u;
            GetWindowThreadProcessId(ptr, out u);
            Process process = Process.GetProcessById(u);
            if (process.ProcessName == "cmd")
                AttachConsole(process.Id);
            else
                AllocConsole();
        }

        public static void CloseConsole()
        {
            FreeConsole();
        }

        #endregion

        #region System methods

        public static bool IsFileTypeRelated(string fileTypeName)
        {
            string keyName = fileTypeName;
            RegistryKey isExCommand = null;
            try
            {
                isExCommand = Registry.ClassesRoot.OpenSubKey(keyName);
                if (isExCommand == null)
                    return false;
                else
                    return isExCommand.GetValue("Create").ToString() == Application.ExecutablePath;
            }
            catch
            {
                return false;
            }
        }

        public static void UnrelateFileType(string fileTypeName)
        {
            string keyName = fileTypeName;
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(fileTypeName);
            }
            catch
            {
            }
        }

        public static void RelateFileType(string fileExt, string fileTypeName, string fileTypeDesc, string icoIndex)
        {
            string keyName = fileTypeName;
            string keyValue = fileTypeDesc;
            RegistryKey isExCommand = null;
            bool isCreateRetistry = true;
            try
            {
                isExCommand = Registry.ClassesRoot.OpenSubKey(keyName);
                if (isExCommand == null)
                {
                    isCreateRetistry = true;
                }
                else
                {
                    if (isExCommand.GetValue("Create").ToString() == Application.ExecutablePath)
                    {
                        isCreateRetistry = false;
                    }
                    else
                    {
                        Registry.ClassesRoot.DeleteSubKeyTree(keyName);
                        isCreateRetistry = true;
                    }
                }
            }
            catch
            {
                isCreateRetistry = true;
            }

            if (isCreateRetistry)
            {
                try
                {
                    RegistryKey key = Registry.ClassesRoot.CreateSubKey(keyName);
                    key.SetValue("Create", Application.ExecutablePath);
                    RegistryKey keyIcon = key.CreateSubKey("DefaultIcon");
                    keyIcon.SetValue("", Application.ExecutablePath + "," + icoIndex);
                    key.SetValue("", keyValue);
                    key = key.CreateSubKey("Shell");
                    key = key.CreateSubKey("Open");
                    key = key.CreateSubKey("Command");
                    key.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
                    keyName = fileExt;
                    keyValue = fileTypeName;
                    key = Registry.ClassesRoot.CreateSubKey(keyName);
                    key.SetValue("", keyValue);
                }
                catch
                {
                }
            }
        }

        public static void OpenWebsite(string url)
        {
            try
            {
                Process.Start(GetDefaultBrowserPath(), "\"" + url + "\"");
            }
            catch
            {
            }
        }

        private static string GetDefaultBrowserPath()
        {
            string key = @"http\shell\open\command";
            RegistryKey registryKey =
            Registry.ClassesRoot.OpenSubKey(key, false);

            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }

        #endregion

        #region Image methods

        private static Bitmap screenShot = null;

        private static long screenShotTime = 0;

        public static Size ScreenShotSize
        {
            get;
            set;
        }

        public static Bitmap ScreenShootAroundCursor()
        {
            if (screenShot == null || screenShot.Width != ScreenShotSize.Width || screenShot.Height != ScreenShotSize.Height)
            {
                screenShot = new Bitmap(ScreenShotSize.Width, ScreenShotSize.Height, PixelFormat.Format32bppArgb);
                screenShotTime = DateTime.Now.Ticks;
            }

            long now = DateTime.Now.Ticks;
            if (now - screenShotTime < 1)
                return screenShot;
            screenShotTime = now;

            Point cursor = Cursor.Position;
            using (Graphics gdest = Graphics.FromImage(screenShot))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    BitBlt
                    (
                        hDC,
                        0, 0, screenShot.Width, screenShot.Height,
                        hSrcDC,
                        cursor.X - ScreenShotSize.Width / 2, cursor.Y - ScreenShotSize.Height / 2,
                        (int)CopyPixelOperation.SourceCopy
                    );
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenShot;
        }

        public static Color GetCenterColor(Bitmap bmp)
        {
            int x = bmp.Width / 2;
            int y = bmp.Height / 2;

            return bmp.GetPixel(x, y);
        }

        public static Image LoadImageFile(string filePath)
        {
            Image result = null;

            try
            {
                switch (Path.GetExtension(filePath).ToLower())
                {
                    case ".svg":
                        {
                            SvgDocument svgDoc = SvgDocument.Open(filePath);
                            svgDoc.Transforms = new SvgTransformCollection();
                            svgDoc.Transforms.Add(new SvgScale(Package.Current.ProjectData.VectorTextureScale, Package.Current.ProjectData.VectorTextureScale));
                            svgDoc.Width = new SvgUnit(svgDoc.Width.Type, svgDoc.Width.Value * Package.Current.ProjectData.VectorTextureScale);
                            svgDoc.Height = new SvgUnit(svgDoc.Height.Type, svgDoc.Height.Value * Package.Current.ProjectData.VectorTextureScale);
                            result = svgDoc.Draw();
                        }
                        break;
                    case ".tga":
                        {
                            result = Paloma.TargaImage.LoadTargaImage(filePath);
                        }
                        break;
                    default:
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                result = Image.FromStream(fs);
                            }
                        }
                        break;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Util.Output(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Util.Output("File not found: \"" + ex.FileName + "\"");
            }

            return result;
        }

        public static void ExportGif(string outPath, int delay, params Image[] frames)
        {
            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.SetTransparent(Color.FromArgb(0, 255, 255, 255));
            e.Start(outPath);
            e.SetDelay(delay);
            e.SetRepeat(0);
            foreach (Image f in frames)
                e.AddFrame(f);
            e.Finish();
        }

        public static Image[] ImportGif(string inPath)
        {
            GifDecoder d = new GifDecoder();
            d.Read(inPath);
            List<Image> result = new List<Image>();
            for (int i = 0, count = d.GetFrameCount(); i < count; i++)
                result.Add(d.GetFrame(i));

            return result.ToArray();
        }

        #endregion

        #region Window methods

        public static void Notice(IWin32Window wnd, string text)
        {
            if (Windowed())
                MessageBox.Show(wnd, text, "Frameshop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                Console.Out.WriteLine(text);
        }

        public static void Warning(IWin32Window wnd, string text)
        {
            if (Windowed())
                MessageBox.Show(wnd, text, "Frameshop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                Console.Error.WriteLine(text);
        }

        public static void Error(IWin32Window wnd, string text)
        {
            if (Windowed())
                MessageBox.Show(wnd, text, "Frameshop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Console.Error.WriteLine(text);
        }

        public static DialogResult AskYesNo(IWin32Window wnd, string text)
        {
            return MessageBox.Show(wnd, text, "Frameshop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult AskYesNoCancel(IWin32Window wnd, string text)
        {
            return MessageBox.Show(wnd, text, "Frameshop", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static string InputBox(IWin32Window wnd, string prompt, string defaultResponse = "")
        {
            string inputStr = Interaction.InputBox(prompt, "Frameshop", defaultResponse, ((Control)wnd).Location.X, ((Control)wnd).Location.Y);
            if (inputStr != "")
                return inputStr;

            GCHandle gh = GCHandle.Alloc(inputStr, GCHandleType.Pinned);
            IntPtr strPtr = gh.AddrOfPinnedObject();
            GCHandle gh1 = GCHandle.Alloc("", GCHandleType.Pinned);
            IntPtr emptyStrPtr = gh1.AddrOfPinnedObject();
            gh.Free();
            gh1.Free();

            return (strPtr == emptyStrPtr) ? null : inputStr;
        }

        public static void Output(string txt)
        {
            if (Windowed())
                FormOutput.GetInstance().Append(txt);
            else
                Console.Out.WriteLine(txt);
        }

        public static bool Windowed()
        {
            return FormMain.GetInstance() != null;
        }

        public static Rectangle WindowToGame(Control c, Rectangle r)
        {
            int cx = c.Size.Width / 2;
            int cy = c.Size.Height / 2;

            return new Rectangle(r.X - cx, r.Y - cy, r.Width, r.Height);
        }

        public static Rectangle GameToWindow(Control c, Rectangle r)
        {
            int cx = c.Size.Width / 2;
            int cy = c.Size.Height / 2;

            return new Rectangle(r.X + cx, r.Y + cy, r.Width, r.Height);
        }

        #endregion

        #region Packing methods

        #region Data processing delegates

        internal delegate object GrabberFunc(Frame frame, out Size size);

        internal delegate void FillerAction(object tgt, int x, int y, object src, int i, int j);

        internal delegate void InformerAction(PList plist, Frame frame);

        #region Specified processors

        public static void FillColored(object tgt, int x, int y, object src, int i, int j)
        {
            Bitmap bmp = (Bitmap)tgt;
            Bitmap ori = (Bitmap)src;
            Color c = Color.FromArgb(0, Color.White);
            if (i < ori.Width && j < ori.Height)
                c = ori.GetPixel(i, j);
            bmp.SetPixel(x, y, c);
        }

        public static object GrabColored(Frame frame, out Size size)
        {
            if (frame.Rotated)
            {
                Bitmap src = new Bitmap(frame.Image);

                size = new Size(frame.Image.Height, frame.Image.Width);
                Bitmap result = new Bitmap(size.Width, size.Height);
                for (int j = 0; j < frame.Image.Height; j++)
                {
                    for (int i = 0; i < frame.Image.Width; i++)
                    {
                        Color c = src.GetPixel(i, j);
                        result.SetPixel(j, i, c);
                    }
                }

                return result;
            }
            else
            {
                Bitmap result = new Bitmap(frame.Image);
                size = result.Size;

                return result;
            }
        }

        public static void InformColored(PList plist, Frame frame)
        {
            // Do nothing
        }

        public static void InformIndexed(PList plist, Frame frame)
        {
            plist["palette"] = frame.PaletteName;
        }

        #endregion

        #endregion

        #region Target data rectangle operators
        
        private static Rectangle? FindSpace(Page page, Frame frame, Point[,] bitset)
        {
            Size s = frame.Rotated ?
                new Size(frame.Image.Size.Height, frame.Image.Size.Width) :
                frame.Image.Size;
            s.Width += page.Padding * 2;
            s.Height += page.Padding * 2;
            for (int j = 0; j <= bitset.GetLength(1) - s.Height; j++)
            {
                int ii = -1;
                for (int i = 0; i <= bitset.GetLength(0) - s.Width; )
                {
                    if (bitset[i, j].X != 0)
                    {
                        i = bitset[i, j].X;
                        continue;
                    }

                    ii = i;
                    break;
                }

                if (ii == -1)
                    continue;

                bool cont_j = false;
                for (int y = 0; y < s.Height; y++)
                {
                    for (int x = 0; x < s.Width; x++)
                    {
                        if (bitset[ii + x, j + y].X != 0)
                        {
                            cont_j = true;
                            break;
                        }
                    }
                    if (cont_j)
                        break;
                }
                if (cont_j)
                    continue;

                // Found
                return new Rectangle(ii, j, s.Width, s.Height);
            }

            return null;
        }

        private static void FillSpace(Page page, Frame frame, Rectangle space, PList plist, object tgt, Point[,] bitset,
            GrabberFunc grabber,
            FillerAction filler,
            InformerAction informer)
        {
            Size s;
            object src = grabber(frame, out s);
            Size ps = new Size(s.Width + page.Padding * 2, s.Height + page.Padding * 2);
            for (int _h = 0; _h < ps.Height; _h++)
            {
                for (int _w = 0; _w < ps.Width; _w++)
                {
                    int x = _w + space.X;
                    int y = _h + space.Y;
                    if (frame.Rotated)
                        x = ps.Width - _w + space.X;
                    bitset[x, y] = new Point(space.X + space.Width, space.Y + space.Height);
                    if (x + page.Padding < bitset.GetLength(0) && y + page.Padding < bitset.GetLength(1))
                        filler(tgt, x + page.Padding, y + page.Padding, src, _w, _h);
                }
            }

            Size srcSize = frame.Rotated ? new Size(s.Height, s.Width) : s;
            PList framesPList = plist["frames"];
            framesPList[frame.Name] = new PList();
            PList framePList = framesPList[frame.Name];
            framePList["frame"] = ToString(new Rectangle(new Point(space.Left + page.Padding, space.Top + page.Padding), s));
            framePList["offset"] = ToString(Point.Empty);
            framePList["rotated"] = frame.Rotated;
            framePList["sourceColorRect"] = ToString(new Rectangle(Point.Empty, srcSize));
            framePList["sourceSize"] = ToString(srcSize);
            informer(framePList, frame);
        }

        #endregion

        #region Packing

        public class PackingMessageFilter : IDisposable
        {
            public static int Poped = 0;

            public PackingMessageFilter()
            {
            }

            public void Dispose()
            {
                Poped--;
            }
        }

        public enum PackResult
        {
            OK,
            Missing,
            NoSpace
        }

        public static PackResult Pack
        (
            int pageIndex,
            object tgt,
            ref PList plist,
            GrabberFunc grabber,
            FillerAction filler,
            InformerAction informer,
            List<Frame> cannotHold,
            List<Frame> rotated
        )
        {
            PackResult result = PackResult.OK;
            List<Frame> missingFrames = null;

            if (plist == null)
                plist = new PList();
            else
                plist.Clear();
            var frameNames = Package.Current.GetFrameNamesInPage(pageIndex);
            if (frameNames != null && frameNames.Count() != 0)
            {
                List<Frame> frames = new List<Frame>();
                foreach (string frameName in frameNames)
                {
                    Frame frame = Package.Current[frameName];
                    if (frame == null || frame.Invalid)
                    {
                        if (missingFrames == null) missingFrames = new List<Frame>();
                        missingFrames.Add(frame);
                    }
                    else
                    {
                        frames.Add(frame);
                    }
                }
                if (missingFrames != null)
                {
                    if (PackingMessageFilter.Poped == 0)
                    {
                        FormMissingFrames fmf = new FormMissingFrames(missingFrames);
                        if (fmf.ShowDialog(FormMain.GetInstance()) == DialogResult.OK)
                        {
                            var vfns = from f in fmf.Pathes
                                       where !string.IsNullOrEmpty(f)
                                       select f;
                            FormMain.GetInstance().AddImage(vfns.ToArray());

                            for (int i = 0; i < fmf.Pathes.Count; i++)
                            {
                                string nfn = fmf.Pathes[i];
                                if (!string.IsNullOrEmpty(nfn))
                                {
                                    missingFrames[i].FilePath = nfn;
                                }
                            }
                        }
                    }

                    result = PackResult.Missing;
                }

                var orderedFrames = frames.OrderByDescending(_f => _f.OrderScore);

                plist["frames"] = new PList();
                PList metadata = new PList();
                plist["metadata"] = metadata;
                Page page = Package.Current.GetPage(pageIndex);
                metadata["textureName"] = page.Name;
                metadata["size"] = ToString(page.TextureSize);
                Size s = page.TextureSize;
                Point[,] bitset = new Point[s.Width, s.Height];
                for (int i = 0; i < s.Width; i++)
                {
                    for (int j = 0; j < s.Height; j++)
                        bitset[i, j] = Point.Empty;
                }
                foreach (Frame frame in orderedFrames)
                {
                    frame.Rotated = false;
                    Rectangle? space = FindSpace(page, frame, bitset);
                    if (space != null)
                    {
                        FillSpace(page, frame, space.Value, plist, tgt, bitset, grabber, filler, informer);
                    }
                    else
                    {
                        if (page.AllowRotation)
                        {
                            bool rot = frame.Rotated;
                            frame.Rotated = true;
                            space = FindSpace(page, frame, bitset);
                            if (space != null)
                            {
                                FillSpace(page, frame, space.Value, plist, tgt, bitset, grabber, filler, informer);

                                if (rotated != null)
                                    rotated.Add(frame);
                            }
                            else
                            {
                                frame.Rotated = rot;

                                if (cannotHold != null)
                                    cannotHold.Add(frame);
                                if (PackingMessageFilter.Poped == 0)
                                {
                                    Util.Output("No enough space in page " + pageIndex.ToString() + " for frame \"" + frame.Name + "\".");
                                }

                                result = PackResult.NoSpace;
                            }
                        }
                        else
                        {
                            if (cannotHold != null)
                                cannotHold.Add(frame);
                            if (PackingMessageFilter.Poped == 0)
                            {
                                Util.Output("No enough space in page " + pageIndex.ToString() + " for frame \"" + frame.Name + "\".");
                            }

                            result = PackResult.NoSpace;
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("Nothing to do.");
            }

            return result;
        }

        public static bool Pack
        (
            int pageIndex,
            string outputName,
            BinaryWriter bfw,
            List<string> texs,
            List<string> plists,
            List<Frame> cannotHold,
            List<Frame> rotated
        )
        {
            bool result = false;
            PList plist = null;

            Size s = Package.Current.GetPage(pageIndex).TextureSize;

            Bitmap bmp = new Bitmap(s.Width, s.Height);
            Util.PackResult ret = Pack(pageIndex, bmp, ref plist, GrabColored, FillColored, InformColored, cannotHold, rotated);
            result = ret == PackResult.OK;
            if (result)
            {
                if (bfw != null)
                {
                    bfw.WriteInt(pageIndex);
                    bfw.WriteUtfString(Path.GetFileName(outputName), true);
                    bmp.Save(bfw);
                }

                string texFile = outputName + ".png";
                string plistFile = outputName + ".plist";
                bmp.Save(texFile);
                plist.Save(plistFile);
                if (texs != null)
                    texs.Add(texFile);
                if (plists != null)
                    plists.Add(plistFile);
            }

            #region Write binary
            if (bfw != null)
            {
                bfw.WriteInt((int)plist["frames"].Count);
                foreach (dynamic f in plist["frames"])
                {
                    dynamic v = f.Value;
                    Rectangle rect = Util.ParseRect(v["frame"]);
                    Point offset = Util.ParsePoint(v["offset"]);
                    bool _rotated = v["rotated"];
                    Rectangle srcColRect = Util.ParseRect(v["sourceColorRect"]);
                    Size srcSize = Util.ParseSize(v["sourceSize"]);

                    bfw.WriteUtfString(f.Key as string, true);
                    bfw.WriteInt(rect.X);
                    bfw.WriteInt(rect.Y);
                    bfw.WriteInt(rect.Width);
                    bfw.WriteInt(rect.Height);
                    bfw.WriteInt(offset.X);
                    bfw.WriteInt(offset.Y);
                    bfw.WriteBoolean(_rotated);
                    bfw.WriteInt(srcColRect.X);
                    bfw.WriteInt(srcColRect.Y);
                    bfw.WriteInt(srcColRect.Width);
                    bfw.WriteInt(srcColRect.Height);
                    bfw.WriteInt(srcSize.Width);
                    bfw.WriteInt(srcSize.Height);
                }
            }
            #endregion

            return result;
        }

        #endregion

        #endregion

        #region RAII

        public class RAII : IDisposable
        {
            private static HashSet<string> hashSet = new HashSet<string>();

            private string _key = null;

            public static bool Has(string key)
            {
                return hashSet.Contains(key);
            }

            public RAII(string key)
            {
                hashSet.Add(key);
                _key = key;
            }

            public void Dispose()
            {
                hashSet.Remove(_key);
            }
        }

        public class Disabler : IDisposable
        {
            private Control target = null;

            public Disabler(Control tgt)
            {
                target = tgt;
                target.Enabled = false;
            }

            public void Dispose()
            {
                target.Enabled = true;
            }
        }

        public class FrameOutputLoadInformation : IDisposable
        {
            public FrameOutputLoadInformation()
            {
                Frame.OutputLoadInformation = true;
            }

            public void Dispose()
            {
                Frame.OutputLoadInformation = false;
            }
        }

        #endregion
    }
}
