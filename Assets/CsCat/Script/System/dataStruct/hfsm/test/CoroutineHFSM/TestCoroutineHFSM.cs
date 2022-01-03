namespace CsCat
{
	public class TestCoroutineHFSM : CoroutineHFSM
	{
		public TestCoroutineHFSM(GameEntity owner) : base(owner)
		{
		}
		public override void InitStates()
		{
			base.InitStates();
			this.AddSubDirectState<A_CoroutineHFSMState>("a");
			this.AddSubDirectState<B_CoroutineHFSMState>("b");
			this.SetDefaultSubDirectState("a");
		}

		public void Test1()
		{
			ChangeToState("a");
		}

		public void Test2()
		{
			ChangeToState("b");
		}
	}
}

