namespace CsCat
{
	public class YieldWaitForSeconds : YieldBase
	{
		public float remain_duration;

		public YieldWaitForSeconds(float remain_duration)
		{
			this.remain_duration = remain_duration;
		}

		public override bool IsDone(float deltaTime)
		{
			if (!CheckIsStarted())
				return false;

			remain_duration -= deltaTime;
			return remain_duration < 0;
		}
	}
}