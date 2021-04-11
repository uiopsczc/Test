using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIColorScope Color(Color color_new)
    {
      return new GUIColorScope(color_new);
    }
  }
}