using System.Collections.Generic;

namespace CsCat
{
	public class BehaviourTreeCompositeNode : BehaviourTreeNode
	{
		#region field

		public List<BehaviourTreeNode> childList = new List<BehaviourTreeNode>();

		#endregion

		#region public method

		public void AddChild(BehaviourTreeNode child)
		{
			childList.Add(child);
			child.parent = this;
		}

		#endregion

		#region override method

		public override T GetChild<T>(bool loop = false)
		{
			for (var i = 0; i < childList.Count; i++)
			{
				var child = childList[i];
				if (!loop)
				{
					if (child is T node)
						return node;
				}
				else
				{
					BehaviourTreeNode grandchild = child.GetChild<T>(loop);
					if (grandchild is T)
						return (T) grandchild;
				}
			}

			return null;
		}

		public override void Interrupt()
		{
			for (var i = 0; i < childList.Count; i++)
			{
				var child = childList[i];
				child.Interrupt();
			}

			base.Interrupt();
		}

		public override void RestStatus()
		{
			base.RestStatus();
			for (var i = 0; i < childList.Count; i++)
			{
				var child = childList[i];
				child.RestStatus();
			}
		}

		#endregion
	}
}