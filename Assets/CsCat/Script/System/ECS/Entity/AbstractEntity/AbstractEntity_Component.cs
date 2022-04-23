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
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
				if (component != null)
					yield return component;
			}
		}

		public IEnumerable<AbstractComponent> ForeachComponent(Type componentType)
		{
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
				if (component != null && componentType.IsInstanceOfType(component))
					yield return component;
			}
		}

		public IEnumerable<T> ForeachComponent<T>() where T : AbstractComponent
		{
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				if (GetComponent(componentKeyList[i]) is T component)
					yield return component;
			}
		}


		void _OnDespawn_Component()
		{
			keyToComponentDict.Clear();
			foreach (var keyValue in typeToComponentListDict)
			{
				var componentList = keyValue.Value;
				componentList.Clear();
				PoolCatManagerUtil.Despawn(componentList);
			}
			typeToComponentListDict.Clear();
			componentKeyList.Clear();
			componentTypeList.Clear();
		}
	}
}