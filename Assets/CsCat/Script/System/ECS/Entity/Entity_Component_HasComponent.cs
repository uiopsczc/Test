using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		public bool IsHasComponent(string componentKey)
		{
			return this.GetComponent(componentKey) != null;
		}

		public bool IsHasComponentStrictly<T>() where T : Component
		{
			return IsHasComponentStrictly(typeof(T));
		}

		public bool IsHasComponentStrictly(Type componentType)
		{
			return IsHasComponent(componentType.FullName);
		}

		public bool IsRawHasComponent(string componentKey)
		{
			return this._keyToComponentPoolItemIndexDict.TryGetValue(componentKey, out var componentPoolItemIndex);
		}

		public bool IsRawHasComponentStrictly<T>()
		{
			return IsRawHasComponentStrictly(typeof(T));
		}

		public bool IsRawHasComponentStrictly(Type componentType)
		{
			return IsRawHasComponent(componentType.FullName);
		}

		public bool IsRawHasComponent<T>()
		{
			return IsRawHasComponent(typeof(T));
		}

		public bool IsRawHasComponent(Type componentType)
		{
			for (int i = 0; i < this._componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = componentPoolItemIndex.GetValue<Component>();
				if (component.GetType().IsSubTypeOf(componentType))
					return true;
			}
			return false;
		}
	}
}