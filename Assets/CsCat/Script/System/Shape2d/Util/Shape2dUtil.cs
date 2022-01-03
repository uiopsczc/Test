using UnityEngine;

namespace CsCat
{
	public class Shape2dUtil
	{
		public static Vector2 Rotate(Vector2 p, float angle)
		{
			//float cos = Mathf.Cos(Mathf.Deg2Rad * angle);
			//float sin = Mathf.Sin(Mathf.Deg2Rad * angle);
			//return new Vector2(cos * p.x + sin * p.y, -sin * p.x + cos * p.y);

			Quaternion rotation = Quaternion.Euler(0, 0, angle);
			Vector2 result = (rotation * p).ToVector2();
			return result;
		}
	}
}