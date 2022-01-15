using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		public void ApplySetting(Vector3 position, Vector3 eulerAngles, float fov)
		{
			ApplySetting(position, Quaternion.Euler(eulerAngles), fov);
		}

		public void ApplySetting(Vector3 position, Quaternion rotation, float fov)
		{
			this.currentPosition = position;
			this.currentEulerAngles = rotation.eulerAngles;
			this.currentRotation = rotation;
			this.currentFOV = fov;


			this.orgPosition = position;
			this.orgEulerAngles = rotation.eulerAngles;
			this.orgRotation = rotation;
			this.orgFOV = fov;
		}
	}
}