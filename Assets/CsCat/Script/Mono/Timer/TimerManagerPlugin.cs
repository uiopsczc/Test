using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class TimerManagerPlugin
	{
		public TimerManager timerManager;
		public Dictionary<Timer, bool> dict = new Dictionary<Timer, bool>();

		public TimerManagerPlugin(TimerManager timerManager)
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
			Timer timer = PoolCatManagerUtil.Spawn<Timer>();
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

		public Timer RemoveTimer(Timer timer)
		{
			if (!dict.ContainsKey(timer))
				return null;
			timerManager.RemoveTimer(timer);
			dict.Remove(timer);
			return timer;
		}

		public void RemoveAllTimers()
		{
			foreach (var timer in dict.Keys)
				timerManager.RemoveTimer(timer);
			dict.Clear();
		}

		public void SetIsPaused(bool isPaused)
		{
			foreach (var timer in dict.Keys)
				timer.SetIsPaused(isPaused);
		}


		public void Destroy()
		{
			RemoveAllTimers();
		}
	}
}