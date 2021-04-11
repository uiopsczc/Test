
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
  // Begin a vertical group and get its rect back.
  public class EditorGUILayoutBeginVerticalScope : IDisposable
  {
    public EditorGUILayoutBeginVerticalScope(params GUILayoutOption[] options)
    {
      EditorGUILayout.BeginVertical(options);
    }

    public EditorGUILayoutBeginVerticalScope(GUIStyle style, params GUILayoutOption[] options)
    {
      EditorGUILayout.BeginVertical(style, options);
    }

    public EditorGUILayoutBeginVerticalScope(ref Rect rect, params GUILayoutOption[] options)
    {
      rect = EditorGUILayout.BeginVertical(options);
    }

    public EditorGUILayoutBeginVerticalScope(ref Rect rect, GUIStyle style, params GUILayoutOption[] options)
    {
      rect = EditorGUILayout.BeginVertical(style, options);
    }

    public void Dispose()
    {
      EditorGUILayout.EndVertical();
    }
  }
}
#endif