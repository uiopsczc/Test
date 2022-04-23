using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public AbstractComponent AddComponent(AbstractComponent component, string componentKey = null)
		{
			if (componentKey != null)
				component.key = componentKey;
			if (component.key != null && this.keyToComponentDict.ContainsKey(component.key))
			{
				LogCat.error("duplicate add component:", component.key, component.GetType());
				return null;
			}

			bool isKeyUsingParentIdPool = componentKey == null;
			if (isKeyUsingParentIdPool)
			{
				componentKey = componentKeyIdPool.Get().ToString();
				//再次检查键值
				if (this.keyToComponentDict.ContainsKey(componentKey))
				{
					LogCat.error("duplicate add component:", component.key, component.GetType());
					return null;
				}
			}

			component.key = componentKey;
			component.isKeyUsingParentIdPool = isKeyUsingParentIdPool;
			_AddComponentRelationship(component);
			return component;
		}

		public AbstractComponent AddComponentWithoutInit(string componentKey, Type componentType)
		{
			var component = PoolCatManagerUtil.Spawn(componentType) as AbstractComponent;
			return AddComponent(component, componentKey);
		}

		public T AddComponentWithoutInit<T>(string componentKey = null) where T : AbstractComponent
		{
			return AddComponentWithoutInit(componentKey, typeof(T)) as T;
		}

		public AbstractComponent AddComponent(string componentKey, Type componentType, params object[] initArgs)
		{
			var component = AddComponentWithoutInit(componentKey, componentType);
			if (component == null) //没有加成功
				return null;
			component.InvokeMethod("Init", false, initArgs);
			component.PostInit();
			component.SetIsEnabled(true);
			return component;
		}

		public T AddComponent<T>(string componentKey, params object[] initArgs) where T : AbstractComponent
		{
			return AddComponent(componentKey, typeof(T), initArgs) as T;
		}

		void _AddComponentRelationship(AbstractComponent component)
		{
			component.entity = this;
			keyToComponentDict[component.key] = component;
			typeToComponentListDict.GetOrAddDefault(component.GetType(),
					() => PoolCatManagerUtil.Spawn<List<AbstractComponent>>())
				.Add(component);
			componentKeyList.Add(component.key);
			if (!componentTypeList.Contains(component.GetType()))
				componentTypeList.Add(component.GetType());
		}
	}
}