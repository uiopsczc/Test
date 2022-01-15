namespace CsCat
{
	public class YieldNestedCoroutine : YieldBase
	{
		public PausableCoroutine coroutine;

		public YieldNestedCoroutine(PausableCoroutine coroutine)
		{
			this.coroutine = coroutine;
		}

		public override bool IsDone(float deltaTime)
		{
			if (!CheckIsStarted())
				return false;

			return coroutine.isFinished;
		}
	}
}