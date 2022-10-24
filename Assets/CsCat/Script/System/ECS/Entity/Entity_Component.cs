using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		protected Dictionary<string, PoolObjectIndex>
			keyToComponentPoolObjectIndexDict = new Dictionary<string, PoolObjectIndex>();
		protected List<PoolObjectIndex> componentPoolObjectIndexList = new List<PoolObjectIndex>();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<Component> ForeachComponent()
		{
			for (int i = 0; i < componentPoolObjectIndexList.Count; i++)
			{
				var componentPoolObjectIndex = componentPoolObjectIndexList[i];
				var component = _GetComponent(componentPoolObjectIndex);
				if (component != null)
					yield return component;
			}
		}

		void _OnDespawn_Component()
		{
			keyToComponentPoolObjectIndexDict.Clear();
			componentPoolObjectIndexList.Clear();
		}
	}
}