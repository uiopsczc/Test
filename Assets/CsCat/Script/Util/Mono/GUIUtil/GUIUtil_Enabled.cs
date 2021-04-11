namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIEnabledScope Enabled(bool is_enabled_new)
    {
      return new GUIEnabledScope(is_enabled_new);
    }
  }
}