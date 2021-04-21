using System;
using System.Reflection;

namespace CsCat
{
  public partial class MemberAccessorPool
  {
    /// <summary>
    ///   calssType 和 bindingFlags  组成的唯一键值
    /// </summary>
    public class MemberAccessorClasssType
    {
      #region ctor

      public MemberAccessorClasssType(Type class_type, BindingFlags bindingFlags)
      {
        this.class_type = class_type;
        this.bindingFlags = bindingFlags;
      }

      #endregion

      #region field

      /// <summary>
      ///   类型
      /// </summary>
      public Type class_type;

      /// <summary>
      ///   BindingFlags
      /// </summary>
      public BindingFlags bindingFlags;

      #endregion

      #region override method

      public override bool Equals(object obj)
      {
        if (obj == null || GetType() != obj.GetType()) return false;
        var other = (MemberAccessorClasssType)obj;
        return class_type.Equals(other.class_type) && bindingFlags.Equals(other.bindingFlags);
      }

      public override int GetHashCode()
      {
        return ObjectUtil.GetHashCode(class_type, bindingFlags);
      }

      #endregion
    }
  }
}