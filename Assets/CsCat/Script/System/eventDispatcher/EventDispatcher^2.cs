using System;

namespace CsCat
{
	public class EventDispatcher<P0, P1> : IDespawn, IEventDispatcher
	{
		private ValueListDictionary<string, KeyValuePairCat<Action<P0, P1>, bool>> listenerDict =
			new ValueListDictionary<string, KeyValuePairCat<Action<P0, P1>, bool>>();

		public Action<P0, P1> AddListener(string eventName, Action<P0, P1> handler)
		{
			var handlerInfo = PoolCatManagerUtil.Spawn<KeyValuePairCat<Action<P0, P1>, bool>>().Init(handler, true);
			listenerDict.Add(eventName, handlerInfo);
			return handler;
		}

		public void IRemoveListener(string eventName, object handler)
		{
			if (!listenerDict.ContainsKey(eventName))
				return;
			foreach (var handlerInfo in listenerDict[eventName])
			{
				if (handlerInfo.key != handler)
					continue;
				if (!handlerInfo.value) continue;
				handlerInfo.value = false;
			}
		}

		public bool RemoveListener(string eventName, Action<P0, P1> handler)
		{
			if (!listenerDict.ContainsKey(eventName))
				return false;
			foreach (var handlerInfo in listenerDict[eventName])
			{
				if (handlerInfo.key != handler) continue;
				if (!handlerInfo.value) continue;
				handlerInfo.value = false;
				return true;
			}

			return false;
		}

		public void RemoveAllListeners()
		{
			foreach (var handlerInfoList in listenerDict.Values)
			{
				foreach (var handlerInfo in handlerInfoList)
					handlerInfo.value = false;
			}
			CheckRemoved();
			CheckEmpty();
		}

		public void Broadcast(string eventName, P0 p0, P1 p1)
		{
			if (!this.listenerDict.ContainsKey(eventName))
				return;
			int count = listenerDict[eventName].Count;
			for (int i = 0; i < count; i++)
			{
				var handlerInfo = listenerDict[eventName][i];
				if (handlerInfo.value == false)
					continue;
				handlerInfo.key(p0, p1);
			}

			CheckRemoved();
			CheckEmpty();
		}

		private void CheckRemoved()
		{
			foreach (var handlerInfoList in listenerDict.Values)
			{
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
			foreach (var eventName in listenerDict.Keys)
			{
				if (listenerDict[eventName].Count == 0)
					listenerDict.Remove(eventName);
			}
		}

		public void OnDespawn()
		{
			RemoveAllListeners();
		}
	}
}