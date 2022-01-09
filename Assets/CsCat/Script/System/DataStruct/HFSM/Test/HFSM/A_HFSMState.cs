namespace CsCat
{
	public class A_HFSMState : HFSMState
	{
		public override void Init()
		{
			base.Init();
			this.AddListener(this.GetRootHFSM().eventDispatchers, "hello", () => LogCat.warn(this.key));
		}

		public override void Enter(object[] args)
		{
			base.Enter(args);
			LogCat.log("Enter A_HFSMState", key);

		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			LogCat.log("Execute A_HFSMState", key);
		}

		public override void Exit(object[] args)
		{
			base.Exit(args);
			LogCat.log("Exit A_HFSMState", key);
		}
	}
}

