#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		public static bool Toggle(ref bool value)
		{
			return EditorGUILayoutToggleScope.Toggle(ref value);
		}

		public static bool Toggle(string label, ref bool value)
		{
			return EditorGUILayoutToggleScope.Toggle(label, ref value);
		}

		public static bool Toggle(ref bool value, GUIStyle style)
		{
			return EditorGUILayoutToggleScope.Toggle(ref value, style);
		}

		public static bool Toggle(string label, ref bool value, GUIStyle style)
		{
			return EditorGUILayoutToggleScope.Toggle(label, ref value, style);
		}

		public static bool Toggle(GUIContent label, ref bool value)
		{
			return EditorGUILayoutToggleScope.Toggle(label, ref value);
		}

		public static bool Toggle(GUIContent label, ref bool value, GUIStyle style)
		{
			return EditorGUILayoutToggleScope.Toggle(label, ref value, style);
		}
	}
}
#endif