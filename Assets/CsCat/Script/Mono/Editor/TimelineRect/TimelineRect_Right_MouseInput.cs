using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class TimelineRect
  {
    bool is_mouse_left_button_pressed = false;

    private bool sb = false;
    void Handle_Right_MouseInput()
    {
      if (Event.current.isMouse)
      {
        if (this.resizableRects.is_resizing)
          return;
        switch (Event.current.type)
        {
          case EventType.MouseDown:
            if (Event.current.button == 0)
            {
              is_mouse_left_button_pressed = true;
              EditorMouseInput.touch_point = Event.current.mousePosition;
              EditorMouseInput.last_touch_point = EditorMouseInput.touch_point;
              Rect timeline_rect = new Rect(view_rect);
              timeline_rect.x = 0;
              timeline_rect.height = TimelineRectConst.Timeline_Track_Height;
              if (timeline_rect.Contains(EditorMouseInput.touch_point))
                EditorMouseInput.status = EditorMouseInputStatus.Retime;
              else if (IsMouseDownOfSelectedItem())
                ;
              else if (TryToSelectUnselectedItem())
                EditorMouseInput.status = EditorMouseInputStatus.Selected;
              else if (Event.current.control)
                ;
              else
                EditorMouseInput.status = EditorMouseInputStatus.Normal;
            }else if (Event.current.button == 1)
            {
              Rect position_rect = new Rect();
              position_rect.position = Event.current.mousePosition;
              OnMouseRightButtonClick(position_rect);
            }
            break;
          case EventType.MouseDrag:
            if (is_mouse_left_button_pressed)
            {
              EditorMouseInput.touch_point = Event.current.mousePosition;
              switch (EditorMouseInput.status)
              {
                case EditorMouseInputStatus.Normal:
                  EditorMouseInput.status = EditorMouseInputStatus.Selecting;
                  UpdateSelectedItems();
                  break;
                case EditorMouseInputStatus.Retime:
                  EditorMouseInput.status = EditorMouseInputStatus.Retime;
                  break;
                case EditorMouseInputStatus.Selected:
                  if (!Event.current.control)
                  {
                    EditorMouseInput.status = EditorMouseInputStatus.DraggingSelected;
                    OnDraggingSelected();
                  }
                  else
                  {
                    EditorMouseInput.status = EditorMouseInputStatus.Selecting;
                    UpdateSelectedItems();
                  }

                  break;
                case EditorMouseInputStatus.Selecting:
                  EditorMouseInput.status = EditorMouseInputStatus.Selecting;
                  UpdateSelectedItems();
                  break;
                case EditorMouseInputStatus.DraggingSelected:
                  EditorMouseInput.status = EditorMouseInputStatus.DraggingSelected;
                  OnDraggingSelected();
                  break;
              }
            }
            break;
        }
      }
      else if (Event.current.isKey)
      {
        if (Event.current.type == EventType.KeyDown)
        {
          switch (Event.current.keyCode)
          {
            case KeyCode.V:
              if (Event.current.alt)
                DoEditorCommand(EditorCommand.Paste);
              break;
            case KeyCode.D:
              if (Event.current.alt)
                DoEditorCommand(EditorCommand.Delete);
              break;

            default:
              break;
          }
        }
      }


      if (Event.current.rawType == EventType.MouseUp)
      {
        if (is_mouse_left_button_pressed)
        {
          EditorMouseInput.touch_point = Event.current.mousePosition;
          is_mouse_left_button_pressed = false;
          switch (EditorMouseInput.status)
          {
            case EditorMouseInputStatus.Retime:
            case EditorMouseInputStatus.Selecting:
              EditorMouseInput.status =
                IsHasSelectedItem() ? EditorMouseInputStatus.Selected : EditorMouseInputStatus.Normal;
              break;
            case EditorMouseInputStatus.DraggingSelected:
              EditorMouseInput.status = EditorMouseInputStatus.Selected;
              break;
          }
        }
      }
    }

    void OnEditorMouseInputStatusChanged(EditorMouseInputStatus editorMouseInputStatus)
    {
      switch (editorMouseInputStatus)
      {
        case EditorMouseInputStatus.Retime:
          var time = EditorMouseInput.touch_point.x / width_per_second;
          this.play_time = time < 0 ? 0 : time;
          AutoScrollPosition();
          break;
        case EditorMouseInputStatus.Selecting:
        case EditorMouseInputStatus.DraggingSelected:
          AutoScrollPosition();
          break;
      }

      on_editorMouseInputStatus_change_callback?.Invoke(editorMouseInputStatus);
    }

    void AutoScrollPosition()
    {
      const float sample_ratio = 1 / 30f;
      if (EditorMouseInput.touch_point.x - scroll_position.x > scroll_rect.width * 0.85f)
        scroll_position.x += width_per_second * sample_ratio;
      else if (EditorMouseInput.touch_point.x - scroll_position.x < scroll_rect.width * 0.15f)
        scroll_position.x -= width_per_second * sample_ratio;
    }
    

    bool TryToSelectUnselectedItem()
    {
      if (try_to_select_unselected_item_callback != null)
        return try_to_select_unselected_item_callback(Event.current.mousePosition);
      return false;
    }

    bool IsMouseDownOfSelectedItem()
    {
      if (is_mouse_down_of_selected_item != null)
        return is_mouse_down_of_selected_item(Event.current.mousePosition);
      return false;
    }

    bool IsHasSelectedItem()
    {
      if (is_has_selected_item != null)
        return is_has_selected_item();
      return false;
    }

    void UpdateSelectedItems()
    {
      update_selected_items_callback?.Invoke(EditorMouseInput.selected_rect);
    }

    void OnDraggingSelected()
    {
      on_dragging_selected_callback?.Invoke(Event.current.delta.x / width_per_second);
    }

    void DoEditorCommand(EditorCommand editorCommand)
    {
      on_do_editorCommand_callback?.Invoke(editorCommand);
    }

    void OnMouseRightButtonClick(Rect position_rect)
    {
      this.on_mouse_right_button_click_callback?.Invoke(position_rect);
    }
  }
}