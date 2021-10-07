using System;
using UnityEngine;

namespace CsCat
{
    /// <summary>
    ///   GUI.BeginScrollView
    /// </summary>
    public class GUIBeginScrollViewScope : IDisposable
    {
        public GUIBeginScrollViewScope(Rect position, ref Vector2 scrollPosition, Rect viewRect)
        {
            scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect);
        }

        public GUIBeginScrollViewScope(Rect position, ref Vector2 scrollPosition, Rect viewRect,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical)
        {
            scrollPosition =
                GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical);
        }

        public GUIBeginScrollViewScope(Rect position, ref Vector2 scrollPosition, Rect viewRect,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar)
        {
            scrollPosition =
                GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar);
        }

        public GUIBeginScrollViewScope(Rect position, ref Vector2 scrollPosition, Rect viewRect,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
        {
            scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal,
                alwaysShowVertical,
                horizontalScrollbar, verticalScrollbar);
        }

        public void Dispose()
        {
            GUI.EndScrollView();
        }
    }
}