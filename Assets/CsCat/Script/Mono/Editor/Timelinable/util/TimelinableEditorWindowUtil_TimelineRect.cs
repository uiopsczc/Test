using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static partial class TimelinableEditorWindowUtil
  {
    public static void OnDrawTracksLeftSideCallback(TimelinableSequenceBase sequence, TimelineRect timelineRect)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return;
      for (int i = 0; i < sequence.tracks.Length; i++)
      {
        TimelinableTrackBase track = sequence.tracks[i];
        var track_color = i % 2 == 0 ? Color.gray.SetA(0.2f) : Color.gray.SetA(0.4f);
        var left_track_rect = timelineRect.GetLeftTrackRect(i);
        using (new GUILayout.AreaScope(left_track_rect))
        {
          EditorGUI.DrawRect(new Rect(0, 0, left_track_rect.width, left_track_rect.height), track_color);
          EditorGUILayout.LabelField(track.name, GUIStyleConst.Label_Bold_MiddleCenter_Style);
        }
      }
    }

    public static void OnDrawTracksRightSideCallback(TimelinableSequenceBase sequence, TimelineRect timelineRect)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return;
      for (int i = 0; i < sequence.tracks.Length; i++)
      {
        TimelinableTrackBase track = sequence.tracks[i];
        var track_color = i % 2 == 0 ? Color.gray.SetA(0.2f) : Color.gray.SetA(0.4f);
        var right_track_rect = timelineRect.GetRightTrackRect(i);

        using (new GUILayout.AreaScope(right_track_rect))
        {
          EditorGUI.DrawRect(new Rect(0, 0, right_track_rect.width, right_track_rect.height), track_color);
          //Draw ItemInfo
          for (int j = 0; j < track.itemInfoes.Length; j++)
          {
            var itemInfo = track.itemInfoes[j];
            var itemInfo_rect = timelineRect.GetRightTrackRect(itemInfo.time, itemInfo.duration, i);
            itemInfo.rect = itemInfo_rect;

            itemInfo_rect.y = 0;
            var style = new GUIStyle(GUIStyleConst.Label_Bold_MiddleLeft_Style.SetName(GUI.skin.box));
            if (itemInfo.is_selected || itemInfo.IsTimeInside(track.cur_time))
              GUIUtil.Box(itemInfo_rect,
                string.Format("{0}[{1}]<color=red>{2}</color>", itemInfo.name, j, itemInfo.is_selected ? "*" : ""),
                style, Color.blue.SetA(0.5f));
            else
              GUIUtil.Box(itemInfo_rect, string.Format("{0}[{1}]", itemInfo.name, j), style, Color.gray.SetA(0.5f));
          }
        }
      }
    }

    public static bool IsMouseDownOfSelectedItem(Vector2 mouse_position, TimelinableSequenceBase sequence)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return false;
      foreach (var track in sequence.tracks)
      {
        foreach (var itemInfo in track.itemInfoes)
        {
          if (itemInfo.rect.Contains(mouse_position) && itemInfo.is_selected)
            return true;
        }
      }

      return false;
    }

    public static bool TryToSelectUnselectedItemCallback(Vector2 mouse_position, TimelinableSequenceBase sequence)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return false;
      TimelinableItemInfoBase select_new_unselected_itemInfo = null;
      foreach (var track in sequence.tracks)
      {
        foreach (var itemInfo in track.itemInfoes)
        {
          if (itemInfo.rect.Contains(mouse_position) && !itemInfo.is_selected)
          {
            select_new_unselected_itemInfo = itemInfo;
            select_new_unselected_itemInfo.is_selected = true;
            break;
          }
        }

        if (select_new_unselected_itemInfo != null)
          break;
      }

      if (!Event.current.control)
      {
        foreach (var track in sequence.tracks)
        {
          foreach (var itemInfo in track.itemInfoes)
          {
            if (select_new_unselected_itemInfo != itemInfo)
              itemInfo.is_selected = false;
          }
        }
      }


      if (select_new_unselected_itemInfo != null)
        return true;
      return false;
    }


    public static void UpdateSelectedItemsCallback(Rect selecting_rect, TimelinableSequenceBase sequence)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return;
      foreach (var track in sequence.tracks)
      {
        foreach (var itemInfo in track.itemInfoes)
        {
          if (!Event.current.control || !itemInfo.is_selected)
            itemInfo.is_selected = itemInfo.rect.Overlaps(selecting_rect);
        }
      }
    }

    public static void OnDraggingSelectedCallback(float delta_duration, TimelinableSequenceBase sequence, float play_time)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return;
      foreach (var track in sequence.tracks)
      {
        bool is_track_has_selected = false;
        foreach (var itemInfo in track.itemInfoes)
        {
          if (itemInfo.is_selected)
          {
            itemInfo.time += delta_duration;
            is_track_has_selected = true;
          }
        }

        if (is_track_has_selected)
        {
          Array.Sort(track.itemInfoes);
          track.Retime(play_time);
        }
      }
    }

    public static bool IsHasSelectedItem(TimelinableSequenceBase sequence)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return false;
      foreach (var track in sequence.tracks)
      {
        foreach (var itemInfo in track.itemInfoes)
        {
          if (itemInfo.is_selected)
            return true;
        }
      }

      return false;
    }

    public static void OnPlayTimeChangeCallback(TimelinableSequenceBase sequence, float play_time)
    {
      if (sequence == null || sequence.tracks.IsNullOrEmpty())
        return;
      sequence.Retime(play_time);
    }

    public static void OnMouseRightButtonClickCallback(Rect position_rect, TimelinableTrackBase[] tracks)
    {
      GUIContent[] menu_items = new[] {new GUIContent("Past"), new GUIContent("Delete")};
      EditorUtility.DisplayCustomMenu(position_rect, menu_items, -1, OnMenuItem, tracks);
    }

    private static void OnMenuItem(object userData, string[] options, int selected)
    {
      TimelinableTrackBase[] tracks = userData as TimelinableTrackBase[];
      switch (selected)
      {
        case 0:
          OnDoEditorCommandCallback(EditorCommand.Paste, tracks);
          break;
        case 1:
          OnDoEditorCommandCallback(EditorCommand.Delete, tracks);
          break;
        default:
          break;
      }
    }


    public static void OnDoEditorCommandCallback(EditorCommand editorCommand, TimelinableTrackBase[] tracks)
    {
      switch (editorCommand)
      {
        case EditorCommand.Copy:
          break;
        case EditorCommand.Paste:
          DoEditorOperation_Paste(tracks);
          break;
        case EditorCommand.Delete:
          DoEditorOperation_Delete(tracks);
          break;
        default:
          break;
      }
    }

    private static void DoEditorOperation_Paste(TimelinableTrackBase[] tracks)
    {
      if (tracks.IsNullOrEmpty())
        return;
      using (var d = new DelayEditHandlerScope(null))
      {
        foreach (var track in tracks)
        {
          foreach (var itemInfo in track.itemInfoes)
          {
            if (itemInfo.is_selected)
            {
              var _itemInfo = itemInfo; //形成闭包
              var _track = track;
              d.ToCallback(() =>
              {
                var to_add_itemInfo = _itemInfo.GetType().CreateInstance<TimelinableItemInfoBase>();
                to_add_itemInfo.CopyFrom(_itemInfo);
                to_add_itemInfo.time = _track.cur_time;
                _track.AddItemInfo(to_add_itemInfo);
              });
            }
          }
        }
      }
    }


    private static void DoEditorOperation_Delete(TimelinableTrackBase[] tracks)
    {
      if (tracks.IsNullOrEmpty())
        return;
      using (var d = new DelayEditHandlerScope(null))
      {
        foreach (var track in tracks)
        {
          foreach (var itemInfo in track.itemInfoes)
          {
            if (itemInfo.is_selected)
            {
              var _itemInfo = itemInfo; //形成闭包
              var _track = track; //形成闭包
              d.ToCallback(() => { _track.RemoveItemInfo(_itemInfo); });
            }
          }
        }
      }
    }
  }
}