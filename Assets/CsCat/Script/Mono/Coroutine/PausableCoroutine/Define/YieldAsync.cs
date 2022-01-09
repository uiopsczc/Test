using UnityEngine;

namespace CsCat
{
	public class YieldAsync : YieldBase
	{
		public AsyncOperation asyncOperation;

		public YieldAsync(AsyncOperation asyncOperation)
		{
			this.asyncOperation = asyncOperation;
		}

		public override bool IsDone(float deltaTime)
		{
			if (!CheckIsStarted())
				return false;

			return asyncOperation.isDone;
		}
	}
}