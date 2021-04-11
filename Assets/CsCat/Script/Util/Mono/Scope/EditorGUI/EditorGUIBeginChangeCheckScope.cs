
#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
  public class EditorGUIBeginChangeCheckScope : IDisposable
  {
    private bool is_EndChangeCheck = false;
    private bool is_changed;


    public EditorGUIBeginChangeCheckScope()
    {
      EditorGUI.BeginChangeCheck();
    }

    public bool IsChanged
    {
      get
      {
        if (!is_EndChangeCheck)
        {
          is_changed = EditorGUI.EndChangeCheck();
          is_EndChangeCheck = true;
        }

        return is_changed;
      }
    }

    public void Dispose()
    {
      if (is_EndChangeCheck)
        return;
      is_changed = EditorGUI.EndChangeCheck();
      is_EndChangeCheck = true;
    }
  }
}
#endif