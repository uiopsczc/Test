using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    private bool is_playing;
    private float pre_realtimeSinceStartup_of_play_time;
    private float play_speed=1f;

    public void DrawLeftTop()
    {
      using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
      {
        using (var check1 = new EditorGUIBeginChangeCheckScope())
        {
          GUILayout.Toggle(is_playing, EditorIconGUIContent.GetSystem(EditorIconGUIContentType.PlayButton),
            EditorStyles.toolbarButton); //Play
          if (check1.IsChanged)
            SwitchPlay();
        }


        using (var check3 = new EditorGUIBeginChangeCheckScope())
        {
          GUILayout.Toggle(AnimationMode.InAnimationMode(),
            EditorIconGUIContent.GetSystem(EditorIconGUIContentType.Animation_Record),
            EditorStyles.toolbarButton); //Animate
          if (check3.IsChanged)
          {
            if (AnimationMode.InAnimationMode())
            {
              AnimationMode.StopAnimationMode();
            }
            else
              AnimationMode.StartAnimationMode();
          }
        }

        using (new EditorGUIUtilityLabelWidthScope(30))
        {
          frame_count_per_second = EditorGUILayout.IntField("FPS", frame_count_per_second, GUILayout.Width(30+25));
          if (frame_count_per_second <= 1)
            frame_count_per_second = 1;
        }

        using (new EditorGUIUtilityLabelWidthScope(70))
        {
          play_speed = EditorGUILayout.FloatField("play_speed", play_speed, GUILayout.Width(70+25));
          if (play_speed <= 0)
            play_speed = 1;
        }

        GUILayout.FlexibleSpace();
      }
    }


    void SwitchPlay()
    {
      if (!is_playing)
        Play();
      else
        Pause();
    }
    

    void Play()
    {
      is_playing = true;
      pre_realtimeSinceStartup_of_play_time = Time.realtimeSinceStartup;
      on_play_callback?.Invoke();
    }

    void Pause()
    {
      is_playing = false;
      on_pause_callback?.Invoke();
    }

  }
}