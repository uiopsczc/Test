
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class GUILayoutContentScope : IDisposable
  {
    private readonly float old;

    public GUILayoutContentScope()
    {
      GUILayout.BeginHorizontal();
      GUILayout.BeginHorizontal("AS TextArea", GUILayout.MinHeight(10f));
      GUILayout.BeginVertical();
      GUILayout.Space(2f);
      old = EditorGUIUtility.labelWidth;
      EditorGUIUtility.labelWidth = old - 10;
    }

    public void Dispose()
    {
      GUILayout.Space(3f);
      GUILayout.EndVertical();
      GUILayout.EndHorizontal();
      GUILayout.Space(3f);
      GUILayout.EndHorizontal();

      GUILayout.Space(3f);
      EditorGUIUtility.labelWidth = old;
    }
  }
}
#endif