using System;

namespace CsCat
{
  public interface IPropertyObserver
  {
    /// <summary>
    /// 添加propertyName属性更改listener
    /// </summary>
    /// <param name="name"></param>
    /// <param name="listener"></param>
    void AddPropertyChangedListener(string property_name, Action<string, object, object> listener);

    /// <summary>
    /// 获得propertyName属性的值
    /// </summary>
    /// <param name="property_name"></param>
    /// <returns></returns>
    object GetPropertyValue(string property_name);

    /// <summary>
    /// 移除propertyName属性的listener
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="listener"></param>
    void RemovePropertyChangedListener(string property_name, Action<string, object, object> listener);
  }
}

