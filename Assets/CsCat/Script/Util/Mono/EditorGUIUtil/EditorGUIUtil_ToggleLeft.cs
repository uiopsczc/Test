#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUIUtil
	{
		public static bool ToggleLeft(Rect position, string label, ref bool value)
		{
			return EditorGUIToggleLeftScope.ToggleLeft(position, label, ref value);
		}

		public static bool ToggleLeft(Rect position, string label, ref bool value, GUIStyle labelStyle)
		{
			return EditorGUIToggleLeftScope.ToggleLeft(position, label, ref value, labelStyle);
		}

		public static bool ToggleLeft(Rect position, GUIContent label, ref bool value, GUIStyle labelStyle)
		{
			return EditorGUIToggleLeftScope.ToggleLeft(position, label, ref value, labelStyle);
		}

		public static bool ToggleLeft(Rect position, GUIContent label, ref bool value)
		{
			return EditorGUIToggleLeftScope.ToggleLeft(position, label, ref value);
		}
	}
}
#endif