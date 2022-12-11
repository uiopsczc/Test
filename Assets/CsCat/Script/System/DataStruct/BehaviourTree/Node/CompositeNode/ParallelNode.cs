namespace CsCat
{
	/// <summary>
	///   全部返回成功才会成功（或者一个返回失败）
	/// </summary>
	public class ParallelNode : BehaviourTreeCompositeNode
	{
		#region override method

		public override BehaviourTreeNodeStatus Update()
		{
			if (childList == null || childList.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			var successCount = 0;
			for (var i = 0; i < childList.Count; i++)
			{
				var child = childList[i];
				var childStatus = child.Update();
				if (childStatus == BehaviourTreeNodeStatus.Fail)
				{
					status = BehaviourTreeNodeStatus.Fail;
					return status;
				}

				if (childStatus == BehaviourTreeNodeStatus.Success)
					successCount++;
			}

			if (successCount == childList.Count)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			status = BehaviourTreeNodeStatus.Running;
			return status;
		}

		#endregion
	}
}