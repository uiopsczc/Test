using UnityEngine;

namespace CsCat
{
	public class CameraShakeData
	{
		public float frame_time;
		public float duration;
		public Vector3? pos_shake_range;
		public Vector3? pos_shake_frequency;
		public Vector3? eulerAngles_shake_range;
		public Vector3? eulerAngles_shake_frequency;
		public float? fov_shake_range;
		public float? fov_shake_frequency;

		public CameraShakeData(float duration, Vector3? pos_shake_range, Vector3? pos_shake_frequency,
		  Vector3? eulerAngles_shake_range,
		  Vector3? eulerAngles_shake_frequency, float? fov_shake_range, float? fov_shake_frequency)
		{
			this.frame_time = 0;
			this.duration = duration;
			this.pos_shake_range = pos_shake_range;
			this.pos_shake_frequency = pos_shake_frequency;
			this.eulerAngles_shake_range = eulerAngles_shake_range;
			this.eulerAngles_shake_frequency = eulerAngles_shake_frequency;
			this.fov_shake_range = fov_shake_range;
			this.fov_shake_frequency = fov_shake_frequency;
		}
	}
}



