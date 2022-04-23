using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public AbstractComponent GetComponent(string componentKey)
		{
			if (this.keyToComponentDict.TryGetValue(componentKey, out var component))
			{
				if (!component.IsDestroyed())
					return component;
			}
			return null;
		}

		public T GetComponent<T>(string componentKey) where T : AbstractComponent
		{
			return GetComponent(componentKey) as T;
		}

		public AbstractComponent GetComponent(Type componentType)
		{
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
				if (component != null && componentType.IsInstanceOfType(component))
					return component;
			}
			return null;
		}

		public T GetComponent<T>() where T : AbstractComponent
		{
			return GetComponent(typeof(T)) as T;
		}

		//效率问题引入的
		public AbstractComponent GetComponentStrictly(Type componentType)
		{
			if (!this.typeToComponentListDict.ContainsKey(componentType))
				return null;
			for (var i = 0; i < typeToComponentListDict[componentType].Count; i++)
			{
				var component = typeToComponentListDict[componentType][i];
				if (!component.IsDestroyed())
					return component;
			}

			return null;
		}

		public T GetComponentStrictly<T>() where T : AbstractComponent
		{
			return GetComponentStrictly(typeof(T)) as T;
		}


		public AbstractComponent[] GetComponents(Type componentType)
		{
			List<AbstractComponent> list = new List<AbstractComponent>();
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
				if (component != null && componentType.IsInstanceOfType(component))
					list.Add(component);
			}
			return list.ToArray();
		}

		public T[] GetComponents<T>() where T : AbstractComponent
		{
			return (T[])GetComponents(typeof(T));
		}


		public AbstractComponent[] GetComponentsStrictly(Type componentType)
		{
			List<AbstractComponent> list = new List<AbstractComponent>();
			if (!this.typeToComponentListDict.ContainsKey(componentType))
				return list.ToArray();
			var componentList = typeToComponentListDict[componentType];
			for (var i = 0; i < componentList.Count; i++)
			{
				var component = componentList[i];
				if (!component.IsDestroyed())
					list.Add(component);
			}

			return list.ToArray();
		}

		public T[] GetComponentsStrictly<T>() where T : AbstractComponent
		{
			return (T[])GetComponentsStrictly(typeof(T));
		}
	}
}