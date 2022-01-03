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
		protected string propName;

		/// <summary>
		/// 是否已经
		/// </summary>
		protected bool isRegisted;

		/// <summary>
		/// 属性观察者
		/// </summary>
		protected IPropertyObserver propertyObserver;

		/// <summary>
		/// binder
		/// </summary>
		protected object binder;

		#endregion

		#region property

		/// <summary>
		/// getter代理
		/// </summary>
		public Func<object, object> GetterProxy { get; set; }

		/// <summary>
		/// 属性观察者
		/// </summary>
		public IPropertyObserver PropertyObserver => this.propertyObserver;

		#endregion

		#region delegate

		/// <summary>
		/// 值被改变时候的调用
		/// Register中会对其进行observer的listener的引用
		/// </summary>
		protected Action<string, object, object> valueChangedHandler;

		#endregion

		#region ctor

		public PropBinder(object binder)
		{
			this.binder = binder;
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

			this.Bind(propName, handler, propertyObserver);
		}

		/// <summary>
		/// 获取propName
		/// </summary>
		/// <returns></returns>
		public string GetPropName()
		{
			return this.propName;
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
			if (!this.isRegisted)
				this.Regist();
		}

		/// <summary>
		/// 取消属性绑定
		/// </summary>
		public void Unbind()
		{
			this.Unregist();
			this.propertyObserver = null;
			this.propName = null;
		}

		#endregion

		#region protected method

		/// <summary>
		/// 注册对应mPropName属性的listener
		/// </summary>
		/// <returns></returns>
		protected bool Regist()
		{
			this.isRegisted = true;
			if (this.propertyObserver != null)
			{
				this.propertyObserver.AddPropertyChangedListener(this.propName, this.OnValueChanged);
				return true;
			}

			return false;
		}

		/// <summary>
		/// 注册对应mPropName属性的listener
		/// </summary>
		/// <returns></returns>
		protected bool Unregist()
		{
			this.isRegisted = false;
			if (this.propertyObserver != null)
			{
				this.propertyObserver.RemovePropertyChangedListener(this.propName, this.OnValueChanged);
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
		private void Bind(string propName, Action<string, object, object> handler, IPropertyObserver observer)
		{
			this.Unregist();
			this.propertyObserver = observer;
			this.propName = propName;
			this.valueChangedHandler = handler;
			this.Regist();
			CheckBinderIsValidObject();
		}

		/// <summary>
		/// 属性的值改变的时候调用
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		private void OnValueChanged(string propertyName, object oldValue, object newValue)
		{
			if (!CheckBinderIsValidObject())
				return;

			if (this.GetterProxy != null)
				newValue = this.GetterProxy(newValue);

			this.valueChangedHandler(this.propName, oldValue, newValue);
		}

		/// <summary>
		/// 检查binder是不是有效
		/// </summary>
		/// <returns></returns>
		bool CheckBinderIsValidObject()
		{
			if (!this.binder.IsValidObject())
			{
				this.Unregist();
				return false;
			}

			return true;
		}

		#endregion
	}
}