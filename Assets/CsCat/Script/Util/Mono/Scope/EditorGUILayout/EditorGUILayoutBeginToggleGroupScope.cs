#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    // Begin a vertical group with a toggle to enable or disable all the controls within at once.
    public class EditorGUILayoutBeginToggleGroupScope : IDisposable
    {
        public EditorGUILayoutBeginToggleGroupScope(GUIContent label, ref bool isToggle)
        {
            isToggle = EditorGUILayout.BeginToggleGroup(label, isToggle);
        }

        public EditorGUILayoutBeginToggleGroupScope(string label, ref bool isToggle)
        {
            isToggle = EditorGUILayout.BeginToggleGroup(label, isToggle);
        }

        public void Dispose()
        {
            EditorGUILayout.EndToggleGroup();
        }
    }
}
#endif