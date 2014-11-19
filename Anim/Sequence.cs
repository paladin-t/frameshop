using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Frameshop.Data;

namespace Frameshop.Anim
{
    internal class Sequence
    {
        public event EventHandler<FrameNameChangedEventArgs> FrameNameChanged;

        public event EventHandler<FrameOffsetChangedEventArgs> FrameOffsetChanged;

        public event EventHandler<FrameAlphaChangedEventArgs> FrameAlphaChanged;

        private int nameSeed = -1;

        private List<AnimFrame> frames = null;

        public IEnumerable<AnimFrame> Frames
        {
            get
            {
                return frames;
            }
        }

        public int Count
        {
            get
            {
                return frames.Count;
            }
        }

        public AnimFrame this[int index]
        {
            get
            {
                return frames[index];
            }
        }

        public Sequence()
        {
            frames = new List<AnimFrame>();
        }

        public AnimFrame Add(string page, string frame)
        {
            AnimFrame a = null;
            if (page == null && frame == null)
            {
                a = new BlankAnimFrame(this);
            }
            else
            {
                a = new AnimFrame(this, page, frame);
                a.FrameNameChanged += a_FrameNameChanged;
                a.FrameOffsetChanged += a_FrameOffsetChanged;
                a.FrameAlphaChanged += a_FrameAlphaChanged;
            }
            string n = null;
            do
            {
                nameSeed++;
                n = nameSeed.ToString();
            } while (frames.Count((f) => { return f.Name == n; }) != 0);
            a.Name = n;
            frames.Add(a);

            return a;
        }

        public void RemoveAt(int index)
        {
            frames[index].FrameNameChanged -= a_FrameNameChanged;
            frames[index].FrameOffsetChanged -= a_FrameOffsetChanged;
            frames[index].FrameAlphaChanged -= a_FrameAlphaChanged;
            frames.RemoveAt(index);
        }

        public void Clear()
        {
            foreach (AnimFrame f in frames)
            {
                f.FrameNameChanged -= a_FrameNameChanged;
                f.FrameOffsetChanged -= a_FrameOffsetChanged;
            }
            frames.Clear();

            nameSeed = -1;
        }

        private void a_FrameNameChanged(object sender, FrameNameChangedEventArgs e)
        {
            if (FrameNameChanged != null)
                FrameNameChanged(sender, e);
        }

        private void a_FrameOffsetChanged(object sender, FrameOffsetChangedEventArgs e)
        {
            if (FrameOffsetChanged != null)
                FrameOffsetChanged(sender, e);
        }

        private void a_FrameAlphaChanged(object sender, FrameAlphaChangedEventArgs e)
        {
            if (FrameAlphaChanged != null)
                FrameAlphaChanged(sender, e);
        }

