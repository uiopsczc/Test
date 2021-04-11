
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
  // Begin a vertical group with a toggle to enable or disable all the controls within at once.
  public class EditorGUILayoutBeginToggleGroupScope : IDisposable
  {
    public EditorGUILayoutBeginToggleGroupScope(GUIContent label, ref bool is_toggle)
    {
      is_toggle = EditorGUILayout.BeginToggleGroup(label, is_toggle);
    }

    public EditorGUILayoutBeginToggleGroupScope(string label, ref bool is_toggle)
    {
      is_toggle = EditorGUILayout.BeginToggleGroup(label, is_toggle);
    }

    public void Dispose()
    {
      EditorGUILayout.EndToggleGroup();
    }
  }
}
#endif