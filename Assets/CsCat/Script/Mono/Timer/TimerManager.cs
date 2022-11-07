using System;
using System.Collections.Generic;

namespace CsCat
{
	public class TimerManager
	{
		#region field

		/// <summary>
		/// update的timer
		/// </summary>
		public List<Timer> updateTimerList = new List<Timer>();

		/// <summary>
		/// lateUpdate的timer
		/// </summary>
		public List<Timer> lateUpdateTimerList = new List<Timer>();

		/// <summary>
		/// fixedUpdate的timer
		/// </summary>
		public List<Timer> fixedUpdateTimerList = new List<Timer>();

		private bool isUpdating;

		#endregion


		#region public method

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
			AddTimer(timer);
			return timer;
		}

		private List<Timer> GetTimerList(UpdateModeCat updateMode)
		{
			switch (updateMode)
			{
				case UpdateModeCat.Update:
					return this.updateTimerList;
				case UpdateModeCat.LateUpdate:
					return this.lateUpdateTimerList;
				case UpdateModeCat.FixedUpdate:
					return this.fixedUpdateTimerList;
				default:
					return null;
			}
		}


		/// <summary>
		/// 添加到对应的TimerList中
		/// </summary>
		/// <param name="timer"></param>
		public void AddTimer(Timer timer)
		{
			this.Add(this.GetTimerList(timer.updateMode), timer);
		}

		/// <summary>
		/// 添加到对应的TimerList中
		/// </summary>
		/// <param name="timer"></param>
		public void RemoveTimer(Timer timer, int? index = null)
		{
			List<Timer> timerList = this.GetTimerList(timer.updateMode);
			if (index != null)
				timerList.RemoveAt(index.Value);
			else
				timerList.Remove(timer);

			timer.Finish();
			PoolCatManager.Default.DespawnValue(timer);
		}

		#endregion

		#region private method

		/// <summary>
		/// 添加timer
		/// </summary>
		/// <param name="timerList"></param>
		/// <param name="timer"></param>
		private void Add(List<Timer> timerList, Timer timer)
		{
			bool isTimerExist = false;
			int index = timerList.Count;
			for (int i = 0; i < timerList.Count; i++)
			{
				Timer curTimer = timerList[i];
				if (!isTimerExist)
					isTimerExist = (curTimer == timer);

				if (timer.priority > curTimer.priority)
					index = i - 1;
			}

			if (!isTimerExist)
			{
				timerList.Insert(index, timer);
				timer.Start();
			}
		}


		/// <summary>
		/// 1.添加toaddTimerList的timer到对应的TimerList中
		/// 2.将timerList中是IsFinished的Timer移除
		/// 3.执行timer.DoUpdate的updateFunc
		/// </summary>
		/// <param name="timerList"></param>
		/// <param name="isFixed"></param>
		private void DoUpdate(List<Timer> timerList, float deltaTime, float unscaledDeltaTime)
		{
			if (timerList.Count <= 0)
				return;

			this.isUpdating = true;

			int count = timerList.Count;
			for (int j = 0; j < count; j++)
			{
				Timer timer = timerList[j];
				if (timer.isFinished) //如果该timer的状态是结束的话，从timerList中移除
					continue;
				timer.DoUpdate(deltaTime, unscaledDeltaTime);
			}

			// check remove
			for (int j = timerList.Count - 1; j >= 0; j--)
			{
				Timer timer = timerList[j];
				if (timer.isFinished)
					this.RemoveTimer(timer, j);
			}

			this.isUpdating = false;
		}

		#endregion


		public void Update(float deltaTime, float unscaledDeltaTime)
		{
			DoUpdate(this.updateTimerList, deltaTime, unscaledDeltaTime);
		}


		public void FixedUpdate(float deltaTime, float unscaledDeltaTime)
		{
			DoUpdate(this.fixedUpdateTimerList, deltaTime, unscaledDeltaTime);
		}

		public void LateUpdate(float deltaTime, float unscaledDeltaTime)
		{
			DoUpdate(this.lateUpdateTimerList, deltaTime, unscaledDeltaTime);
		}
	}
}