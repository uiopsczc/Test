using System;
#if UNITY_EDITOR
using UnityEditor;
namespace CsCat
{
  public class EditorGUIBeginToggleGroupScope : IDisposable
  {
    public EditorGUIBeginToggleGroupScope(bool is_toggle, string name = "")
    {
      toggle = EditorGUILayout.BeginToggleGroup(name, is_toggle);
    }

    public bool toggle { get; set; }

    public void Dispose()
    {
      EditorGUILayout.EndToggleGroup();
    }
  }
}
#endif