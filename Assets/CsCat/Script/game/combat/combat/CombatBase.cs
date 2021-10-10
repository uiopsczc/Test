using System;
using System.Collections;

namespace CsCat
{
  public partial class CombatBase : TickObject
  {
    private bool is_started;
    private bool is_finished;
    private Hashtable arg_dict;
    private bool is_fixed_duration_update = true;
    public float time;
    public int frame;
    private float fixed_update_remain_duration;
    public GameLevelBase gameLevel;
    public EffectManager effectManager;
    public CameraManager cameraManager;
    public UnitManager unitManager;
    public PathManager pathManager;
    public SpellManager spellManager;
    public RandomManager randomManager = new RandomManager();

    public override TimerManager timerManager
    {
      get { return cache.GetOrAddDefault(() => { return new TimerManager(); }); }
    }

    public void Init(Hashtable arg_dict)
    {
      base.Init();
      this.arg_dict = arg_dict;

      randomManager.SetSeed(this.arg_dict.GetOrGetDefault2<int>("random_seed", () => (int)DateTime.Now.Ticks));
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
      this.is_finished = false;
      this.fixed_update_remain_duration = CombatConst.Fixed_Update_Duration;
      this.is_started = true;
      var gameLevel_class =
        TypeUtil.GetType(arg_dict.GetOrGetDefault2<string>("gameLevel_class_path",
          () => typeof(GameLevelBase).ToString()));
      this.gameLevel = this.AddChild(null, gameLevel_class) as GameLevelBase;
      this.gameLevel.Start();
    }

    public override bool IsCanUpdate()
    {
      return this.IsStarted() && !this.IsFinished() && base.IsCanUpdate();
    }

    public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.is_fixed_duration_update)
      {
        this.frame = this.frame + 1;
        this.time = this.time + deltaTime;
        if (!this.IsCanUpdate())
          return;
        base.Update(deltaTime, unscaledDeltaTime);
      }
      else
      {
        this.fixed_update_remain_duration = this.fixed_update_remain_duration - deltaTime;
        var _deltaTime = CombatConst.Fixed_Update_Duration;
        var _unscaledDeltaTime = CombatConst.Fixed_Update_Duration;
        while (this.fixed_update_remain_duration <= 0)
        {
          this.frame = this.frame + 1;
          this.time = this.time + deltaTime;
          this.fixed_update_remain_duration = this.fixed_update_remain_duration + _deltaTime;
          if (!this.IsCanUpdate())
            return;
          base.Update(_deltaTime, _unscaledDeltaTime);
        }
      }
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      this.timerManager.Update(deltaTime, unscaledDeltaTime);
    }

    protected override void __LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__LateUpdate(deltaTime, unscaledDeltaTime);
      this.timerManager.Update(deltaTime, unscaledDeltaTime);
    }

    protected override void __FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__FixedUpdate(deltaTime, unscaledDeltaTime);
      this.timerManager.FixedUpdate(deltaTime, unscaledDeltaTime);
    }


    public void SetIsFinished(bool is_finished)
    {
      this.is_finished = is_finished;
    }

    public bool IsStarted()
    {
      return this.is_started;
    }

    public bool IsFinished()
    {
      return this.is_finished;
    }

  }
}