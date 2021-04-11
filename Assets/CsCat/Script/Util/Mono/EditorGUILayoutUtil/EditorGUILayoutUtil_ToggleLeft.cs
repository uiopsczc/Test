#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static bool ToggleLeft(string label, ref bool value)
    {
      return EditorGUILayoutToggleLeftScope.ToggleLeft(label, ref value);
    }

    public static bool ToggleLeft(string label, ref bool value, GUIStyle label_style)
    {
      return EditorGUILayoutToggleLeftScope.ToggleLeft(label, ref value, label_style);
    }

    public static bool ToggleLeft(GUIContent label, ref bool value, GUIStyle label_style)
    {
      return EditorGUILayoutToggleLeftScope.ToggleLeft(label, ref value, label_style);
    }

    public static bool ToggleLeft(GUIContent label, ref bool value)
    {
      return EditorGUILayoutToggleLeftScope.ToggleLeft(label, ref value);
    }
  }
}
#endif