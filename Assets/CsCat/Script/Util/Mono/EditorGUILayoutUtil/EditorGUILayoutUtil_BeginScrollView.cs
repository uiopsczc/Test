#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginScrollViewScope(ref scrollPosition, options);
		}

		public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			bool isAlwaysShowHorizontal,
			bool isAlwaysShowVertical, params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginScrollViewScope(ref scrollPosition, isAlwaysShowHorizontal,
				isAlwaysShowVertical,
				options);
		}

		public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			GUIStyle horizontalScrollbar,
			GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginScrollViewScope(ref scrollPosition, horizontalScrollbar,
				verticalScrollbar,
				options);
		}

		public static EditorGUILayoutBeginScrollViewScope BeginScrollView(ref Vector2 scrollPosition,
			bool isAlwaysShowHorizontal,
			bool isAlwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar,
			GUIStyle background,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginScrollViewScope(ref scrollPosition, isAlwaysShowHorizontal,
				isAlwaysShowVertical,
				horizontalScrollbar, verticalScrollbar, background, options);
		}
	}
}
#endif