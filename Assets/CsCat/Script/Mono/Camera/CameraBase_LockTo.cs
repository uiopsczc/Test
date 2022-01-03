using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private Transform lock_to_transform;

		// 锁定
		public void SetLockTo(Transform lock_to_transform)
		{
			this.lock_to_transform = lock_to_transform;
		}

		public void SetLockTo(GameObject gameObject)
		{
			SetLockTo(gameObject.transform);
		}

		public void ApplyLockTo(float deltaTime)
		{
			Vector3 position = this.lock_to_transform.position;
			graphicComponent.transform.position = Vector3.Lerp(graphicComponent.transform.position, position, deltaTime);
		}



	}
}



