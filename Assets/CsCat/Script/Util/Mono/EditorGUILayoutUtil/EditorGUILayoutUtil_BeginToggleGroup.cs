#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
    public partial class EditorGUILayoutUtil
    {
        public static EditorGUILayoutBeginToggleGroupScope BeginToggleGroup(GUIContent label, ref bool isToggle)
        {
            return new EditorGUILayoutBeginToggleGroupScope(label, ref isToggle);
        }

        public static EditorGUILayoutBeginToggleGroupScope BeginToggleGroup(string label, ref bool isToggle)
        {
            return new EditorGUILayoutBeginToggleGroupScope(label, ref isToggle);
        }
    }
}
#endif