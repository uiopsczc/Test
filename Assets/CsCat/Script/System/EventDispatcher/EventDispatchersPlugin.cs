using System;
using System.Collections.Generic;

namespace CsCat
{
	//单个eventDispatchers使用，但创建的时候需要指明是哪一个EventDispatchers，使用的时候就是对那个EventDispatchers进行事件监听
	public class EventDispatchersPlugin
	{
		private readonly Dictionary<IEventDispatcher, Dictionary<string, List<Delegate>>> _listenerDict =
			new Dictionary<IEventDispatcher, Dictionary<string, List<Delegate>>>(); //<eventDispatcher, <eventName,list<handler>>>

		private readonly EventDispatchers _eventDispatchers;

		public EventDispatchersPlugin(EventDispatchers eventDispatchers)
		{
			this._eventDispatchers = eventDispatchers;
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		private void AddToListenerDict(string eventName, Action handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				_listenerDict[eventDispatcher] = new ValueListDictionary<string, Delegate>();
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				_listenerDict[eventDispatcher][eventName] = new List<Delegate>();
			_listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict(string eventName, Action handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			_listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0>(string eventName, Action<P0> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				_listenerDict[eventDispatcher] = new ValueListDictionary<string, Delegate>();
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				_listenerDict[eventDispatcher][eventName] = new List<Delegate>();
			_listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0>(string eventName, Action<P0> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			_listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0, P1>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				_listenerDict[eventDispatcher] = new ValueListDictionary<string, Delegate>();
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				_listenerDict[eventDispatcher][eventName] = new List<Delegate>();
			_listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0, P1>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			_listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0, P1, P2>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				_listenerDict[eventDispatcher] = new ValueListDictionary<string, Delegate>();
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				_listenerDict[eventDispatcher][eventName] = new List<Delegate>();
			_listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0, P1, P2>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			_listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		private void AddToListenerDict<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0, P1, P2, P3>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				_listenerDict[eventDispatcher] = new ValueListDictionary<string, Delegate>();
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				_listenerDict[eventDispatcher][eventName] = new List<Delegate>();
			_listenerDict[eventDispatcher][eventName].Add(handler);
		}

		private void RemoveFromListenerDict<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			var eventDispatcher = this._eventDispatchers.GetEventDispatcher<P0, P1, P2, P3>();
			if (!_listenerDict.ContainsKey(eventDispatcher))
				return;
			if (!_listenerDict[eventDispatcher].ContainsKey(eventName))
				return;
			_listenerDict[eventDispatcher][eventName].Remove(handler);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action AddListener(string eventName, Action handler)
		{
			AddToListenerDict(eventName, handler);
			return this._eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener(string eventName, Action handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this._eventDispatchers.RemoveListener(eventName, handler);
		}

		public void Broadcast(string eventName)
		{
			this._eventDispatchers.Broadcast(eventName);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0> AddListener<P0>(string eventName, Action<P0> handler)
		{
			AddToListenerDict(eventName, handler);
			return this._eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0>(string eventName, Action<P0> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this._eventDispatchers.RemoveListener(eventName, handler);
		}

		public void Broadcast<P0>(string eventName, P0 p0)
		{
			this._eventDispatchers.Broadcast(eventName, p0);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1> AddListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			AddToListenerDict(eventName, handler);
			return this._eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this._eventDispatchers.RemoveListener(eventName, handler);
		}

		public void Broadcast<P0, P1>(string eventName, P0 p0, P1 p1)
		{
			this._eventDispatchers.Broadcast(eventName, p0, p1);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2> AddListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			AddToListenerDict(eventName, handler);
			return this._eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this._eventDispatchers.RemoveListener(eventName, handler);
		}

		public void Broadcast<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
		{
			this._eventDispatchers.Broadcast(eventName, p0, p1, p2);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			AddToListenerDict(eventName, handler);
			return this._eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			RemoveFromListenerDict(eventName, handler);
			return this._eventDispatchers.RemoveListener(eventName, handler);
		}

		public void Broadcast<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			this._eventDispatchers.Broadcast(eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			foreach (var keyValue in _listenerDict)
			{
				var dict = keyValue.Value;
				var eventDispatcher = keyValue.Key;
				foreach (var keyValue2 in dict)
				{
					var handlerList = keyValue2.Value;
					var eventName = keyValue2.Key;
					for (int i = 0; i < handlerList.Count; i++)
					{
						eventDispatcher.IRemoveListener(eventName, handlerList[i]);
					}
				}
			}
			_listenerDict.Clear();
		}

		public void Destroy()
		{
			RemoveAllListeners();
		}
	}
}