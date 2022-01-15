using UnityEngine;

namespace CsCat
{
	public class LineEffectComponent : SourceTargetEffectComponent
	{
		private float speed;
		private float accSpeed;
		private float staySourceDuration;


		public void Init(IPosition sourceIPosition,
		  IPosition targetIPosition, float staySourceDuration = 0, float speed = 0,
		  float accSpeed = 0)
		{
			base.Init();
			this.sourceIPosition = sourceIPosition;
			this.targetIPosition = targetIPosition;
			SetSocket();
			this.speed = speed;
			this.accSpeed = accSpeed;
			this.staySourceDuration = staySourceDuration;

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}



		protected override void Calculate(float deltaTime)
		{
			this.staySourceDuration = this.staySourceDuration - deltaTime;

			if (this.staySourceDuration >= 0)
			{
				this.sourcePosition = this.sourceIPosition.GetPosition();
				this.targetPosition = this.targetIPosition.GetPosition();
				this.currentPosition = this.sourcePosition;
				CalculateEulerAngles();
				return;
			}

			this.speed += this.accSpeed;
			float remainDuration = Vector3.Distance(this.currentPosition, this.targetPosition) / this.speed;
			float pct = Mathf.Clamp01(deltaTime / remainDuration);
			this.currentPosition = Vector3.Lerp(this.currentPosition, this.targetPosition, pct);

			this.CalculateEulerAngles();
			if (pct == 1)
				OnEffectReach();
		}


	}
}



