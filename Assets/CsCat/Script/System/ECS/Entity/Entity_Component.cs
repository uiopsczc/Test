using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		protected Dictionary<string, IPoolItemIndex>
			_keyToComponentPoolItemIndexDict = new Dictionary<string, IPoolItemIndex>();
		protected List<IPoolItemIndex> _componentPoolItemIndexList = new List<IPoolItemIndex>();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<Component> ForeachComponent()
		{
			for (int i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				if (component != null)
					yield return component;
			}
		}

		private void _Despawn_Component()
		{
			_keyToComponentPoolItemIndexDict.Clear();
			_componentPoolItemIndexList.Clear();
		}
	}
}