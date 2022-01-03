using UnityEngine;

namespace CsCat
{
	public class CameraShakeResult
	{
		public Vector3 posistion;
		public Vector3 eulerAngles;
		public float fov;

		public CameraShakeResult(Vector3 posistion, Vector3 eulerAngles, float fov)
		{
			this.posistion = posistion;
			this.eulerAngles = eulerAngles;
			this.fov = fov;
		}
	}
}




