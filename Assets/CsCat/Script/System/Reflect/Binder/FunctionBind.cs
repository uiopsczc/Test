using System;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	/// 绑定的propertyName属性的值改变的时候触发mBindedFunc的调用
	/// 需要手动设置绑定的属性 Bind(object propertyOwner, string propertyName);
	/// 需要调用SetFunc来设置属性改变时候的调用的方法
	/// </summary>
	public class FunctionBind : BaseBind
	{
		#region delegate

		/// <summary>
		/// 绑定的propertyName属性的值改变的时候触发mBindedFunc的调用
		/// </summary>
		private Action<GameObject, object, object> _bindedFunc;

		#endregion

		#region override method

		/// <summary>
		/// 绑定的propertyName属性的值改变的时候触发mBindedFunc的调用
		/// </summary>
		/// <param name="func"></param>
		public void SetFunc(Action<GameObject, object, object> func)
		{
			this._bindedFunc = func;
		}

		/// <summary>
		/// 属性的值改变的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal override void OnValueChanged(string propertyName, object oldValue, object newValue)
		{
			_bindedFunc?.Invoke(gameObject, oldValue, newValue);
		}

		#endregion
	}
}