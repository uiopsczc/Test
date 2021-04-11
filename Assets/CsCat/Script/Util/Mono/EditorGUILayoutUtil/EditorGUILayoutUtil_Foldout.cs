#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static bool Foldout(ref bool is_foldout, GUIContent content, bool is_toggle_on_label_click, GUIStyle style)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content, is_toggle_on_label_click, style);
    }

    public static bool Foldout(ref bool is_foldout, GUIContent content, bool is_toggle_on_label_click)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content, is_toggle_on_label_click);
    }

    public static bool Foldout(ref bool is_foldout, GUIContent content, GUIStyle style)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content, style);
    }

    public static bool Foldout(ref bool is_foldout, GUIContent content)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content);
    }

    public static bool Foldout(ref bool is_foldout, string content, bool is_toggle_on_label_click, GUIStyle style)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content, is_toggle_on_label_click, style);
    }

    public static bool Foldout(ref bool is_foldout, string content, bool is_toggle_on_label_click)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content, is_toggle_on_label_click);
    }

    public static bool Foldout(ref bool is_foldout, string content, GUIStyle style)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content, style);
    }

    public static bool Foldout(ref bool is_foldout, string content)
    {
      return EditorGUILayoutFoldoutScope.Foldout(ref is_foldout, content);
    }
  }
}
#endif