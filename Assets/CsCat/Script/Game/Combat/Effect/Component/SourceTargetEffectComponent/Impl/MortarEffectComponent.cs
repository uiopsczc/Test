using UnityEngine;

namespace CsCat
{
	//迫击炮弹道
	public class MortarEffectComponent : SourceTargetEffectComponent
	{
		private float _allDuration;
		private float _startAngle;
		private Vector3 _direction;
		private Vector3 _gravity;
		private float _height;
		private float _remainDuration;
		private Vector3 _startPosition;
		private Vector3 _velocity;
		private float _vertical;

		protected void _Init(IPosition sourceIPosition,
		  IPosition targetIPosition
		  , Vector3 gravity, float startAngle)
		{
			base._Init();
			this.sourceIPosition = sourceIPosition;
			this.targetIPosition = targetIPosition;
			SetSocket();
			this._gravity = gravity;
			this._startAngle = startAngle;

			_InitFields();

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}

		void _InitFields()
		{
			this.sourcePosition = sourceIPosition.GetPosition();
			this.targetPosition = targetIPosition.GetPosition();
			this.currentEulerAngles = Quaternion.LookRotation(targetPosition - sourcePosition, Vector3.up).eulerAngles;
			var targetPositionXZ = targetPosition.SetZeroY();
			var sourcePositionXZ = sourcePosition.SetZeroY();
			var distance = Vector3.Distance(targetPositionXZ, sourcePositionXZ);
			var rad = Mathf.Atan2(_startAngle, distance);
			var dirHorizon = (targetPositionXZ - sourcePositionXZ).normalized / Mathf.Tan(rad);
			var dir = dirHorizon + new Vector3(0, 1, 0);
			var gravityY = Mathf.Abs(this._gravity.y);
			var height = sourcePosition.y - targetPosition.y;
			var rate = Mathf.Tan(rad) * gravityY * distance /
					   Mathf.Sqrt(2 * gravityY * (height + distance * Mathf.Tan(rad)));

			this._velocity = dir * rate;
			this._remainDuration = distance / (dirHorizon.magnitude * rate);
			this._allDuration = _remainDuration;
			this._startPosition = sourcePosition;
			this._vertical = rate;
			this._direction = _velocity;
			this._height = _startPosition.y;

			this.currentPosition = _startPosition;
		}


		protected override void Calculate(float deltaTime)
		{
			_remainDuration = _remainDuration - deltaTime;
			if (_remainDuration <= 0)
			{
				OnEffectReach();
				return;
			}
			_direction = _direction + _gravity * deltaTime;
			this.currentEulerAngles = Quaternion.LookRotation(_direction).eulerAngles;
			var passDuration = _allDuration - _remainDuration;
			var interp = _remainDuration / _allDuration;
			var newPosition = _startPosition * interp + targetPosition * (1 - interp);
			var height = this._height + _vertical * passDuration + _gravity.y * passDuration * passDuration * 0.5f;
			newPosition.y = height;
			this.currentPosition = newPosition;
		}
	}
}