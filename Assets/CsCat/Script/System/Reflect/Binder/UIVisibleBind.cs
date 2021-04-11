using System;
using UnityEngine;

namespace CsCat
{
  public class UIVisibleBind : BaseBind
  {
    #region field

    private CanvasRenderer _canvasRenderer;

    /// <summary>
    /// 上次的值
    /// </summary>
    private object last_value;

    #endregion

    #region property

    private CanvasRenderer canvasRenderer
    {
      get
      {
        if (this._canvasRenderer == null)
        {
          this._canvasRenderer = base.GetComponent<CanvasRenderer>();
        }

        return _canvasRenderer;
      }
    }

    #endregion

    #region virtual method

    /// <summary>
    /// 属性的值改变的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal override void OnValueChanged(string property_name, object old_value, object new_value)
    {
      this.last_value = new_value;

      bool is_active = this.last_value != null && Convert.ToBoolean(this.last_value);
      if (this.canvasRenderer != null) //如果canvasRenderer直接设置alpha
      {
        this.canvasRenderer.SetAlpha(is_active ? 1 : 0);
        return;
      }
      else //否则设置gameObject的active
      {
        base.gameObject.SetActive(is_active);
      }
    }

    /// <summary>
    /// enable的时候，如果之前没绑定过的，进行绑定
    /// 根据lastValue设置canvasRenderer的alpha
    /// </summary>
    protected override void OnEnable()
    {
      base.OnEnable();
      bool is_active = this.last_value != null && Convert.ToBoolean(this.last_value);
      if (this.canvasRenderer != null)
      {
        this.canvasRenderer.SetAlpha(is_active ? 1 : 0);
      }
    }

    #endregion



  }
}