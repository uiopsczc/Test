#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class EditorGUILayoutToggleScope
	{
		public static bool Toggle(ref bool value)
		{
			value = EditorGUILayout.Toggle(value);
			return value;
		}

		public static bool Toggle(string label, ref bool value)
		{
			value = EditorGUILayout.Toggle(label, value);
			return value;
		}

		public static bool Toggle(ref bool value, GUIStyle style)
		{
			value = EditorGUILayout.Toggle(value, style);
			return value;
		}

		public static bool Toggle(string label, ref bool value, GUIStyle style)
		{
			value = EditorGUILayout.Toggle(label, value, style);
			return value;
		}

		public static bool Toggle(GUIContent label, ref bool value)
		{
			value = EditorGUILayout.Toggle(label, value);
			return value;
		}

		public static bool Toggle(GUIContent label, ref bool value, GUIStyle style)
		{
			value = EditorGUILayout.Toggle(label, value, style);
			return value;
		}
	}
}
#endif