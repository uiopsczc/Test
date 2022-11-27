using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		protected bool _RemoveChild(IPoolItemIndex childPoolItemIndex)
		{
			var child = _GetChild(childPoolItemIndex);
			if (child==null)
				return false;
			child.DoDestroy();
			_RemoveChildRelationship(child);
			child.Despawn();
			return true;
		}

		public bool RemoveChild(string childKey)
		{
			if (this._keyToChildPoolItemIndexDict.TryGetValue(childKey, out var childPoolItemIndex))
				return _RemoveChild(childPoolItemIndex);
			return false;
		}

		public bool RemoveChild(Type childType)
		{
			return RemoveChild(childType.FullName);
		}

		public bool RemoveChild<T>() where T : TreeNode
		{
			return RemoveChild(typeof(T));
		}

		public void RemoveAllChildren()
		{
			for (var i = 0; i < _childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = _childPoolItemIndexList[i];
				if (_RemoveChild(childPoolItemIndex))
					i--;
			}
		}

		////////////////////////////////////////////////////////////////////
		

		private void _RemoveChildRelationship(TreeNode child)
		{
			if (this._keyToChildPoolItemIndexDict.TryGetValue(child.GetType().FullName,
				out var poolItemIndex))
				_childPoolItemIndexList.Remove(poolItemIndex);
		}


		//主要作用是将IsDestroyed的Child从child_list中删除,配合Foreach的GetChildren使用
		private void _CheckDestroyedChildren()
		{
			for (int i = _childPoolItemIndexList.Count - 1; i >= 0; i--)
			{
				var childPoolItemIndex = _childPoolItemIndexList[i];
				var child = childPoolItemIndex.GetValue<TreeNode>();
				if (!child.IsDestroyed()) continue;
				_RemoveChildRelationship(child);
				child.Despawn();
			}
		}
	}
}