namespace CsCat
{
  public class TestSubHFSM11 : HFSM
  {
    public override void InitStates()
    {
      base.InitStates();
      this.AddSubDirectState<A_HFSMState>("a11");
      this.AddSubDirectState<B_HFSMState>("b11");
      this.AddSubDirectHFSM<TestSubHFSM111>("sub_hfsm111");
      this.SetDefaultSubDirectState("a11");
    }
  }
}

