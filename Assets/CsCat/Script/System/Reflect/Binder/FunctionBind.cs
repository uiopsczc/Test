using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  /// 绑定的propertyName属性的值改变的时候触发mBindedFunc的调用
  /// 需要手动设置绑定的属性 Bind(object propertyOwner, string propertyName);
  /// 需要调用SetFunc来设置属性改变时候的调用的方法
  /// </summary>
  public class FunctionBind : BaseBind
  {
    #region delegate

    /// <summary>
    /// 绑定的propertyName属性的值改变的时候触发mBindedFunc的调用
    /// </summary>
    private Action<GameObject, object, object> bindedFunc;

    #endregion

    #region override method

    /// <summary>
    /// 绑定的propertyName属性的值改变的时候触发mBindedFunc的调用
    /// </summary>
    /// <param name="func"></param>
    public void SetFunc(Action<GameObject, object, object> func)
    {
      this.bindedFunc = func;
    }

    /// <summary>
    /// 属性的值改变的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal override void OnValueChanged(string property_name, object old_value, object new_value)
    {
      if (this.bindedFunc != null)
      {
        this.bindedFunc(gameObject, old_value, new_value);
      }
    }

    #endregion

  }
}

