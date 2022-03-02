using System;
using UnityEngine;

namespace CsCat
{
	//弹道
	public class SourceTargetEffectComponent : EffectComponent
	{
		protected string sourceSocketName;
		protected string targetSocketName;

		public Vector3 sourcePosition;
		public Vector3 targetPosition;
		public IPosition sourceIPosition;
		public IPosition targetIPosition;
		public Vector3 currentPosition;
		public Vector3 currentEulerAngles;

		public Action onReachCallback;
		
		public void SetSocket()
		{
			this.sourceSocketName = this.effectEntity.cfgEffectData.socketName1 ?? "missile";
			this.targetSocketName = this.effectEntity.cfgEffectData.socketName2 ?? "chest";

			sourceIPosition?.SetSocketName(this.sourceSocketName);
			targetIPosition?.SetSocketName(this.targetSocketName);
		}




		// 计算sourcePosition,targetPosition,eulerAngles
		protected virtual void Calculate(float deltaTime)
		{
			this.sourcePosition = this.sourceIPosition.GetPosition();
			this.targetPosition = this.targetIPosition.GetPosition();
			this.currentPosition = this.sourcePosition;
			CalculateEulerAngles();
		}

		public void CalculateEulerAngles()
		{
			Vector3 diff = this.targetPosition - this.currentPosition;
			this.currentEulerAngles = diff.Equals(Vector3.zero) ? Vector3.zero : Quaternion.LookRotation(diff, Vector3.up).eulerAngles;
		}

		public virtual void OnEffectReach()
		{
			onReachCallback?.Invoke();
			this.effectEntity.OnEffectReach();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			Calculate(deltaTime);
			this.effectEntity.ApplyToTransformComponent(this.currentPosition, this.currentEulerAngles);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this.onReachCallback = null;
		}
	}
}


