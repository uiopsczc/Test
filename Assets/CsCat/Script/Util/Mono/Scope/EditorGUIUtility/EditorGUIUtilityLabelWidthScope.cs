#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
  public class EditorGUIUtilityLabelWidthScope : IDisposable
  {
    private float org_lableWidth;

    public EditorGUIUtilityLabelWidthScope(float new_lableWidth)
    {
      org_lableWidth = EditorGUIUtility.labelWidth;
      EditorGUIUtility.labelWidth = new_lableWidth;
    }

    public void Dispose()
    {
      EditorGUIUtility.labelWidth = org_lableWidth;
    }
  }
}
#endif