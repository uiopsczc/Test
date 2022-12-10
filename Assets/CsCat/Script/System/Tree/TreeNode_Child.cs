using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		protected Dictionary<string, IPoolItemIndex>
			_keyToChildPoolItemIndexDict = new Dictionary<string, IPoolItemIndex>();
		protected List<IPoolItemIndex> _childPoolItemIndexList = new List<IPoolItemIndex>();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<TreeNode> ForeachChild()
		{
			for (int i = 0; i < _childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = _childPoolItemIndexList[i];
				var child = _GetChild(childPoolItemIndex);
				if (child != null)
					yield return child;
			}
		}

		void _Despawn_Child()
		{
			_keyToChildPoolItemIndexDict.Clear();
			_childPoolItemIndexList.Clear();
		}
	}
}