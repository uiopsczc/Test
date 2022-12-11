using System;
using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	///   属性观察者
	/// </summary>
	public class PropObserver : IPropNotify
	{
		#region field

		/// <summary>
		///   该类的全部属性listener
		/// </summary>
		protected Dictionary<string, List<Action<string, object, object>>> _propListenerDict =
			new Dictionary<string, List<Action<string, object, object>>>();

		#endregion

		#region ctor

		static PropObserver()
		{
			PropertyObserverFactory.RegistPropertyObserver(typeof(PropObserver),
				obj => new BindPropObserver(obj));
		}

		#endregion

		#region public method

		public virtual void OnNewCreate()
		{
		}

		public virtual void OnLoaded()
		{
		}

		/// <summary>
		///   添加propertyName属性的listener
		///   添加到propListenerDict中
		///   不会重复添加
		/// </summary>
		/// <param name="property_name"></param>
		/// <param name="listener"></param>
		public void AddPropListener(string property_name, Action<string, object, object> listener)
		{
			if (_propListenerDict.TryGetValue(property_name, out var list)) //防止重复添加
			{
				list.Add(listener);
				return;
			}

			list = new List<Action<string, object, object>> { listener };
			_propListenerDict.Add(property_name, list);
		}

		/// <summary>
		///   通知propertyName属性更改了
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue">更改前的值</param>
		/// <param name="newValue">更改后的值</param>
		public void NotifyPropChanged(string propertyName, object oldValue, object newValue)
		{
			if (_propListenerDict.TryGetValue(propertyName, out var list))
				for (var i = 0; i < list.Count; i++)
				{
					var listener = list[i];
					listener?.Invoke(propertyName, oldValue, newValue);
				}
		}

		/// <summary>
		///   移除propertyName属性的listener
		/// </summary>
		/// <param name="property_name"></param>
		/// <param name="listener"></param>
		public void RemovePropListener(string property_name, Action<string, object, object> listener)
		{
			if (_propListenerDict.TryGetValue(property_name, out var list)) list.Remove(listener);
		}

		#region edit by czq  //直接传当前类名作为Name

		public void AddClassListener(Action<string, object, object> listener)
		{
			AddPropListener(GetType().Name, listener);
		}

		public void RemoveClassListener(Action<string, object, object> listener)
		{
			RemovePropListener(GetType().Name, listener);
		}

		public void NotifyClassChange()
		{
			NotifyPropChanged(GetType().Name, this, this);
		}

		public void RemoveAllListeners()
		{
			_propListenerDict.Clear();
		}

		#endregion

		#endregion
	}
}