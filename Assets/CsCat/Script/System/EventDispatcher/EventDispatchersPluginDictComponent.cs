
using System;
using System.Collections.Generic;

namespace CsCat
{
	//多个eventDispatchers使用，使用的时候需要指明是那个eventDispatchers
	public class EventDispatchersPluginDictComponent : AbstractComponent
	{
		public EventDispatchersPluginDict eventDispatchersPluginDict = new EventDispatchersPluginDict();

		public Action AddListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return eventDispatchersPluginDict.AddListener(eventDispatchers, eventName, handler);
		}
		public bool RemoveListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return eventDispatchersPluginDict.RemoveListener(eventDispatchers, eventName, handler);
		}
		public void Broadcast(EventDispatchers eventDispatchers, string eventName)
		{
			eventDispatchersPluginDict.Broadcast(eventDispatchers, eventName);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0> AddListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return eventDispatchersPluginDict.AddListener(eventDispatchers, eventName, handler);
		}
		public bool RemoveListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return eventDispatchersPluginDict.RemoveListener(eventDispatchers, eventName, handler);
		}
		public void Broadcast<P0>(EventDispatchers eventDispatchers, string eventName, P0 p0)
		{
			eventDispatchersPluginDict.Broadcast(eventDispatchers, eventName, p0);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1> AddListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			return eventDispatchersPluginDict.AddListener(eventDispatchers, eventName, handler);
		}
		public bool RemoveListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			return eventDispatchersPluginDict.RemoveListener(eventDispatchers, eventName, handler);
		}
		public void Broadcast<P0, P1>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1)
		{
			eventDispatchersPluginDict.Broadcast(eventDispatchers, eventName, p0, p1);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2> AddListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			return eventDispatchersPluginDict.AddListener(eventDispatchers, eventName, handler);
		}
		public bool RemoveListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2> handler)
		{
			return eventDispatchersPluginDict.RemoveListener(eventDispatchers, eventName, handler);
		}
		public void Broadcast<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2)
		{
			eventDispatchersPluginDict.Broadcast(eventDispatchers, eventName, p0, p1, p2);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			return eventDispatchersPluginDict.AddListener(eventDispatchers, eventName, handler);
		}
		public bool RemoveListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1, P2, P3> handler)
		{
			return eventDispatchersPluginDict.RemoveListener(eventDispatchers, eventName, handler);
		}
		public void Broadcast<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			eventDispatchersPluginDict.Broadcast(eventDispatchers, eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			eventDispatchersPluginDict.RemoveAllListeners();
		}

		protected override void _Reset()
		{
			base._Reset();
			RemoveAllListeners();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			RemoveAllListeners();
		}
	}
}
