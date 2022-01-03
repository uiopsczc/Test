#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		public static EditorGUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(int add = 1,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalIndentLevelScope(add, options);
		}

		public static EditorGUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(GUIStyle style, int add = 1,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalIndentLevelScope(style, add, options);
		}

		public static EditorGUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(ref Rect rect, int add = 1,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalIndentLevelScope(ref rect, add, options);
		}

		public static EditorGUILayoutBeginVerticalIndentLevelScope BeginVerticalIndentLevel(ref Rect rect,
			GUIStyle style, int add = 1,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalIndentLevelScope(ref rect, style, add, options);
		}
	}
}
#endif