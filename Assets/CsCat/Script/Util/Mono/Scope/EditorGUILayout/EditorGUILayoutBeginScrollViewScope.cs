
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
  public class EditorGUILayoutBeginScrollViewScope : IDisposable
  {
    public EditorGUILayoutBeginScrollViewScope(ref Vector2 scroll_position, params GUILayoutOption[] options)
    {
      scroll_position = EditorGUILayout.BeginScrollView(scroll_position, options);
    }

    public EditorGUILayoutBeginScrollViewScope(ref Vector2 scroll_position, GUIStyle style,
      params GUILayoutOption[] options)
    {
      scroll_position = EditorGUILayout.BeginScrollView(scroll_position, style, options);
    }

    public EditorGUILayoutBeginScrollViewScope(ref Vector2 scroll_position, bool is_always_show_horizontal,
      bool is_always_show_vertical, params GUILayoutOption[] options)
    {
      scroll_position =
        EditorGUILayout.BeginScrollView(scroll_position, is_always_show_horizontal, is_always_show_vertical, options);
    }

    public EditorGUILayoutBeginScrollViewScope(ref Vector2 scroll_position, GUIStyle horizontalScrollbar,
      GUIStyle verticalScrollbar, params GUILayoutOption[] options)
    {
      scroll_position =
        EditorGUILayout.BeginScrollView(scroll_position, horizontalScrollbar, verticalScrollbar, options);
    }

    public EditorGUILayoutBeginScrollViewScope(ref Vector2 scroll_position, bool is_always_show_horizontal,
      bool is_always_show_vertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background,
      params GUILayoutOption[] options)
    {
      scroll_position = EditorGUILayout.BeginScrollView(scroll_position, is_always_show_horizontal,
        is_always_show_vertical,
        horizontalScrollbar, verticalScrollbar, background, options);
    }

    public void Dispose()
    {
      EditorGUILayout.EndScrollView();
    }
  }
}
#endif