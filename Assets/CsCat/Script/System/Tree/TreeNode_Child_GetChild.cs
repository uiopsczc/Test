using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		private TreeNode _GetChild(IPoolItemIndex childPoolItemIndex)
		{
			var child = childPoolItemIndex.GetValue<TreeNode>();
			if (child != null && !child.IsDestroyed())
				return child;
			return null;
		}

		public TreeNode GetChild(string childKey)
		{
			if (this.keyToChildPoolItemIndexDict.TryGetValue(childKey, out var childPoolItemIndex))
				return _GetChild(childPoolItemIndex);
			return null;
		}

		public T GetChild<T>(string childKey)where T:TreeNode
		{
			return GetChild(childKey) as T;
		}

		public T GetChildStrictly<T>() where T : TreeNode
		{
			return GetChildStrictly(typeof(T)) as T;
		}

		public TreeNode GetChildStrictly(Type childType)
		{
			return GetChild(childType.FullName);
		}

		public T GetChild<T>() where T : TreeNode
		{
			return GetChild(typeof(T)) as T;
		}

		public TreeNode GetChild(Type childType)
		{
			for (int i = 0; i < this.childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = childPoolItemIndexList[i];
				var child = childPoolItemIndex.GetValue<TreeNode>();
				if (!child.IsDestroyed() && child.GetType().IsSubTypeOf(childType))
					return child;
			}

			return null;
		}

		
	}
}