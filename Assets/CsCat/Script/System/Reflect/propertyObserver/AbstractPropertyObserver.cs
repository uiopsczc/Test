using System;

namespace CsCat
{
  /// <summary>
  ///   抽象属性观察者
  /// </summary>
  public abstract class AbstractPropertyObserver : IPropertyObserver
  {
    #region field

    /// <summary>
    ///   属性的拥有者
    /// </summary>
    protected object prop_owner;

    #endregion

    #region ctor

    public AbstractPropertyObserver(object prop_owner)
    {
      this.prop_owner = prop_owner;
    }

    #endregion

    #region virtual method

    /// <summary>
    ///   获得propertyName属性的值
    ///   先查fieldInfo，再查property
    /// </summary>
    /// <param name="property_name"></param>
    /// <returns></returns>
    public virtual object GetPropertyValue(string property_name)
    {
      var type = prop_owner.GetType();
      var fieldInfo = type.GetFieldInfo(property_name, BindingFlagsConst.Instance);
      if (fieldInfo != null) return fieldInfo.GetValue(prop_owner);
      var propertyInfo = type.GetPropertyInfo(property_name, BindingFlagsConst.Instance);
      if (propertyInfo != null) return propertyInfo.GetValue(prop_owner, null);
      return null;
    }

    #endregion

    #region abstract method

    /// <summary>
    ///   添加propertyName属性更改listener
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    public abstract void AddPropertyChangedListener(string property_name, Action<string, object, object> listener);

    /// <summary>
    ///   移除propertyName属性的listener
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    public abstract void RemovePropertyChangedListener(string property_name, Action<string, object, object> listener);

    #endregion
  }
}