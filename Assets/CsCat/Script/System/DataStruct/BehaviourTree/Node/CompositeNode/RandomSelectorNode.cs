namespace CsCat
{
	public class RandomSelectorNode : BehaviourTreeCompositeNode
	{
		protected RandomManager randomManager;

		public RandomSelectorNode(RandomManager randomManager = null)
		{
			this.randomManager = randomManager ?? Client.instance.randomManager;
		}

		#region override method

		public override BehaviourTreeNodeStatus Update()
		{
			if (child_list == null || child_list.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			var random = randomManager.RandomInt(0, child_list.Count);
			var child_status = child_list[random].Update();
			status = child_status;
			return status;
		}

		#endregion
	}
}