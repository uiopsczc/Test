#if UNITY_EDITOR
namespace CsCat
{
  public partial class GUILayoutUtil
  {
    public GUILayoutContentScope Content()
    {
      return new GUILayoutContentScope();
    }
  }
}
#endif