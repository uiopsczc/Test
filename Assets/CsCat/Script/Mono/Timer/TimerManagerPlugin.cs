using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class TimerManagerPlugin
  {
    public TimerManager timerManager;
    public Dictionary<Timer, bool> timer_dict = new Dictionary<Timer, bool>();

    public TimerManagerPlugin(TimerManager timerManager)
    {
      this.timerManager = timerManager;
    }

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
      Timer timer = new Timer(updateFunc, delay, interval, need_run_count, updateMode, is_use_unscaledDeltaTime,
        priority, func_args);
      return AddTimer(timer);
    }

    public Timer AddTimer(Timer timer)
    {
      timerManager.AddTimer(timer);
      timer_dict[timer] = true;
      return timer;
    }

    public Timer RemoveTimer(Timer timer)
    {
      if (!timer_dict.ContainsKey(timer))
        return null;
      timerManager.RemoveTimer(timer);
      timer_dict.Remove(timer);
      return timer;
    }

    public void RemoveAllTimers()
    {
      foreach (var timer in this.timer_dict.Keys)
        timerManager.RemoveTimer(timer);
      timer_dict.Clear();
    }

    public void SetIsPaused(bool is_paused)
    {
      foreach (var timer in this.timer_dict.Keys)
        timer.SetIsPaused(is_paused);
    }


    public void Destroy()
    {
      RemoveAllTimers();
    }

  }
}