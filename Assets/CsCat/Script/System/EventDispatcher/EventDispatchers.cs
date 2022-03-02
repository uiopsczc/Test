using System;
using System.Collections.Generic;

namespace CsCat
{
	public class EventDispatchers
	{
		private const string EventDispatcherName = "EventDispatcher";
		private readonly Dictionary<string, bool> _argsDict = new Dictionary<string, bool>(); //<argTypes, bool>

		private Dictionary<string, IEventDispatcher>
			eventDispatcherDict = new Dictionary<string, IEventDispatcher>(); //<argTypes, eventDispatcher>

		public EventDispatcher GetEventDispatcher()
		{
			var args = EventDispatcherName;
			if (_argsDict.ContainsKey(args))
			{
				var result = eventDispatcherDict.Get<EventDispatcher>(EventDispatcherName);
				return result;
			}
			else
			{
				_argsDict[args] = true;
				var result = new EventDispatcher();
				eventDispatcherDict[args] = result;
				return result;
			}
		}

		public EventDispatcher<P0> GetEventDispatcher<P0>()
		{
			var args = EventDispatcherName + typeof(P0);
			if (_argsDict.ContainsKey(args))
			{
				var result = eventDispatcherDict.Get<EventDispatcher<P0>>(args);
				return result;
			}
			else
			{
				_argsDict[args] = true;
				var result = new EventDispatcher<P0>();
				eventDispatcherDict[args] = result;
				return result;
			}
		}

		public EventDispatcher<P0, P1> GetEventDispatcher<P0, P1>()
		{
			var args = EventDispatcherName + typeof(P0) + typeof(P1);
			if (_argsDict.ContainsKey(args))
			{
				var result = eventDispatcherDict.Get<EventDispatcher<P0, P1>>(args);
				return result;
			}
			else
			{
				_argsDict[args] = true;
				var result = new EventDispatcher<P0, P1>();
				eventDispatcherDict[args] = result;
				return result;
			}
		}

		public EventDispatcher<P0, P1, P2> GetEventDispatcher<P0, P1, P2>()
		{
			var args = EventDispatcherName + typeof(P0) + typeof(P1) + typeof(P2);
			if (_argsDict.ContainsKey(args))
			{
				var result = eventDispatcherDict.Get<EventDispatcher<P0, P1, P2>>(args);
				return result;
			}
			else
			{
				_argsDict[args] = true;
				var result = new EventDispatcher<P0, P1, P2>();
				eventDispatcherDict[args] = result;
				return result;
			}
		}

		public EventDispatcher<P0, P1, P2, P3> GetEventDispatcher<P0, P1, P2, P3>()
		{
			var args = EventDispatcherName + typeof(P0) + typeof(P1) + typeof(P2) + typeof(P3);
			if (_argsDict.ContainsKey(args))
			{
				var result = eventDispatcherDict.Get<EventDispatcher<P0, P1, P2, P3>>(args);
				return result;
			}
			else
			{
				_argsDict[args] = true;
				var result = new EventDispatcher<P0, P1, P2, P3>();
				eventDispatcherDict[args] = result;
				return result;
			}
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

		public void Broadcast(string eventName)
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

		public void Broadcast<P0>(string eventName, P0 p0)
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

		public void Broadcast<P0, P1>(string eventName, P0 p0, P1 p1)
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

		public void Broadcast<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
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

		public void Broadcast<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			this.GetEventDispatcher<P0, P1, P2, P3>().Broadcast(eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			foreach (var keyValue in eventDispatcherDict)
			{
				IEventDispatcher eventDispatcher = keyValue.Value;
				eventDispatcher.RemoveAllListeners();
			}
			eventDispatcherDict.Clear();
			_argsDict.Clear();
			eventDispatcherDict.Clear();
		}
	}
}