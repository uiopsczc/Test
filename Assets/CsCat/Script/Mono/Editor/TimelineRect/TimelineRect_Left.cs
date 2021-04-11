using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    public void DrawLeft()
    {
      using (new GUILayout.AreaScope(this.resizableRects.rects[0]))
      {
        DrawLeftTop();
        DrawLeftBottom();
        on_draw_tracks_left_side_callback?.Invoke();
      }
    }
  }
}