using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class TimerManagerPluginComponent : AbstractComponent
	{
		public TimerManagerPlugin timerManagerPlugin;
		public void Init(TimerManagerPlugin timerManagerPlugin)
		{
			base.Init();
			this.timerManagerPlugin = timerManagerPlugin;
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
		public Timer AddTimer(Func<object[], bool> updateFunc, float delay = 0, float interval = 0, int needRunCount = 0,
		  UpdateModeCat updateMode = UpdateModeCat.Update, bool isUseUnscaledDeltaTime = false, int priority = 1,
		  params object[] updateFuncArgs)
		{
			return this.timerManagerPlugin.AddTimer(updateFunc, delay, interval, needRunCount,
			  updateMode, isUseUnscaledDeltaTime, priority,
			  updateFuncArgs);
		}

		public Timer AddTimer(Timer timer)
		{
			return this.timerManagerPlugin.AddTimer(timer);
		}

		public Timer RemoveTimer(Timer timer)
		{
			return this.timerManagerPlugin.RemoveTimer(timer);
		}

		public void RemoveAllTimers()
		{
			this.timerManagerPlugin.RemoveAllTimers();
		}

		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			this.timerManagerPlugin.SetIsPaused(isPaused);
		}

		protected override void _Reset()
		{
			base._Reset();
			RemoveAllTimers();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			RemoveAllTimers();
		}


	}
}