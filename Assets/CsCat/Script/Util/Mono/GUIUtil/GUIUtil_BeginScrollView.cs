using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scroll_position, Rect view_rect)
    {
      return new GUIBeginScrollViewScope(position, ref scroll_position, view_rect);
    }

    public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scroll_position, Rect view_rect,
      bool is_always_show_horizontal,
      bool is_always_show_vertical)
    {
      return new GUIBeginScrollViewScope(position, ref scroll_position, view_rect, is_always_show_horizontal,
        is_always_show_vertical);
    }

    public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scroll_position, Rect view_rect,
      GUIStyle horizontal_scrollbar,
      GUIStyle vertical_scrollbar)
    {
      return new GUIBeginScrollViewScope(position, ref scroll_position, view_rect, horizontal_scrollbar,
        vertical_scrollbar);
    }

    public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scroll_position, Rect view_rect,
      bool is_always_show_horizontal,
      bool is_always_show_vertical, GUIStyle horizontal_scrollbar, GUIStyle vertical_scrollbar)
    {
      return new GUIBeginScrollViewScope(position, ref scroll_position, view_rect, is_always_show_horizontal,
        is_always_show_vertical,
        horizontal_scrollbar, vertical_scrollbar);
    }
  }
}