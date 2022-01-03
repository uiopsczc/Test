#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUIUtil
	{
		public static bool Toggle(Rect position, ref bool value)
		{
			return EditorGUIToggleScope.Toggle(position, ref value);
		}

		public static bool Toggle(Rect position, string label, ref bool value)
		{
			return EditorGUIToggleScope.Toggle(position, label, ref value);
		}

		public static bool Toggle(Rect position, ref bool value, GUIStyle style)
		{
			return EditorGUIToggleScope.Toggle(position, ref value, style);
		}

		public static bool Toggle(Rect position, string label, ref bool value, GUIStyle style)
		{
			return EditorGUIToggleScope.Toggle(position, label, ref value, style);
		}

		public static bool Toggle(Rect position, GUIContent label, ref bool value)
		{
			return EditorGUIToggleScope.Toggle(position, label, ref value);
		}

		public static bool Toggle(Rect position, GUIContent label, ref bool value, GUIStyle style)
		{
			return EditorGUIToggleScope.Toggle(position, label, ref value, style);
		}
	}
}
#endif