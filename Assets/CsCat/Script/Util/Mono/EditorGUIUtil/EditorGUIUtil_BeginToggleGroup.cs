#if UNITY_EDITOR
namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static EditorGUIBeginToggleGroupScope BeginToggleGroup(bool is_toggle, string name = "")
    {
      return new EditorGUIBeginToggleGroupScope(is_toggle, name);
    }
  }
}
#endif