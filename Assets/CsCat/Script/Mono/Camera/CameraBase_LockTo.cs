using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private Transform _lockToTransform;

		// 锁定
		public void SetLockTo(Transform lockToTransform)
		{
			this._lockToTransform = lockToTransform;
		}

		public void SetLockTo(GameObject gameObject)
		{
			SetLockTo(gameObject.transform);
		}

		public void ApplyLockTo(float deltaTime)
		{
			Vector3 position = this._lockToTransform.position;
			graphicComponent.transform.position = Vector3.Lerp(graphicComponent.transform.position, position, deltaTime);
		}



	}
}



