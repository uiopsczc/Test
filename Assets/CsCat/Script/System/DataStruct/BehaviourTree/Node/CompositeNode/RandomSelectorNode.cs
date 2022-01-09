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
			if (childList == null || childList.Count == 0)
			{
				status = BehaviourTreeNodeStatus.Success;
				return status;
			}

			var random = randomManager.RandomInt(0, childList.Count);
			var childStatus = childList[random].Update();
			status = childStatus;
			return status;
		}

		#endregion
	}
}