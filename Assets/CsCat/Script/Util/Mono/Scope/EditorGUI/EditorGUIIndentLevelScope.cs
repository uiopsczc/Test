using System;
#if UNITY_EDITOR
using UnityEditor;
namespace CsCat
{
  public class EditorGUIIndentLevelScope : IDisposable
  {
    private readonly int add = 1;

    public EditorGUIIndentLevelScope(int add = 1)
    {
      this.add = add;
      EditorGUI.indentLevel += add;
    }

    public void Dispose()
    {
      EditorGUI.indentLevel -= add;
    }
  }
}
#endif