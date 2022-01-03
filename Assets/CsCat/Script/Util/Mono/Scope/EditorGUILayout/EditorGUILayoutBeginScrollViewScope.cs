#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class EditorGUILayoutBeginScrollViewScope : IDisposable
	{
		public EditorGUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, options);
		}

		public EditorGUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, GUIStyle style,
			params GUILayoutOption[] options)
		{
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, options);
		}

		public EditorGUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, bool isAlwaysShowHorizontal,
			bool isAlwaysShowVertical, params GUILayoutOption[] options)
		{
			scrollPosition =
				EditorGUILayout.BeginScrollView(scrollPosition, isAlwaysShowHorizontal, isAlwaysShowVertical,
					options);
		}

		public EditorGUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, GUIStyle horizontalScrollbar,
			GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			scrollPosition =
				EditorGUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
		}

		public EditorGUILayoutBeginScrollViewScope(ref Vector2 scrollPosition, bool isAlwaysShowHorizontal,
			bool isAlwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background,
			params GUILayoutOption[] options)
		{
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, isAlwaysShowHorizontal,
				isAlwaysShowVertical,
				horizontalScrollbar, verticalScrollbar, background, options);
		}

		public void Dispose()
		{
			EditorGUILayout.EndScrollView();
		}
	}
}
#endif