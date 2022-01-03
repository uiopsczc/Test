#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	// Begin a horizontal group and get its rect back.
	public class EditorGUILayoutBeginHorizontalScope : IDisposable
	{
		public EditorGUILayoutBeginHorizontalScope(params GUILayoutOption[] options)
		{
			EditorGUILayout.BeginHorizontal(options);
		}

		public EditorGUILayoutBeginHorizontalScope(GUIStyle style, params GUILayoutOption[] options)
		{
			EditorGUILayout.BeginHorizontal(style, options);
		}

		public EditorGUILayoutBeginHorizontalScope(ref Rect rect, params GUILayoutOption[] options)
		{
			rect = EditorGUILayout.BeginHorizontal(options);
		}

		public EditorGUILayoutBeginHorizontalScope(ref Rect rect, GUIStyle style, params GUILayoutOption[] options)
		{
			rect = EditorGUILayout.BeginHorizontal(style, options);
		}

		public void Dispose()
		{
			EditorGUILayout.EndHorizontal();
		}
	}
}
#endif