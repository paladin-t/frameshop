using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BalloonCS
{
	/// <summary>
	/// Summary description for FMSInPlaceTip.
	/// </summary>
	public class InPlaceBalloon : IDisposable
	{
		private Control m_parent;

		private string m_text = "FMS Inplace Tooltip Control Display Message";
		private string m_title = "FMS Inplace Tooltip Message";
		private TooltipIcon m_titleIcon = TooltipIcon.None;
		private int m_maxWidth = 250;

		[StructLayout(LayoutKind.Sequential)]
		private struct TOOLINFO
		{
			public int cbSize;
			public int uFlags;
			public IntPtr hwnd;
			public IntPtr uId;
			public Rectangle rect;
			public IntPtr hinst;
			[MarshalAs(UnmanagedType.LPTStr)] 
			public string lpszText;
			public uint lParam;
		}

		private const string TOOLTIPS_CLASS = "tooltips_class32";
		private const int WS_POPUP = unchecked((int)0x80000000);
		private const int WM_USER = 0x0400;
		private const int SWP_NOSIZE = 0x0001;
		private const int SWP_NOMOVE = 0x0002;
		private const int SWP_NOACTIVATE = 0x0010;

		private const int TTS_ALWAYSTIP = 0x01;
		private const int TTS_NOPREFIX = 0x02;
		private const int TTF_TRANSPARENT = 0x0100;
		private const int TTF_SUBCLASS = 0x0010;

		private const int TTM_SETMAXTIPWIDTH = WM_USER + 24;
		private const int TTM_ADJUSTRECT = WM_USER + 31;
		private const int TTM_ADDTOOL = WM_USER + 50;

		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		[DllImport("User32", SetLastError=true)]
		private static extern bool SetWindowPos(
			IntPtr hWnd,
			IntPtr hWndInsertAfter,
			int X,
			int Y,
			int cx,
			int cy,
			int uFlags);

		[DllImport("User32", SetLastError=true)]
		private static extern int SendMessage(
			IntPtr hWnd,
			int Msg,
			int wParam,
			IntPtr lParam);

		private InPlaceTool m_tool = null;

		public InPlaceBalloon()
		{
			m_tool = new InPlaceTool();
		}

		protected virtual void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					// release managed resources if any
				}
				
				// release unmanaged resource
				m_tool.DestroyHandle();

				// Note that this is not thread safe.
				// Another thread could start disposing the object
				// after the managed resources are disposed,
				// but before the disposed flag is set to true.
				// If thread safety is necessary, it must be
				// implemented by the client.
			}
			disposed = true;
		}
		
		// Finalizer 
		~InPlaceBalloon()      
		{
			Dispose(false);
		}

		public void SetToolTip(Control parent, Rectangle rect, string tipText)
		{
			System.Diagnostics.Debug.Assert(parent!=null, "parent is null", "parent cant be null");
			m_text = tipText;
			
			CreateParams cp = new CreateParams();
			cp.ClassName = TOOLTIPS_CLASS;
			cp.Style = WS_POPUP | TTS_NOPREFIX | TTS_ALWAYSTIP;

			cp.Parent = parent.Handle;

			// create the tool
			m_tool.CreateHandle(cp);

			// adjust the rectangle
			Rectangle r = rect;
			IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(r));
			Marshal.StructureToPtr(r, p, true);
			SendMessage(m_tool.Handle, TTM_ADJUSTRECT, -1, p);
			r = (Rectangle)Marshal.PtrToStructure(p, typeof(Rectangle));
			Marshal.FreeHGlobal(p);

			// make sure we make it the top level window
			SetWindowPos(
				m_tool.Handle, HWND_TOPMOST, r.Left, r.Top, 0, 0,
				SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);

			// create and fill in the tool tip info
			TOOLINFO ti = new TOOLINFO();
			ti.cbSize = Marshal.SizeOf(ti);
			ti.uFlags = TTF_TRANSPARENT | TTF_SUBCLASS;
				
			ti.hwnd = parent.Handle;
			ti.lpszText = m_text;
			ti.rect = parent.ClientRectangle;

			// add the tool tip
			IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
			Marshal.StructureToPtr(ti, ptrStruct, true);

			SendMessage(m_tool.Handle, TTM_ADDTOOL, 0, ptrStruct);

			Marshal.FreeHGlobal(ptrStruct);

			SendMessage(m_tool.Handle, TTM_SETMAXTIPWIDTH, 
				0, new IntPtr(m_maxWidth));

			//			IntPtr ptrTitle = Marshal.StringToHGlobalAuto(m_title);
			//
			//			WinDeclns.SendMessage(
			//				m_tool.Handle, TTDeclns.TTM_SETTITLE, 
			//				(int)m_titleIcon, ptrTitle);
			//
			//			Marshal.FreeHGlobal(ptrTitle);

		}


		/// <summary>
		/// Sets or gets the Title.
		/// </summary>
		public string Title
		{
			get
			{
				return m_title;
			}
			set
			{
				m_title = value;
			}
		}

		/// <summary>
		/// Sets or gets the display icon.
		/// </summary>
		public TooltipIcon TitleIcon
		{
			get
			{
				return m_titleIcon;
			}
			set
			{
				m_titleIcon = value;
			}
		}

		/// <summary>
		/// Sets or gets the display text.
		/// </summary>
		public string Text
		{
			get
			{
				return m_text;
			}
			set
			{
				m_text = value;
			}
		}

		/// <summary>
		/// Sets or gets the parent.
		/// </summary>
		public Control Parent
		{
			get
			{
				return m_parent;
			}
			set
			{
				m_parent = value;
			}
		}

		private bool disposed = false;
		public void Dispose()
		{
			Dispose(true);
			// Take yourself off the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}


	}

	internal class InPlaceTool : NativeWindow
	{
		protected override void WndProc(ref Message m)
		{
			base.WndProc (ref m);

//			if(m.Msg==5)//SW_SHOW
//			{
//				System.Diagnostics.Debug.WriteLine(m.Msg);
//			}
//			if(m.Msg==0)//SW_HIDE
//			{
//				System.Diagnostics.Debug.WriteLine(m.Msg);
//			}

		}

	}

}
