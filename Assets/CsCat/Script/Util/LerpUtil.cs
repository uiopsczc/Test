using UnityEngine;

namespace CsCat
{
	public class LerpUtil
	{
		public static Rect Lerp(Rect a, Rect b, float lerpPCT)
		{
			return new Rect(
				Vector2.Lerp(a.position, b.position, lerpPCT),
				Vector2.Lerp(a.size, b.size, lerpPCT));
		}
	}
}