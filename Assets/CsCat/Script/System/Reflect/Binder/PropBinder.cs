using System;

namespace CsCat
{
  /// <summary>
  /// 单个属性绑定
  /// </summary>
  public class PropBinder
  {
    #region field

    /// <summary>
    /// 要被绑定的属性
    /// </summary>
    protected string prop_name;

    /// <summary>
    /// 是否已经
    /// </summary>
    protected bool is_registed;

    /// <summary>
    /// 属性观察者
    /// </summary>
    protected IPropertyObserver propertyObserver;

    /// <summary>
    /// binder
    /// </summary>
    protected object binder;

    #endregion

    #region property

    /// <summary>
    /// getter代理
    /// </summary>
    public Func<object, object> GetterProxy { get; set; }

    /// <summary>
    /// 属性观察者
    /// </summary>
    public IPropertyObserver PropertyObserver
    {
      get { return this.propertyObserver; }
    }

    #endregion

    #region delegate

    /// <summary>
    /// 值被改变时候的调用
    /// Register中会对其进行observer的listener的引用
    /// </summary>
    protected Action<string, object, object> valueChangedHandler;

    #endregion

    #region ctor

    public PropBinder(object binder)
    {
      this.binder = binder;
    }

    #endregion

    #region public method

    /// <summary>
    /// 对属性进行绑定
    /// </summary>
    /// <param name="prop_owner"></param>
    /// <param name="prop_name"></param>
    /// <param name="handler"></param>
    public void Bind(object prop_owner, string prop_name, Action<string, object, object> handler)
    {
      IPropertyObserver propertyObserver = PropertyObserverFactory.CreatePropertyObserver(prop_owner);
      if (propertyObserver == null)
      {
        return;
      }

      this.Bind(prop_name, handler, propertyObserver);
    }

    /// <summary>
    /// 获取propName
    /// </summary>
    /// <returns></returns>
    public string GetPropName()
    {
      return this.prop_name;
    }

    /// <summary>
    /// 销毁的时候取消绑定
    /// </summary>
    public void OnDestroy()
    {
      this.Unbind();
    }

    /// <summary>
    /// enable的时候，如果之前没绑定过的，进行绑定
    /// </summary>
    public void OnEnable()
    {
      if (!this.is_registed)
      {
        this.Regist();
      }
    }

    /// <summary>
    /// 取消属性绑定
    /// </summary>
    public void Unbind()
    {
      this.Unregist();
      this.propertyObserver = null;
      this.prop_name = null;
    }

    #endregion

    #region protected method

    /// <summary>
    /// 注册对应mPropName属性的listener
    /// </summary>
    /// <returns></returns>
    protected bool Regist()
    {
      this.is_registed = true;
      if (this.propertyObserver != null)
      {
        this.propertyObserver.AddPropertyChangedListener(this.prop_name, this.OnValueChanged);
        return true;
      }

      return false;
    }

    /// <summary>
    /// 注册对应mPropName属性的listener
    /// </summary>
    /// <returns></returns>
    protected bool Unregist()
    {
      this.is_registed = false;
      if (this.propertyObserver != null)
      {
        this.propertyObserver.RemovePropertyChangedListener(this.prop_name, this.OnValueChanged);
        return true;
      }

      return false;
    }

    #endregion

    #region private method

    /// <summary>
    /// 对属性进行绑定【私有】
    /// </summary>
    /// <param name="prop_name"></param>
    /// <param name="handler"></param>
    /// <param name="observer"></param>
    private void Bind(string prop_name, Action<string, object, object> handler, IPropertyObserver observer)
    {
      this.Unregist();
      this.propertyObserver = observer;
      this.prop_name = prop_name;
      this.valueChangedHandler = handler;
      this.Regist();
      CheckBinderIsValideObject();
    }

    /// <summary>
    /// 属性的值改变的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    private void OnValueChanged(string property_name, object old_value, object new_value)
    {
      if (!CheckBinderIsValideObject())
      {
        return;
      }

      if (this.GetterProxy != null)
      {
        new_value = this.GetterProxy(new_value);
      }

      this.valueChangedHandler(this.prop_name, old_value, new_value);
    }

    /// <summary>
    /// 检查binder是不是有效
    /// </summary>
    /// <returns></returns>
    bool CheckBinderIsValideObject()
    {
      if (!this.binder.IsValidObject())
      {
        this.Unregist();
        return false;
      }

      return true;
    }

    #endregion


  }
}


