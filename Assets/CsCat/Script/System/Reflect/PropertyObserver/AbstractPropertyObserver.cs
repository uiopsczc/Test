using System;

namespace CsCat
{
	/// <summary>
	///   抽象属性观察者
	/// </summary>
	public abstract class AbstractPropertyObserver : IPropertyObserver
	{
		#region field

		/// <summary>
		///   属性的拥有者
		/// </summary>
		protected object _propOwner;

		#endregion

		#region ctor

		public AbstractPropertyObserver(object propOwner)
		{
			this._propOwner = propOwner;
		}

		#endregion

		#region virtual method

		/// <summary>
		///   获得propertyName属性的值
		///   先查fieldInfo，再查property
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		public virtual object GetPropertyValue(string propertyName)
		{
			var type = _propOwner.GetType();
			var fieldInfo = type.GetFieldInfo(propertyName, BindingFlagsConst.Instance);
			if (fieldInfo != null) return fieldInfo.GetValue(_propOwner);
			var propertyInfo = type.GetPropertyInfo(propertyName, BindingFlagsConst.Instance);
			return propertyInfo != null ? propertyInfo.GetValue(_propOwner, null) : null;
		}

		#endregion

		#region abstract method

		/// <summary>
		///   添加propertyName属性更改listener
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="listener"></param>
		public abstract void AddPropertyChangedListener(string propertyName, Action<string, object, object> listener);

		/// <summary>
		///   移除propertyName属性的listener
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="listener"></param>
		public abstract void
			RemovePropertyChangedListener(string propertyName, Action<string, object, object> listener);

		#endregion
	}
}