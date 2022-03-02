using UnityEngine;

namespace CsCat
{
	//电弧
	public class SpinLineEffectComponent : SourceTargetEffectComponent
	{
		private float _spinSpeed;
		private Vector3 _forwardDir;
		private float _forwardSpeed;
		private Vector3 _startPosition;
		private Vector3 _spinDir;
		private float _startSpinAngle;
		private float _spinLength;
		private float _elapsedDuration;

		public void Init(Vector3 startPosition, Vector3 spinDir, float startSpinAngle,
		  float spinSpeed, float spinLength, Vector3 forwardDir, float forwardSpeed = 0)
		{
			base.Init();
			this._spinSpeed = spinSpeed;
			this._forwardDir = forwardDir;
			this._forwardSpeed = forwardSpeed;
			this._spinDir = spinDir;
			this._startPosition = startPosition;
			this._startSpinAngle = startSpinAngle;
			this._spinLength = spinLength;
			this._elapsedDuration = 0;

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}

		protected override void Calculate(float deltaTime)
		{
			this._elapsedDuration = this._elapsedDuration + deltaTime;
			Vector3 forwardDistance = this._forwardSpeed * this._elapsedDuration * _forwardDir;
			Vector3 arcDir = Quaternion.AngleAxis(this._startSpinAngle + this._spinSpeed * this._elapsedDuration, Vector3.up) *
							  this._spinDir; //电弧当前朝向
			this.currentPosition = this._startPosition + forwardDistance + arcDir * this._spinLength; // 电弧当前结束位置。
			this.currentEulerAngles = Vector3.zero;
		}
	}
}



