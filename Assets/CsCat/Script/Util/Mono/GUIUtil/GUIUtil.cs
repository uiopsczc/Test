using System;
using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		/// <summary>
		///   设置action中的GUI的controlName
		///   可以通过 GUI.FocusControl(controlName);  来重新获取焦点
		/// </summary>
		/// <param name="controlName"></param>
		/// <param name="action"></param>
		public static void SetControlName(string controlName, Action action)
		{
			GUI.SetNextControlName(controlName);
			action();
		}
	}
}