using System;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   属性观察者
  /// </summary>
  public class PropObserver : IPropNotify
  {
    #region field

    /// <summary>
    ///   该类的全部属性listener
    /// </summary>
    protected Dictionary<string, List<Action<string, object, object>>> prop_listener_dict =
      new Dictionary<string, List<Action<string, object, object>>>();

    #endregion

    #region ctor

    static PropObserver()
    {
      PropertyObserverFactory.RegistPropertyObserver(typeof(PropObserver),
        obj => { return new BindPropObserver(obj); });
    }

    #endregion

    #region public method

    public virtual void OnNewCreate()
    {
    }

    public virtual void OnLoaded()
    {
    }

    /// <summary>
    ///   添加propertyName属性的listener
    ///   添加到propListenerDict中
    ///   不会重复添加
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    public void AddPropListener(string property_name, Action<string, object, object> listener)
    {
      List<Action<string, object, object>> list;
      if (prop_listener_dict.TryGetValue(property_name, out list)) //防止重复添加
      {
        list.Add(listener);
        return;
      }

      list = new List<Action<string, object, object>>();
      list.Add(listener);
      prop_listener_dict.Add(property_name, list);
    }

    /// <summary>
    ///   通知propertyName属性更改了
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value">更改前的值</param>
    /// <param name="new_value">更改后的值</param>
    public void NotifyPropChanged(string property_name, object old_value, object new_value)
    {
      List<Action<string, object, object>> list;
      if (prop_listener_dict.TryGetValue(property_name, out list))
        for (var i = 0; i < list.Count; i++)
        {
          var listener = list[i];
          if (listener != null) listener(property_name, old_value, new_value);
        }
    }

    /// <summary>
    ///   移除propertyName属性的listener
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    public void RemovePropListener(string property_name, Action<string, object, object> listener)
    {
      List<Action<string, object, object>> list;
      if (prop_listener_dict.TryGetValue(property_name, out list)) list.Remove(listener);
    }

    #region edit by czq  //直接传当前类名作为Name

    public void AddClassListener(Action<string, object, object> listener)
    {
      AddPropListener(GetType().Name, listener);
    }

    public void RemoveClassListener(Action<string, object, object> listener)
    {
      RemovePropListener(GetType().Name, listener);
    }

    public void NotifyClassChange()
    {
      NotifyPropChanged(GetType().Name, this, this);
    }

    public void RemoveAllListeners()
    {
      prop_listener_dict.Clear();
    }

    #endregion

    #endregion
  }
}