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
			if (child_list == null || child_list.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			BehaviourTreeNodeStatus child_status;
			var success_count = 0;
			foreach (var child in child_list)
			{
				child_status = child.Update();
				if (child_status == BehaviourTreeNodeStatus.Fail)
				{
					status = BehaviourTreeNodeStatus.Fail;
					return status;
				}

				if (child_status == BehaviourTreeNodeStatus.Success)
					success_count++;
			}

			if (success_count == child_list.Count)
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