#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
    public class EditorGUILayoutFoldoutScope
    {
        public static bool Foldout(ref bool isFoldout, GUIContent content, bool isToggleOnLabelClick,
            GUIStyle style)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, isToggleOnLabelClick, style);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, GUIContent content, bool isToggleOnLabelClick)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, isToggleOnLabelClick, EditorStyles.foldout);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, GUIContent content, GUIStyle style)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, false, style);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, GUIContent content)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, false, EditorStyles.foldout);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, string content, bool isToggleOnLabelClick, GUIStyle style)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, isToggleOnLabelClick, style);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, string content, bool isToggleOnLabelClick)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, isToggleOnLabelClick, EditorStyles.foldout);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, string content, GUIStyle style)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, false, style);
            return isFoldout;
        }

        public static bool Foldout(ref bool isFoldout, string content)
        {
            isFoldout = EditorGUILayout.Foldout(isFoldout, content, false, EditorStyles.foldout);
            return isFoldout;
        }
    }
}
#endif