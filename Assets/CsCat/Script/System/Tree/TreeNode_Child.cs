using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		protected Dictionary<string, IPoolItemIndex>
			keyToChildPoolItemIndexDict = new Dictionary<string, IPoolItemIndex>();
		protected List<IPoolItemIndex> childPoolItemIndexList = new List<IPoolItemIndex>();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<TreeNode> ForeachChild()
		{
			for (int i = 0; i < childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = childPoolItemIndexList[i];
				var child = _GetChild(childPoolItemIndex);
				if (child != null)
					yield return child;
			}
		}

		void _OnDespawn_Child()
		{
			keyToChildPoolItemIndexDict.Clear();
			childPoolItemIndexList.Clear();
		}
	}
}