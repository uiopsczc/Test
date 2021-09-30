using System;

namespace CsCat
{
  public partial class GameComponent
  {
	  protected TimerManagerPlugin timerManagerPlugin => cache.GetOrAddDefault(() => new TimerManagerPlugin(GetGameEntity().GetComponent<TimerManagerPluginComponent>().timerManagerPlugin.timerManager));

	  /// <summary>
    /// duration needRunCount 里面随便一个触碰底线都会结束
    /// </summary>
    /// <param name="func">返回false表示不继续执行，结束</param>
    /// <param name="interval">触发间隔  0 每帧都触发</param>
    /// <param name="need_run_count">触发次数 0:一直触发</param>
    /// <param name="duration">整个过程的时间 不会包含delay</param>
    /// <param name="delay">第一次的延迟时间</param>
    /// <param name="mode"></param>
    /// <param name="parent"></param>
    /// <param name="priority"></param>
    public Timer AddTimer(Func<object[], bool> updateFunc, float delay = 0, float interval = 0, int need_run_count = 0,
      UpdateModeCat updateMode = UpdateModeCat.Update, bool is_use_unscaledDeltaTime = false, int priority = 1,
      params object[] func_args)
    {
      return timerManagerPlugin.AddTimer(updateFunc, delay, interval, need_run_count, updateMode,
        is_use_unscaledDeltaTime, priority, func_args);
    }

    public Timer AddTimer(Timer timer)
    {
      return timerManagerPlugin.AddTimer(timer);
    }

    public Timer RemoveTimer(Timer timer)
    {
      return timerManagerPlugin.RemoveTimer(timer);
    }

    public void RemoveAllTimers()
    {
			if(this.cache.ContainsKey<TimerManagerPlugin>())
      timerManagerPlugin.RemoveAllTimers();
    }

    public void SetIsPaused_Timers(bool is_paused)
    {
      timerManagerPlugin.SetIsPaused(is_paused);
    }

  }
}