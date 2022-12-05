using System;
using System.Collections.Generic;

namespace CsCat
{
	public class ListenerDictComponent:Component
	{
		private readonly ListenerDict _listenerDict = new ListenerDict();

		public (EventDispatchers eventDispatchers, string eventName, Action handler) AddListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return _listenerDict.AddListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			_listenerDict.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener((EventDispatchers eventDispatchers, string eventName, Action handler) listenerInfo)
		{
			_listenerDict.RemoveListener(listenerInfo);
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0> handler) AddListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return _listenerDict.AddListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			_listenerDict.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0>((EventDispatchers eventDispatchers, string eventName, Action<P0> handler) listenerInfo)
		{
			_listenerDict.RemoveListener(listenerInfo);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler) AddListener<P0,P1>(EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler)
		{
			return _listenerDict.AddListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0,P1>(EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler)
		{
			_listenerDict.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0,P1>((EventDispatchers eventDispatchers, string eventName, Action<P0,P1> handler) listenerInfo)
		{
			_listenerDict.RemoveListener(listenerInfo);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1,P2> handler) AddListener<P0, P1,P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1,P2> handler)
		{
			return _listenerDict.AddListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0, P1,P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			_listenerDict.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0, P1, P2>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler) listenerInfo)
		{
			_listenerDict.RemoveListener(listenerInfo);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler) AddListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			return _listenerDict.AddListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			_listenerDict.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void RemoveListener<P0, P1, P2, P3>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler) listenerInfo)
		{
			_listenerDict.RemoveListener(listenerInfo);
		}

		public void RemoveAllListeners()
		{
			_listenerDict.RemoveAllListeners();
		}

		protected override void _Reset()
		{
			RemoveAllListeners();
			base._Reset();
		}

		protected override void _Destroy()
		{
			RemoveAllListeners();
			base._Destroy();
		}
	}
}