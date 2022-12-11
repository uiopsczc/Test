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
		public string formatString;


		private Text _text;

		#endregion


		#region override method

		/// <summary>
		/// 属性的值改变的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal override void _OnValueChanged(string propertyName, object oldValue, object newValue)
		{
			if (this._text == null)
			{
				this._text = base.GetComponent<Text>();
			}

			string text = (newValue == null) ? StringConst.String_Empty : newValue.ToString();
			if (!string.IsNullOrEmpty(this.formatString))
				text = string.Format(this.formatString, text);

			this._text.text = text;
		}

		#endregion
	}
}