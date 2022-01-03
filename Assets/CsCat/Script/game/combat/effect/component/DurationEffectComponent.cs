using System;

namespace CsCat
{
	public class DurationEffectComponent : EffectComponent
	{
		private float duration;
		private float remain_duration;

		public Action no_remain_duration_callback;

		public void Init(float duration)
		{
			base.Init();
			SetDuration(duration);
		}


		public void SetDuration(float duration)
		{
			this.duration = duration;
			this.remain_duration = duration;
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.remain_duration = this.remain_duration - deltaTime;
			if (this.remain_duration <= 0)
				OnNoRemainDuration();
		}

		protected virtual void OnNoRemainDuration()
		{
			effectEntity.OnNoRemainDuration();
			no_remain_duration_callback?.Invoke();
		}


	}
}



