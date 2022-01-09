using UnityEngine;

namespace CsCat
{
	public class YieldCustomYieldInstruction : YieldBase
	{
		public CustomYieldInstruction customYield;

		public YieldCustomYieldInstruction(CustomYieldInstruction customYield)
		{
			this.customYield = customYield;
		}

		public override bool IsDone(float deltaTime)
		{
			if (!CheckIsStarted())
				return false;
			return !customYield.keepWaiting;
		}
	}
}