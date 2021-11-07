
using System;
using System.Collections.Generic;

namespace CsCat
{
	public class EventDispatchersPluginDict
	{
		private Dictionary<EventDispatchers, EventDispatchersPlugin> dict = new Dictionary<EventDispatchers, EventDispatchersPlugin>();

		public EventDispatchersPlugin GetEventDispatchersPlugin(EventDispatchers eventDispatchers)
		{
			eventDispatchers = eventDispatchers ?? Client.instance.eventDispatchers;
			return dict.GetOrAddDefault(eventDispatchers, () => new EventDispatchersPlugin(eventDispatchers));
		}

		public Action AddListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).AddListener(eventName, handler);
		}
		public bool RemoveListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).RemoveListener(eventName, handler);
		}
		public void Broadcast(EventDispatchers eventDispatchers, string eventName)
		{
			GetEventDispatchersPlugin(eventDispatchers).Broadcast(eventName);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0> AddListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).AddListener(eventName, handler);
		}
		public bool RemoveListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).RemoveListener(eventName, handler);
		}
		public void Broadcast<P0>(EventDispatchers eventDispatchers, string eventName, P0 p0)
		{
			GetEventDispatchersPlugin(eventDispatchers).Broadcast(eventName, p0);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1> AddListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).AddListener(eventName, handler);
		}
		public bool RemoveListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).RemoveListener(eventName, handler);
		}
		public void Broadcast<P0, P1>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1)
		{
			GetEventDispatchersPlugin(eventDispatchers).Broadcast(eventName, p0, p1);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2> AddListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).AddListener(eventName, handler);
		}
		public bool RemoveListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).RemoveListener(eventName, handler);
		}
		public void Broadcast<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2)
		{
			GetEventDispatchersPlugin(eventDispatchers).Broadcast(eventName, p0, p1, p2);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).AddListener(eventName, handler);
		}
		public bool RemoveListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			return GetEventDispatchersPlugin(eventDispatchers).RemoveListener(eventName, handler);
		}
		public void Broadcast<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			GetEventDispatchersPlugin(eventDispatchers).Broadcast(eventName, p0, p1, p2, p3);
		}


		public void RemoveAllListeners()
		{
			foreach (var eventDispatchersPlugin in dict.Values)
				eventDispatchersPlugin.RemoveAllListeners();
			dict.Clear();
		}
	}
}
