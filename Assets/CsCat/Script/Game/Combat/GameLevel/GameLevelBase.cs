namespace CsCat
{
	public partial class GameLevelBase : TickObject
	{
		private bool _isStarted;
		private bool _isFinished;

		public override void Start()
		{
			base.Start();
			this._isStarted = true;
		}

		public override bool IsCanUpdate()
		{
			return this.IsStarted() && !this.IsFinished() && base.IsCanUpdate();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
		}

		public bool CheckWin()
		{
			return false;
		}

		public bool CheckLose()
		{
			return false;
		}

		public void SetIsFinished(bool isFinished)
		{
			this._isFinished = isFinished;
		}

		public bool IsStarted()
		{
			return this._isStarted;
		}

		public bool IsFinished()
		{
			return this._isFinished;
		}
	}
}