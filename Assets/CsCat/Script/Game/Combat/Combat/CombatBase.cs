using System;
using System.Collections;

namespace CsCat
{
	public partial class CombatBase : TickObject
	{
		private bool _isStarted;
		private bool _isFinished;
		private Hashtable _argDict;
		private bool _isFixedDurationUpdate = true;
		private float _fixedUpdateRemainDuration;

		public float time;
		public int frame;
		public GameLevelBase gameLevel;
		public EffectManager effectManager;
		public CameraManager cameraManager;
		public UnitManager unitManager;
		public PathManager pathManager;
		public SpellManager spellManager;
		public RandomManager randomManager = new RandomManager();

		public override TimerManager timerManager => cache.GetOrAddDefault(() => new TimerManager());

		public void Init(Hashtable arg_dict)
		{
			base.Init();
			this._argDict = arg_dict;

			randomManager.SetSeed(this._argDict.GetOrGetDefault2("randomSeed", () => (int)DateTime.Now.Ticks));
			effectManager = AddChild<EffectManager>("EffectManager");
			cameraManager = AddChild<CameraManager>("CameraManager");
			unitManager = AddChild<UnitManager>("UnitManager");
			pathManager = AddChild<PathManager>("PathManager");
			spellManager = AddChild<SpellManager>("SpellManager");
		}

		public override void Start()
		{
			base.Start();
			LogCat.log("=============== Combat:Start ===============");
			this.time = 0;
			this._isFinished = false;
			this._fixedUpdateRemainDuration = CombatConst.Fixed_Update_Duration;
			this._isStarted = true;
			var gameLevelClass =
			  TypeUtil.GetType(_argDict.GetOrGetDefault2("gameLevelClassPath",
				() => typeof(GameLevelBase).ToString()));
			this.gameLevel = this.AddChild<GameLevelBase>(null, gameLevelClass);
			this.gameLevel.Start();
		}

		public override bool IsCanUpdate()
		{
			return this.IsStarted() && !this.IsFinished() && base.IsCanUpdate();
		}

		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this._isFixedDurationUpdate)
			{
				this.frame = this.frame + 1;
				this.time = this.time + deltaTime;
				if (!this.IsCanUpdate())
					return;
				base.Update(deltaTime, unscaledDeltaTime);
			}
			else
			{
				this._fixedUpdateRemainDuration = this._fixedUpdateRemainDuration - deltaTime;
				var deltaTimeValue = CombatConst.Fixed_Update_Duration;
				var unscaledDeltaTimeValue = CombatConst.Fixed_Update_Duration;
				while (this._fixedUpdateRemainDuration <= 0)
				{
					this.frame = this.frame + 1;
					this.time = this.time + deltaTime;
					this._fixedUpdateRemainDuration = this._fixedUpdateRemainDuration + deltaTimeValue;
					if (!this.IsCanUpdate())
						return;
					base.Update(deltaTimeValue, unscaledDeltaTimeValue);
				}
			}
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.timerManager.Update(deltaTime, unscaledDeltaTime);
		}

		protected override void _LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._LateUpdate(deltaTime, unscaledDeltaTime);
			this.timerManager.Update(deltaTime, unscaledDeltaTime);
		}

		protected override void _FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._FixedUpdate(deltaTime, unscaledDeltaTime);
			this.timerManager.FixedUpdate(deltaTime, unscaledDeltaTime);
		}


		public void SetIsFinished(bool isFinished)
		{
			this._isFinished = isFinished;
		}

		public bool IsStarted()
		{
			return this._isStarted;
		}

		public bool IsFinished()
		{
			return this._isFinished;
		}

	}
}