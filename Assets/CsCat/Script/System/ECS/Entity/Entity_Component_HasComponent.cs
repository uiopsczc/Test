using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public bool HasComponent(string componentKey)
		{
			return this.GetComponent(componentKey) != null;
		}

		public bool HasComponentStrictly<T>() where T : Component
		{
			return HasComponentStrictly(typeof(T));
		}

		public bool HasComponentStrictly(Type componentType)
		{
			return HasComponent(componentType.FullName);
		}

		public bool RawHasComponent(string componentKey)
		{
			return this.keyToComponentPoolItemIndexDict.TryGetValue(componentKey, out var componentPoolItemIndex);
		}

		public bool RawHasComponentStrictly<T>()
		{
			return RawHasComponentStrictly(typeof(T));
		}

		public bool RawHasComponentStrictly(Type componentType)
		{
			return RawHasComponent(componentType.FullName);
		}

		public bool RawHasComponent<T>()
		{
			return RawHasComponent(typeof(T));
		}

		public bool RawHasComponent(Type componentType)
		{
			for (int i = 0; i < this.componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = componentPoolItemIndexList[i];
				var component = componentPoolItemIndex.GetValue<Component>();
				if (component.GetType().IsSubTypeOf(componentType))
					return true;
			}
			return false;
		}
	}
}