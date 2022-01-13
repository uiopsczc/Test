using UnityEngine;

namespace CsCat
{
	public class CatmullRomPoint
	{
		public Vector3 position;
		public float createTime;

		public CatmullRomPoint(Vector3 position, float createTime)
		{
			this.position = position;
			this.createTime = createTime;
		}
	}
}