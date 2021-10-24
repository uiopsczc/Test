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
        public PropBinder maxValueBinder { get; private set; }

        /// <summary>
        /// 当前curValue绑定
        /// </summary>
        public PropBinder curValueBinder { get; private set; }


        protected Slider silder
        {
            get
            {
                if (this._slider == null)
                    this._slider = base.GetComponent<Slider>();

                return _slider;
            }
        }

        #endregion

        #region ctor

        public SliderBind()
        {
            this.maxValueBinder = new PropBinder(this);
            this.curValueBinder = new PropBinder(this);
        }

        #endregion

        #region public method

        /// <summary>
        /// 进行最大值的绑定
        /// </summary>
        /// <param name="maxPropOwner"></param>
        /// <param name="maxPropName"></param>
        public void BindMaxValue(object maxPropOwner, string maxPropName)
        {
            this.maxValueBinder.Bind(maxPropOwner, maxPropName, this.OnMaxValueChanged);
        }

        /// <summary>
        /// 进行当前值的绑定
        /// </summary>
        /// <param name="curValuePropOwner"></param>
        /// <param name="curValuePropName"></param>
        public void BindCurValue(object curValuePropOwner, string curValuePropName)
        {
            this.curValueBinder.Bind(curValuePropOwner, curValuePropName, this.OnCurValueChanged);
        }

        #endregion

        #region protected method

        /// <summary>
        /// 销毁的时候取消绑定
        /// </summary>
        protected virtual void OnDestroy()
        {
            this.curValueBinder.OnDestroy();
            this.maxValueBinder.OnDestroy();
        }

        /// <summary>
        /// enable的时候，如果之前没绑定过的，进行绑定
        /// </summary>
        protected virtual void OnEnable()
        {
            this.curValueBinder.OnEnable();
            this.maxValueBinder.OnEnable();
        }

        #endregion

        #region private method

        /// <summary>
        /// 最大值变动的时候调用
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnMaxValueChanged(string propertyName, object oldValue, object newValue)
        {
            this.silder.maxValue = (float) Convert.ToDouble(newValue);
        }

        /// <summary>
        /// 当前值变动的时候调用
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnCurValueChanged(string propertyName, object oldValue, object newValue)
        {
            this.silder.value = (float) Convert.ToDouble(newValue);
        }

        #endregion
    }
}