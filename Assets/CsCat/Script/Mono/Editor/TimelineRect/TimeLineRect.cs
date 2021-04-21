using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    public Action on_draw_tracks_left_side_callback; //画Tracks的左边,需要外部添加具体实现
    public Action on_draw_tracks_right_side_callback; //画Tracks的右边,需要外部添加具体实现

    public Func<Vector2, bool> is_mouse_down_of_selected_item; //当前鼠标是否是在selected_item中点下,需要外部添加具体实现
    public Func<Vector2, bool> try_to_select_unselected_item_callback; //尝试选中未选中的item, 需要外部添加具体实现
    public Action<Rect> update_selected_items_callback; //更新选中的item,需要外部需要添加具体实现
    public Action<float> on_dragging_selected_callback; //拖动选中的items触发的callback,需要外部需要添加具体实现
    public Func<bool> is_has_selected_item; //是否有的item,需要外部添加具体实现

    public Action<Rect> on_mouse_right_button_click_callback; //右键点击的时候触发的callback,需要添加具体实现

    public Action<EditorMouseInputStatus> on_editorMouseInputStatus_change_callback; //需要添加具体实现
    public Action<EditorCommand> on_do_editorCommand_callback; //触发EditorCommand时触发的callback,需要外部添加具体实现

    public Action on_play_callback; //播放触发的callback,需要外部添加具体实现
    public Action on_pause_callback; //暂停播放触发的callback,需要添加具体实现

    public Action<float> on_play_time_change_callback; //当playtime改变时候触发的action,需要添加具体实现
    public Action<float> on_animating_callback; //当animating时触发的callback,需要外部添加具体实现

    private Func<Rect> total_rect_func;
    private Vector2 scroll_position;
    public Rect scroll_rect;
    private Rect view_rect;
    public float duration;
    public HorizontalResizableRects resizableRects;

    private float track_offset_y = 2 * TimelineRectConst.Timeline_Track_Height;

    public Rect total_rect
    {
      get { return total_rect_func(); }
    }

    public Rect left_rect
    {
      get { return this.resizableRects.rects[0]; }
    }

    public Rect left_padding_rect
    {
      get { return this.resizableRects.padding_rects[0]; }
    }

    public Rect right_rect
    {
      get { return this.resizableRects.rects[1]; }
    }

    public Rect right_padding_rect
    {
      get { return this.resizableRects.padding_rects[1]; }
    }


    public TimelineRect(Func<Rect> total_rect_func, float duration = 0)
    {
      this.total_rect_func = total_rect_func;
      this.resizableRects = new HorizontalResizableRects(total_rect_func, new[] { 140f });
      //      this.resizableRects.SetCanResizable(false);
      SetDuration(duration);
    }

    public void OnEnable()
    {
      EditorMouseInput.on_status_changed += OnEditorMouseInputStatusChanged;
      EditorMouseInput.status = EditorMouseInputStatus.Normal;
    }

    public void OnDisable()
    {
      if (AnimationMode.InAnimationMode())
        AnimationMode.StopAnimationMode();
      EditorMouseInput.on_status_changed -= OnEditorMouseInputStatusChanged;
    }

    public void SetDuration(float duration)
    {
      if (duration >= this.duration)
      {
        if (duration < scroll_rect.width / width_per_second)
          this.duration = scroll_rect.width / width_per_second;
        else
          this.duration = duration;
      }
    }

    public void OnGUI()
    {
      this.resizableRects.OnGUI();
      DrawLeft();
      DrawRight();
      OnUpdatePlaying();
      HandleInput();
    }

    void OnUpdatePlaying()
    {
      if (is_playing)
      {
        float deta_time = Time.realtimeSinceStartup - this.pre_realtimeSinceStartup_of_play_time;
        this.pre_realtimeSinceStartup_of_play_time = Time.realtimeSinceStartup;
        this.play_time += deta_time * play_speed;

        //根据play_time自动设置scroll_postion
        if (play_time > duration * 0.75f)
          SetDuration(play_time / 0.75f);//duration 每次增长为原来的(1/0.75f)陪
        scroll_position.x = Mathf.Max(scroll_position.x, play_time * width_per_second - scroll_rect.width * 0.5f);
      }
    }


    void HandleInput()
    {
      if (this.total_rect.Contains(Event.current.mousePosition))
      {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
          switch (Event.current.keyCode)
          {
            case KeyCode.P:
              SwitchPlay();
              Event.current.Use();
              break;
            case KeyCode.LeftArrow:
              play_time -= frame_duration;
              break;
            case KeyCode.RightArrow:
              play_time += frame_duration;
              break;
          }
        }
      }
    }


    public Rect GetLeftTrackRect(int track_index)
    {
      Rect rect = new Rect();
      rect.x = 0;
      rect.y = track_offset_y + TimelineRectConst.Track_Height * track_index;
      rect.height = TimelineRectConst.Track_Height;
      rect.width = this.resizableRects.rects[0].width;
      return rect;
    }


    public Rect GetRightTrackRect(float time, float duration, int track_index)
    {
      Rect rect = new Rect();
      rect.x = time * width_per_second;
      rect.y = track_offset_y + TimelineRectConst.Track_Height * track_index;
      rect.height = TimelineRectConst.Track_Height;
      rect.width = duration * width_per_second;
      return rect;
    }

    public Rect GetRightTrackRect(int track_index)
    {
      Rect rect = new Rect();
      rect.x = 0;
      rect.y = track_offset_y + TimelineRectConst.Track_Height * track_index;
      rect.height = TimelineRectConst.Track_Height;
      rect.width = view_rect.width;
      return rect;
    }
  }
}