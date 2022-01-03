#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		public static EditorGUILayoutBeginVerticalScope BeginVertical(params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalScope(options);
		}

		public static EditorGUILayoutBeginVerticalScope BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalScope(style, options);
		}

		public static EditorGUILayoutBeginVerticalScope BeginVertical(ref Rect rect, params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalScope(ref rect, options);
		}

		public static EditorGUILayoutBeginVerticalScope BeginVertical(ref Rect rect, GUIStyle style,
			params GUILayoutOption[] options)
		{
			return new EditorGUILayoutBeginVerticalScope(ref rect, style, options);
		}
	}
}
#endif