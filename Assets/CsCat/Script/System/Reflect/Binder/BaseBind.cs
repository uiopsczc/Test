using UnityEngine;

namespace CsCat
{
  /// <summary>
  /// 绑定基类
  /// </summary>
  public class BaseBind : MonoBehaviour
  {
    #region property

    /// <summary>
    /// propBinder
    /// </summary>
    public PropBinder propBinder { get; private set; }

    #endregion

    #region ctor

    /// <summary>
    /// 该构造函数会先于Awake
    /// </summary>
    public BaseBind()
    {
      this.propBinder = new PropBinder(this);
    }

    #endregion

    #region virtual method

    /// <summary>
    /// 对属性进行绑定
    /// </summary>
    /// <param name="property_owner"></param>
    /// <param name="property_name"></param>
    /// <returns></returns>
    public virtual BaseBind Bind(object property_owner, string property_name)
    {
      this.propBinder.Bind(property_owner, property_name,
        (prop_name, old_value, new_value) =>
        {
          this.OnValueChanged(this.propBinder.GetPropName(), old_value, new_value);
        });
      return this;
    }

    /// <summary>
    /// 属性的值改变的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal virtual void OnValueChanged(string property_name, object old_value, object new_value)
    {
    }


    /// <summary>
    /// 销毁的时候取消绑定
    /// </summary>
    protected virtual void OnDestroy()
    {
      this.propBinder.OnDestroy();
    }

    /// <summary>
    /// enable的时候，如果之前没绑定过的，进行绑定
    /// </summary>
    protected virtual void OnEnable()
    {
      this.propBinder.OnEnable();
    }

    #endregion


  }
}


