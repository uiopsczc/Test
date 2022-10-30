using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public Component GetComponent(string componentKey)
		{
			if (this.keyToComponentPoolItemIndexDict.TryGetValue(componentKey, out var componentPoolItemIndex))
				return _GetComponent(componentPoolItemIndex);
			return null;
		}

		public T GetComponent<T>() where T : Component
		{
			return GetComponent(typeof(T)) as T;
		}

		public Component GetComponent(Type componentType)
		{
			return GetComponent(componentType.FullName);
		}

		private Component _GetComponent(IPoolItemIndex componentPoolItemIndex)
		{
			var component = componentPoolItemIndex.GetValue<Component>();
			if (component!=null && !component.IsDestroyed())
				return component;
			return null;
		}
	}
}