#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
    public partial class EditorGUIUtil
    {
        public static bool isChanged => EditorGUI.EndChangeCheck();

        public static EditorGUIBeginChangeCheckScope BeginChangeCheck()
        {
            return new EditorGUIBeginChangeCheckScope();
        }
    }
}
#endif