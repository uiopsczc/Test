namespace CsCat
{
	public class SequenceNode : BehaviourTreeCompositeNode
	{
		#region override method

		public override BehaviourTreeNodeStatus Update()
		{
			if (childList == null || childList.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			foreach (var child in childList)
			{
				var childStatus = child.Update();
				if (childStatus == BehaviourTreeNodeStatus.Running || childStatus == BehaviourTreeNodeStatus.Fail)
				{
					status = childStatus;
					return status;
				}
			}

			status = BehaviourTreeNodeStatus.Success;
			return status;
		}

		#endregion
	}
}