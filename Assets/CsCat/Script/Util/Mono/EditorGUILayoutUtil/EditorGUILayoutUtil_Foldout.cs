#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		public static bool Foldout(ref bool isFoldout, GUIContent content, bool isToggleOnLabelClick,
			GUIStyle style)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content, isToggleOnLabelClick, style);
		}

		public static bool Foldout(ref bool isFoldout, GUIContent content, bool isToggleOnLabelClick)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content, isToggleOnLabelClick);
		}

		public static bool Foldout(ref bool isFoldout, GUIContent content, GUIStyle style)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content, style);
		}

		public static bool Foldout(ref bool isFoldout, GUIContent content)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content);
		}

		public static bool Foldout(ref bool isFoldout, string content, bool isToggleOnLabelClick, GUIStyle style)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content, isToggleOnLabelClick, style);
		}

		public static bool Foldout(ref bool isFoldout, string content, bool isToggleOnLabelClick)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content, isToggleOnLabelClick);
		}

		public static bool Foldout(ref bool isFoldout, string content, GUIStyle style)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content, style);
		}

		public static bool Foldout(ref bool isFoldout, string content)
		{
			return EditorGUILayoutFoldoutScope.Foldout(ref isFoldout, content);
		}
	}
}
#endif