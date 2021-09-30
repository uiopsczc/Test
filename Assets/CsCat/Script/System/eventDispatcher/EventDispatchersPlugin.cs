using System;
using System.Collections.Generic;

namespace CsCat
{
	public class EventDispatchersPlugin
	{
		private Dictionary<object, Dictionary<string, List<object>>> listenerDict =
			new Dictionary<object, Dictionary<string, List<object>>>();//<eventDispatcher, <eventName,list<handler>>>
		private EventDispatchers eventDispatchers;
		public EventDispatchersPlugin(EventDispatchers eventDispatchers)
		{
			this.eventDispatchers = eventDispatchers;
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		private void AddToListenerDict(string eventName, Action handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher();
			if(!listenerDict.ContainsKey(eventDispatcher))
				listenerDict[eventDispatcher] = new ValueListDictionary<string, object>();
			if(!listenerDict[eventDispatcher].ContainsKey(eventName))
				listenerDict[eventDispatcher][eventName] = new List<object>();
			listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict(string eventName, Action handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher();
			if (!listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0>(string eventName, Action<P0> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				listenerDict[eventDispatcher] = new ValueListDictionary<string, object>();
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				listenerDict[eventDispatcher][eventName] = new List<object>();
			listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0>(string eventName, Action<P0> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0,P1>(string eventName, Action<P0, P1> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0, P1>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				listenerDict[eventDispatcher] = new ValueListDictionary<string, object>();
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				listenerDict[eventDispatcher][eventName] = new List<object>();
			listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0, P1>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0, P1,P2>(string eventName, Action<P0, P1, P2> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0, P1, P2>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				listenerDict[eventDispatcher] = new ValueListDictionary<string, object>();
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				listenerDict[eventDispatcher][eventName] = new List<object>();
			listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0,P1,P2>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0, P1, P2,P3>(string eventName, Action<P0, P1, P2,P3> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0, P1, P2, P3>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				listenerDict[eventDispatcher] = new ValueListDictionary<string, object>();
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				listenerDict[eventDispatcher][eventName] = new List<object>();
			listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0, P1, P2,P3>(string eventName, Action<P0, P1, P2,P3> handler)
		{
			var eventDispatcher = this.eventDispatchers.GetEventDispatcher<P0, P1, P2,P3>();
			if (!listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action AddListener(string eventName, Action handler)
		{
			AddToListenerDict(eventName, handler);
			return this.eventDispatchers.AddListener(eventName, handler);
		}
		public bool RemoveListener(string eventName, Action handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this.eventDispatchers.RemoveListener(eventName, handler);
		}
		public void Broadcast(string eventName)
		{
			this.eventDispatchers.Broadcast(eventName);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0> AddListener<P0>(string eventName, Action<P0> handler)
		{
			AddToListenerDict(eventName, handler);
			return this.eventDispatchers.AddListener(eventName, handler);
		}
		public bool RemoveListener<P0>(string eventName, Action<P0> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this.eventDispatchers.RemoveListener(eventName, handler);
		}
		public void Broadcast<P0>(string eventName, P0 p0)
		{
			this.eventDispatchers.Broadcast(eventName,p0);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0,P1> AddListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			AddToListenerDict(eventName, handler);
			return this.eventDispatchers.AddListener(eventName, handler);
		}
		public bool RemoveListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this.eventDispatchers.RemoveListener(eventName, handler);
		}
		public void Broadcast<P0, P1>(string eventName, P0 p0, P1 p1)
		{
			this.eventDispatchers.Broadcast(eventName, p0, p1);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1,P2> AddListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			AddToListenerDict(eventName, handler);
			return this.eventDispatchers.AddListener(eventName, handler);
		}
		public bool RemoveListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this.eventDispatchers.RemoveListener(eventName, handler);
		}
		public void Broadcast<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
		{
			this.eventDispatchers.Broadcast(eventName, p0, p1,p2);
		}
		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2,P3> AddListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			AddToListenerDict(eventName, handler);
			return this.eventDispatchers.AddListener(eventName, handler);
		}
		public bool RemoveListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this.eventDispatchers.RemoveListener(eventName, handler);
		}
		public void Broadcast<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			this.eventDispatchers.Broadcast(eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			foreach (IEventDispatcher eventDispatcher in listenerDict.Keys)
			{
				foreach (string eventName in listenerDict[eventDispatcher].Keys)
				{
					var handler = listenerDict[eventDispatcher][eventName];
					eventDispatcher.IRemoveListener(eventName, handler);
				}
			}
			listenerDict.Clear();
		}

		public void Destroy()
		{
			RemoveAllListeners();
			
		}
	}
}