using System;

namespace CsCat
{
  /// <summary>
  /// Timer，拥有类似hierarchy的结构，
  /// 有父timer    _parent
  /// 也有子孩子timers children
  /// </summary>
  public class Timer: IDespawn
  {
    #region field
    private bool _is_use_unscaledDeltaTime;
    /// <summary>
    /// 该timer的顺序
    /// </summary>
    internal int priority;
    /// <summary>
    /// 该timer的状态
    /// </summary>
    private TimerStatus _status;
    /// <summary>
    ///timeScale
    /// </summary>
    private float _timeScale = 1;
    /// <summary>
    /// update模式
    /// </summary>
    private UpdateModeCat _updateMode;
    private float _delay;
    private float _interval;
    private int _need_run_count;
    private long _cur_run_count;
    private float _passed_time;
    public TimerStatus status_pre; //之前的status
    private object[] _updateFunc_args;
    #endregion

    #region property

    /// <summary>
    /// 第一次延迟多少秒运行
    /// </summary>
    public float delay { get=>_delay; set=>_delay = value; }
    /// <summary>
    /// 每隔多少秒运行一次
    /// </summary>
    public float interval { get=> _interval; set=> _interval=value; }
    /// <summary>
    /// 总共需要运行多少次
    /// </summary>
    public int need_run_count { get=> _need_run_count; set=> _need_run_count= value; }
    /// <summary>
    /// 当前运行次数
    /// </summary>
    public long cur_run_count { get=> _cur_run_count; set=> _cur_run_count = value; }
    /// <summary>
    /// 是否已结束
    /// </summary>
    public bool is_finished => status == TimerStatus.Finished;

    /// <summary>
    /// 已经消逝的时间，从开始到现在的时间，包括delay的时间
    /// </summary>
    public float passed_time { get=> _passed_time; set=> _passed_time=value; }
    /// <summary>
    /// 当前状态
    /// </summary>
    public TimerStatus status { get => this._status; private set => this._status = value; }
    /// <summary>
    /// timeScale
    /// </summary>
    public float timeScale
    {
      get => this._timeScale;
      set
      {
        float org_timeScale = this._timeScale;
        if (org_timeScale != value) return;
        this._timeScale = value;
        this.OnTimeScaleChange(org_timeScale, value);
      }
    }

    /// <summary>
    /// Update函数
    /// 返回false表示不继续执行，结束
    /// </summary>
    public Func<object[], bool> updateFunc => this._updateFunc;

    public object[] updateFunc_args => this._updateFunc_args;

    /// <summary>
    /// UpdateModeCat
    /// </summary>
    public UpdateModeCat updateMode => this._updateMode;

    public bool is_use_unscaledDeltaTime => this._is_use_unscaledDeltaTime;

    #endregion

    #region delegate
    /// <summary>
    /// 当timeScale改变的时候的回调
    /// </summary>
    public Action<float, float> on_timeScale_changed;
    /// <summary>
    /// update回调
    /// 返回false表示不继续执行，结束
    /// </summary>
    private Func<object[], bool> _updateFunc;
    #endregion





    #region ctor

    public Timer()
    {
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
    public void Init(Func<object[], bool> updateFunc, float delay = 0, float interval = 0, int need_run_count = 0,
      UpdateModeCat updateMode = UpdateModeCat.Update, bool is_use_unscaledDeltaTime = false, int priority = 1,
      params object[] updateFunc_args)
    {
      this._updateFunc = updateFunc;
      this._updateMode = updateMode;
      this.priority = priority;
      this.interval = interval;
      this.need_run_count = need_run_count;
      this.delay = delay;
      this._updateFunc_args = updateFunc_args;
      this._is_use_unscaledDeltaTime = is_use_unscaledDeltaTime;
    }

    public void Start()
    {
      this._status = TimerStatus.Running;
    }

    #endregion

    #region public method

    /// <summary>
    /// 当前执行时间对应需要执行的次数
    /// </summary>
    /// <returns></returns>
    public long CalcCurrentNeedRunCount()
    {
      float exec_time = CalcExecTime();
      if (exec_time <= 0)
        return 0;
      if (interval == 0)
        return ValidCurrentNeedRunCount(cur_run_count + 1);
      //      return 1;
      long current_need_run_count = (long)Math.Floor(exec_time / interval) + 1;
      return ValidCurrentNeedRunCount(current_need_run_count);

    }

    /// <summary>
    /// 当前执行的时间  不会包含delay 
    /// </summary>
    /// <returns></returns>
    public float CalcExecTime()
    {
      float exec_time = this.passed_time - this.delay;
      return exec_time;
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void SetIsPaused(bool is_paused)
    {
      if (is_paused && this.status == TimerStatus.Paused)
        return;
      if (!is_paused && this.status != TimerStatus.Paused)
        return;
      if (is_paused)
        status_pre = this.status;
      this.status = is_paused ? TimerStatus.Paused : status_pre;
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
      Finish();
    }

    public void Finish()
    {
      this.status = TimerStatus.Finished;
    }

    #endregion

    #region internal method

    /// <summary>
    /// update
    /// </summary>
    /// <param name="deltaTime"></param>
    internal void DoUpdate(float deltaTime, float unscaledDeltaTime)
    {
      if (status != TimerStatus.Running)
        return;
      //运行状态
      float target_deltaTime = (is_use_unscaledDeltaTime ? unscaledDeltaTime : deltaTime * timeScale);
//      float exec_time = CalcExecTime(); //执行的时间，不包括delay的时间
      passed_time += target_deltaTime; //已经消逝的时间，从开始到现在的时间，包括delay的时间
      if (passed_time < delay)
        return;

      //大过delay时间
      long current_need_run_count = CalcCurrentNeedRunCount(); //当前执行时间对应需要执行的次数
      int delta_count = (int)(current_need_run_count - cur_run_count); //需要增量执行的次数

      if (delta_count > 0)
      {
//        float valid_detalTime = CalcExecTime() - exec_time;
//        float each_deltaTime = valid_detalTime / delta_count; //每次的deltaTime
        for (int i = 0; i < delta_count; i++)
        {
          bool is_not_finished = this.updateFunc(this._updateFunc_args);
          if (!is_not_finished)
          {
            Finish();
            return;
          }

          cur_run_count++;
        }
      }

      if (need_run_count > 0 && need_run_count <= cur_run_count)
        Finish();
    }

    #endregion

    #region protected method

    /// <summary>
    /// 当TimeScale改变
    /// </summary>
    protected void OnTimeScaleChange(float org_timeScale, float cur_timeScale)
    {
      on_timeScale_changed?.Invoke(org_timeScale, cur_timeScale);
    }

    #endregion

    #region private method

    /// <summary>
    /// 检测当前执行的次数是否会越界
    /// </summary>
    /// <param name="current_need_run_count"></param>
    /// <returns></returns>
    private long ValidCurrentNeedRunCount(long current_need_run_count)
    {
      if (need_run_count > 0 && need_run_count < current_need_run_count)
        return need_run_count;
      return current_need_run_count;
    }

    #endregion

    public void OnDespawn()
    {
      _is_use_unscaledDeltaTime = false;
      priority = 1;
      _status = TimerStatus.Not_Started;
      _timeScale = 1;
      _updateMode = UpdateModeCat.Update;
      _delay = 0;
      _interval = 0;
      _need_run_count = 0;
      _cur_run_count = 0;
      _passed_time = 0;
      status_pre = TimerStatus.Not_Started;
      _updateFunc_args = null;
      on_timeScale_changed = null;
      _updateFunc = null;
    }
  }
}
