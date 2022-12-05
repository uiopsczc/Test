using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class TimerDictTreeNode : TreeNode
	{
		private TimerDict _timerDict;
		protected void _Init(TimerDict timerDict)
		{
			base._Init();
			this._timerDict = timerDict;
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
			return this._timerDict.AddTimer(updateFunc, delay, interval, needRunCount,
			  updateMode, isUseUnscaledDeltaTime, priority,
			  updateFuncArgs);
		}

		public Timer AddTimer(Timer timer)
		{
			return this._timerDict.AddTimer(timer);
		}

		public void RemoveTimer(Timer timer)
		{
			this._timerDict.RemoveTimer(timer);
		}

		public void RemoveAllTimers()
		{
			this._timerDict.RemoveAllTimers();
		}

		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			this._timerDict.SetIsPaused(isPaused);
		}

		protected override void _Reset()
		{
			RemoveAllTimers();
			base._Reset();
		}

		protected override void _Destroy()
		{
			RemoveAllTimers();
			base._Destroy();
		}


	}
}