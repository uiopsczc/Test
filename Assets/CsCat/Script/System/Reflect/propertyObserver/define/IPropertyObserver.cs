using System;

namespace CsCat
{
	public interface IPropertyObserver
	{
		/// <summary>
		/// 添加propertyName属性更改listener
		/// </summary>
		/// <param name="name"></param>
		/// <param name="listener"></param>
		void AddPropertyChangedListener(string propertyName, Action<string, object, object> listener);

		/// <summary>
		/// 获得propertyName属性的值
		/// </summary>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		object GetPropertyValue(string propertyName);

		/// <summary>
		/// 移除propertyName属性的listener
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="listener"></param>
		void RemovePropertyChangedListener(string propertyName, Action<string, object, object> listener);
	}
}