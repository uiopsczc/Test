using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public bool RemoveComponent(AbstractComponent component)
		{
			if (component.IsDestroyed())
				return false;
			component.DoDestroy();
			_RemoveComponentRelationship(component);
			_DespawnComponentKey(component);
			component.Despawn();
			return true;
		}

		public bool RemoveComponent(string componentKey)
		{
			if (!this.keyToComponentDict.ContainsKey(componentKey))
				return false;
			return RemoveComponent(this.keyToComponentDict[componentKey]);
		}

		public bool RemoveComponent(Type componentType)
		{
			var component = this.GetComponent(componentType);
			if (component != null)
			{
				this.RemoveComponent(component);
				return true;
			}
			return false;
		}

		public bool RemoveComponent<T>() where T : AbstractComponent
		{
			return RemoveComponent(typeof(T));
		}

		public bool RemoveComponentStrictly(Type componentType)
		{
			var component = this.GetComponentStrictly(componentType);
			if (component != null)
			{
				RemoveComponent(component);
				return true;
			}
			return false;
		}

		public bool RemoveComponentStrictly<T>() where T : AbstractComponent
		{
			return RemoveComponentStrictly(typeof(T));
		}

		public bool RemoveComponents(Type componentType)
		{
			var components = this.GetComponents(componentType);
			if (!components.IsNullOrEmpty())
			{
				for (var i = 0; i < components.Length; i++)
				{
					var component = components[i];
					this.RemoveComponent(component);
				}
				return true;
			}

			return false;
		}

		public bool RemoveComponents<T>() where T : AbstractComponent
		{
			return RemoveComponents(typeof(T));
		}


		public bool RemoveComponentsStrictly(Type componentType)
		{
			var components = this.GetComponentsStrictly(componentType);
			if (!components.IsNullOrEmpty())
			{
				for (var i = 0; i < components.Length; i++)
				{
					var component = components[i];
					this.RemoveComponent(component);
				}
				return true;
			}
			return false;
		}

		public bool RemoveComponentsStrictly<T>() where T : AbstractComponent
		{
			return RemoveComponentsStrictly(typeof(T));
		}

		public void RemoveAllComponents()
		{
			for (var i = 0; i < componentKeyList.Count; i++)
			{
				var componentKey = componentKeyList[i];
				if (RemoveComponent(componentKey))
					i--;
			}
		}

		////////////////////////////////////////////////////////////////////
		

		private void _RemoveComponentRelationship(AbstractComponent component)
		{
			this.keyToComponentDict.Remove(component.key);
			this.typeToComponentListDict[component.GetType()].Remove(component);
			this.componentKeyList.Remove(component.key);
		}

		private void _DespawnComponentKey(AbstractComponent component)
		{
			if (component.isKeyUsingParentIdPool)
			{
				componentKeyIdPool.Despawn(component.key);
				component.isKeyUsingParentIdPool = false;
			}
		}


		//主要作用是将IsDestroyed的Component从component_list中删除,配合Foreach的GetComponents使用
		private void _CheckDestroyedComponents()
		{
			for (int i = componentKeyList.Count - 1; i >= 0; i--)
			{
				var componentKey = componentKeyList[i];
				var component = keyToComponentDict[componentKey];
				if (component.IsDestroyed())
				{
					_RemoveComponentRelationship(component);
					_DespawnComponentKey(component);
					component.Despawn();
				}
			}
		}
	}
}