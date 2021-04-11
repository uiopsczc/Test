using UnityEngine;

namespace CsCat
{
  public class LerpUtil
  {
    public static Rect Lerp(Rect a, Rect b, float t)
    {
      return new Rect(
        Vector2.Lerp(a.position, b.position, t),
        Vector2.Lerp(a.size, b.size, t));
    }
  }
}