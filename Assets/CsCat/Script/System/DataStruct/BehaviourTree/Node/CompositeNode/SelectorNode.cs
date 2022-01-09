namespace CsCat
{
	public class SelectorNode : BehaviourTreeCompositeNode
	{
		#region override method

		public override BehaviourTreeNodeStatus Update()
		{
			if (child_list == null || child_list.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			foreach (var child in child_list)
			{
				var child_status = child.Update();
				if (child_status == BehaviourTreeNodeStatus.Running || child_status == BehaviourTreeNodeStatus.Success)
				{
					status = child_status;
					return status;
				}
			}

			status = BehaviourTreeNodeStatus.Fail;
			return status;
		}

		#endregion
	}
}