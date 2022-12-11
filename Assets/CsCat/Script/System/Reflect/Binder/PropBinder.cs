using System;

namespace CsCat
{
	/// <summary>
	/// 单个属性绑定
	/// </summary>
	public class PropBinder
	{
		#region field

		/// <summary>
		/// 要被绑定的属性
		/// </summary>
		protected string _propName;

		/// <summary>
		/// 是否已经
		/// </summary>
		protected bool _isRegisted;

		/// <summary>
		/// 属性观察者
		/// </summary>
		protected IPropertyObserver _propertyObserver;

		/// <summary>
		/// binder
		/// </summary>
		protected object _binder;

		#endregion

		#region property

		/// <summary>
		/// getter代理
		/// </summary>
		public Func<object, object> GetterProxy { get; set; }

		/// <summary>
		/// 属性观察者
		/// </summary>
		public IPropertyObserver PropertyObserver => this._propertyObserver;

		#endregion

		#region delegate

		/// <summary>
		/// 值被改变时候的调用
		/// Register中会对其进行observer的listener的引用
		/// </summary>
		protected Action<string, object, object> _valueChangedHandler;

		#endregion

		#region ctor

		public PropBinder(object binder)
		{
			this._binder = binder;
		}

		#endregion

		#region public method

		/// <summary>
		/// 对属性进行绑定
		/// </summary>
		/// <param name="propOwner"></param>
		/// <param name="propName"></param>
		/// <param name="handler"></param>
		public void Bind(object propOwner, string propName, Action<string, object, object> handler)
		{
			IPropertyObserver propertyObserver = PropertyObserverFactory.CreatePropertyObserver(propOwner);
			if (propertyObserver == null)
				return;

			this._Bind(propName, handler, propertyObserver);
		}

		/// <summary>
		/// 获取propName
		/// </summary>
		/// <returns></returns>
		public string GetPropName()
		{
			return this._propName;
		}

		/// <summary>
		/// 销毁的时候取消绑定
		/// </summary>
		public void OnDestroy()
		{
			this.Unbind();
		}

		/// <summary>
		/// enable的时候，如果之前没绑定过的，进行绑定
		/// </summary>
		public void OnEnable()
		{
			if (!this._isRegisted)
				this._Regist();
		}

		/// <summary>
		/// 取消属性绑定
		/// </summary>
		public void Unbind()
		{
			this._Unregist();
			this._propertyObserver = null;
			this._propName = null;
		}

		#endregion

		#region protected method

		/// <summary>
		/// 注册对应mPropName属性的listener
		/// </summary>
		/// <returns></returns>
		protected bool _Regist()
		{
			this._isRegisted = true;
			if (this._propertyObserver != null)
			{
				this._propertyObserver.AddPropertyChangedListener(this._propName, this._OnValueChanged);
				return true;
			}

			return false;
		}

		/// <summary>
		/// 注册对应mPropName属性的listener
		/// </summary>
		/// <returns></returns>
		protected bool _Unregist()
		{
			this._isRegisted = false;
			if (this._propertyObserver != null)
			{
				this._propertyObserver.RemovePropertyChangedListener(this._propName, this._OnValueChanged);
				return true;
			}

			return false;
		}

		#endregion

		#region private method

		/// <summary>
		/// 对属性进行绑定【私有】
		/// </summary>
		/// <param name="propName"></param>
		/// <param name="handler"></param>
		/// <param name="observer"></param>
		private void _Bind(string propName, Action<string, object, object> handler, IPropertyObserver observer)
		{
			this._Unregist();
			this._propertyObserver = observer;
			this._propName = propName;
			this._valueChangedHandler = handler;
			this._Regist();
			_CheckBinderIsValidObject();
		}

		/// <summary>
		/// 属性的值改变的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private void _OnValueChanged(string propertyName, object oldValue, object newValue)
		{
			if (!_CheckBinderIsValidObject())
				return;

			if (this.GetterProxy != null)
				newValue = this.GetterProxy(newValue);

			this._valueChangedHandler(this._propName, oldValue, newValue);
		}

		/// <summary>
		/// 检查binder是不是有效
		/// </summary>
		/// <returns></returns>
		bool _CheckBinderIsValidObject()
		{
			if (!this._binder.IsValidObject())
			{
				this._Unregist();
				return false;
			}

			return true;
		}

		#endregion
	}
}