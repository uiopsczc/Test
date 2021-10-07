#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
    public partial class EditorGUIUtil
    {
        public static bool InspectorTitlebar(Rect position, ref bool isFoldout, Object targetObj, bool isExpandable)
        {
            return EditorGUIInspectorTitlebarScope.InspectorTitlebar(position, ref isFoldout, targetObj,
                isExpandable);
        }

        public static bool InspectorTitlebar(Rect position, ref bool isFoldout, Object[] targetObjs,
            bool isExpandable)
        {
            return EditorGUIInspectorTitlebarScope.InspectorTitlebar(position, ref isFoldout, targetObjs,
                isExpandable);
        }
    }
}
#endif