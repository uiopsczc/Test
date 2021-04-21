using UnityEngine;

namespace CsCat
{
  public partial class GUILayoutUtil
  {
    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position);
    }

    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position,
      params GUILayoutOption[] options)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position, options);
    }

    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position, GUIStyle style)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position, style);
    }

    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position, GUIStyle style,
      params GUILayoutOption[] options)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position, style, options);
    }

    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position, GUIStyle horizontal_scrollBar,
      GUIStyle vertical_scrollBar)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position, horizontal_scrollBar, vertical_scrollBar);
    }

    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position, GUIStyle horizontal_scrollBar,
      GUIStyle vertical_scrollBar, params GUILayoutOption[] options)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position, horizontal_scrollBar, vertical_scrollBar, options);
    }

    public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position, bool is_always_show_horizontal,
      bool is_always_show_vertical, params GUILayoutOption[] options)
    {
      return new GUILayoutBeginScrollViewScope(ref scroll_position, is_always_show_horizontal, is_always_show_vertical,
        options);
    }
  }
}