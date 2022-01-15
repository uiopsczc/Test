using UnityEngine;

namespace CsCat
{
	public class CameraShakeData
	{
		public float frameTime;
		public float duration;
		public Vector3? posShakeRange;
		public Vector3? posShakeFrequency;
		public Vector3? eulerAnglesShakeRange;
		public Vector3? eulerAnglesShakeFrequency;
		public float? fovShakeRange;
		public float? fovShakeFrequency;

		public CameraShakeData(float duration, Vector3? posShakeRange, Vector3? posShakeFrequency,
		  Vector3? eulerAnglesShakeRange,
		  Vector3? eulerAnglesShakeFrequency, float? fovShakeRange, float? fovShakeFrequency)
		{
			this.frameTime = 0;
			this.duration = duration;
			this.posShakeRange = posShakeRange;
			this.posShakeFrequency = posShakeFrequency;
			this.eulerAnglesShakeRange = eulerAnglesShakeRange;
			this.eulerAnglesShakeFrequency = eulerAnglesShakeFrequency;
			this.fovShakeRange = fovShakeRange;
			this.fovShakeFrequency = fovShakeFrequency;
		}
	}
}



