using UnityEngine;

namespace CsCat
{
	public partial class CameraBase : TickObject
	{
		public Camera camera;

		private CameraOperation _currentOperation;
		private float _lerpSpeed = 4;

		private Vector3 _orgPosition;
		private Vector3 _orgEulerAngles;
		private Quaternion _orgRotation;
		private float _orgFOV;

		private Vector3 _currentPosition;
		private Vector3 _currentEulerAngles;
		private Quaternion _currentRotation;
		private float _currentFOV;


		public void Init(Camera camera, bool isNotDestroyGameObject)
		{
			base.Init();
			this.camera = camera;
			graphicComponent.SetGameObject(this.camera.gameObject, isNotDestroyGameObject);

			_orgPosition = graphicComponent.transform.position;
			_orgEulerAngles = graphicComponent.transform.eulerAngles;
			_orgRotation = graphicComponent.transform.rotation;
			_orgFOV = this.camera.fieldOfView;

			_currentPosition = graphicComponent.transform.position;
			_currentEulerAngles = graphicComponent.transform.eulerAngles;
			_currentRotation = graphicComponent.transform.rotation;
			_currentFOV = this.camera.fieldOfView;
		}

		public override bool IsCanUpdate()
		{
			return !this.isCanNotUpdate && base.IsCanUpdate();
		}

		protected override void _LateUpdate(float deltaTime, float unscaledDeltaTime)
		{
			base._LateUpdate(deltaTime, unscaledDeltaTime);
			switch (this._currentOperation)
			{
				case CameraOperation.None:
					graphicComponent.transform.position = this._currentPosition;
					break;
				case CameraOperation.Lock_To_Target:
					if (this._lockToTransform)
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

			this._currentPosition = graphicComponent.transform.position;
			this._currentRotation = graphicComponent.transform.rotation;
			this._currentFOV = this.camera.fieldOfView;

			ApplyShakeScreen(deltaTime);

			ApplyMoveRange(deltaTime);
		}
	}
}