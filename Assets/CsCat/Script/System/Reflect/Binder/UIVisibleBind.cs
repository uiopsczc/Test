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
        private object lastValue;

        #endregion

        #region property

        private CanvasRenderer canvasRenderer
        {
            get
            {
                if (this._canvasRenderer == null)
                    this._canvasRenderer = base.GetComponent<CanvasRenderer>();
                return _canvasRenderer;
            }
        }

        #endregion

        #region virtual method

        /// <summary>
        /// 属性的值改变的时候调用
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        internal override void OnValueChanged(string propertyName, object oldValue, object newValue)
        {
            this.lastValue = newValue;

            bool isActive = this.lastValue != null && Convert.ToBoolean(this.lastValue);
            if (this.canvasRenderer != null) //如果canvasRenderer直接设置alpha
            {
                this.canvasRenderer.SetAlpha(isActive ? 1 : 0);
                return;
            }
            //否则设置gameObject的active
            base.gameObject.SetActive(isActive);
        }

        /// <summary>
        /// enable的时候，如果之前没绑定过的，进行绑定
        /// 根据lastValue设置canvasRenderer的alpha
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            bool isActive = this.lastValue != null && Convert.ToBoolean(this.lastValue);
            if (this.canvasRenderer != null)
                this.canvasRenderer.SetAlpha(isActive ? 1 : 0);
        }

        #endregion
    }
}