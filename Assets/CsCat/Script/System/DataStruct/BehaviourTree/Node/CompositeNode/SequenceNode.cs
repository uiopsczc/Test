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

			for (var i = 0; i < childList.Count; i++)
			{
				var child = childList[i];
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