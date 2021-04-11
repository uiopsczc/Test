using System;
using UnityEngine;

namespace CsCat
{
  public class GUILayoutBeginHorizontalScope : IDisposable
  {
    public GUILayoutBeginHorizontalScope()
    {
      GUILayout.BeginHorizontal();
    }

    public GUILayoutBeginHorizontalScope(params GUILayoutOption[] layoutOptions)
    {
      GUILayout.BeginHorizontal(layoutOptions);
    }

    public GUILayoutBeginHorizontalScope(GUIStyle guiStyle, params GUILayoutOption[] layoutOptions)
    {
      GUILayout.BeginHorizontal(guiStyle, layoutOptions);
    }

    public void Dispose()
    {
      GUILayout.EndHorizontal();
    }
  }
}