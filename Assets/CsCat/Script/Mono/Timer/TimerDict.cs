using System;
using System.Collections.Generic;

namespace CsCat
{
	public class TimerDict
	{
		public TimerManager timerManager;
		public Dictionary<Timer, bool> dict = new Dictionary<Timer, bool>();

		public TimerDict(TimerManager timerManager)
		{
			this.timerManager = timerManager;
		}

		/// <summary>
		/// duration needRunCount 里面随便一个触碰底线都会结束
		/// </summary>
		/// <param name="func">返回false表示不继续执行，结束</param>
		/// <param name="interval">触发间隔  0 每帧都触发</param>
		/// <param name="needRunCount">触发次数 0:一直触发</param>
		/// <param name="duration">整个过程的时间 不会包含delay</param>
		/// <param name="delay">第一次的延迟时间</param>
		/// <param name="mode"></param>
		/// <param name="parent"></param>
		/// <param name="priority"></param>
		public Timer AddTimer(Func<object[], bool> updateFunc, float delay = 0, float interval = 0,
			int needRunCount = 0,
			UpdateModeCat updateMode = UpdateModeCat.Update, bool isUseUnscaledDeltaTime = false, int priority = 1,
			params object[] updateFuncArgs)
		{
			Timer timer = PoolCatManager.Default.SpawnValue<Timer>(null, null);
			timer.Init(updateFunc, delay, interval, needRunCount, updateMode, isUseUnscaledDeltaTime,
				priority, updateFuncArgs);
			return AddTimer(timer);
		}

		public Timer AddTimer(Timer timer)
		{
			timerManager.AddTimer(timer);
			dict[timer] = true;
			return timer;
		}

		public void RemoveTimer(Timer timer)
		{
			if (dict.TryGetValue(timer, out bool hasTimer))
			{
				timerManager.RemoveTimer(timer);
				dict.Remove(timer);
				PoolCatManager.Default.DespawnValue(timer);
			}
		}

		public void RemoveAllTimers()
		{
			foreach (var kv in dict)
			{
				var timer = kv.Key;
				timerManager.RemoveTimer(timer);
				PoolCatManager.Default.DespawnValue(timer);
			}
			dict.Clear();
		}

		public void SetIsPaused(bool isPaused)
		{
			foreach (var kv in dict)
			{
				var timer = kv.Key;
				timer.SetIsPaused(isPaused);
			}
		}


		public void Destroy()
		{
			RemoveAllTimers();
		}
	}
}