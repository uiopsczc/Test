namespace CsCat
{
	public class YieldWaitForSeconds : YieldBase
	{
		public float remainDuration;

		public YieldWaitForSeconds(float remainDuration)
		{
			this.remainDuration = remainDuration;
		}

		public override bool IsDone(float deltaTime)
		{
			if (!_CheckIsStarted())
				return false;

			remainDuration -= deltaTime;
			return remainDuration < 0;
		}
	}
}