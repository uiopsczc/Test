using System;

namespace CsCat
{
	public partial class GameEntity
	{
		public virtual TimerManager timerManager => _cache.GetOrAddDefault(() => this.GetParent<GameEntity>().timerManager);

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
			params object[] funcArgs)
		{
			return this.timerManagerPluginComponent.AddTimer(updateFunc, delay, interval, needRunCount, updateMode,
				isUseUnscaledDeltaTime, priority, funcArgs);
		}

		public Timer AddTimer(Timer timer)
		{
			return this.timerManagerPluginComponent.AddTimer(timer);
		}

		public Timer RemoveTimer(Timer timer)
		{
			return this.timerManagerPluginComponent.RemoveTimer(timer);
		}

		public void RemoveAllTimers()
		{
			this.timerManagerPluginComponent.RemoveAllTimers();
		}
	}
}