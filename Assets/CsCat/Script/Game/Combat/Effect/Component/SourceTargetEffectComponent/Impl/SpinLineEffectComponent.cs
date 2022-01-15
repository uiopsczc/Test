using UnityEngine;

namespace CsCat
{
	//电弧
	public class SpinLineEffectComponent : SourceTargetEffectComponent
	{
		private float spinSpeed;
		private Vector3 forwardDir;
		private float forwardSpeed;
		private Vector3 startPosition;
		private Vector3 spinDir;
		private float startSpinAngle;
		private float spinLength;
		private float elapsedDuration;

		public void Init(Vector3 startPosition, Vector3 spinDir, float startSpinAngle,
		  float spinSpeed, float spinLength, Vector3 forwardDir, float forwardSpeed = 0)
		{
			base.Init();
			this.spinSpeed = spinSpeed;
			this.forwardDir = forwardDir;
			this.forwardSpeed = forwardSpeed;
			this.spinDir = spinDir;
			this.startPosition = startPosition;
			this.startSpinAngle = startSpinAngle;
			this.spinLength = spinLength;
			this.elapsedDuration = 0;

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}

		protected override void Calculate(float deltaTime)
		{
			this.elapsedDuration = this.elapsedDuration + deltaTime;
			Vector3 forwardDistance = this.forwardSpeed * this.elapsedDuration * forwardDir;
			Vector3 arcDir = Quaternion.AngleAxis(this.startSpinAngle + this.spinSpeed * this.elapsedDuration, Vector3.up) *
							  this.spinDir; //电弧当前朝向
			this.currentPosition = this.startPosition + forwardDistance + arcDir * this.spinLength; // 电弧当前结束位置。
			this.currentEulerAngles = Vector3.zero;
		}
	}
}



