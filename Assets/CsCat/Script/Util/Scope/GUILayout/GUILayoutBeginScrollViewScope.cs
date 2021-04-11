using System;
using UnityEngine;

namespace CsCat
{
  public class GUILayoutBeginScrollViewScope : IDisposable
  {
    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition);
    }

    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, params GUILayoutOption[] options)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition, options);
    }

    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, GUIStyle style)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition, style);
    }

    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition, style, options);
    }

    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, GUIStyle horizontalScrollBar,
      GUIStyle verticalScrollBar)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition, horizontalScrollBar, verticalScrollBar);
    }

    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, GUIStyle horizontalScrollBar,
      GUIStyle verticalScrollBar,
      params GUILayoutOption[] options)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition, horizontalScrollBar, verticalScrollBar, options);
    }

    public GUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical,
      params GUILayoutOption[] options)
    {
      scrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
    }



    public void Dispose()
    {
      GUILayout.EndScrollView();
    }
  }
}