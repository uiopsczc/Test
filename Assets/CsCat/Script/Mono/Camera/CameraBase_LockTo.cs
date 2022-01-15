using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private Transform lockToTransform;

		// 锁定
		public void SetLockTo(Transform lockToTransform)
		{
			this.lockToTransform = lockToTransform;
		}

		public void SetLockTo(GameObject gameObject)
		{
			SetLockTo(gameObject.transform);
		}

		public void ApplyLockTo(float deltaTime)
		{
			Vector3 position = this.lockToTransform.position;
			graphicComponent.transform.position = Vector3.Lerp(graphicComponent.transform.position, position, deltaTime);
		}



	}
}



