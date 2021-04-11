using System;
#if UNITY_EDITOR
using UnityEditor;
namespace CsCat
{
  public class EditorGUILabelWidthScope : IDisposable
  {
    private readonly float label_width_pre;

    public EditorGUILabelWidthScope(float w)
    {
      label_width_pre = EditorGUIUtility.labelWidth;
      EditorGUIUtility.labelWidth = w;
    }

    public void Dispose()
    {
      EditorGUIUtility.labelWidth = label_width_pre;
    }
  }
}
#endif