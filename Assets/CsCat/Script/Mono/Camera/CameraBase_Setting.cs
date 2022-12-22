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
			this._currentPosition = position;
			this._currentEulerAngles = rotation.eulerAngles;
			this._currentRotation = rotation;
			this._currentFOV = fov;


			this._orgPosition = position;
			this._orgEulerAngles = rotation.eulerAngles;
			this._orgRotation = rotation;
			this._orgFOV = fov;
		}
	}
}