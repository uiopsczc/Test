using UnityEngine;

namespace CsCat
{
	public partial class CameraBase : TickObject
	{
		public Camera camera;

		private CameraOperation current_operation;
		private float lerp_speed = 4;

		private Vector3 org_position;
		private Vector3 org_eulerAngles;
		private Quaternion org_rotation;
		private float org_fov;

		private Vector3 current_position;
		private Vector3 current_eulerAngles;
		private Quaternion current_rotation;
		private float current_fov;


		public void Init(Camera camera, bool is_not_destroy_gameObject)
		{
			base.Init();
			this.camera = camera;
			graphicComponent.SetGameObject(this.camera.gameObject, is_not_destroy_gameObject);

			org_position = graphicComponent.transform.position;
			org_eulerAngles = graphicComponent.transform.eulerAngles;
			org_rotation = graphicComponent.transform.rotation;
			org_fov = this.camera.fieldOfView;

			current_position = graphicComponent.transform.position;
			current_eulerAngles = graphicComponent.transform.eulerAngles;
			current_rotation = graphicComponent.transform.rotation;
			current_fov = this.camera.fieldOfView;
		}

		public override bool IsCanUpdate()
		{
			return !this.isCanNotUpdate && base.IsCanUpdate();
		}

		protected override void _LateUpdate(float deltaTime, float unscaledDeltaTime)
		{
			base._LateUpdate(deltaTime, unscaledDeltaTime);
			switch (this.current_operation)
			{
				case CameraOperation.None:
					graphicComponent.transform.position = this.current_position;
					break;
				case CameraOperation.Lock_To_Target:
					if (this.lock_to_transform)
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

			this.current_position = graphicComponent.transform.position;
			this.current_rotation = graphicComponent.transform.rotation;
			this.current_fov = this.camera.fieldOfView;

			ApplyShakeScreen(deltaTime);

			ApplyMoveRange(deltaTime);
		}
	}
}



