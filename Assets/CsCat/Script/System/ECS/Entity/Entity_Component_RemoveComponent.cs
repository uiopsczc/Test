using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		protected bool _RemoveComponent(IPoolItemIndex componentPoolItemIndex)
		{
			var component = _GetComponent(componentPoolItemIndex);
			if (component==null)
				return false;
			component.DoDestroy();
			_RemoveComponentRelationship(component);
			component.Despawn();
			return true;
		}

		public bool RemoveComponent(string componentKey)
		{
			if (this._keyToComponentPoolItemIndexDict.TryGetValue(componentKey, out var componentPoolItemIndex))
				return _RemoveComponent(componentPoolItemIndex);
			return false;
		}

		public bool RemoveComponent(Type componentType)
		{
			return RemoveComponent(componentType.FullName);
		}

		public bool RemoveComponent<T>() where T : Component
		{
			return RemoveComponent(typeof(T));
		}

		public void RemoveAllComponents()
		{
			for (var i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				if (_RemoveComponent(componentPoolItemIndex))
					i--;
			}
		}

		////////////////////////////////////////////////////////////////////
		

		private void _RemoveComponentRelationship(Component component)
		{
			if (this._keyToComponentPoolItemIndexDict.TryGetValue(component.GetType().FullName,
				out var poolItemIndex))
				_componentPoolItemIndexList.Remove(poolItemIndex);
		}


		//主要作用是将IsDestroyed的Component从component_list中删除,配合Foreach的GetComponents使用
		private void _CheckDestroyedComponents()
		{
			for (int i = _componentPoolItemIndexList.Count - 1; i >= 0; i--)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = componentPoolItemIndex.GetValue<Component>();
				if (!component.IsDestroyed()) continue;
				_RemoveComponentRelationship(component);
				component.Despawn();
			}
		}
	}
}