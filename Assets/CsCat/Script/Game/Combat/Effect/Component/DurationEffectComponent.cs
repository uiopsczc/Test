using System;

namespace CsCat
{
	public class DurationEffectComponent : EffectComponent
	{
		private float duration;
		private float remainDuration;

		public Action noRemainDurationCallback;

		public void Init(float duration)
		{
			base.Init();
			SetDuration(duration);
		}


		public void SetDuration(float duration)
		{
			this.duration = duration;
			this.remainDuration = duration;
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.remainDuration = this.remainDuration - deltaTime;
			if (this.remainDuration <= 0)
				OnNoRemainDuration();
		}

		protected virtual void OnNoRemainDuration()
		{
			effectEntity.OnNoRemainDuration();
			noRemainDurationCallback?.Invoke();
		}


	}
}