        public bool Open(string file)
        {
            using
            (
                PListKeyNotFoundExceptionEventHolder holder = new PListKeyNotFoundExceptionEventHolder
                ((_s, _e) => { Util.Output("Missing field: " + _e.Key); })
            )
            {
                List<string> missing = null;
                List<string> relocate = null;
                PList plist = new PList(file);
                int c = plist["frame_count"];
                for (int i = 0; i < c; i++)
                {
                    PList p = plist["frames"][i];
                    string framename = p["frame_name"];
                    AnimFrame af = null;
                    if (string.IsNullOrEmpty(framename))
                    {
                        af = new BlankAnimFrame(this);
                    }
                    else if (!Package.Current.FrameNames.Contains(framename))
                    {
                        if (missing == null) missing = new List<string>();

                        missing.Add(framename);

                        continue;
                    }
                    else
                    {
                        af = new AnimFrame(this);
                        af.FrameNameChanged += a_FrameNameChanged;
                        af.FrameOffsetChanged += a_FrameOffsetChanged;
                        af.FrameAlphaChanged += a_FrameAlphaChanged;
                    }

                    frames.Add(af);

                    string pageName = p["page"];
                    if (string.IsNullOrEmpty(pageName))
                    {
                        // Do nothing
                    }
                    else
                    {
                        int pgi = Package.Current.GetPage(pageName);
                        if (pgi == -1 || !Package.Current.GetFrameNamesInPage(pgi).Contains(framename))
                        {
                            if (relocate == null) relocate = new List<string>();

                            relocate.Add("\"" + framename + "\"" + " in page " + "\"" + p["page"] + "\"");
                        }
                    }

                    af.PageName = p["page"];
                    af.Name = p["name"];
                    af.FrameName = framename;
                    af.Time = p["time"];
                    af.Interpolation = p.ContainsKey("interp") ? AnimFrame.ParseInterpolation(p["interp"]) : AnimFrame.InterpolateAlgorithm.Linear;
                    af.Alpha = p.ContainsKey("alpha") ? (byte)p["alpha"] : byte.MaxValue;
                    af.Offset = Util.ToPoint((PList)p["offset"]);
                    af.Rect0 = p.ContainsKey("rect0") ? Util.ToRectangle((PList)p["rect0"]) : new Rectangle(0, 0, 0, 0);
                    af.Rect1 = p.ContainsKey("rect1") ? Util.ToRectangle((PList)p["rect1"]) : new Rectangle(0, 0, 0, 0);
                    af.Rect2 = p.ContainsKey("rect2") ? Util.ToRectangle((PList)p["rect2"]) : new Rectangle(0, 0, 0, 0);
                    af.Rect3 = p.ContainsKey("rect3") ? Util.ToRectangle((PList)p["rect3"]) : new Rectangle(0, 0, 0, 0);
                    af.Rect4 = p.ContainsKey("rect4") ? Util.ToRectangle((PList)p["rect4"]) : new Rectangle(0, 0, 0, 0);
                    af.Tag = p.ContainsKey("tag") ? p["tag"].ToString() : string.Empty;
                }

                if (relocate != null)
                {
                    StringBuilder sb = new StringBuilder();
                    if (relocate.Count == 1)
                        sb.Append("A frame is changed or missing, skipped page information:" + Environment.NewLine);
                    else
                        sb.Append("Some frames are changed or missing, skipped page information:" + Environment.NewLine);
                    foreach (string r in relocate)
                        sb.Append("  " + r + Environment.NewLine);
                    Util.Output(sb.ToString());
                }
                if (missing != null)
                {
                    StringBuilder sb = new StringBuilder();
                    if (missing.Count == 1)
                        sb.Append("A frame is missing:" + Environment.NewLine);
                    else
                        sb.Append("Some frames are missing:" + Environment.NewLine);
                    foreach (string m in missing)
                        sb.Append("  " + m + Environment.NewLine);
                    Util.Warning(null, sb.ToString());
                }

                return true;
            }
        }

        public bool Save(string file)
        {
            PList plist = new PList();
            plist["frame_count"] = Frames.Count();
            plist["frames"] = new List<dynamic>();
            foreach (AnimFrame af in Frames)
            {
                PList p = new PList();
                p["page"] = af.PageName;
                p["name"] = af.Name;
                p["frame_name"] = af.FrameName;
                p["time"] = af.Time;
                p["interp"] = AnimFrame.ToString(af.Interpolation);
                p["alpha"] = (int)af.Alpha;
                p["offset"] = af.Offset.ToPList();
                p["rect0"] = af.Rect0.ToPList();
                p["rect1"] = af.Rect1.ToPList();
                p["rect2"] = af.Rect2.ToPList();
                p["rect3"] = af.Rect3.ToPList();
                p["rect4"] = af.Rect4.ToPList();
                p["tag"] = af.Tag;
                if (af.Interpolation == AnimFrame.InterpolateAlgorithm.Linear) p.Remove("interp");
                if (af.Rect0.IsEmpty) p.Remove("rect0");
                if (af.Rect1.IsEmpty) p.Remove("rect1");
                if (af.Rect2.IsEmpty) p.Remove("rect2");
                if (af.Rect3.IsEmpty) p.Remove("rect3");
                if (af.Rect4.IsEmpty) p.Remove("rect4");
                if (string.IsNullOrEmpty(af.Tag)) p.Remove("tag");
                plist["frames"].Add(p);
            }
            plist.Save(file);

            return true;
        }
    }
}
