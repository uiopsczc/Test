using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    private Rect play_time_line_rect;
    
    private float _play_time;

    public float play_time
    {
      get { return _play_time; }
      set
      {
        float pre_play_time = _play_time;
        _play_time = value < 0 ? 0 : value;
        if (pre_play_time != _play_time)
          OnPlayTimeChange();
      }
    }


    void DrawPlayTimeLine()
    {
      play_time_line_rect.x = play_time * width_per_second;
      play_time_line_rect.y = 0;
      play_time_line_rect.width = 1;
      play_time_line_rect.height = total_rect.height;
      EditorGUI.DrawRect(play_time_line_rect, Color.red);
    }

    void OnPlayTimeChange()
    {
      on_play_time_change_callback?.Invoke(play_time);
      if (!EditorApplication.isPlaying && AnimationMode.InAnimationMode())
      {
        AnimationMode.BeginSampling();
        on_animating_callback?.Invoke(play_time);
        AnimationMode.EndSampling();
        SceneView.RepaintAll();
      }
    }
  }
}