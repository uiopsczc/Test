namespace CsCat
{
  public class A_HFSMState : HFSMState
  {
    public override void Init()
    {
      base.Init();
      this.AddListener("hello".ToEventName(this.GetRootHFSM()), () => LogCat.warn(this.key));
    }

    public override void Enter(object[] args)
    {
      base.Enter(args);
      LogCat.log("Enter A_HFSMState", key);

    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      LogCat.log("Execute A_HFSMState", key);
    }

    public override void Exit(object[] args)
    {
      base.Exit(args);
      LogCat.log("Exit A_HFSMState", key);
    }
  }
}

