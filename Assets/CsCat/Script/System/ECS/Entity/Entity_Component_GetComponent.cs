using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		private Component _GetComponent(IPoolItemIndex componentPoolItemIndex)
		{
			var component = componentPoolItemIndex.GetValue<Component>();
			if (component != null && !component.IsDestroyed())
				return component;
			return null;
		}

		public Component GetComponent(string componentKey)
		{
			if (this._keyToComponentPoolItemIndexDict.TryGetValue(componentKey, out var componentPoolItemIndex))
				return _GetComponent(componentPoolItemIndex);
			return null;
		}

		public T GetComponentStrictly<T>() where T : Component
		{
			return GetComponentStrictly(typeof(T)) as T;
		}

		public Component GetComponentStrictly(Type componentType)
		{
			return GetComponent(componentType.FullName);
		}

		public T GetComponent<T>() where T : Component
		{
			return GetComponent(typeof(T)) as T;
		}

		public Component GetComponent(Type componentType)
		{
			for (int i = 0; i < this._componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = componentPoolItemIndex.GetValue<Component>();
				if (!component.IsDestroyed() && component.GetType().IsSubTypeOf(componentType))
					return component;
			}

			return null;
		}

		
	}
}