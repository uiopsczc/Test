#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position,
      params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginScrollViewScope(ref scroll_position, options);
    }

    public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position,
      bool is_always_show_horizontal,
      bool is_always_show_vertical, params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginScrollViewScope(ref scroll_position, is_always_show_horizontal,
        is_always_show_vertical,
        options);
    }

    public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position,
      GUIStyle horizontal_scrollbar,
      GUIStyle vertical_scrollbar, params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginScrollViewScope(ref scroll_position, horizontal_scrollbar, vertical_scrollbar,
        options);
    }

    public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scroll_position,
      bool is_always_show_horizontal,
      bool is_always_show_vertical, GUIStyle horizontal_scrollbar, GUIStyle vertical_scrollbar, GUIStyle background,
      params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginScrollViewScope(ref scroll_position, is_always_show_horizontal,
        is_always_show_vertical,
        horizontal_scrollbar, vertical_scrollbar, background, options);
    }
  }
}
#endif