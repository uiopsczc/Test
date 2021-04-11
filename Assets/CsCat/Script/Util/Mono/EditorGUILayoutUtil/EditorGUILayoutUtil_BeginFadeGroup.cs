#if UNITY_EDITOR
namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static EditorGUILayoutBeginFadeGroupScope BeginFadeGroup(float value, bool withIndent = false)
    {
      return new EditorGUILayoutBeginFadeGroupScope(value, withIndent);
    }
  }
}
#endif