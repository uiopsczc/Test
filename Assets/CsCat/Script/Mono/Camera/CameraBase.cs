using UnityEngine;

namespace CsCat
{
	public partial class CameraBase : TickObject
	{
		public Camera camera;

		private CameraOperation currentOperation;
		private float lerpSpeed = 4;

		private Vector3 orgPosition;
		private Vector3 orgEulerAngles;
		private Quaternion orgRotation;
		private float orgFOV;

		private Vector3 currentPosition;
		private Vector3 currentEulerAngles;
		private Quaternion currentRotation;
		private float currentFOV;


		public void Init(Camera camera, bool isNotDestroyGameObject)
		{
			base.Init();
			this.camera = camera;
			graphicComponent.SetGameObject(this.camera.gameObject, isNotDestroyGameObject);

			orgPosition = graphicComponent.transform.position;
			orgEulerAngles = graphicComponent.transform.eulerAngles;
			orgRotation = graphicComponent.transform.rotation;
			orgFOV = this.camera.fieldOfView;

			currentPosition = graphicComponent.transform.position;
			currentEulerAngles = graphicComponent.transform.eulerAngles;
			currentRotation = graphicComponent.transform.rotation;
			currentFOV = this.camera.fieldOfView;
		}

		public override bool IsCanUpdate()
		{
			return !this.isCanNotUpdate && base.IsCanUpdate();
		}

		protected override void _LateUpdate(float deltaTime, float unscaledDeltaTime)
		{
			base._LateUpdate(deltaTime, unscaledDeltaTime);
			switch (this.currentOperation)
			{
				case CameraOperation.None:
					graphicComponent.transform.position = this.currentPosition;
					break;
				case CameraOperation.Lock_To_Target:
					if (this.lockToTransform)
						this.ApplyLockTo(deltaTime);
					break;
				case CameraOperation.Delta_Move:
					this.ApplyMoveByDelta(deltaTime);
					break;
				case CameraOperation.Move_To_Target:
					this.ApplyMoveToTarget(deltaTime);
					break;
				default:
					break;
			}

			this.currentPosition = graphicComponent.transform.position;
			this.currentRotation = graphicComponent.transform.rotation;
			this.currentFOV = this.camera.fieldOfView;

			ApplyShakeScreen(deltaTime);

			ApplyMoveRange(deltaTime);
		}
	}
}