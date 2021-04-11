#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static EditorGUILayoutBeginHorizontalScope BeginHorizontal(params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginHorizontalScope(options);
    }

    public static EditorGUILayoutBeginHorizontalScope BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginHorizontalScope(style, options);
    }

    public static EditorGUILayoutBeginHorizontalScope BeginHorizontal(ref Rect rect, params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginHorizontalScope(ref rect, options);
    }

    public static EditorGUILayoutBeginHorizontalScope BeginHorizontal(ref Rect rect, GUIStyle style,
      params GUILayoutOption[] options)
    {
      return new EditorGUILayoutBeginHorizontalScope(ref rect, style, options);
    }
  }
}
#endif