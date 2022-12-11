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
		private float _curValue;
		private float _maxValue;

		#endregion

		#region property

		private Image image
		{
			get
			{
				if (this._image == null)
					this._image = base.GetComponent<Image>();

				return this._image;
			}
		}

		/// <summary>
		/// 最大fillAmount绑定
		/// </summary>
		public PropBinder maxValueBinder { get; private set; }

		/// <summary>
		/// 当前fillAmount绑定
		/// </summary>
		public PropBinder curValueBinder { get; private set; }

		#endregion

		#region virtual method

		/// <summary>
		/// 销毁的时候取消绑定
		/// </summary>
		protected virtual void OnDestroy()
		{
			this.curValueBinder.OnDestroy();
			this.maxValueBinder.OnDestroy();
		}

		/// <summary>
		///  enable的时候，如果之前没绑定过的，进行绑定
		/// </summary>
		protected virtual void OnEnable()
		{
			this.curValueBinder.OnEnable();
			this.maxValueBinder.OnEnable();
		}

		#endregion

		#region public method

		public ImageFillBind()
		{
			this.maxValueBinder = new PropBinder(this);
			this.curValueBinder = new PropBinder(this);
		}

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

		#region internal method

		/// <summary>
		/// 最大值变动的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal void OnMaxValueChanged(string propertyName, object oldValue, object newValue)
		{
			this._maxValue = Convert.ToSingle(newValue);
			this.image.fillAmount = this._curValue / this._maxValue;
		}

		/// <summary>
		/// 当前值变动的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal void OnCurValueChanged(string propertyName, object oldValue, object newValue)
		{
			this._curValue = (float)Convert.ToDouble(newValue);
			this.image.fillAmount = this._curValue / this._maxValue;
		}

		#endregion
	}
}