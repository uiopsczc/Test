#if UNITY_EDITOR
namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static EditorGUILabelWidthScope LabelWidth(float w)
    {
      return new EditorGUILabelWidthScope(w);
    }
  }
}
#endif