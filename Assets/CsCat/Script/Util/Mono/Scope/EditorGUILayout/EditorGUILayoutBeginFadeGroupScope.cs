
#if UNITY_EDITOR
using System;
using UnityEditor;
namespace CsCat
{
  public class EditorGUILayoutBeginFadeGroupScope : IDisposable
  {
    private readonly bool is_with_indent;

    /// <summary>
    ///   Begins a group that can be be hidden/shown and the transition will be animated.
    /// </summary>
    /// <param name="value">A value between 0 and 1, 0 being hidden, and 1 being fully visible.</param>
    /// <param name="is_with_indent">Default is without indent.</param>
    public EditorGUILayoutBeginFadeGroupScope(float value, bool is_with_indent = false)
    {
      EditorGUILayout.BeginFadeGroup(value);
      this.is_with_indent = is_with_indent;
      if (this.is_with_indent)
        EditorGUI.indentLevel++;
    }

    public void Dispose()
    {
      EditorGUILayout.EndFadeGroup();
      if (is_with_indent)
        EditorGUI.indentLevel--;
    }
  }
}
#endif