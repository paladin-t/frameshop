using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Frameshop.Anim;

namespace Frameshop
{
    internal partial class FormAnimationPreview : Form
    {
        private Sequence sequence = null;

        private bool tracking = false;

        private int cursor = 0;
        private int SeqCursor
        {
            get { return cursor; }
            set
            {
                cursor = value;

                if (cursor >= sequence.Frames.Count())
                {
                    if (btnLoop.Checked)
                    {
                        cursor = 0;
                    }
                    else
                    {
                        cursor = 0;
                        timer.Enabled = false;

                        btnPlay.Enabled = true;
                        btnPause.Enabled = false;
                        btnStop.Enabled = false;
                    }
                }
                AnimFrame af = sequence[cursor];
                picView.Frame = af.FrameName;
                AnimFrame af2 = sequence[cursor == sequence.Count - 1 ? 0 : cursor + 1];
                float p = tick / af.Time;
                Point offset = Util.Lerp(af.Offset, af2.Offset, p);
                if (picView.Image != null)
                {
                    int x = (picView.Width - picView.Image.Size.Width) / 2;
                    int y = (picView.Height - picView.Image.Size.Height) / 2;
                    x += offset.X;
                    y += offset.Y;
                    picView.Offset = new Point(x, y);
                    picView.Alpha = af.Alpha;
                }

                trackBar.Value = cursor;

                picView.Invalidate(true);
            }
        }

        private long tick = 0;
        private long lastTime = 0;

        public FormAnimationPreview(Sequence seq)
        {
            InitializeComponent();

            sequence = seq;

            if (seq.Frames.Count() != 0)
                trackBar.Maximum = seq.Frames.Count() - 1;
            else
                trackBar.Maximum = 0;
        }

        private void FormAnimationPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (sequence.Frames.Count() == 0)
                return;

            long now = DateTime.Now.Ticks;
            if (now == lastTime)
                return;

            tick += now - lastTime;
            lastTime = now;

            long t = (long)(TimeSpan.TicksPerSecond * sequence[cursor].Time);
            if (tick >= t)
            {
                tick = 0;

                SeqCursor++;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
                return;

            timer.Enabled = true;

            lastTime = DateTime.Now.Ticks;

            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;

            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;

            SeqCursor = 0;

            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            btnLoop.Checked = !btnLoop.Checked;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            if (tracking)
                return;

            tracking = true;

            SeqCursor = trackBar.Value;

            tracking = false;
        }
    }
}
