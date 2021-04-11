using System;
using System.Reflection;

namespace CsCat
{
  /// <summary>
  /// 当绑定的srcPropOwner的srcPropName属性的值改变的时候，
  /// dstPropOwner的dstPropName属性的值也设置为跟srcPropOwner的srcPropName属性一样的值
  /// </summary>
  public class PropertyBind : BaseBind
  {
    #region field

    /// <summary>
    /// dstPropOwner
    /// </summary>
    private object dst_prop_owner;

    /// <summary>
    /// dstFieldInfo
    /// </summary>
    private FieldInfo dst_fieldInfo;

    /// <summary>
    /// dstPropInfo
    /// </summary>
    private PropertyInfo dst_propInfo;

    #endregion

    #region virtual method

    /// <summary>
    /// 当srcPropOwner的srcPropName属性的值改变的时候，
    /// dstPropOwner的dstPropName属性的值也设置为跟srcPropOwner的srcPropName属性一样的值
    /// </summary>
    /// <param name="src_property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal override void OnValueChanged(string src_property_name, object old_value, object new_value)
    {
      if (this.dst_fieldInfo != null)
      {
        this.dst_fieldInfo.SetValue(this.dst_prop_owner, new_value);
        return;
      }

      if (this.dst_propInfo != null)
      {
        this.dst_propInfo.SetValue(this.dst_prop_owner, new_value, null);
      }
    }

    /// <summary>
    /// 当srcPropOwner的srcPropName属性的值改变的时候，
    /// dstPropOwner的dstPropName属性的值也设置为跟srcPropOwner的srcPropName属性一样的值
    /// </summary>
    /// <param name="src_prop_owner"></param>
    /// <param name="src_prop_name"></param>
    /// <param name="dst_prop_owner"></param>
    /// <param name="dst_prop_name"></param>
    /// <returns></returns>
    public virtual BaseBind Bind(object src_prop_owner, string src_prop_name, object dst_prop_owner,
      string dst_prop_name)
    {
      this.dst_prop_owner = dst_prop_owner;
      Type dst_type = this.dst_prop_owner.GetType();
      this.dst_fieldInfo = dst_type.GetFieldInfo(dst_prop_name);
      if (this.dst_fieldInfo == null)
      {
        this.dst_propInfo = dst_type.GetPropertyInfo(dst_prop_name);
      }

      base.propBinder.Bind(src_prop_owner, src_prop_name, OnValueChanged);
      return this;
    }

    #endregion

  }


}