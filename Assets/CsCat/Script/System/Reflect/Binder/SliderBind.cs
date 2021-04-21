using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  /// <summary>
  /// UISlider的value，maxValue的绑定
  /// 两个绑定 maxValueBinder和 curValueBinder  两个其中的改变都会调用Image的fillAmount
  /// fillAmount =curValueBinder的value /maxValueBinder的value
  /// </summary>
  public class SliderBind : MonoBehaviour
  {
    #region field

    private Slider _slider;

    #endregion

    #region property

    /// <summary>
    /// 最大maxValue绑定
    /// </summary>
    public PropBinder max_value_binder { get; private set; }

    /// <summary>
    /// 当前curValue绑定
    /// </summary>
    public PropBinder cur_value_binder { get; private set; }


    protected Slider silder
    {
      get
      {
        if (this._slider == null)
        {
          this._slider = base.GetComponent<Slider>();
        }

        return _slider;
      }
    }

    #endregion

    #region ctor

    public SliderBind()
    {
      this.max_value_binder = new PropBinder(this);
      this.cur_value_binder = new PropBinder(this);
    }

    #endregion

    #region public method

    /// <summary>
    /// 进行最大值的绑定
    /// </summary>
    /// <param name="max_prop_owner"></param>
    /// <param name="max_prop_name"></param>
    public void BindMaxValue(object max_prop_owner, string max_prop_name)
    {
      this.max_value_binder.Bind(max_prop_owner, max_prop_name, this.OnMaxValueChanged);
    }

    /// <summary>
    /// 进行当前值的绑定
    /// </summary>
    /// <param name="cur_value_prop_owner"></param>
    /// <param name="cur_value_prop_name"></param>
    public void BindCurValue(object cur_value_prop_owner, string cur_value_prop_name)
    {
      this.cur_value_binder.Bind(cur_value_prop_owner, cur_value_prop_name, this.OnCurValueChanged);
    }

    #endregion

    #region protected method

    /// <summary>
    /// 销毁的时候取消绑定
    /// </summary>
    protected virtual void OnDestroy()
    {
      this.cur_value_binder.OnDestroy();
      this.max_value_binder.OnDestroy();
    }

    /// <summary>
    /// enable的时候，如果之前没绑定过的，进行绑定
    /// </summary>
    protected virtual void OnEnable()
    {
      this.cur_value_binder.OnEnable();
      this.max_value_binder.OnEnable();
    }

    #endregion

    #region private method

    /// <summary>
    /// 最大值变动的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    private void OnMaxValueChanged(string property_name, object old_value, object new_value)
    {
      this.silder.maxValue = (float)Convert.ToDouble(new_value);
    }

    /// <summary>
    /// 当前值变动的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    private void OnCurValueChanged(string property_name, object old_value, object new_value)
    {
      this.silder.value = (float)Convert.ToDouble(new_value);
    }

    #endregion

  }
}