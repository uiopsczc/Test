
#if UNITY_EDITOR
using System;
using UnityEditor;
namespace CsCat
{
  public class EditorGUIDisabledGroupScope : IDisposable
  {
    public EditorGUIDisabledGroupScope(bool is_disable)
    {
      EditorGUI.BeginDisabledGroup(is_disable);
    }

    public void Dispose()
    {
      EditorGUI.EndDisabledGroup();
    }
  }
}
#endif