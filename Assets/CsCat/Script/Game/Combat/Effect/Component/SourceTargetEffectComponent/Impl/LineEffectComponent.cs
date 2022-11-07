using UnityEngine;

namespace CsCat
{
	public class LineEffectComponent : SourceTargetEffectComponent
	{
		private float _speed;
		private float _accSpeed;
		private float _staySourceDuration;


		protected void _Init(IPosition sourceIPosition,
		  IPosition targetIPosition, float staySourceDuration = 0, float speed = 0,
		  float accSpeed = 0)
		{
			base._Init();
			this.sourceIPosition = sourceIPosition;
			this.targetIPosition = targetIPosition;
			SetSocket();
			this._speed = speed;
			this._accSpeed = accSpeed;
			this._staySourceDuration = staySourceDuration;

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}



		protected override void Calculate(float deltaTime)
		{
			this._staySourceDuration = this._staySourceDuration - deltaTime;

			if (this._staySourceDuration >= 0)
			{
				this.sourcePosition = this.sourceIPosition.GetPosition();
				this.targetPosition = this.targetIPosition.GetPosition();
				this.currentPosition = this.sourcePosition;
				CalculateEulerAngles();
				return;
			}

			this._speed += this._accSpeed;
			float remainDuration = Vector3.Distance(this.currentPosition, this.targetPosition) / this._speed;
			float pct = Mathf.Clamp01(deltaTime / remainDuration);
			this.currentPosition = Vector3.Lerp(this.currentPosition, this.targetPosition, pct);

			this.CalculateEulerAngles();
			if (pct == 1)
				OnEffectReach();
		}


	}
}



