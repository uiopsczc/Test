using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class AssetBundleManager
	{
		private EventDispatchers _GetEventDispatchers(EventDispatchers eventDispatchers)
		{
			return eventDispatchers ?? this.GetChild<EventDispatchersTreeNode>().eventDispatchers;
		}

		private ListenerDictTreeNode _GetListenerTreeNode()
		{
			return this.GetChild<ListenerDictTreeNode>();
		}
		
		public (EventDispatchers eventDispatchers, string eventName, Action handler) AddListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return _GetListenerTreeNode().AddListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			_GetListenerTreeNode().RemoveListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener((EventDispatchers eventDispatchers, string eventName, Action handler) listenerInfo)
		{
			_GetListenerTreeNode().RemoveListener(listenerInfo);
		}

		public void FireEvent(EventDispatchers eventDispatchers, string eventName)
		{
			_GetEventDispatchers(eventDispatchers).FireEvent(eventName);
		}

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0> handler) AddListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return _GetListenerTreeNode().AddListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			_GetListenerTreeNode().RemoveListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0>((EventDispatchers eventDispatchers, string eventName, Action<P0> handler) listenerInfo)
		{
			_GetListenerTreeNode().RemoveListener(listenerInfo);
		}

		public void FireEvent<P0>(EventDispatchers eventDispatchers, string eventName, P0 p0)
		{
			_GetEventDispatchers(eventDispatchers).FireEvent(eventName, p0);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler) AddListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			return _GetListenerTreeNode().AddListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			_GetListenerTreeNode().RemoveListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0, P1>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler) listenerInfo)
		{
			_GetListenerTreeNode().RemoveListener(listenerInfo);
		}

		public void FireEvent<P0, P1>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1)
		{
			_GetEventDispatchers(eventDispatchers).FireEvent(eventName, p0, p1);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler) AddListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			return _GetListenerTreeNode().AddListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			_GetListenerTreeNode().RemoveListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0, P1, P2>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler) listenerInfo)
		{
			_GetListenerTreeNode().RemoveListener(listenerInfo);
		}

		public void FireEvent<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2)
		{
			_GetEventDispatchers(eventDispatchers).FireEvent(eventName, p0, p1, p2);
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public (EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler) AddListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			return _GetListenerTreeNode().AddListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			_GetListenerTreeNode().RemoveListener(_GetEventDispatchers(eventDispatchers), eventName, handler);
		}

		public void RemoveListener<P0, P1, P2, P3>((EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler) listenerInfo)
		{
			_GetListenerTreeNode().RemoveListener(listenerInfo);
		}

		public void FireEvent<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			_GetEventDispatchers(eventDispatchers).FireEvent(eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			_GetListenerTreeNode().RemoveAllListeners();
		}
	}
}



