using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		protected Dictionary<string, IPoolIndex>
			keyToComponentPoolIndexDict = new Dictionary<string, IPoolIndex>();
		protected List<IPoolIndex> componentPoolIndexList = new List<IPoolIndex>();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<Component> ForeachComponent()
		{
			for (int i = 0; i < componentPoolIndexList.Count; i++)
			{
				var componentPoolIndex = componentPoolIndexList[i];
				var component = _GetComponent(componentPoolIndex);
				if (component != null)
					yield return component;
			}
		}

		void _OnDespawn_Component()
		{
			keyToComponentPoolIndexDict.Clear();
			componentPoolIndexList.Clear();
		}
	}
}