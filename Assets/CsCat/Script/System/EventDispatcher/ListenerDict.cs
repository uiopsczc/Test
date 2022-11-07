using System;
using System.Collections.Generic;

namespace CsCat
{
	public class ListenerDict
	{
		private Dictionary<(EventDispatchers eventDispatchers, string eventName, Delegate handler), Action> _dict =
			new Dictionary<(EventDispatchers eventDispatchers, string eventName, Delegate handler), Action>();

		public (EventDispatchers eventDispatchers, string eventName, Action handler) AddListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			eventDispatchers.AddListener(eventName, handler);
			var listenerInfo = (eventDispatchers, eventName, handler);
			_dict[listenerInfo] = ()=> eventDispatchers.RemoveListener(eventName, handler);
			return listenerInfo;
		}

		public void RemoveListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			var listenerInfo = (eventDispatchers, eventName, handler);
			this.RemoveListener(listenerInfo);
		}

		public void RemoveListener((EventDispatchers eventDispatchers, string eventName, Action handler) listenerInfo)
		{
			_dict.Remove(listenerInfo);
			listenerInfo.eventDispatchers.RemoveListener(listenerInfo.eventName, listenerInfo.handler);
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0> handler) AddListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			eventDispatchers.AddListener(eventName, handler);
			var listenerInfo = (eventDispatchers, eventName, handler);
			_dict[listenerInfo] = () => eventDispatchers.RemoveListener(eventName, handler);
			return listenerInfo;
		}

		public void RemoveListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			var listenerInfo = (eventDispatchers, eventName, handler);
			this.RemoveListener(listenerInfo);
		}

		public void RemoveListener<P0>((EventDispatchers eventDispatchers, string eventName, Action<P0> handler) listenerInfo)
		{
			_dict.Remove(listenerInfo);
			listenerInfo.eventDispatchers.RemoveListener(listenerInfo.eventName, listenerInfo.handler);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler) AddListener<P0,P1>(EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler)
		{
			eventDispatchers.AddListener(eventName, handler);
			var listenerInfo = (eventDispatchers, eventName, handler);
			_dict[listenerInfo] = () => eventDispatchers.RemoveListener(eventName, handler);
			return listenerInfo;
		}

		public void RemoveListener<P0,P1>(EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler)
		{
			var listenerInfo = (eventDispatchers, eventName, handler);
			this.RemoveListener(listenerInfo);
		}

		public void RemoveListener<P0,P1>((EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler) listenerInfo)
		{
			_dict.Remove(listenerInfo);
			listenerInfo.eventDispatchers.RemoveListener(listenerInfo.eventName, listenerInfo.handler);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1,P2> handler) AddListener<P0, P1,P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1,P2> handler)
		{
			eventDispatchers.AddListener(eventName, handler);
			var listenerInfo = (eventDispatchers, eventName, handler);
			_dict[listenerInfo] = () => eventDispatchers.RemoveListener(eventName, handler);
			return listenerInfo;
		}

		public void RemoveListener<P0, P1,P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			var listenerInfo = (eventDispatchers, eventName, handler);
			this.RemoveListener(listenerInfo);
		}

		public void RemoveListener<P0, P1, P2>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler) listenerInfo)
		{
			_dict.Remove(listenerInfo);
			listenerInfo.eventDispatchers.RemoveListener(listenerInfo.eventName, listenerInfo.handler);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler) AddListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			eventDispatchers.AddListener(eventName, handler);
			var listenerInfo = (eventDispatchers, eventName, handler);
			_dict[listenerInfo] = () => eventDispatchers.RemoveListener(eventName, handler);
			return listenerInfo;
		}

		public void RemoveListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			var listenerInfo = (eventDispatchers, eventName, handler);
			this.RemoveListener(listenerInfo);
		}

		public void RemoveListener<P0, P1, P2, P3>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler) listenerInfo)
		{
			_dict.Remove(listenerInfo);
			listenerInfo.eventDispatchers.RemoveListener(listenerInfo.eventName, listenerInfo.handler);
		}

		public void RemoveAllListeners()
		{
			foreach (var kv in _dict)
			{
				var removeListenerAction = kv.Value;
				removeListenerAction();
			}
			_dict.Clear();
		}
	}
}