using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class EditorIconGUIContentEditorWindow : EditorWindow
  {
    private Vector2 scroll_position;

    void OnGUI()
    {
      using (new GUILayoutBeginScrollViewScope(ref scroll_position))
      {
        //鼠标放在按钮上的样式
        foreach (var mouseCursor in EnumUtil.GetValues<MouseCursor>())
        {
          var display_value = string.Format("{0}.{1}", typeof(MouseCursor).GetLastName(), mouseCursor.ToString());
          if (GUILayout.Button(display_value))
            this.ShowNotificationAndLog(display_value);
          EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), mouseCursor);
          GUILayout.Space(10);
        }

        //内置图标
        int column_count = 20;
        using (new GUILayoutBeginHorizontalScope())
        {
          for (int i = 0; i < EnumUtil.GetCount<EditorIconGUIContentType>(); ++i)
          {
            if (i > 0 && i % column_count == 0)
            {
              GUILayout.EndHorizontal();
              GUILayout.BeginHorizontal();
            }

            var gui_content = EditorIconGUIContent.GetSystem((EditorIconGUIContentType)i);
            if (GUILayout.Button(gui_content,
              GUILayout.Width(50), GUILayout.Height(36)))
            {
              string display_value = string.Format("EditorIconGUIContentType.{0}\n{1}",
                EnumUtil.GetName<EditorIconGUIContentType>(i),
                EditorIconGUIContentConst.Icon_GUIContent_Names[i]
                );
              this.ShowNotificationAndLog(display_value);
            }
          }
        }
      }
    }
  }
}