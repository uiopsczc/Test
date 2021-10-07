#if UNITY_EDITOR
namespace CsCat
{
    public partial class EditorGUIUtil
    {
        public static EditorGUIBeginToggleGroupScope BeginToggleGroup(bool isToggle,
            string name = StringConst.String_Empty)
        {
            return new EditorGUIBeginToggleGroupScope(isToggle, name);
        }
    }
}
#endif