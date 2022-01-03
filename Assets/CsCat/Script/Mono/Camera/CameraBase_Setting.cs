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
			this.current_position = position;
			this.current_eulerAngles = rotation.eulerAngles;
			this.current_rotation = rotation;
			this.current_fov = fov;


			this.org_position = position;
			this.org_eulerAngles = rotation.eulerAngles;
			this.org_rotation = rotation;
			this.org_fov = fov;
		}


	}
}




