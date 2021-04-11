using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    public AbstractComponent GetComponent(string component_key)
    {
      if (!this.key_to_component_dict.ContainsKey(component_key))
        return null;
      if (this.key_to_component_dict[component_key].IsDestroyed())
        return null;
      return this.key_to_component_dict[component_key];
    }

    public T GetComponent<T>(string component_key) where T : AbstractComponent
    {
      return GetComponent(component_key) as T;
    }

    public AbstractComponent GetComponent(Type component_type)
    {
      foreach (var component in ForeachComponent(component_type))
        return component;
      return null;
    }

    public T GetComponent<T>() where T : AbstractComponent
    {
      return GetComponent(typeof(T)) as T;
    }

    //效率问题引入的
    public AbstractComponent GetComponentStrictly(Type component_type)
    {
      if (!this.type_to_componentList_dict.ContainsKey(component_type))
        return null;
      foreach (var component in type_to_componentList_dict[component_type])
      {
        if (!component.IsDestroyed())
          return component;
      }
      return null;
    }

    public T GetComponentStrictly<T>() where T : AbstractComponent
    {
      return GetComponentStrictly(typeof(T)) as T;
    }



    public AbstractComponent[] GetComponents(Type component_type)
    {
      List<AbstractComponent> list = new List<AbstractComponent>();
      foreach (var component in ForeachComponent(component_type))
        list.Add(component);
      return list.ToArray();
    }

    public T[] GetComponents<T>() where T : AbstractComponent
    {
      return (T[])GetComponents(typeof(T));
    }
    

    public AbstractComponent[] GetComponentsStrictly(Type component_type)
    {
      List<AbstractComponent> list = new List<AbstractComponent>();
      if (!this.type_to_componentList_dict.ContainsKey(component_type))
        return list.ToArray();
      foreach (var component in type_to_componentList_dict[component_type])
      {
        if (!component.IsDestroyed())
          list.Add(component);
      }
      return list.ToArray();
    }

    public T[] GetComponentsStrictly<T>() where T : AbstractComponent
    {
      return (T[])GetComponentsStrictly(typeof(T));
    }
  }
}