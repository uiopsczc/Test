
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
namespace CsCat
{
  public class EditorGUIFoldoutScope
  {
    public static bool Foldout(Rect position, ref bool is_foldout, GUIContent content, bool is_toggle_on_label_click,
      GUIStyle style)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, is_toggle_on_label_click, style);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, GUIContent content, bool is_toggle_on_label_click)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, is_toggle_on_label_click, EditorStyles.foldout);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, GUIContent content, GUIStyle style)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, false, style);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, GUIContent content)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, false, EditorStyles.foldout);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, string content, bool is_toggle_on_label_click,
      GUIStyle style)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, is_toggle_on_label_click, style);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, string content, bool is_toggle_on_label_click)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, is_toggle_on_label_click, EditorStyles.foldout);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, string content, GUIStyle style)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, false, style);
      return is_foldout;
    }

    public static bool Foldout(Rect position, ref bool is_foldout, string content)
    {
      is_foldout = EditorGUI.Foldout(position, is_foldout, content, false, EditorStyles.foldout);
      return is_foldout;
    }
  }
}
#endif