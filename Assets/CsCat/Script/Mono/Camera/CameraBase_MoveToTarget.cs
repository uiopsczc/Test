using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private Transform _moveToTargetTransform;
		private Vector3? _moveToTargetPosition;
		private Quaternion _moveToTargetRotation;

		private Vector3 _moveToTargetStartPosition;
		private Quaternion _moveToTargetStartRotation;

		private float _moveToTargetDuration;
		private float _moveToTargetCurrentTime;


		private bool _isReachNeedStop;

		public void SetMoveToTarget(Transform moveToTargetTransform, float moveToTargetDuration,
		  Vector3 moveToTargetEulerAngles, Vector3 moveToTargetLookPosition, bool isReachNeedStop = false)
		{
			this._moveToTargetTransform = moveToTargetTransform;
			SetMoveToTarget(this._moveToTargetTransform.position, moveToTargetDuration, moveToTargetEulerAngles,
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
			this._moveToTargetPosition = moveToTargetPosition;

			this._moveToTargetDuration = moveToTargetDuration;
			if (moveToTargetEulerAngles != null)
				this._moveToTargetRotation = Quaternion.Euler(moveToTargetEulerAngles.Value);
			if (moveToTargetLookPosition != null)
				this._moveToTargetRotation =
				  Quaternion.LookRotation(moveToTargetLookPosition.Value - moveToTargetPosition);

			this._isReachNeedStop = isReachNeedStop;

			this._moveToTargetStartPosition = this._currentPosition;
			this._moveToTargetStartRotation = this._currentRotation;

			_moveToTargetCurrentTime = 0;
		}

		public void ApplyMoveToTarget(float deltaTime)
		{
			if (_moveToTargetTransform != null)
				_moveToTargetPosition = _moveToTargetTransform.position;
			this._moveToTargetCurrentTime = this._moveToTargetCurrentTime + deltaTime;
			Vector3 position;
			Quaternion rotation;
			if (this._moveToTargetDuration == 0 || _moveToTargetCurrentTime >= this._moveToTargetDuration)
			{
				position = _moveToTargetPosition.Value;
				rotation = _moveToTargetStartRotation;
				if (this._isReachNeedStop)
					MoveToTargetReset();
			}
			else
			{
				float percent = _moveToTargetCurrentTime.GetPercent(0, _moveToTargetDuration);
				position = Vector3.Lerp(this._moveToTargetStartPosition, _moveToTargetPosition.Value, percent);
				rotation = Quaternion.Slerp(_moveToTargetStartRotation, _moveToTargetStartRotation, percent);
			}

			graphicComponent.transform.position = position;
			graphicComponent.transform.rotation = rotation;
		}

		public void MoveToTargetReset()
		{
			_currentOperation = CameraOperation.None;
			_moveToTargetPosition = null;
			_moveToTargetTransform = null;
			this._isReachNeedStop = false;
		}
	}
}




