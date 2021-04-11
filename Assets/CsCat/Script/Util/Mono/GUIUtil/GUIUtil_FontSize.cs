using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIFontSizeScope FontSize(float size, params GUIStyle[] ps)
    {
      return new GUIFontSizeScope(size, ps);
    }
  }
}