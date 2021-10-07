
#if UNITY_EDITOR
using System;
using UnityEditor;
namespace CsCat
{
  public class EditorGUILayoutBeginFadeGroupScope : IDisposable
  {
    private readonly bool isWithIndent;

    /// <summary>
    ///   Begins a group that can be be hidden/shown and the transition will be animated.
    /// </summary>
    /// <param name="value">A value between 0 and 1, 0 being hidden, and 1 being fully visible.</param>
    /// <param name="isWithIndent">Default is without indent.</param>
    public EditorGUILayoutBeginFadeGroupScope(float value, bool isWithIndent = false)
    {
      EditorGUILayout.BeginFadeGroup(value);
      this.isWithIndent = isWithIndent;
      if (this.isWithIndent)
        EditorGUI.indentLevel++;
    }

    public void Dispose()
    {
      EditorGUILayout.EndFadeGroup();
      if (isWithIndent)
        EditorGUI.indentLevel--;
    }
  }
}
#endif