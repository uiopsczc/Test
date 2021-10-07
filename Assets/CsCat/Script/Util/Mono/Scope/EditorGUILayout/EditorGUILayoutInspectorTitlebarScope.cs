#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
    public class EditorGUILayoutInspectorTitlebarScope
    {
        public static bool InspectorTitlebar(ref bool isFoldout, Object targetObj, bool isExpandable)
        {
            isFoldout = EditorGUILayout.InspectorTitlebar(isFoldout, targetObj, isExpandable);
            return isFoldout;
        }

        public static bool InspectorTitlebar(ref bool isFoldout, Object[] targetObjs, bool isExpandable)
        {
            isFoldout = EditorGUILayout.InspectorTitlebar(isFoldout, targetObjs, isExpandable);
            return isFoldout;
        }
    }
}
#endif