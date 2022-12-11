//一个返回成功则成功，一个返回失败则失败
namespace CsCat
{
	public class ParallelNode2 : BehaviourTreeCompositeNode
	{
		#region override method

		public override BehaviourTreeNodeStatus Update()
		{
			if (childList == null || childList.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

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
				{
					status = BehaviourTreeNodeStatus.Success;
					return status;
				}
			}

			status = BehaviourTreeNodeStatus.Running;
			return status;
		}

		#endregion
	}
}