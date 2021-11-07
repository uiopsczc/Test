using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class AbstractEntity
    {
        public AbstractComponent RemoveComponent(AbstractComponent component)
        {
            if (component.IsDestroyed())
                return null;
            component.Destroy();
            if (!this.isNotDeleteComponentRelationShipImmediately)
            {
                _RemoveComponentRelationship(component);
                _DespawnComponentKey(component);
                component.Despawn();
            }
            else
                this._MarkHasDestroyedComponent();

            return component;
        }

        public AbstractComponent RemoveComponent(string componentKey)
        {
            if (!this.keyToComponentDict.ContainsKey(componentKey))
                return null;
            return RemoveComponent(this.keyToComponentDict[componentKey]);
        }

        public AbstractComponent RemoveComponent(Type componentType)
        {
            var component = this.GetComponent(componentType);
            if (component != null)
                this.RemoveComponent(component);
            return component;
        }

        public T RemoveComponent<T>() where T : AbstractComponent
        {
            return RemoveComponent(typeof(T)) as T;
        }

        public AbstractComponent RemoveComponentStrictly(Type componentType)
        {
            var component = this.GetComponentStrictly(componentType);
            if (component != null)
                RemoveComponent(component);
            return component;
        }

        public T RemoveComponentStrictly<T>() where T : AbstractComponent
        {
            return (T) RemoveComponentStrictly(typeof(T));
        }

        public AbstractComponent[] RemoveComponents(Type componentType)
        {
            var components = this.GetComponents(componentType);
            if (!components.IsNullOrEmpty())
            {
                for (var i = 0; i < components.Length; i++)
                {
                    var component = components[i];
                    this.RemoveComponent(component);
                }
            }

            return components;
        }

        public T[] RemoveComponents<T>() where T : AbstractComponent
        {
            return (T[]) RemoveComponents(typeof(T));
        }


        public AbstractComponent[] RemoveComponentsStrictly(Type componentType)
        {
            var components = this.GetComponentsStrictly(componentType);
            if (!components.IsNullOrEmpty())
            {
                for (var i = 0; i < components.Length; i++)
                {
                    var component = components[i];
                    this.RemoveComponent(component);
                }
            }

            return components;
        }

        public T[] RemoveComponentsStrictly<T>() where T : AbstractComponent
        {
            return (T[]) RemoveComponentsStrictly(typeof(T));
        }

        public void RemoveAllComponents()
        {
            var toRemoveComponentKeyList = PoolCatManagerUtil.Spawn<List<string>>();
            toRemoveComponentKeyList.Capacity = this.componentKeyList.Count;
            toRemoveComponentKeyList.AddRange(componentKeyList);
            for (var i = 0; i < toRemoveComponentKeyList.Count; i++)
            {
                var componentKey = toRemoveComponentKeyList[i];
                RemoveComponent(componentKey);
            }

            toRemoveComponentKeyList.Clear();
            PoolCatManagerUtil.Despawn(toRemoveComponentKeyList);
        }

        ////////////////////////////////////////////////////////////////////
        private void _MarkHasDestroyedComponent()
        {
            if (!this.isHasDestroyedComponent)
            {
                this.isHasDestroyedComponent = true;
                _parent?._MarkHasDestroyedChildComponent();
            }
        }

        private void _MarkHasDestroyedChildComponent()
        {
            if (!this.isHasDestroyedChildComponent)
            {
                this.isHasDestroyedChildComponent = true;
                _parent?._MarkHasDestroyedChildComponent();
            }
        }

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
        private void __CheckDestroyedComponents()
        {
            string componentKey;
            AbstractComponent component;
            for (int i = componentKeyList.Count - 1; i >= 0; i--)
            {
                componentKey = componentKeyList[i];
                component = keyToComponentDict[componentKey];
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