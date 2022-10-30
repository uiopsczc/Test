using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		protected Dictionary<string, IPoolItemIndex>
			keyToComponentPoolItemIndexDict = new Dictionary<string, IPoolItemIndex>();
		protected List<IPoolItemIndex> componentPoolItemIndexList = new List<IPoolItemIndex>();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<Component> ForeachComponent()
		{
			for (int i = 0; i < componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				if (component != null)
					yield return component;
			}
		}

		void _OnDespawn_Component()
		{
			keyToComponentPoolItemIndexDict.Clear();
			componentPoolItemIndexList.Clear();
		}
	}
}