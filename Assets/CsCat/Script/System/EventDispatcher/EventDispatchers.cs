using System;
using System.Collections.Generic;

namespace CsCat
{
	public class EventDispatchers
	{
		private Dictionary<object, IEventDispatcher>
			_eventDispatcherDict = new Dictionary<object, IEventDispatcher>(); //<argTypes, eventDispatcher>

		public EventDispatcher GetEventDispatcher()
		{
			var key = "";
			EventDispatcher result = null;
			if (_eventDispatcherDict.TryGetValue(key, out var eventDispatcher))
				return eventDispatcher as EventDispatcher;
			result = new EventDispatcher();
			_eventDispatcherDict[key] = result;
			return result;
		}

		public EventDispatcher<P0> GetEventDispatcher<P0>()
		{
			var key = typeof(P0);
			EventDispatcher<P0> result = null;
			if (_eventDispatcherDict.TryGetValue(key, out var eventDispatcher))
				return eventDispatcher as EventDispatcher<P0>;
			result = new EventDispatcher<P0>();
			_eventDispatcherDict[key] = result;
			return result;
		}

		public EventDispatcher<P0, P1> GetEventDispatcher<P0, P1>()
		{
			var key = (typeof(P0), typeof(P1));
			EventDispatcher<P0,P1> result = null;
			if (_eventDispatcherDict.TryGetValue(key, out var eventDispatcher))
				return eventDispatcher as EventDispatcher<P0, P1>;
			result = new EventDispatcher<P0, P1>();
			_eventDispatcherDict[key] = result;
			return result;
		}

		public EventDispatcher<P0, P1, P2> GetEventDispatcher<P0, P1, P2>()
		{
			var key = (typeof(P0), typeof(P1), typeof(P2));
			EventDispatcher<P0, P1, P2> result = null;
			if (_eventDispatcherDict.TryGetValue(key, out var eventDispatcher))
				return eventDispatcher as EventDispatcher<P0, P1, P2>;
			result = new EventDispatcher<P0, P1, P2>();
			_eventDispatcherDict[key] = result;
			return result;
		}

		public EventDispatcher<P0, P1, P2, P3> GetEventDispatcher<P0, P1, P2, P3>()
		{
			var key = (typeof(P0), typeof(P1), typeof(P2), typeof(P3));
			EventDispatcher<P0, P1, P2, P3> result = null;
			if (_eventDispatcherDict.TryGetValue(key, out var eventDispatcher))
				return eventDispatcher as EventDispatcher<P0, P1, P2, P3>;
			result = new EventDispatcher<P0, P1, P2, P3>();
			_eventDispatcherDict[key] = result;
			return result;
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action AddListener(string eventName, Action handler)
		{
			return this.GetEventDispatcher().AddListener(eventName, handler);
		}

		public bool RemoveListener(string eventName, Action handler)
		{
			return this.GetEventDispatcher().RemoveListener(eventName, handler);
		}

		public void FireEvent(string eventName)
		{
			this.GetEventDispatcher().Broadcast(eventName);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0> AddListener<P0>(string eventName, Action<P0> handler)
		{
			return this.GetEventDispatcher<P0>().AddListener(eventName, handler);
		}

		public bool RemoveListener<P0>(string eventName, Action<P0> handler)
		{
			return this.GetEventDispatcher<P0>().RemoveListener(eventName, handler);
		}

		public void FireEvent<P0>(string eventName, P0 p0)
		{
			this.GetEventDispatcher<P0>().Broadcast(eventName, p0);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1> AddListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			return this.GetEventDispatcher<P0, P1>().AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			return this.GetEventDispatcher<P0, P1>().RemoveListener(eventName, handler);
		}

		public void FireEvent<P0, P1>(string eventName, P0 p0, P1 p1)
		{
			this.GetEventDispatcher<P0, P1>().Broadcast(eventName, p0, p1);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2> AddListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			return this.GetEventDispatcher<P0, P1, P2>().AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			return this.GetEventDispatcher<P0, P1, P2>().RemoveListener(eventName, handler);
		}

		public void FireEvent<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
		{
			this.GetEventDispatcher<P0, P1, P2>().Broadcast(eventName, p0, p1, p2);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			return this.GetEventDispatcher<P0, P1, P2, P3>().AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			return this.GetEventDispatcher<P0, P1, P2, P3>().RemoveListener(eventName, handler);
		}

		public void FireEvent<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			this.GetEventDispatcher<P0, P1, P2, P3>().Broadcast(eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			foreach (var keyValue in _eventDispatcherDict)
			{
				IEventDispatcher eventDispatcher = keyValue.Value;
				eventDispatcher.RemoveAllListeners();
			}
			_eventDispatcherDict.Clear();
		}
	}
}