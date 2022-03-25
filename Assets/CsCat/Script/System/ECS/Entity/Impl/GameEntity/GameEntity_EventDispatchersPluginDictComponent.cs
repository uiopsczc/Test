using System;

namespace CsCat
{
	public partial class GameEntity
	{
		public Action AddListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return eventDispatchersPluginDictComponent.AddListener(eventDispatchers, eventName, handler);
		}

		public bool RemoveListener(EventDispatchers eventDispatchers, string eventName, Action handler)
		{
			return eventDispatchersPluginDictComponent.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void Broadcast(EventDispatchers eventDispatchers, string eventName)
		{
			eventDispatchersPluginDictComponent.Broadcast(eventDispatchers, eventName);
		}

		public Action<P0> AddListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return eventDispatchersPluginDictComponent.AddListener(eventDispatchers, eventName, handler);
		}

		public bool RemoveListener<P0>(EventDispatchers eventDispatchers, string eventName, Action<P0> handler)
		{
			return eventDispatchersPluginDictComponent.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void Broadcast<P0>(EventDispatchers eventDispatchers, string eventName, P0 p0)
		{
			eventDispatchersPluginDictComponent.Broadcast(eventDispatchers, eventName, p0);
		}

		public Action<P0, P1> AddListener<P0, P1>(EventDispatchers eventDispatchers, string eventName,
			Action<P0, P1> handler)
		{
			return eventDispatchersPluginDictComponent.AddListener(eventDispatchers, eventName, handler);
		}

		public bool RemoveListener<P0, P1>(EventDispatchers eventDispatchers, string eventName, Action<P0, P1> handler)
		{
			return eventDispatchersPluginDictComponent.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void Broadcast<P0, P1>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1)
		{
			eventDispatchersPluginDictComponent.Broadcast(eventDispatchers, eventName, p0, p1);
		}


		public Action<P0, P1, P2> AddListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName,
			Action<P0, P1, P2> handler)
		{
			return eventDispatchersPluginDictComponent.AddListener(eventDispatchers, eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName,
			Action<P0, P1, P2> handler)
		{
			return eventDispatchersPluginDictComponent.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void Broadcast<P0, P1, P2>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2)
		{
			eventDispatchersPluginDictComponent.Broadcast(eventDispatchers, eventName, p0, p1, p2);
		}

		public Action<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName,
			Action<P0, P1, P2, P3> handler)
		{
			return eventDispatchersPluginDictComponent.AddListener(eventDispatchers, eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName,
			Action<P0, P1, P2, P3> handler)
		{
			return eventDispatchersPluginDictComponent.RemoveListener(eventDispatchers, eventName, handler);
		}

		public void Broadcast<P0, P1, P2, P3>(EventDispatchers eventDispatchers, string eventName, P0 p0, P1 p1, P2 p2,
			P3 p3)
		{
			eventDispatchersPluginDictComponent.Broadcast(eventDispatchers, eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			eventDispatchersPluginDictComponent.RemoveAllListeners();
		}
	}
}