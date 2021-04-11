using UnityEngine.UI;

namespace CsCat
{
  /// <summary>
  /// 绑定的propertyName属性的值改变的时候触发OnValueChanged的调用
  /// 结果是Text.text=string.Format(this.FormatString, newValue);
  /// 需要手动设置绑定的属性 Bind(object propertyOwner, string propertyName);
  /// </summary>
  public class TextBind : BaseBind
  {
    #region field

    /// <summary>
    /// 格式化newValue的格式
    /// </summary>
    public string format_string;



    private Text text;

    #endregion


    #region override method

    /// <summary>
    /// 属性的值改变的时候调用
    /// </summary>
    /// <param name="property_name"></param>
    /// <param name="old_value"></param>
    /// <param name="new_value"></param>
    internal override void OnValueChanged(string property_name, object old_value, object new_value)
    {
      if (this.text == null)
      {
        this.text = base.GetComponent<Text>();
      }

      string text = (new_value == null) ? "" : new_value.ToString();
      if (!string.IsNullOrEmpty(this.format_string))
      {
        text = string.Format(this.format_string, text);
      }

      this.text.text = text;
    }

    #endregion


  }
}