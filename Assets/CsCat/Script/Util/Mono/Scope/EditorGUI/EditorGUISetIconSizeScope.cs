
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class EditorGUISetIconSizeScope : IDisposable
  {
    private readonly Vector2 size_pre;

    public EditorGUISetIconSizeScope(Vector2 size_pre)
    {
      this.size_pre = EditorGUIUtility.GetIconSize();
      EditorGUIUtility.SetIconSize(size_pre);
    }


    public void Dispose()
    {
      EditorGUIUtility.SetIconSize(size_pre);
    }
  }
}
#endif