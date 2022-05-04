using System;

namespace CsCat
{
	/// <summary>
	/// Timer，拥有类似hierarchy的结构，
	/// 有父timer    _parent
	/// 也有子孩子timers children
	/// </summary>
	public class Timer : IDeSpawn
	{
		#region field

		private bool _isUseUnscaledDeltaTime;

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
		private int _needRunCount;
		private long _curRunCount;
		private float _passedDuration;
		public TimerStatus preStatus; //之前的status
		private object[] _updateFuncArgs;

		#endregion

		#region property

		/// <summary>
		/// 第一次延迟多少秒运行
		/// </summary>
		public float delay
		{
			get => _delay;
			set => _delay = value;
		}

		/// <summary>
		/// 每隔多少秒运行一次
		/// </summary>
		public float interval
		{
			get => _interval;
			set => _interval = value;
		}

		/// <summary>
		/// 总共需要运行多少次
		/// </summary>
		public int needRunCount
		{
			get => _needRunCount;
			set => _needRunCount = value;
		}

		/// <summary>
		/// 当前运行次数
		/// </summary>
		public long curRunCount
		{
			get => _curRunCount;
			set => _curRunCount = value;
		}

		/// <summary>
		/// 是否已结束
		/// </summary>
		public bool isFinished => status == TimerStatus.Finished;

		/// <summary>
		/// 已经消逝的时间，从开始到现在的时间，包括delay的时间
		/// </summary>
		public float passedDuration
		{
			get => _passedDuration;
			set => _passedDuration = value;
		}

		/// <summary>
		/// 当前状态
		/// </summary>
		public TimerStatus status
		{
			get => this._status;
			private set => this._status = value;
		}

		/// <summary>
		/// timeScale
		/// </summary>
		public float timeScale
		{
			get => this._timeScale;
			set
			{
				float orgTimeScale = this._timeScale;
				if (orgTimeScale != value) return;
				this._timeScale = value;
				this.OnTimeScaleChange(orgTimeScale, value);
			}
		}

		/// <summary>
		/// Update函数
		/// 返回false表示不继续执行，结束
		/// </summary>
		public Func<object[], bool> updateFunc => this._updateFunc;

		public object[] updateFuncArgs => this._updateFuncArgs;

		/// <summary>
		/// UpdateModeCat
		/// </summary>
		public UpdateModeCat updateMode => this._updateMode;

		public bool isUseUnscaledDeltaTime => this._isUseUnscaledDeltaTime;

		#endregion

		#region delegate

		/// <summary>
		/// 当timeScale改变的时候的回调
		/// </summary>
		public Action<float, float> onTimeScaleChanged;

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
		/// <param name="needRunCount">触发次数 0:一直触发</param>
		/// <param name="duration">整个过程的时间 不会包含delay</param>
		/// <param name="delay">第一次的延迟时间</param>
		/// <param name="mode"></param>
		/// <param name="parent"></param>
		/// <param name="priority"></param>
		public void Init(Func<object[], bool> updateFunc, float delay = 0, float interval = 0, int needRunCount = 0,
			UpdateModeCat updateMode = UpdateModeCat.Update, bool isUseUnscaledDeltaTime = false, int priority = 1,
			params object[] updateFuncArgs)
		{
			this._updateFunc = updateFunc;
			this._updateMode = updateMode;
			this.priority = priority;
			this.interval = interval;
			this.needRunCount = needRunCount;
			this.delay = delay;
			this._updateFuncArgs = updateFuncArgs;
			this._isUseUnscaledDeltaTime = isUseUnscaledDeltaTime;
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
			float execTime = CalcExecTime();
			if (execTime <= 0)
				return 0;
			if (interval == 0)
				return ValidCurrentNeedRunCount(curRunCount + 1);
			//      return 1;
			long currentNeedRunCount = (long) Math.Floor(execTime / interval) + 1;
			return ValidCurrentNeedRunCount(currentNeedRunCount);
		}

		/// <summary>
		/// 当前执行的时间  不会包含delay 
		/// </summary>
		/// <returns></returns>
		public float CalcExecTime()
		{
			float execTime = this.passedDuration - this.delay;
			return execTime;
		}

		/// <summary>
		/// 暂停
		/// </summary>
		public void SetIsPaused(bool isPaused)
		{
			if (isPaused && this.status == TimerStatus.Paused)
				return;
			if (!isPaused && this.status != TimerStatus.Paused)
				return;
			if (isPaused)
				preStatus = this.status;
			this.status = isPaused ? TimerStatus.Paused : preStatus;
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
			float targetDeltaTime = (isUseUnscaledDeltaTime ? unscaledDeltaTime : deltaTime * timeScale);
			//      float exec_time = CalcExecTime(); //执行的时间，不包括delay的时间
			passedDuration += targetDeltaTime; //已经消逝的时间，从开始到现在的时间，包括delay的时间
			if (passedDuration < delay)
				return;

			//大过delay时间
			long currentNeedRunCount = CalcCurrentNeedRunCount(); //当前执行时间对应需要执行的次数
			int deltaCount = (int) (currentNeedRunCount - curRunCount); //需要增量执行的次数

			if (deltaCount > 0)
			{
				//        float valid_detalTime = CalcExecTime() - exec_time;
				//        float each_deltaTime = valid_detalTime / delta_count; //每次的deltaTime
				for (int i = 0; i < deltaCount; i++)
				{
					bool isNotFinished = this.updateFunc(this._updateFuncArgs);
					if (!isNotFinished)
					{
						Finish();
						return;
					}

					curRunCount++;
				}
			}

			if (needRunCount > 0 && needRunCount <= curRunCount)
				Finish();
		}

		#endregion

		#region protected method

		/// <summary>
		/// 当TimeScale改变
		/// </summary>
		protected void OnTimeScaleChange(float orgTimeScale, float curTimeScale)
		{
			onTimeScaleChanged?.Invoke(orgTimeScale, curTimeScale);
		}

		#endregion

		#region private method

		/// <summary>
		/// 检测当前执行的次数是否会越界
		/// </summary>
		/// <param name="currentNeedRunCount"></param>
		/// <returns></returns>
		private long ValidCurrentNeedRunCount(long currentNeedRunCount)
		{
			if (needRunCount > 0 && needRunCount < currentNeedRunCount)
				return needRunCount;
			return currentNeedRunCount;
		}

		#endregion

		public void OnDeSpawn()
		{
			_isUseUnscaledDeltaTime = false;
			priority = 1;
			_status = TimerStatus.Not_Started;
			_timeScale = 1;
			_updateMode = UpdateModeCat.Update;
			_delay = 0;
			_interval = 0;
			_needRunCount = 0;
			_curRunCount = 0;
			_passedDuration = 0;
			preStatus = TimerStatus.Not_Started;
			_updateFuncArgs = null;
			onTimeScaleChanged = null;
			_updateFunc = null;
		}
	}
}