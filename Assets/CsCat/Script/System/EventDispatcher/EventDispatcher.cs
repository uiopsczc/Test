using System;

namespace CsCat
{
	public class EventDispatcher : IDespawn, IEventDispatcher
	{
		private ValueListDictionary<string, KeyValuePairCat<Action, bool>> listenerDict =
			new ValueListDictionary<string, KeyValuePairCat<Action, bool>>();

		public Action AddListener(string eventName, Action handler)
		{
			var handlerInfo = PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, bool>>().Init(handler, true);
			listenerDict.Add(eventName, handlerInfo);
			return handler;
		}

		public void IRemoveListener(string eventName, object handler)
		{
			if (listenerDict.TryGetValue(eventName, out var listenerList))
			{
				for (var i = 0; i < listenerList.Count; i++)
				{
					var handlerInfo = listenerList[i];
					if (handlerInfo.key != handler) continue;
					if (!handlerInfo.value) continue;
					handlerInfo.value = false;
					return;
				}
			}
		}

		public bool RemoveListener(string eventName, Action handler)
		{
			if (listenerDict.TryGetValue(eventName, out var listenerList))
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
			foreach (var handlerInfoList in listenerDict.Values)
			{
				for (var i = 0; i < handlerInfoList.Count; i++)
				{
					var handlerInfo = handlerInfoList[i];
					handlerInfo.value = false;
				}
			}
		}

		public void Broadcast(string eventName)
		{
			if (listenerDict.TryGetValue(eventName, out var listenerList))
			{
				int count = listenerList.Count;
				for (int i = 0; i < count; i++)
				{
					var handlerInfo = listenerList[i];
					if (handlerInfo.value == false)
						continue;
					handlerInfo.key();
				}

				CheckRemoved();
				CheckEmpty();
			}
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
			CheckRemoved();
			CheckEmpty();
		}
	}
}