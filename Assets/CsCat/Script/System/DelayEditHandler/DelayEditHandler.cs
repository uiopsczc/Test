using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DelayEditHandler
	{
		private readonly object editTarget;
		private Action toCallback;

		public DelayEditHandler(object editTarget)
		{
			this.editTarget = editTarget;
		}

		public object this[object key]
		{
			set { ToSet(key, value); }
		}

		public void ToSet(object key, object value)
		{
			ToCallback(() => editTarget.SetPropertyValue("Item", value, new object[] { key }));
		}


		public void ToAdd(params object[] args)
		{
			ToCallback(() => editTarget.InvokeMethod("Add", true, args));
		}

		public void ToRemove(params object[] args)
		{
			ToCallback(() => editTarget.InvokeMethod("Remove", true, args));
		}

		public void ToRemoveAt(int toRemoveIndex)
		{
			ToCallback(() => editTarget.InvokeMethod("RemoveAt", true, toRemoveIndex));
		}

		public void ToRemoveAt_Stack(int toRemoveIndex)
		{
			ToCallback_Stack(() => editTarget.InvokeMethod("RemoveAt", true, toRemoveIndex));
		}

		public void ToCallback(Action toCallback)
		{
			this.toCallback += toCallback;
		}

		//后入先出
		public void ToCallback_Stack(Action toCallback)
		{
			this.toCallback += toCallback + this.toCallback;
		}


		public void Handle()
		{
			this.toCallback?.Invoke();
			this.toCallback = null;
		}
	}
}