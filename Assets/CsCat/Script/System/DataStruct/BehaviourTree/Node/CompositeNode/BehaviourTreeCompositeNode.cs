using System.Collections.Generic;

namespace CsCat
{
	public class BehaviourTreeCompositeNode : BehaviourTreeNode
	{
		#region field

		public List<BehaviourTreeNode> child_list = new List<BehaviourTreeNode>();

		#endregion

		#region public method

		public void AddChild(BehaviourTreeNode child)
		{
			child_list.Add(child);
			child.parent = this;
		}

		#endregion

		#region override method

		public override T GetChild<T>(bool loop = false)
		{
			foreach (var child in child_list)
				if (!loop)
				{
					if (child is T)
						return (T)child;
				}
				else
				{
					BehaviourTreeNode childchild = child.GetChild<T>(loop);
					if (childchild is T)
						return (T)childchild;
				}

			return null;
		}

		public override void Interrupt()
		{
			foreach (var child in child_list)
				child.Interrupt();
			base.Interrupt();
		}

		public override void RestStatus()
		{
			base.RestStatus();
			foreach (var child in child_list)
				child.RestStatus();
		}

		#endregion
	}
}