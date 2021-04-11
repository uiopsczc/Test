using System;
using System.Reflection;

namespace CsCat
{
  /// <summary>
  ///   FieldMember
  /// </summary>
  public sealed class FieldMember : MemberAccessor
  {
    #region field

    private readonly FieldInfo fieldInfo;

    #endregion

    #region ctor

    public FieldMember(FieldInfo fieldInfo)
    {
      this.fieldInfo = fieldInfo;
      getter = owner => fieldInfo.GetValue(owner); //设置getter方法
      setter = (owner, value) => { fieldInfo.SetValue(owner, value); }; //设置setter方法
    }

    #endregion

    #region property

    /// <summary>
    ///   该属性类型
    /// </summary>
    public override Type member_type => fieldInfo.FieldType;

    /// <summary>
    ///   该属性的信息
    /// </summary>
    public override MemberInfo memberInfo => fieldInfo;

    #endregion
  }
}