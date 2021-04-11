namespace CsCat
{
  public class TestSubHFSM111 : HFSM
  {
    public override void InitStates()
    {
      base.InitStates();
      this.AddSubDirectState<A_HFSMState>("a111");
      this.AddSubDirectState<B_HFSMState>("b111");
      this.SetDefaultSubDirectState("a111");
    }
  }
}

