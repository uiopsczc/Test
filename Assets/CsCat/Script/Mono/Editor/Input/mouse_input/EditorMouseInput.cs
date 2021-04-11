using System;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
  public static class EditorMouseInput
  {
    public static Action<EditorMouseInputStatus> on_status_changed;
    static EditorMouseInputStatus _status = EditorMouseInputStatus.Normal;

    public static EditorMouseInputStatus status
    {
      get { return _status; }
      set
      {
        _status = value;
        on_status_changed?.Invoke(_status);
      }
    }

    public static Vector2 touch_point;
    public static Vector2 last_touch_point;

    public static Rect selected_rect
    {
      get
      {
        Vector2 min = Vector2.Min(touch_point, last_touch_point);
        Vector2 max = Vector2.Max(touch_point, last_touch_point);
        Rect selected_rect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        return selected_rect;
      }
    }

    public static bool IsSelectedOf(Rect rect)
    {
      return selected_rect.Overlaps(rect);
    }

    public static void DrawSelectBox()
    {
      if (_status == EditorMouseInputStatus.Selecting)
      {
        Vector2 min = Vector2.Min(last_touch_point, touch_point);
        Vector2 max = Vector2.Max(last_touch_point, touch_point);
        if (!min.Equals(max))
        {
          float w = max.x - min.x;
          float h = max.y - min.y;
          Rect[] selectBoxSides = new Rect[4];
          selectBoxSides[0].Set(min.x, min.y, w, 1); // top
          selectBoxSides[1].Set(min.x, max.y, w, 1); // bottom
          selectBoxSides[2].Set(min.x, min.y, 1, h); // left
          selectBoxSides[3].Set(max.x, min.y, 1, h);
          Rect background = new Rect(min, new Vector2(w, h));
          Color color = Color.gray;
          color.a = 0.5f;
          EditorGUI.DrawRect(background, color);
          for (int i = 0; i < selectBoxSides.Length; i++)
          {
            EditorGUI.DrawRect(selectBoxSides[i], Color.white);
          }
        }
      }
    }
  }
}


