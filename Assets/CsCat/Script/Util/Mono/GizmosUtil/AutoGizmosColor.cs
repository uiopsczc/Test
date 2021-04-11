using UnityEngine;

namespace CsCat
{
  public class GizmosUtil
  {
    public static GizmosColorScope Color(Color color_new)
    {
      return new GizmosColorScope(color_new);
    }
  }
}