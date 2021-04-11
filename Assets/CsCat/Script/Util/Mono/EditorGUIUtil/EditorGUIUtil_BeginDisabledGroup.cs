#if UNITY_EDITOR
namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static EditorGUIDisabledGroupScope DisabledGroup(bool is_disable)
    {
      return new EditorGUIDisabledGroupScope(is_disable);
    }
  }
}
#endif