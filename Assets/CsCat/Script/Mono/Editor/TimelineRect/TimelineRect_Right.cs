using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    public void DrawRight()
    {
      SetDuration(this.duration);
      scroll_rect = this.resizableRects.padding_rects[1];
      view_rect.Set(0, 0, duration * width_per_second, scroll_rect.height - 16);
      using (var s = new GUI.ScrollViewScope(scroll_rect, scroll_position, view_rect, true, false))
      {
        scroll_position = s.scrollPosition;
        DrawTimelineTrack();
        on_draw_tracks_right_side_callback?.Invoke();
        DrawPlayTimeLine();
        Handle_Right_MouseInput();
        EditorMouseInput.DrawSelectBox();
      }
      HandleScrollWheelEvent();
    }

    void HandleScrollWheelEvent()
    {
      if (Event.current.type == EventType.ScrollWheel)
      {
        if (total_rect.Contains(Event.current.mousePosition))
          width_size_per_second -= Event.current.delta.y / 100;
      }
    }
  }
}