using System;

namespace CsCat
{
	public class MethodInvoker
	{
		private readonly object _target;
		private readonly string _methodName;
		private object[] _args;
		private readonly Delegate _delegation;

		public MethodInvoker(object target, string methodName, params object[] args)
		{
			this._target = target;
			this._methodName = methodName;
			this._args = args;
		}

		public MethodInvoker(Delegate delegation)
		{
			this._delegation = delegation;
		}

		public object Invoke(params object[] args)
		{
			if (args.Length != 0)
				this._args = args;
			//二者只会有一个被调用
			if (_delegation != null)
				return _delegation.DynamicInvoke(new object[] { this._args });
			return this._target.InvokeMethod<object>(this._methodName, false, this._args);
		}

		public T Invoke<T>(params object[] args)
		{
			return (T)Invoke(args);
		}
	}
}