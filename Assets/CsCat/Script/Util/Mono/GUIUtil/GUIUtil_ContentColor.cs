using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIContentColorScope ContentColor(Color color_new)
    {
      return new GUIContentColorScope(color_new);
    }
  }
}