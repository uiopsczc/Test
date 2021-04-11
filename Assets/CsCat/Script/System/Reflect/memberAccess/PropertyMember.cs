using System;
using System.Reflection;

namespace CsCat
{
  /// <summary>
  ///   PropertyMember
  /// </summary>
  public sealed class PropertyMember : MemberAccessor
  {
    #region field

    private readonly PropertyInfo propertyInfo;

    #endregion


    #region ctor

    public PropertyMember(PropertyInfo propertyInfo)
    {
      this.propertyInfo = propertyInfo;
      getter = container => propertyInfo.GetValue(container, null); //设置getter方法
      setter = (container, value) => { propertyInfo.SetValue(container, value, null); }; //设置setter方法
    }

    #endregion

    #region property

    /// <summary>
    ///   该属性类型
    /// </summary>
    public override Type member_type => propertyInfo.PropertyType;

    /// <summary>
    ///   该属性的信息
    /// </summary>
    public override MemberInfo memberInfo => propertyInfo;

    #endregion
  }
}