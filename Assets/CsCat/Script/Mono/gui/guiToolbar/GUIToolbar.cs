#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class GUIToolbar
  {
    private List<GUIContent> button_guiContent_list;
    private int _selected_index = -1;
    private bool[] is_highlighted;

    public int selected_index
    {
      get { return _selected_index; }
      set { _selected_index = value; }
    }

    public Action<GUIToolbar, int, int> OnToolSelected;

    public GUIToolbar(List<GUIContent> button_guiContent_list)
    {
      this.button_guiContent_list = new List<GUIContent>(button_guiContent_list);
      is_highlighted = new bool[this.button_guiContent_list.Count];
    }

    public void SetHighlight(int index, bool is_highlight)
    {
      if (index >= 0 && index < is_highlighted.Length)
        is_highlighted[index] = is_highlight;
    }

    public void DrawGUI(Vector2 position, Vector2 button_size, Color? _background_color, Color? _outline_color)
    {
      Color background_color = _background_color.GetValueOrDefault(Color.white);
      Color outline_color = _outline_color.GetValueOrDefault(Color.black);
      using (new GUIColorScope())
      {
        int button_count = button_guiContent_list.Count;
        Rect toolbar_rect = new Rect(position.x, position.y, button_count * button_size.y, button_size.y);
        using (new GUILayoutBeginAreaScope(toolbar_rect))
        {
          DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, toolbar_rect.size), background_color,
            outline_color);
          using (new GUILayoutBeginHorizontalScope())
          {
            if (is_highlighted.Length != button_guiContent_list.Count)
              Array.Resize(ref is_highlighted, button_guiContent_list.Count);

            int button_padding = 4;
            Rect tool_button_rect = new Rect(button_padding, button_padding, toolbar_rect.size.y - 2 * button_padding,
              toolbar_rect.size.y - 2 * button_padding);
            for (int index = 0; index < button_guiContent_list.Count; ++index)
            {
              DrawToolbarButton(tool_button_rect, index);
              tool_button_rect.x = tool_button_rect.xMax + 2 * button_padding;
            }
          }
        }
      }
    }

    public void TriggerButton(int index)
    {
      int pre_index = _selected_index;
      _selected_index = index;
      if (OnToolSelected != null)
        OnToolSelected(this, _selected_index, pre_index);
    }

    private void DrawToolbarButton(Rect tool_button_rect, int index)
    {
      int icon_padding = 6;
      Rect toolIcon_rect = new Rect(tool_button_rect.x + icon_padding, tool_button_rect.y + icon_padding,
        tool_button_rect.size.x - 2 * icon_padding, tool_button_rect.size.y - 2 * icon_padding);

      if (is_highlighted[index])
        GUI.color = GUIToolbarConst.Highlith_Color;
      else
        GUI.color = _selected_index == index ? GUIToolbarConst.Active_Color : GUIToolbarConst.Disable_Color;
      if (GUI.Button(tool_button_rect, button_guiContent_list[index]))
        TriggerButton(index);
      GUI.color = Color.white;
      if (button_guiContent_list[index].image)
        GUI.DrawTexture(toolIcon_rect, button_guiContent_list[index].image);
    }
  }
}

#endif