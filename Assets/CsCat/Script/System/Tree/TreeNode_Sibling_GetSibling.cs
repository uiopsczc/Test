using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		private TreeNode _GetSibling(IPoolItemIndex siblingPoolItemIndex)
		{
			return this.GetParent<TreeNode>()._GetChild(siblingPoolItemIndex);
		}

		public TreeNode GetSibling(string siblingKey)
		{
			return this.GetParent<TreeNode>().GetChild(siblingKey);
		}

		public T GetSibling<T>(string siblingKey)where T:TreeNode
		{
			return this.GetParent<TreeNode>().GetChild<T>(siblingKey);
		}

		public T GetSiblingStrictly<T>() where T : TreeNode
		{
			return this.GetParent<TreeNode>().GetChildStrictly<T>();
		}

		public TreeNode GetSiblingStrictly(Type siblingType)
		{
			return this.GetParent<TreeNode>().GetChildStrictly(siblingType);
		}

		public T GetSibling<T>() where T : TreeNode
		{
			return this.GetParent<TreeNode>().GetChild<T>();
		}

		public TreeNode GetSibling(Type siblingType)
		{
			return this.GetParent<TreeNode>().GetChild(siblingType);
		}

		
	}
}