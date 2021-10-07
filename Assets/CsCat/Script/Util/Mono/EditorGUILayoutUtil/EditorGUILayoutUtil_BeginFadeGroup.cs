#if UNITY_EDITOR
namespace CsCat
{
    public partial class EditorGUILayoutUtil
    {
        public static EditorGUILayoutBeginFadeGroupScope BeginFadeGroup(float value, bool isWithIndent = false)
        {
            return new EditorGUILayoutBeginFadeGroupScope(value, isWithIndent);
        }
    }
}
#endif