#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
    public class EditorGUIUtilityLabelWidthScope : IDisposable
    {
        private float orgLableWidth;

        public EditorGUIUtilityLabelWidthScope(float newLableWidth)
        {
            orgLableWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = newLableWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = orgLableWidth;
        }
    }
}
#endif