using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		public bool HasChild(string childKey)
		{
			return this.GetChild(childKey) != null;
		}

		public bool HasChildStrictly<T>() where T : TreeNode
		{
			return HasChildStrictly(typeof(T));
		}

		public bool HasChildStrictly(Type childType)
		{
			return HasChild(childType.FullName);
		}

		public bool RawHasChild(string childKey)
		{
			return this.keyToChildPoolItemIndexDict.TryGetValue(childKey, out var childPoolItemIndex);
		}

		public bool RawHasChildStrictly<T>()
		{
			return RawHasChildStrictly(typeof(T));
		}

		public bool RawHasChildStrictly(Type childType)
		{
			return RawHasChild(childType.FullName);
		}

		public bool RawHasChild<T>()
		{
			return RawHasChild(typeof(T));
		}

		public bool RawHasChild(Type childType)
		{
			for (int i = 0; i < this.childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = childPoolItemIndexList[i];
				var child = childPoolItemIndex.GetValue<TreeNode>();
				if (child.GetType().IsSubTypeOf(childType))
					return true;
			}
			return false;
		}
	}
}