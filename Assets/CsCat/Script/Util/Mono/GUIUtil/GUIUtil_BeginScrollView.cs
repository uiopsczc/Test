using UnityEngine;

namespace CsCat
{
    public partial class GUIUtil
    {
        public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scrollPosition,
            Rect viewRect)
        {
            return new GUIBeginScrollViewScope(position, ref scrollPosition, viewRect);
        }

        public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scrollPosition,
            Rect viewRect,
            bool isAlwaysShowHorizontal,
            bool isAlwaysShowVertical)
        {
            return new GUIBeginScrollViewScope(position, ref scrollPosition, viewRect, isAlwaysShowHorizontal,
                isAlwaysShowVertical);
        }

        public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scrollPosition,
            Rect viewRect,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar)
        {
            return new GUIBeginScrollViewScope(position, ref scrollPosition, viewRect, horizontalScrollbar,
                verticalScrollbar);
        }

        public static GUIBeginScrollViewScope BeginScrollView(Rect position, ref Vector2 scrollPosition,
            Rect viewRect,
            bool isAlwaysShowHorizontal,
            bool isAlwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
        {
            return new GUIBeginScrollViewScope(position, ref scrollPosition, viewRect, isAlwaysShowHorizontal,
                isAlwaysShowVertical,
                horizontalScrollbar, verticalScrollbar);
        }
    }
}