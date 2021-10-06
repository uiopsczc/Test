using UnityEngine;

namespace CsCat
{
    public class LerpUtil
    {
        public static Rect Lerp(Rect a, Rect b, float lerpPct)
        {
            return new Rect(
                Vector2.Lerp(a.position, b.position, lerpPct),
                Vector2.Lerp(a.size, b.size, lerpPct));
        }
    }
}