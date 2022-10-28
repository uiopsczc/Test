using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		bool CheckCanAddComponentType(Type componentType)
		{
			var componentKey = componentType.FullName;
			if (this.keyToComponentPoolIndexDict.ContainsKey(componentKey))
			{
				LogCat.error("duplicate add component:", componentKey, componentType);
				return false;
			}
			return true;
		}
		protected Component _AddComponent(Type componentType, IPoolIndex componentPoolIndex)
		{
			var component = componentPoolIndex.GetValue() as Component;
			component.SetEntityPoolObjectIndex(this._poolObjectIndex);
			_AddComponentRelationship(componentType, componentPoolObjectIndex);
			return component;
		}

		public Component AddComponentWithoutInit(Type componentType)
		{
			if (CheckCanAddComponentType(componentType))
				return null;
			var (componentPoolItem, componentPoolIndex) = this.GetPoolManager().Spawn(componentType);
			return _AddComponent(componentType, componentPoolIndex);
		}

		public T AddComponentWithoutInit<T>() where T : Component
		{
			return AddComponentWithoutInit(typeof(T)) as T;
		}

		public Component AddComponent(Type componentType, params object[] initArgs)
		{
			var component = AddComponentWithoutInit(componentType);
			if (component == null) //没有加成功
				return null;
			component.InvokeMethod("Init", false, initArgs);
			component.PostInit();
			component.SetIsEnabled(true);
			return component;
		}

		public T AddComponent<T>(params object[] initArgs) where T : Component
		{
			return AddComponent(typeof(T), initArgs) as T;
		}

		void _AddComponentRelationship(Type componentType, PoolObjectIndex componentPoolObjectIndex)
		{
			var key = componentType.FullName;
			keyToComponentPoolIndexDict[key] = componentPoolObjectIndex;
			componentPoolIndexList.Add(componentPoolObjectIndex);
		}
	}
}