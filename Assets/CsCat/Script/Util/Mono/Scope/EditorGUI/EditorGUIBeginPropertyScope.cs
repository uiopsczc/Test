
using UnityEngine;
#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
  // Create a Property wrapper, useful for making regular GUI controls work with SerializedProperty.
  public class EditorGUIBeginPropertyScope : IDisposable
  {
    public EditorGUIBeginPropertyScope(Rect total_position, GUIContent label, SerializedProperty property)
    {
      EditorGUI.BeginProperty(total_position, label, property);
    }

    public void Dispose()
    {
      EditorGUI.EndProperty();
    }
  }
}
#endif