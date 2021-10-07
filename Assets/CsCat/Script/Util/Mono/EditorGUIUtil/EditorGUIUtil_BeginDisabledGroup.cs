#if UNITY_EDITOR
namespace CsCat
{
    public partial class EditorGUIUtil
    {
        public static EditorGUIDisabledGroupScope DisabledGroup(bool isDisable)
        {
            return new EditorGUIDisabledGroupScope(isDisable);
        }
    }
}
#endif