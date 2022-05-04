using System;

namespace CsCat
{
	public class EventDispatcher<P0, P1, P2> : IDeSpawn, IEventDispatcher
	{
		private readonly ValueListDictionary<string, KeyValuePairCat<Action<P0, P1, P2>, bool>> _listenerDict =
			new ValueListDictionary<string, KeyValuePairCat<Action<P0, P1, P2>, bool>>();

		public Action<P0, P1, P2> AddListener(string eventName, Action<P0, P1, P2> handler)
		{
			var handlerInfo = PoolCatManagerUtil.Spawn<KeyValuePairCat<Action<P0, P1, P2>, bool>>().Init(handler, true);
			_listenerDict.Add(eventName, handlerInfo);
			return handler;
		}

		public void IRemoveListener(string eventName, Delegate handler)
		{
			RemoveListener(eventName, (Action<P0, P1, P2>)handler);

		}
		public bool RemoveListener(string eventName, Action<P0, P1, P2> handler)
		{
			if (_listenerDict.TryGetValue(eventName, out var listenerList))
			{
				for (var i = 0; i < listenerList.Count; i++)
				{
					var handlerInfo = listenerList[i];
					if (handlerInfo.key != handler) continue;
					if (!handlerInfo.value) continue;
					handlerInfo.value = false;
					return true;
				}
			}


			return false;
		}

		public void RemoveAllListeners()
		{
			foreach (var keyValue in _listenerDict)
			{
				var handlerInfoList = keyValue.Value;
				for (var i = 0; i < handlerInfoList.Count; i++)
				{
					var handlerInfo = handlerInfoList[i];
					handlerInfo.value = false;
				}
			}
			CheckRemoved();
			CheckEmpty();
		}

		public void Broadcast(string eventName, P0 p0, P1 p1, P2 p2)
		{
			if (_listenerDict.TryGetValue(eventName, out var listenerList))
			{
				int count = listenerList.Count;
				for (int i = 0; i < count; i++)
				{
					var handlerInfo = listenerList[i];
					if (handlerInfo.value == false)
						continue;
					handlerInfo.key(p0, p1, p2);
				}

				CheckRemoved();
				CheckEmpty();
			}

		}

		private void CheckRemoved()
		{
			foreach (var keyValue in _listenerDict)
			{
				var handlerInfoList = keyValue.Value;
				for (int i = handlerInfoList.Count - 1; i >= 0; i--)
				{
					var handlerInfo = handlerInfoList[i];
					if (handlerInfo.value) continue;
					handlerInfoList.RemoveAt(i);
					handlerInfo.Despawn();
				}
			}
		}

		private void CheckEmpty()
		{
			foreach (var keyValue in _listenerDict)
			{
				var eventName = keyValue.Key;
				if (_listenerDict[eventName].Count == 0)
					_listenerDict.Remove(eventName);
			}
		}

		public void OnDeSpawn()
		{
			RemoveAllListeners();
		}
	}
}