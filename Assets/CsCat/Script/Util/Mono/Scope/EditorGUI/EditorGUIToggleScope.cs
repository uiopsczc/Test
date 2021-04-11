#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
  public class EditorGUIToggleScope
  {
    public static bool Toggle(Rect position, ref bool value)
    {
      value = EditorGUI.Toggle(position, value);
      return value;
    }

    public static bool Toggle(Rect position, string label, ref bool value)
    {
      value = EditorGUI.Toggle(position, label, value);
      return value;
    }

    public static bool Toggle(Rect position, ref bool value, GUIStyle style)
    {
      value = EditorGUI.Toggle(position, value, style);
      return value;
    }

    public static bool Toggle(Rect position, string label, ref bool value, GUIStyle style)
    {
      value = EditorGUI.Toggle(position, label, value, style);
      return value;
    }

    public static bool Toggle(Rect position, GUIContent label, ref bool value)
    {
      value = EditorGUI.Toggle(position, label, value);
      return value;
    }

    public static bool Toggle(Rect position, GUIContent label, ref bool value, GUIStyle style)
    {
      value = EditorGUI.Toggle(position, label, value, style);
      return value;
    }
  }
}
#endif