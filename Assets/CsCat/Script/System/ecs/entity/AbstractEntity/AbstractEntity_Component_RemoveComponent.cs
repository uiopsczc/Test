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
      if (!this.is_not_delete_component_relationShip_immediately)
      {
        __RemoveComponentRelationship(component);
        __DespawnComponentKey(component);
        component.Despawn();
      }
      else
        this.__MarkHasDestroyedComponent();
      return component;
    }

    public AbstractComponent RemoveComponent(string component_key)
    {
      if (!this.key_to_component_dict.ContainsKey(component_key))
        return null;
      return RemoveComponent(this.key_to_component_dict[component_key]);
    }
    
    public AbstractComponent RemoveComponent(Type component_type)
    {
      var component = this.GetComponent(component_type);
      if (component != null)
        this.RemoveComponent(component);
      return component;
    }

    public T RemoveComponent<T>() where T : AbstractComponent
    {
      return RemoveComponent(typeof(T)) as T;
    }

    public AbstractComponent RemoveComponentStrictly(Type component_type)
    {
      var component = this.GetComponentStrictly(component_type);
      if (component != null)
        RemoveComponent(component);
      return component;
    }

    public T RemoveComponentStrictly<T>() where T : AbstractComponent
    {
      return (T)RemoveComponentStrictly(typeof(T));
    }

    public AbstractComponent[] RemoveComponents(Type component_type)
    {
      var components = this.GetComponents(component_type);
      if (!components.IsNullOrEmpty())
      {
        foreach (var component in components)
          this.RemoveComponent(component);
      }
      return components;
    }

    public T[] RemoveComponents<T>() where T : AbstractComponent
    {
      return (T[])RemoveComponents(typeof(T));
    }
    

    public AbstractComponent[] RemoveComponentsStrictly(Type component_type)
    {
      var components = this.GetComponentsStrictly(component_type);
      if (!components.IsNullOrEmpty())
      {
        foreach (var component in components)
          this.RemoveComponent(component);
      }
      return components;
    }

    public T[] RemoveComponentsStrictly<T>() where T : AbstractComponent
    {
      return (T[])RemoveComponentsStrictly(typeof(T));
    }

    public void RemoveAllComponents()
    {
      var to_remove_component_key_list = PoolCatManagerUtil.Spawn<List<string>>();
      to_remove_component_key_list.Capacity = this.component_key_list.Count;
      to_remove_component_key_list.AddRange(component_key_list);
      foreach (var component_key in to_remove_component_key_list)
        RemoveComponent(component_key);
      to_remove_component_key_list.Clear();
      PoolCatManagerUtil.Despawn(to_remove_component_key_list);
    }

    ////////////////////////////////////////////////////////////////////
    private void __MarkHasDestroyedComponent()
    {
      if (!this.is_has_destroyed_component)
      {
        this.is_has_destroyed_component = true;
        _parent?.__MarkHasDestroyedChildComponent();
      }
    }

    private void __MarkHasDestroyedChildComponent()
    {
      if (!this.is_has_destroyed_child_component)
      {
        this.is_has_destroyed_child_component = true;
        _parent?.__MarkHasDestroyedChildComponent();
      }
    }

    private void __RemoveComponentRelationship(AbstractComponent component)
    {
      this.key_to_component_dict.Remove(component.key);
      this.type_to_componentList_dict[component.GetType()].Remove(component);
      this.component_key_list.Remove(component.key);
    }

    private void __DespawnComponentKey(AbstractComponent component)
    {
      if (component.is_key_using_parent_idPool)
      {
        component_key_idPool.Despawn(component.key);
        component.is_key_using_parent_idPool = false;
      }
    }


    //主要作用是将IsDestroyed的Component从component_list中删除,配合Foreach的GetComponents使用
    private void __CheckDestroyedComponents()
    {
      string component_key;
      AbstractComponent component;
      for (int i = component_key_list.Count - 1; i >= 0; i--)
      {
        component_key = component_key_list[i];
        component = key_to_component_dict[component_key];
        if (component.IsDestroyed())
        {
          __RemoveComponentRelationship(component);
          __DespawnComponentKey(component);
          component.Despawn();
        }
      }
    }

  }
}