namespace CsCat
{
	public class B_HFSMState : HFSMState
	{
		public override void Enter(object[] args)
		{
			LogCat.log("Enter B_HFSMState", key);
			base.Enter(args);
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			LogCat.log("Execute B_HFSMState", key);
		}

		public override void Exit(object[] args)
		{
			LogCat.log("Exit B_HFSMState", key);
			base.Exit(args);
		}
	}
}

