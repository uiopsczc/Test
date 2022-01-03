using UnityEngine;

namespace CsCat
{
	/// <summary>
	/// 绑定基类
	/// </summary>
	public class BaseBind : MonoBehaviour
	{
		#region property

		/// <summary>
		/// propBinder
		/// </summary>
		public PropBinder propBinder { get; private set; }

		#endregion

		#region ctor

		/// <summary>
		/// 该构造函数会先于Awake
		/// </summary>
		public BaseBind()
		{
			this.propBinder = new PropBinder(this);
		}

		#endregion

		#region virtual method

		/// <summary>
		/// 对属性进行绑定
		/// </summary>
		/// <param name="propertyOwner"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public virtual BaseBind Bind(object propertyOwner, string propertyName)
		{
			this.propBinder.Bind(propertyOwner, propertyName,
				(propName, oldValue, newValue) =>
				{
					this.OnValueChanged(this.propBinder.GetPropName(), oldValue, newValue);
				});
			return this;
		}

		/// <summary>
		/// 属性的值改变的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal virtual void OnValueChanged(string propertyName, object oldValue, object newValue)
		{
		}


		/// <summary>
		/// 销毁的时候取消绑定
		/// </summary>
		protected virtual void OnDestroy()
		{
			this.propBinder.OnDestroy();
		}

		/// <summary>
		/// enable的时候，如果之前没绑定过的，进行绑定
		/// </summary>
		protected virtual void OnEnable()
		{
			this.propBinder.OnEnable();
		}

		#endregion
	}
}