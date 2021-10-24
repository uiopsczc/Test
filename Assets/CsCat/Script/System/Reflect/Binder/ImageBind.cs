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
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        internal override void OnValueChanged(string propertyName, object oldValue, object newValue)
        {
            if (this.image == null)
                this.image = base.GetComponent<Image>();

            //TODO
        }

        #endregion
    }
}