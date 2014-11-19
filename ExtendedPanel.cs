using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frameshop
{

	internal partial class ExtendedPanel : Panel
	{
		//========================================================================================================================
		public ExtendedPanel()
		{

			SetStyle(ControlStyles.OptimizedDoubleBuffer |
						ControlStyles.UserPaint |
						ControlStyles.AllPaintingInWmPaint,
						true);

			SetStyle(ControlStyles.Selectable, false);

		}
		//========================================================================================================================


		#region Methods


		#region Overrides


		//========================================================================================================================
		/// <summary>
		/// Function Called when the User press a key
		/// used to know if the caracter  is valid
		/// </summary>
		/// <param name="charCode"></param>
		/// <returns></returns>
		protected override bool IsInputChar(char charCode)
		{
			// all the keyboard keys are allowed
			return true;

		}
		//========================================================================================================================



		//========================================================================================================================
		/// <summary>
		/// Function Called when the User press a key
		/// used to know if the caracter  is valid
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool IsInputKey(Keys keyData)
		{
			// all the keyboard keys are allowed
			return true;
		}
		//========================================================================================================================


		#endregion Overrides


		//========================================================================================================================
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
		}
		//========================================================================================================================


		#endregion Methods


	}

}
