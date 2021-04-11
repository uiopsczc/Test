using System.Collections.Generic;
using UnityEngine;


namespace CsCat
{
  public partial class LogCat
  {

    private static List<string> gui_message_list = new List<string>();
    private static GUIStyle _gui_font_style;
    private static GUIStyle gui_font_style
    {
      get
      {
        if (_gui_font_style == null)
        {
          _gui_font_style = new GUIStyle();
//          _gui_font_style.normal.background = null;    //设置背景填充
//          _gui_font_style.normal.textColor = Color.red; //设置字体颜色
          _gui_font_style.fontSize = 30;
        }

        return _gui_font_style;
      }
    }
    

    public static void Flush_GUI()
    {
      //      GUILayout.Label(string.Format("fps:{0}",(int)(1/Time.unscaledDeltaTime)), gui_font_style);//FPS太快了，看不清的
      foreach (var gui_message in gui_message_list)
        GUILayout.Label(gui_message, gui_font_style);
    }

  }
}