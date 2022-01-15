using System;
using System.Collections;

namespace CsCat
{
	public partial class CombatBase : TickObject
	{
		private bool isStarted;
		private bool isFinished;
		private Hashtable argDict;
		private bool isFixedDurationUpdate = true;
		public float time;
		public int frame;
		private float fixedUpdateRemainDuration;
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
			this.argDict = arg_dict;

			randomManager.SetSeed(this.argDict.GetOrGetDefault2<int>("random_seed", () => (int)DateTime.Now.Ticks));
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
			this.isFinished = false;
			this.fixedUpdateRemainDuration = CombatConst.Fixed_Update_Duration;
			this.isStarted = true;
			var gameLevelClass =
			  TypeUtil.GetType(argDict.GetOrGetDefault2<string>("gameLevel_class_path",
				() => typeof(GameLevelBase).ToString()));
			this.gameLevel = this.AddChild(null, gameLevelClass) as GameLevelBase;
			this.gameLevel.Start();
		}

		public override bool IsCanUpdate()
		{
			return this.IsStarted() && !this.IsFinished() && base.IsCanUpdate();
		}

		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.isFixedDurationUpdate)
			{
				this.frame = this.frame + 1;
				this.time = this.time + deltaTime;
				if (!this.IsCanUpdate())
					return;
				base.Update(deltaTime, unscaledDeltaTime);
			}
			else
			{
				this.fixedUpdateRemainDuration = this.fixedUpdateRemainDuration - deltaTime;
				var deltaTimeValue = CombatConst.Fixed_Update_Duration;
				var unscaledDeltaTimeValue = CombatConst.Fixed_Update_Duration;
				while (this.fixedUpdateRemainDuration <= 0)
				{
					this.frame = this.frame + 1;
					this.time = this.time + deltaTime;
					this.fixedUpdateRemainDuration = this.fixedUpdateRemainDuration + deltaTimeValue;
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
			this.isFinished = isFinished;
		}

		public bool IsStarted()
		{
			return this.isStarted;
		}

		public bool IsFinished()
		{
			return this.isFinished;
		}

	}
}