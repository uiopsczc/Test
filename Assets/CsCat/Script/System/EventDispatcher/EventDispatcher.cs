using System;
using System.Collections.Generic;

namespace CsCat
{
	public class EventDispatcher :IEventDispatcher
	{
		private readonly ValueListDictionary<string, (Action handler, bool isValid)> _listenerDict =
			new ValueListDictionary<string, (Action handler, bool isValid)>();

		public Action AddListener(string eventName, Action handler)
		{
			_listenerDict.Add(eventName, (handler, true));
			return handler;
		}
		

		public void IRemoveListener(string eventName, Delegate handler)
		{
			RemoveListener(eventName, (Action)handler);
		}

		public bool RemoveListener(string eventName, Action handler)
		{
			if (_listenerDict.TryGetValue(eventName, out var listenerList))
			{
				for (var i = 0; i < listenerList.Count; i++)
				{
					var handlerInfo = listenerList[i];
					if (handlerInfo.handler != handler) continue;
					if (!handlerInfo.isValid) continue;
					handlerInfo.isValid = false;
					return true;
				}
			}
			return false;
		}

		public void RemoveAllListeners()
		{
			_listenerDict.Clear();
		}

		public void Broadcast(string eventName)
		{
			if (_listenerDict.TryGetValue(eventName, out var listenerList))
			{
				for (int i = 0; i < listenerList.Count; i++)
				{
					var handlerInfo = listenerList[i];
					if (handlerInfo.isValid == false)
					{
						listenerList.RemoveAt(i);
						i--;//使i保持不变，因为有i++
						continue;
					}
					handlerInfo.handler();
				}
			}
		}
	}
}