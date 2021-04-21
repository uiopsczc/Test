using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{

  public class EditorIconTextureEditorWindow : EditorWindow
  {
    private Vector2 scroll_position;

    void OnGUI()
    {
      using (new GUILayoutBeginScrollViewScope(ref scroll_position))
      {
        //内置图标
        int column_count = 20;
        using (new GUILayoutBeginHorizontalScope())
        {
          for (int i = 0; i < EnumUtil.GetCount<EditorIconTextureType>(); ++i)
          {
            if (i > 0 && i % column_count == 0)
            {
              GUILayout.EndHorizontal();
              GUILayout.BeginHorizontal();
            }

            if (GUILayout.Button(EditorIconTexture.GetSystem((EditorIconTextureType)i),
              GUILayout.Width(50), GUILayout.Height(36)))
            {
              string display_value = string.Format("EditorIconTextureType.{0}\n{1}",
                EnumUtil.GetName<EditorIconTextureType>(i),
                EditorIconTextureConst.Icon_Texture_Names[i]
                );
              this.ShowNotificationAndLog(display_value);
            }
          }
        }
      }
    }
  }
}