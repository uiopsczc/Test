using System;
using System.CodeDom;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    public AbstractComponent AddComponent(AbstractComponent component, string component_key = null)
    {
      if (component_key != null)
        component.key = component_key;
      if (component.key != null && this.key_to_component_dict.ContainsKey(component.key))
      {
        LogCat.error("duplicate add component:", component.key, component.GetType());
        return null;
      }

      bool is_key_using_parent_idPool = component_key == null;
      if (is_key_using_parent_idPool)
      {
        component_key = component_key_idPool.Get().ToString();
        //再次检查键值
        if (this.key_to_component_dict.ContainsKey(component_key))
        {
          LogCat.error("duplicate add component:", component.key, component.GetType());
          return null;
        }
      }

      component.key = component_key;
      component.is_key_using_parent_idPool = is_key_using_parent_idPool;
      component.entity = this;


      __AddComponentRelationship(component);
      return component;
    }

    public AbstractComponent AddComponentWithoutInit(string component_key, Type component_type)
    {
      var component = PoolCatManagerUtil.Spawn(component_type) as AbstractComponent;
      return AddComponent(component, component_key);
    }

    public T AddComponentWithoutInit<T>(string component_key = null) where T : AbstractComponent
    {
      return AddComponentWithoutInit(component_key, typeof(T)) as T;
    }

    public AbstractComponent AddComponent(string component_key, Type component_type, params object[] init_args)
    {
      var component = AddComponentWithoutInit(component_key, component_type);
      if (component == null) //没有加成功
        return null;
      component.InvokeMethod("Init", false, init_args);
      component.PostInit();
      component.SetIsEnabled(true);
      return component;
    }

    public T AddComponent<T>(string component_key, params object[] init_args) where T : AbstractComponent
    {
      return AddComponent(component_key, typeof(T), init_args) as T;
    }

    void __AddComponentRelationship(AbstractComponent component)
    {
      key_to_component_dict[component.key] = component;
      type_to_componentList_dict.GetOrAddDefault(component.GetType(), () => PoolCatManagerUtil.Spawn<List<AbstractComponent>>())
        .Add(component);
      component_key_list.Add(component.key);
      if (!component_type_list.Contains(component.GetType()))
        component_type_list.Add(component.GetType());
    }
  }
}