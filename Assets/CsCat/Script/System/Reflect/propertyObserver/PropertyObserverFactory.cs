using System;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   属性观察者工厂
  /// </summary>
  public class PropertyObserverFactory
  {
    #region field

    /// <summary>
    ///   所有的属性观察者
    /// </summary>
    private static readonly Dictionary<Type, Func<object, IPropertyObserver>> observer_dict =
      new Dictionary<Type, Func<object, IPropertyObserver>>();

    #endregion

    #region static method

    /// <summary>
    ///   创建属性观察者
    /// </summary>
    /// <param name="prop_owner"></param>
    /// <returns></returns>
    public static IPropertyObserver CreatePropertyObserver(object prop_owner)
    {
      if (prop_owner == null) return null;
      foreach (var current in observer_dict)
        if (current.Key.IsAssignableFrom(prop_owner.GetType()))
          return current.Value(prop_owner);
      return null;
    }

    /// <summary>
    ///   对type注册属性观察者PropertyObserver
    /// </summary>
    /// <param name="type"></param>
    /// <param name="creator"></param>
    public static void RegistPropertyObserver(Type type, Func<object, IPropertyObserver> creator)
    {
      observer_dict[type] = creator;
    }

    /// <summary>
    ///   移除对type注册属性观察者PropertyObserver
    /// </summary>
    /// <param name="type"></param>
    public static void UnregistPropertyObserver(Type type)
    {
      if (observer_dict.ContainsKey(type)) observer_dict[type] = null;
    }

    #endregion
  }
}