using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private Transform moveToTargetTransform;
		private Vector3? moveToTargetPosition;
		private Quaternion moveToTargetRotation;

		private Vector3 moveToTargetStartPosition;
		private Quaternion moveToTargetStartRotation;

		private float moveToTargetDuration;
		private float moveToTargetCurrentTime;


		private bool isReachNeedStop;

		public void SetMoveToTarget(Transform moveToTargetTransform, float moveToTargetDuration,
		  Vector3 moveToTargetEulerAngles, Vector3 moveToTargetLookPosition, bool isReachNeedStop = false)
		{
			this.moveToTargetTransform = moveToTargetTransform;
			SetMoveToTarget(this.moveToTargetTransform.position, moveToTargetDuration, moveToTargetEulerAngles,
			  moveToTargetLookPosition, isReachNeedStop);
		}

		public void SetMoveToTarget(GameObject moveToGameObject, float moveToTargetDuration,
		  Vector3 moveToTargetEulerAngles, Vector3 moveToTargetLookPosition, bool isReachNeedStop = false)
		{
			SetMoveToTarget(moveToGameObject.transform, moveToTargetDuration, moveToTargetEulerAngles,
			  moveToTargetLookPosition, isReachNeedStop);
		}

		public void SetMoveToTarget(Vector3 moveToTargetPosition, float moveToTargetDuration,
		  Vector3? moveToTargetEulerAngles, Vector3? moveToTargetLookPosition, bool isReachNeedStop = false)

		{
			this.moveToTargetPosition = moveToTargetPosition;

			this.moveToTargetDuration = moveToTargetDuration;
			if (moveToTargetEulerAngles != null)
				this.moveToTargetRotation = Quaternion.Euler(moveToTargetEulerAngles.Value);
			if (moveToTargetLookPosition != null)
				this.moveToTargetRotation =
				  Quaternion.LookRotation(moveToTargetLookPosition.Value - moveToTargetPosition);

			this.isReachNeedStop = isReachNeedStop;

			this.moveToTargetStartPosition = this.currentPosition;
			this.moveToTargetStartRotation = this.currentRotation;

			moveToTargetCurrentTime = 0;
		}

		public void ApplyMoveToTarget(float deltaTime)
		{
			if (moveToTargetTransform != null)
				moveToTargetPosition = moveToTargetTransform.position;
			this.moveToTargetCurrentTime = this.moveToTargetCurrentTime + deltaTime;
			Vector3 position;
			Quaternion rotation;
			if (this.moveToTargetDuration == 0 || moveToTargetCurrentTime >= this.moveToTargetDuration)
			{
				position = moveToTargetPosition.Value;
				rotation = moveToTargetStartRotation;
				if (this.isReachNeedStop)
					MoveToTargetReset();
			}
			else
			{
				float percent = moveToTargetCurrentTime.GetPercent(0, moveToTargetDuration);
				position = Vector3.Lerp(this.moveToTargetStartPosition, moveToTargetPosition.Value, percent);
				rotation = Quaternion.Slerp(moveToTargetStartRotation, moveToTargetStartRotation, percent);
			}

			graphicComponent.transform.position = position;
			graphicComponent.transform.rotation = rotation;
		}

		public void MoveToTargetReset()
		{
			currentOperation = CameraOperation.None;
			moveToTargetPosition = null;
			moveToTargetTransform = null;
			this.isReachNeedStop = false;
		}
	}
}




