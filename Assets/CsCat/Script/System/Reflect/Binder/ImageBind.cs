using UnityEngine.UI;

namespace CsCat
{
  /// <summary>
  /// 功能待开发
  /// </summary>
  public class ImageBind : BaseBind
  {
    #region field

    private Image image;

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
      if (this.image == null)
      {
        this.image = base.GetComponent<Image>();
      }

      //TODO
    }

    #endregion

  }
}