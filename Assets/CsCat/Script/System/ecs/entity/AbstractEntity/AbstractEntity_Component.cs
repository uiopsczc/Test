using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		protected Dictionary<string, AbstractComponent>
			keyToComponentDict = new Dictionary<string, AbstractComponent>();

		protected Dictionary<Type, List<AbstractComponent>> typeToComponentListDict =
			new Dictionary<Type, List<AbstractComponent>>();

		protected List<string> componentKeyList = new List<string>();
		protected List<Type> componentTypeList = new List<Type>();
		protected IdPool componentKeyIdPool = new IdPool();

		//////////////////////////////////////////////////////////////////////
		// 按加入的顺序遍历
		//////////////////////////////////////////////////////////////////////
		//按加入的顺序遍历
		public IEnumerable<AbstractComponent> ForeachComponent()
		{
			AbstractComponent component;
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				component = GetComponent(componentKeyList[i]);
				if (component != null)
					yield return component;
			}
		}

		public IEnumerable<AbstractComponent> ForeachComponent(Type component_type)
		{
			AbstractComponent component = null;
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				component = GetComponent(componentKeyList[i]);
				if (component != null && component_type.IsInstanceOfType(component))
					yield return component;
			}
		}

		public IEnumerable<T> ForeachComponent<T>() where T : AbstractComponent
		{
			T component = null;
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				component = GetComponent(componentKeyList[i]) as T;
				if (component != null)
					yield return component;
			}
		}


		void _OnDespawn_Component()
		{
			keyToComponentDict.Clear();
			foreach (var componentList in typeToComponentListDict.Values)
			{
				componentList.Clear();
				PoolCatManagerUtil.Despawn(componentList);
			}

			typeToComponentListDict.Clear();
			componentKeyList.Clear();
			componentTypeList.Clear();
		}
	}
}