namespace CsCat
{
	public class DecortorNode : BehaviourTreeCompositeNode
	{
		#region ctor

		public DecortorNode(BehaviourTreeNodeStatus until_status, int count_limit = -1)
		{
			this.until_status = until_status;
			this.count_limit = count_limit;
		}

		#endregion

		#region override method

		/// <summary>
		///   只包含一个子节点
		/// </summary>
		/// <returns></returns>
		public override BehaviourTreeNodeStatus Update()
		{
			if (child_list == null || child_list.Count == 0)
			{
				cur_count = 0;
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			var child = child_list[0];
			var child_status = child.Update();
			if (child_status == until_status)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			if (cur_count == -1)
			{
				cur_count = 0;
				status = BehaviourTreeNodeStatus.Running;
				return status;
			}

			cur_count++;
			if (cur_count >= count_limit)
			{
				cur_count = 0;
				status = BehaviourTreeNodeStatus.Fail;
				return status;
			}

			status = BehaviourTreeNodeStatus.Running;
			return status;
		}

		#endregion

		#region field

		public BehaviourTreeNodeStatus until_status;
		public int count_limit;
		public int cur_count;

		#endregion
	}
}