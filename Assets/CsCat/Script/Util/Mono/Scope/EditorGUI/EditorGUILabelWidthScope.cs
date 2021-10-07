using System;
#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
    public class EditorGUILabelWidthScope : IDisposable
    {
        private readonly float _preLabelWidth;

        public EditorGUILabelWidthScope(float lableWidth)
        {
            _preLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = lableWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = _preLabelWidth;
        }
    }
}
#endif