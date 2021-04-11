using System;
using UnityEngine;

namespace CsCat
{
  public class GUIToggleTween
  {
    public bool is_opened = true;
    public float value = 1;

    [NonSerialized]
    public Rect last_rect;

    [NonSerialized]
    public float last_update_time;

    /** Update the visibility in Layout to avoid complications with different events not drawing the same thing */
    bool _is_need_to_show = true;
    public bool is_need_to_show
    {
      get
      {
        if (Event.current.type == EventType.Layout)
          _is_need_to_show = is_opened || value > 0F;
        return _is_need_to_show;
      }
    }

    public static implicit operator bool(GUIToggleTween tween)
    {
      return tween.is_need_to_show;
    }
  }
}