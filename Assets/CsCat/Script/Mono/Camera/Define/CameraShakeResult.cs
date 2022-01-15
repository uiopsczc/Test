using UnityEngine;

namespace CsCat
{
	public class CameraShakeResult
	{
		public Vector3 position;
		public Vector3 eulerAngles;
		public float fov;

		public CameraShakeResult(Vector3 position, Vector3 eulerAngles, float fov)
		{
			this.position = position;
			this.eulerAngles = eulerAngles;
			this.fov = fov;
		}
	}
}