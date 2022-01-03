#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class EditorGUIToggleLeftScope
	{
		public static bool ToggleLeft(Rect position, string label, ref bool value)
		{
			value = EditorGUI.ToggleLeft(position, label, value, EditorStyles.label);
			return value;
		}

		public static bool ToggleLeft(Rect position, string label, ref bool value, GUIStyle label_style)
		{
			value = EditorGUI.ToggleLeft(position, label, value, label_style);
			return value;
		}

		public static bool ToggleLeft(Rect position, GUIContent label, ref bool value, GUIStyle label_style)
		{
			value = EditorGUI.ToggleLeft(position, label, value, label_style);
			return value;
		}

		public static bool ToggleLeft(Rect position, GUIContent label, ref bool value)
		{
			value = EditorGUI.ToggleLeft(position, label, value, EditorStyles.label);
			return value;
		}
	}
}
#endif