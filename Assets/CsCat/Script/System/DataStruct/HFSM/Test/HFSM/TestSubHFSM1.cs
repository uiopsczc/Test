namespace CsCat
{
	public class TestSubHFSM1 : HFSM
	{
		public override void InitStates()
		{
			base.InitStates();
			this.AddSubDirectState<A_HFSMState>("a1");
			this.AddSubDirectState<B_HFSMState>("b1");
			this.AddSubDirectHFSM<TestSubHFSM11>("sub_hfsm11");
			this.SetDefaultSubDirectState("a1");
		}
	}
}

