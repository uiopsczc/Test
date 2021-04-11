using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIBackgroundColorScope BackgroundColor(Color color_new)
    {
      return new GUIBackgroundColorScope(color_new);
    }
  }
}