namespace CsCat
{
  public class TestHFSM : HFSM
  {
    public TestHFSM(GameEntity owner) : base(owner)
    {
    }
    public override void InitStates()
    {
      base.InitStates();
      this.AddSubDirectState<A_HFSMState>("a");
      this.AddSubDirectState<B_HFSMState>("b");
      this.AddSubDirectHFSM<TestSubHFSM1>("sub_hfsm1");
      this.SetDefaultSubDirectState("a");
    }

    public void Test1()
    {
      ChangeToState("a", false);
    }

    public void Test2()
    {
      ChangeToState("b", false);
    }

    public void Test3()
    {
      ChangeToState("a1", false);
    }

    public void Test4()
    {
      ChangeToState("a11", false);
    }

    public void Test5()
    {
      ChangeToState("a111", false);
    }
  }
}

