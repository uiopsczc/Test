using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    private float width_size_per_second = 1f;
    private int frame_count_per_second = 10;

    private float frame_duration
    {
      get { return 1f / frame_count_per_second; }
    }

    public float width_per_second
    {
      get { return TimelineRectConst.Width_Per_Second * width_size_per_second; }
    }


    void DrawTimelineTrack()
    {
      Color track_background_color = AnimationMode.InAnimationMode()
        ? new Color(211 / 255f, 127 / 255f, 127 / 255f, 0.5f)
        : new Color(173 / 255f, 205 / 255f, 211 / 255f, 0.5f);
      EditorGUI.DrawRect(new Rect(0, 0, this.duration * width_per_second, TimelineRectConst.Timeline_Track_Height), track_background_color);

      EditorGUI.DrawRect(new Rect(0, TimelineRectConst.Timeline_Track_Height, this.duration * width_per_second, 1), new Color(153 / 255f, 153 / 255f, 153 / 255f, 1));
      float min_time = scroll_position.x / width_per_second; //当前时间条的最小时间
      float max_time = scroll_rect.width / width_per_second + min_time; //当前时间条的最大时间
      GUIStyle label_style = new GUIStyle(GUI.skin.label);
      label_style.normal.textColor = Color.black;
      Color color = Color.gray;

      for (int i = (int) min_time; i <= max_time; i++)
      {
        //画秒的字符串
        string current_second = string.Format("{0}s", i);
        GUIContent second_content = current_second.ToGUIContent();
        Vector2 second_content_cellSize = GUI.skin.label.CalcSize(second_content);
        Rect second_content_rect = new Rect();
        second_content_rect.x = i * width_per_second;
        second_content_rect.y = TimelineRectConst.Timeline_Track_Height - second_content_cellSize.y - 3;
        second_content_rect.size = second_content_cellSize;
        GUI.Label(second_content_rect, second_content, label_style);
        //画秒的刻度
        Rect second_splie_line_rect = new Rect();
        second_splie_line_rect.x = i * width_per_second;
        second_splie_line_rect.height = TimelineRectConst.Second_Split_Line_Height;
        second_splie_line_rect.y = TimelineRectConst.Timeline_Track_Height - second_splie_line_rect.height;
        second_splie_line_rect.width = 1;
        //画秒的子刻度
        for (int j = 0; j < frame_count_per_second; j++)
        {
          Rect second_sub_split_line_rect = new Rect();
          second_sub_split_line_rect.x = second_splie_line_rect.x + j * width_per_second / frame_count_per_second;
          second_sub_split_line_rect.width = 1;
          second_sub_split_line_rect.height = TimelineRectConst.Second_Sub_Split_Line_Height;
          second_sub_split_line_rect.y = TimelineRectConst.Timeline_Track_Height - second_sub_split_line_rect.height;
          Color second_sub_slipt_line_color = color;
          second_sub_slipt_line_color.a = 0.8f;
          EditorGUI.DrawRect(second_sub_split_line_rect, second_sub_slipt_line_color);
        }

        EditorGUI.DrawRect(second_splie_line_rect, color);
      }
    }
  }
}