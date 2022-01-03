using UnityEngine;

namespace CsCat
{
	public class PivotInfo
	{
		public float x;
		public float y;
		public string name;
		public Vector2 pivot;

		public PivotInfo(float x, float y, string name)
		{
			this.x = x;
			this.y = y;
			pivot = new Vector2(x, y);
			this.name = name;
		}
	}
}