using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public Component GetComponent(string componentKey)
		{
			if (this.keyToComponentPoolIndexDict.TryGetValue(componentKey, out var componentPoolObjectIndex))
				return _GetComponent(componentPoolObjectIndex);
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

		private Component _GetComponent(PoolObjectIndex componentPoolObjectIndex)
		{
			var component = componentPoolObjectIndex.GetValue<Component>();
			if (component != null && !component.IsDestroyed())
				return component;
			return null;
		}
	}
}