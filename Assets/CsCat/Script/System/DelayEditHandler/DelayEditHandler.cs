using System;

namespace CsCat
{
	public class DelayEditHandler
	{
		private readonly object _editTarget;
		private Action _toCallback;

		public DelayEditHandler(object editTarget)
		{
			this._editTarget = editTarget;
		}

		public object this[object key]
		{
			set { ToSet(key, value); }
		}

		public void ToSet(object key, object value)
		{
			ToCallback(() => _editTarget.SetPropertyValue("Item", value, new[] { key }));
		}


		public void ToAdd(params object[] args)
		{
			ToCallback(() => _editTarget.InvokeMethod("Add", true, args));
		}

		public void ToRemove(params object[] args)
		{
			ToCallback(() => _editTarget.InvokeMethod("Remove", true, args));
		}

		public void ToRemoveAt(int toRemoveIndex)
		{
			ToCallback(() => _editTarget.InvokeMethod("RemoveAt", true, toRemoveIndex));
		}

		public void ToRemoveAt_Stack(int toRemoveIndex)
		{
			ToCallback_Stack(() => _editTarget.InvokeMethod("RemoveAt", true, toRemoveIndex));
		}

		public void ToCallback(Action toCallback)
		{
			this._toCallback += toCallback;
		}

		//后入先出
		public void ToCallback_Stack(Action toCallback)
		{
			this._toCallback += toCallback + this._toCallback;
		}


		public void Handle()
		{
			this._toCallback?.Invoke();
			this._toCallback = null;
		}
	}
}