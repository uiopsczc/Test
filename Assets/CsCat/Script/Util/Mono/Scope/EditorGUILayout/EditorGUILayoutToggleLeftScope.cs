
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
namespace CsCat
{
  public class EditorGUILayoutToggleLeftScope
  {
    public static bool ToggleLeft(string label, ref bool value)
    {
      value = EditorGUILayout.ToggleLeft(label, value, EditorStyles.label);
      return value;
    }

    public static bool ToggleLeft(string label, ref bool value, GUIStyle label_style)
    {
      value = EditorGUILayout.ToggleLeft(label, value, label_style);
      return value;
    }

    public static bool ToggleLeft(GUIContent label, ref bool value, GUIStyle label_style)
    {
      value = EditorGUILayout.ToggleLeft(label, value, label_style);
      return value;
    }

    public static bool ToggleLeft(GUIContent label, ref bool value)
    {
      value = EditorGUILayout.ToggleLeft(label, value, EditorStyles.label);
      return value;
    }
  }
}
#endif