using System;

namespace CsCat
{
	public class DurationEffectComponent : EffectComponent
	{
		private float _duration;
		private float _remainDuration;

		public Action noRemainDurationCallback;

		public void Init(float duration)
		{
			base.Init();
			SetDuration(duration);
		}


		public void SetDuration(float duration)
		{
			this._duration = duration;
			this._remainDuration = duration;
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this._remainDuration = this._remainDuration - deltaTime;
			if (this._remainDuration <= 0)
				OnNoRemainDuration();
		}

		protected virtual void OnNoRemainDuration()
		{
			effectEntity.OnNoRemainDuration();
			noRemainDurationCallback?.Invoke();
		}


	}
}



