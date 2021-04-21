using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  /// <summary>
  /// UIImage的fillAmount绑定
  /// 两个绑定 maxValueBinder和 curValueBinder  两个其中的改变都会调用Image的fillAmount
  /// fillAmount =curValueBinder的value /maxValueBinder的value
  /// </summary>
  public class ImageFillBind : MonoBehaviour
  {
    #region field

    private Image _image;
    private float cur_value;
    private float max_value;

    #endregion

    #region property

    private Image image
    {
      get
      {
        if (this._image == null)
        {
          this._image = base.GetComponent<Image>();
        }

        return this._image;
      }
    }

    /// <summary>
    /// 最大fillAmount绑定
    /// </summary>
    public PropBinder max_value_binder { get; private set; }

    /// <summary>
    /// 当前fillAmount绑定
    /// </summary>
    public PropBinder cur_value_binder { get; private set; }

    #endregion

    #region virtual method

    /// <summary>
    /// 销毁的时候取消绑定
    /// </summary>
    protected virtual void OnDestroy()
    {
      this.cur_value_binder.OnDestroy();
      this.max_value_binder.OnDestroy();
    }

    /// <summary>
    ///  enable的时候，如果之前没绑定过的，进行绑定
    /// </summary>
    protected virtual void OnEnable()
    {
      this.cur_value_binder.OnEnable();
      this.max_value_binder.OnEnable();
    }

    #endregion

    #region public method

    public ImageFillBind()
    {
      this.max_value_binder = new PropBinder(this);
      this.cur_value_binder = new PropBinder(this);
    }

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

    #region internal method

    /// <summary>
    /// 最大值变动的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal void OnMaxValueChanged(string property_name, object old_value, object new_value)
    {

      this.max_value = Convert.ToSingle(new_value);
      this.image.fillAmount = this.cur_value / this.max_value;
    }

    /// <summary>
    /// 当前值变动的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal void OnCurValueChanged(string property_name, object old_value, object new_value)
    {
      this.cur_value = (float)Convert.ToDouble(new_value);
      this.image.fillAmount = this.cur_value / this.max_value;
    }

    #endregion





  }
}