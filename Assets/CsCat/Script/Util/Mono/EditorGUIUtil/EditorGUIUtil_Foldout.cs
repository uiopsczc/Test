#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
    public partial class EditorGUIUtil
    {
        public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content,
            bool isToggleOnLabelClick,
            GUIStyle style)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content, isToggleOnLabelClick, style);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content,
            bool isToggleOnLabelClick)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content, isToggleOnLabelClick);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content, GUIStyle style)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content, style);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, GUIContent content)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, string content, bool isToggleOnLabelClick,
            GUIStyle style)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content, isToggleOnLabelClick, style);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, string content, bool isToggleOnLabelClick)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content, isToggleOnLabelClick);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, string content, GUIStyle style)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content, style);
        }

        public static bool Foldout(Rect position, ref bool isFoldout, string content)
        {
            return EditorGUIFoldoutScope.Foldout(position, ref isFoldout, content);
        }
    }
}
#endif