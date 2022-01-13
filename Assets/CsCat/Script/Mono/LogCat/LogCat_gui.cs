using System.Collections.Generic;
using UnityEngine;


namespace CsCat
{
	public partial class LogCat
	{

		private static List<string> guiMessageList = new List<string>();
		private static GUIStyle _guiFontStyle;
		private static GUIStyle guiFontStyle
		{
			get
			{
				if (_guiFontStyle == null)
				{
					_guiFontStyle = new GUIStyle();
					//          _gui_font_style.normal.background = null;    //设置背景填充
					//          _gui_font_style.normal.textColor = Color.red; //设置字体颜色
					_guiFontStyle.fontSize = 30;
				}

				return _guiFontStyle;
			}
		}


		public static void Flush_GUI()
		{
			//      GUILayout.Label(string.Format("fps:{0}",(int)(1/Time.unscaledDeltaTime)), gui_font_style);//FPS太快了，看不清的
			for (var i = 0; i < guiMessageList.Count; i++)
			{
				var guiMessage = guiMessageList[i];
				GUILayout.Label(guiMessage, guiFontStyle);
			}
		}

	}
}