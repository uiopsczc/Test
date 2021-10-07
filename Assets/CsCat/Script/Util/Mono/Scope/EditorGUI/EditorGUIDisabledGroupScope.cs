#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
    public class EditorGUIDisabledGroupScope : IDisposable
    {
        public EditorGUIDisabledGroupScope(bool isDisable)
        {
            EditorGUI.BeginDisabledGroup(isDisable);
        }

        public void Dispose()
        {
            EditorGUI.EndDisabledGroup();
        }
    }
}
#endif