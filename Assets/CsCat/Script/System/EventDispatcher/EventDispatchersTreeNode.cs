
using System;
using System.Collections.Generic;

namespace CsCat
{
	//多个eventDispatchers使用，使用的时候需要指明是那个eventDispatchers
	public class EventDispatchersTreeNode : TreeNode
	{
		public EventDispatchers eventDispatchers = new EventDispatchers();

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action AddListener(string eventName, Action handler)
		{
			return eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener(string eventName, Action handler)
		{
			return eventDispatchers.RemoveListener(eventName, handler);
		}

		public void FireEvent(string eventName)
		{
			eventDispatchers.FireEvent(eventName);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0> AddListener<P0>(string eventName, Action<P0> handler)
		{
			return eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0>(string eventName, Action<P0> handler)
		{
			return eventDispatchers.RemoveListener(eventName, handler);
		}

		public void FireEvent<P0>(string eventName, P0 p0)
		{
			eventDispatchers.FireEvent(eventName, p0);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1> AddListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			return eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1>(string eventName, Action<P0, P1> handler)
		{
			return eventDispatchers.RemoveListener(eventName, handler);
		}

		public void FireEvent<P0, P1>(string eventName, P0 p0, P1 p1)
		{
			eventDispatchers.FireEvent(eventName, p0, p1);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2> AddListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			return eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
		{
			return eventDispatchers.RemoveListener(eventName, handler);
		}

		public void FireEvent<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
		{
			eventDispatchers.FireEvent(eventName, p0, p1, p2);
		}

		////////////////////////////////////////////////////////////////////////////////////////////
		public Action<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			return eventDispatchers.AddListener(eventName, handler);
		}

		public bool RemoveListener<P0, P1, P2, P3>(string eventName, Action<P0, P1, P2, P3> handler)
		{
			return eventDispatchers.RemoveListener(eventName, handler);
		}

		public void FireEvent<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
		{
			eventDispatchers.FireEvent(eventName, p0, p1, p2, p3);
		}

		public void RemoveAllListeners()
		{
			eventDispatchers.RemoveAllListeners();
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
