namespace CsCat
{
	public class DecortorNode : BehaviourTreeCompositeNode
	{
		#region ctor

		public DecortorNode(BehaviourTreeNodeStatus untilStatus, int countLimit = -1)
		{
			this.untilStatus = untilStatus;
			this.countLimit = countLimit;
		}

		#endregion

		#region override method

		/// <summary>
		///   只包含一个子节点
		/// </summary>
		/// <returns></returns>
		public override BehaviourTreeNodeStatus Update()
		{
			if (childList == null || childList.Count == 0)
			{
				curCount = 0;
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			var child = childList[0];
			var childStatus = child.Update();
			if (childStatus == untilStatus)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			if (curCount == -1)
			{
				curCount = 0;
				status = BehaviourTreeNodeStatus.Running;
				return status;
			}

			curCount++;
			if (curCount >= countLimit)
			{
				curCount = 0;
				status = BehaviourTreeNodeStatus.Fail;
				return status;
			}

			status = BehaviourTreeNodeStatus.Running;
			return status;
		}

		#endregion

		#region field

		public BehaviourTreeNodeStatus untilStatus;
		public int countLimit;
		public int curCount;

		#endregion
	}
}