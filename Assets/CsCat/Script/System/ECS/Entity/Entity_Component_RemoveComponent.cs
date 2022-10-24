using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		protected bool _RemoveComponent(PoolObjectIndex componentPoolObjectIndex)
		{
			var component = _GetComponent(componentPoolObjectIndex);
			if (component==null)
				return false;
			component.DoDestroy();
			_RemoveComponentRelationship(component);
			component.Despawn();
			return true;
		}

		public bool RemoveComponent(string componentKey)
		{
			if (this.keyToComponentPoolObjectIndexDict.TryGetValue(componentKey, out var componentPoolObjectIndex))
				return _RemoveComponent(componentPoolObjectIndex);
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
			for (var i = 0; i < componentPoolObjectIndexList.Count; i++)
			{
				var componentPoolObjectIndex = componentPoolObjectIndexList[i];
				if (_RemoveComponent(componentPoolObjectIndex))
					i--;
			}
		}

		////////////////////////////////////////////////////////////////////
		

		private void _RemoveComponentRelationship(Component component)
		{
			if (this.keyToComponentPoolObjectIndexDict.TryGetValue(component.GetType().FullName,
				out var poolObjectIndex))
				componentPoolObjectIndexList.Remove(poolObjectIndex);
		}


		//主要作用是将IsDestroyed的Component从component_list中删除,配合Foreach的GetComponents使用
		private void _CheckDestroyedComponents()
		{
			for (int i = componentPoolObjectIndexList.Count - 1; i >= 0; i--)
			{
				var componentPoolObjectIndex = componentPoolObjectIndexList[i];
				var component = componentPoolObjectIndex.GetValue<Component>();
				if (component.IsDestroyed())
				{
					_RemoveComponentRelationship(component);
					component.Despawn();
				}
			}
		}
	}
}