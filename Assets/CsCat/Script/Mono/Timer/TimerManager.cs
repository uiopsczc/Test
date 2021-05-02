using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CsCat
{
  public class TimerManager
  {
    #region field

    /// <summary>
    /// update的timer
    /// </summary>
    public List<Timer> update_timer_list = new List<Timer>();

    /// <summary>
    /// lateUpdate的timer
    /// </summary>
    public List<Timer> lateUpdate_timer_list = new List<Timer>();

    /// <summary>
    /// fixedUpdate的timer
    /// </summary>
    public List<Timer> fixedUpdate_timer_list = new List<Timer>();

    private bool is_updating;

    #endregion

    #region property

    #endregion


    #region public method

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
      params object[] updateFunc_args)
    {
      Timer timer = PoolCatManagerUtil.Spawn<Timer>();
      timer.Init(updateFunc, delay, interval, need_run_count, updateMode, is_use_unscaledDeltaTime,
        priority, updateFunc_args);
      AddTimer(timer);
      return timer;
    }

    private List<Timer> GetTimerList(UpdateModeCat updateMode)
    {
      switch (updateMode)
      {
        case UpdateModeCat.Update:
          return this.update_timer_list;
        case UpdateModeCat.LateUpdate:
          return this.lateUpdate_timer_list;
        case UpdateModeCat.FixedUpdate:
          return this.fixedUpdate_timer_list;
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
      List<Timer> timer_list = this.GetTimerList(timer.updateMode);
      if (index != null)
        timer_list.RemoveAt(index.Value);
      else
        timer_list.Remove(timer);

      timer.Finish();
      PoolCatManagerUtil.Despawn(timer);
    }

    #endregion

    #region private method

    /// <summary>
    /// 添加timer
    /// </summary>
    /// <param name="timer_list"></param>
    /// <param name="timer"></param>
    private void Add(List<Timer> timer_list, Timer timer)
    {
      bool is_timer_exist = false;
      int index = timer_list.Count;
      for (int i = 0; i < timer_list.Count; i++)
      {
        Timer _timer = timer_list[i];
        if (!is_timer_exist)
        {
          is_timer_exist = (_timer == timer);
        }

        if (timer.priority > _timer.priority)
        {
          index = i - 1;
        }
      }

      if (!is_timer_exist)
      {
        timer_list.Insert(index, timer);
        timer.Start();
      }
    }
    

    /// <summary>
    /// 1.添加toaddTimerList的timer到对应的TimerList中
    /// 2.将timerList中是IsFinished的Timer移除
    /// 3.执行timer.DoUpdate的updateFunc
    /// </summary>
    /// <param name="timer_list"></param>
    /// <param name="isFixed"></param>
    private void DoUpdate(List<Timer> timer_list, float deltaTime, float unscaledDeltaTime)
    {
      if (timer_list.Count <= 0)
      {
        return;
      }

      this.is_updating = true;

      int count = timer_list.Count;
      for (int j = 0; j < count; j++)
      {
        Timer timer = timer_list[j];
        if (timer.is_finished) //如果该timer的状态是结束的话，从timerList中移除
          continue;
        timer.DoUpdate(deltaTime, unscaledDeltaTime);
      }

      // check remove
      for (int j = timer_list.Count - 1; j >= 0; j--)
      {
        Timer timer = timer_list[j];
        if (timer.is_finished)
          this.RemoveTimer(timer,j);
      }

      this.is_updating = false;
    }

    #endregion




    public void Update(float deltaTime, float unscaledDeltaTime)
    {
      DoUpdate(this.update_timer_list, deltaTime, unscaledDeltaTime);
    }


    public void FixedUpdate(float deltaTime, float unscaledDeltaTime)
    {
      DoUpdate(this.fixedUpdate_timer_list, deltaTime, unscaledDeltaTime);
    }

    public void LateUpdate(float deltaTime, float unscaledDeltaTime)
    {
      DoUpdate(this.lateUpdate_timer_list, deltaTime, unscaledDeltaTime);
    }



  }
}