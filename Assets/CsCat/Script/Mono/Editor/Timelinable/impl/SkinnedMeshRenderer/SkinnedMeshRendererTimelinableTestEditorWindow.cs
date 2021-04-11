using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class SkinnedMeshRendererTimelinableTestEditorWindow : EditorWindow
  {
    private HorizontalResizableRects resizableRects;
    private TimelineRect timelineRect;

    SkinnedMeshRendererTimelinableSequence sequence;

    
    void Awake()
    {
      this.resizableRects =
        new HorizontalResizableRects(() => new Rect(0, 0, this.position.width, this.position.height), new[] {300f});
      this.timelineRect = new TimelineRect(() => resizableRects.rects[1]);
      this.timelineRect.on_draw_tracks_left_side_callback = () => TimelinableEditorWindowUtil.OnDrawTracksLeftSideCallback(sequence, timelineRect);
      this.timelineRect.on_draw_tracks_right_side_callback = () => TimelinableEditorWindowUtil.OnDrawTracksRightSideCallback(sequence, timelineRect);

      this.timelineRect.is_mouse_down_of_selected_item = (mouse_position) => TimelinableEditorWindowUtil.IsMouseDownOfSelectedItem(mouse_position, sequence);
      this.timelineRect.try_to_select_unselected_item_callback = (mouse_position) => TimelinableEditorWindowUtil.TryToSelectUnselectedItemCallback(mouse_position, sequence);
      this.timelineRect.update_selected_items_callback = (selecting_rect) => TimelinableEditorWindowUtil.UpdateSelectedItemsCallback(selecting_rect, sequence);
      this.timelineRect.on_dragging_selected_callback = (delta_duration) => TimelinableEditorWindowUtil.OnDraggingSelectedCallback(delta_duration, sequence, timelineRect.play_time);
      this.timelineRect.is_has_selected_item = () => TimelinableEditorWindowUtil.IsHasSelectedItem(sequence);

      this.timelineRect.on_mouse_right_button_click_callback = (position_rect) => TimelinableEditorWindowUtil.OnMouseRightButtonClickCallback(position_rect, sequence.tracks);

      this.timelineRect.on_do_editorCommand_callback = (editorCommand)=> TimelinableEditorWindowUtil.OnDoEditorCommandCallback(editorCommand, sequence.tracks);

      this.timelineRect.on_play_time_change_callback = (play_time) => TimelinableEditorWindowUtil.OnPlayTimeChangeCallback(sequence, play_time);
    }

    public void OnEnable()
    {
      this.timelineRect.OnEnable();
    }

    public void OnDisable()
    {
      this.timelineRect.OnDisable();
      sequence?.OnSequenceDisable();
    }


    void OnGUI()
    {
      this.resizableRects.OnGUI();
      DrawLeft();
      this.timelineRect.OnGUI();
      Repaint();
    }
    
  }
}