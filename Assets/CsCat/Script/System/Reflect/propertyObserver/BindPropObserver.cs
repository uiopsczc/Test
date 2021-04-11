using System;

namespace CsCat
{
  /// <summary>
  ///   绑定属性观察者
  /// </summary>
  public class BindPropObserver : AbstractPropertyObserver
  {
    #region ctor

    public BindPropObserver(object prop_owner) : base(prop_owner)
    {
    }

    #endregion

    #region override method

    /// <summary>
    ///   添加propertyName属性更改listener
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    public override void AddPropertyChangedListener(string property_name, Action<string, object, object> listener)
    {
      ((PropObserver) prop_owner).AddPropListener(property_name, listener);
    }

    /// <summary>
    ///   移除propertyName属性的listener
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    public override void RemovePropertyChangedListener(string property_name, Action<string, object, object> listener)
    {
      ((PropObserver) prop_owner).RemovePropListener(property_name, listener);
    }

    #endregion
  }
}