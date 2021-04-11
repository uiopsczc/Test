#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class GUILayoutToggleAreaScope : IDisposable
  {
    public GUILayoutToggleAreaScope(GUIToggleTween tween, string text, Action action = null) : this(tween, text, action,
      EditorStyles.helpBox)
    {
    }

    public GUILayoutToggleAreaScope(GUIToggleTween tween, string text, Action action, GUIStyle style)
    {
      //计算要显示的大小
      Rect last_rect = tween.last_rect;
      last_rect.height = last_rect.height < 29 ? 29 : last_rect.height;
      last_rect.height -= 29;
      // t 时间
      // b 开始值
      // c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
      // d 总时长
      float pct = EaseCat.Quad.EaseOut2(0, 1, tween.value);
      last_rect.height *= pct;
      last_rect.height += 29;
      last_rect.height = Mathf.Round(last_rect.height);

      //显示区域布局、开始Area、BeginVertical计算真实大小
      Rect got_last_rect = GUILayoutUtility.GetRect(new GUIContent(), style, GUILayout.Height(last_rect.height));
      GUILayout.BeginArea(last_rect, style);
      Rect new_rect = EditorGUILayout.BeginVertical();

      if (Event.current.type == EventType.Repaint || Event.current.type == EventType.ScrollWheel)
      {
        //计算真实大小
        new_rect.x = got_last_rect.x;
        new_rect.y = got_last_rect.y;
        new_rect.width = got_last_rect.width;
        new_rect.height += style.padding.top + style.padding.bottom;
        tween.last_rect = new_rect;

        //计算插值
        if (Event.current.type == EventType.Repaint)
        {
          float deltaTime = Time.realtimeSinceStartup - tween.last_update_time;
          tween.last_update_time = Time.realtimeSinceStartup;
          tween.value = Mathf.Clamp01(tween.is_opened
            ? tween.value + deltaTime / GUILayoutToggleAreaScopeConst.Tween_Duration
            : tween.value - deltaTime / GUILayoutToggleAreaScopeConst.Tween_Duration);
        }
      }

      using (new GUILayoutBeginHorizontalScope())
      {
        using (new GUIBackgroundColorScope(Color.gray.SetA(0.5f)))
        {
          if (GUILayout.Button(
            string.Format("{0}{1}", tween.is_opened ? "▼" : "►", text),
            GUIStyleConst.Label_Bold_MiddleLeft_Style))
            tween.is_opened = !tween.is_opened;
        }

        action?.Invoke();
      }

      GUILayout.Space(11);
    }

    public void Dispose()
    {
      EditorGUILayout.EndVertical();
      GUILayout.EndArea();
    }
  }
}
#endif