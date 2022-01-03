using UnityEngine;

namespace CsCat
{
	public class YieldWWW : YieldBase
	{
		public WWW www;

		public YieldWWW(WWW www)
		{
			this.www = www;
		}

		public override bool IsDone(float deltaTime)
		{
			if (!CheckIsStarted())
				return false;

			return www.isDone;
		}
	}
}