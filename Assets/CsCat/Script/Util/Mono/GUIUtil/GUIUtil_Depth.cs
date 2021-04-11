namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIDepthScope Depth(int depth_new)
    {
      return new GUIDepthScope(depth_new);
    }
  }
}