using UnityEngine;

namespace CsCat
{
	//迫击炮弹道
	public class MortarEffectComponent : SourceTargetEffectComponent
	{
		private float allDuration;
		private float startAngle;
		private Vector3 direction;
		private Vector3 gravity;
		private float height;
		private float remainDuration;
		private Vector3 startPosition;
		private Vector3 velocity;
		private float vertical;

		public void Init(IPosition sourceIPosition,
		  IPosition targetIPosition
		  , Vector3 gravity, float startAngle)
		{
			base.Init();
			this.sourceIPosition = sourceIPosition;
			this.targetIPosition = targetIPosition;
			SetSocket();
			this.gravity = gravity;
			this.startAngle = startAngle;

			__InitFields();

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}

		void __InitFields()
		{
			this.sourcePosition = sourceIPosition.GetPosition();
			this.targetPosition = targetIPosition.GetPosition();
			this.currentEulerAngles = Quaternion.LookRotation(targetPosition - sourcePosition, Vector3.up).eulerAngles;
			var targetPositionXZ = targetPosition.SetZeroY();
			var sourcePositionXZ = sourcePosition.SetZeroY();
			var distance = Vector3.Distance(targetPositionXZ, sourcePositionXZ);
			var rad = Mathf.Atan2(startAngle, distance);
			var dirHorizon = (targetPositionXZ - sourcePositionXZ).normalized / Mathf.Tan(rad);
			var dir = dirHorizon + new Vector3(0, 1, 0);
			var gravityY = Mathf.Abs(this.gravity.y);
			var height = sourcePosition.y - targetPosition.y;
			var rate = Mathf.Tan(rad) * gravityY * distance /
					   Mathf.Sqrt(2 * gravityY * (height + distance * Mathf.Tan(rad)));

			this.velocity = dir * rate;
			this.remainDuration = distance / (dirHorizon.magnitude * rate);
			this.allDuration = remainDuration;
			this.startPosition = sourcePosition;
			this.vertical = rate;
			this.direction = velocity;
			this.height = startPosition.y;

			this.currentPosition = startPosition;
		}


		protected override void Calculate(float deltaTime)
		{
			remainDuration = remainDuration - deltaTime;
			if (remainDuration <= 0)
			{
				OnEffectReach();
				return;
			}
			direction = direction + gravity * deltaTime;
			this.currentEulerAngles = Quaternion.LookRotation(direction).eulerAngles;
			var passDuration = allDuration - remainDuration;
			var interp = remainDuration / allDuration;
			var newPosition = startPosition * interp + targetPosition * (1 - interp);
			var height = this.height + vertical * passDuration + gravity.y * passDuration * passDuration * 0.5f;
			newPosition.y = height;
			this.currentPosition = newPosition;
		}
	}
}