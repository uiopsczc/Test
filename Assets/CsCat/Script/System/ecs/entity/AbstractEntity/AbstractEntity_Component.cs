using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    protected Dictionary<string, AbstractComponent> key_to_component_dict = new Dictionary<string, AbstractComponent>();

    protected Dictionary<Type, List<AbstractComponent>> type_to_componentList_dict =
      new Dictionary<Type, List<AbstractComponent>>();

    protected List<string> component_key_list = new List<string>();
    protected List<Type> component_type_list = new List<Type>();
    protected IdPool component_key_idPool = new IdPool();

    //////////////////////////////////////////////////////////////////////
    // 按加入的顺序遍历
    //////////////////////////////////////////////////////////////////////
    //按加入的顺序遍历
    public IEnumerable<AbstractComponent> ForeachComponent()
    {
      AbstractComponent component;
      for (int i = 0; i < component_key_list.Count; i++)
      {
        component = GetComponent(component_key_list[i]);
        if (component != null)
          yield return component;
      }
    }

    public IEnumerable<AbstractComponent> ForeachComponent(Type component_type)
    {
      AbstractComponent component = null;
      for (int i = 0; i < component_key_list.Count; i++)
      {
        component = GetComponent(component_key_list[i]);
        if (component != null&& component_type.IsInstanceOfType(component))
          yield return component;
      }
    }

    public IEnumerable<T> ForeachComponent<T>() where T : AbstractComponent
    {
      T component = null;
      for (int i = 0; i < component_key_list.Count; i++)
      {
        component = GetComponent(component_key_list[i]) as T;
        if (component != null)
          yield return component;
      }
    }
    
    

    void __OnDespawn_Component()
    {
      key_to_component_dict.Clear();
      foreach (var componentList in type_to_componentList_dict.Values)
      {
        componentList.Clear();
        PoolCatManagerUtil.Despawn(componentList);
      }
      type_to_componentList_dict.Clear();
      component_key_list.Clear();
      component_type_list.Clear();
    }
  }
}