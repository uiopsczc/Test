#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
    public partial class EditorGUILayoutUtil
    {
        public static bool InspectorTitlebar(ref bool isFoldout, Object targetObj, bool isExpandable)
        {
            return EditorGUILayoutInspectorTitlebarScope.InspectorTitlebar(ref isFoldout, targetObj, isExpandable);
        }

        public static bool InspectorTitlebar(ref bool isFoldout, Object[] targetObjs, bool isExpandable)
        {
            return EditorGUILayoutInspectorTitlebarScope.InspectorTitlebar(ref isFoldout, targetObjs, isExpandable);
        }
    }
}
#endif