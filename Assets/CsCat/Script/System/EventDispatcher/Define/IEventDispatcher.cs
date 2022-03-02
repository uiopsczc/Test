using System;

namespace CsCat
{
	public interface IEventDispatcher
	{
		void IRemoveListener(string eventName, Delegate handler);
		void RemoveAllListeners();
	}
}