using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		private bool _CheckCanAddChildType(Type childType, string childKey = null)
		{
			childKey = childKey??childType.FullName;
			if (this._keyToChildPoolItemIndexDict.ContainsKey(childKey))
			{
				LogCat.error("duplicate add child:", childKey, childType);
				return false;
			}
			return true;
		}
		protected TreeNode _AddChild(Type childType, IPoolItemIndex childPoolItemIndex, string childKey = null)
		{
			childKey = childKey ?? childType.FullName;
			var child = childPoolItemIndex.GetValue<TreeNode>();
			child.SetPoolItemIndex(childPoolItemIndex);
			child.SetParentPoolItemIndex(this._poolItemIndex);
			child.SetKey(childKey);
			_AddChildRelationship(childType, childPoolItemIndex);
			return child;
		}

		public TreeNode AddChildWithoutInit(Type childType, string childKey = null)
		{
			if (_CheckCanAddChildType(childType, childKey))
				return null;
			var (childPoolItem, childPoolItemIndex) = this.GetPoolManager().Spawn(childType);
			return _AddChild(childType, childPoolItemIndex, childKey);
		}

		public T AddChildWithoutInit<T>(string childKey = null) where T : TreeNode
		{
			return AddChildWithoutInit(typeof(T), childKey) as T;
		}

		public TreeNode AddChild(Type childType, string childKey, params object[] initArgs)
		{
			var child = AddChildWithoutInit(childType, childKey);
			if (child == null) //没有加成功
				return null;
			child.DoInit(initArgs);
			child.SetIsEnabled(true);
			return child;
		}

		public T AddChild<T>(string childKey, params object[] initArgs) where T : TreeNode
		{
			return AddChild(typeof(T), childKey, initArgs) as T;
		}

		private void _AddChildRelationship(Type childType, IPoolItemIndex childPoolItemIndex, string childKey = null)
		{
			childKey = childKey??childType.FullName;
			_keyToChildPoolItemIndexDict[childKey] = childPoolItemIndex;
			_childPoolItemIndexList.Add(childPoolItemIndex);
		}
	}
}