#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class EditorGUIFoldoutScope
	{
		public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content, bool isToggleOnLabelClick,
			GUIStyle style)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, isToggleOnLabelClick, style);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content, bool isToggleOnLabelClick)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, isToggleOnLabelClick, EditorStyles.foldout);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content, GUIStyle style)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, false, style);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, false, EditorStyles.foldout);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, string content, bool isToggleOnLabelClick,
			GUIStyle style)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, isToggleOnLabelClick, style);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, string content, bool isToggleOnLabelClick)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, isToggleOnLabelClick, EditorStyles.foldout);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, string content, GUIStyle style)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, false, style);
			return isFoldout;
		}

		public static bool Foldout(Rect position, ref bool isFoldout, string content)
		{
			isFoldout = EditorGUI.Foldout(position, isFoldout, content, false, EditorStyles.foldout);
			return isFoldout;
		}
	}
}
#endif