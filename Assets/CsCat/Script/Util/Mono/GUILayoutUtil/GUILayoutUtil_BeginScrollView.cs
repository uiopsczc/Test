using UnityEngine;

namespace CsCat
{
	public partial class GUILayoutUtil
	{
		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition);
		}

		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			params GUILayoutOption[] options)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition, options);
		}

		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition, GUIStyle style)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition, style);
		}

		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition, GUIStyle style,
			params GUILayoutOption[] options)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition, style, options);
		}

		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			GUIStyle horizontalScrollBar,
			GUIStyle verticalScrollBar)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition, horizontalScrollBar, verticalScrollBar);
		}

		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			GUIStyle horizontalScrollBar,
			GUIStyle verticalScrollBar, params GUILayoutOption[] options)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition, horizontalScrollBar, verticalScrollBar,
				options);
		}

		public static GUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			bool isAlwaysShowHorizontal,
			bool isAlwaysShowVertical, params GUILayoutOption[] options)
		{
			return new GUILayoutBeginScrollViewScope(ref scrollPosition, isAlwaysShowHorizontal,
				isAlwaysShowVertical,
				options);
		}
	}
}