using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PoolCat<T>
	{
		protected virtual T _Spawn()
		{
			return _spawnFunc != null ? _spawnFunc() : (T)Activator.CreateInstance(typeof(T));
		}

		public virtual (PoolItem<T> poolItem, PoolIndex<T> poolIndex) Spawn()
		{
			return this.Spawn(null);
		}

		public virtual (PoolItem<T> poolItem, PoolIndex<T> poolIndex) Spawn(Action<T> onSpawnCallback = null)
		{
			PoolItem<T> poolItem;
			PoolIndex<T> poolIndex;
			for (var i = 0; i < _poolItemList.Count; i++)
			{
				poolItem = _poolItemList[i];
				if (poolItem.IsDespawned())
				{
					poolItem.SetIsDespawned(false);
					onSpawnCallback?.Invoke(poolItem.GetValue());
					poolIndex = new PoolIndex<T>(this, i);
					return (poolItem, poolIndex);
				}
			}
			int index = _poolItemList.Count;
			T value = _Spawn();
			poolItem = new PoolItem<T>(value, false);
			onSpawnCallback?.Invoke(poolItem.GetValue());
			_poolItemList.Add(poolItem);
			poolIndex = new PoolIndex<T>(this, index);
			return (poolItem, poolIndex);
		}

		public virtual T SpawnValue(Action<T> onSpawnCallback = null)
		{
			var (poolItem, poolIndex) = this.Spawn(onSpawnCallback);
			OnSpawnValue(poolItem, poolIndex);
			return poolItem.GetValue();
		}

		protected void OnSpawnValue(PoolItem<T> poolItem, PoolIndex<T> poolIndex)
		{
			var value = poolItem.GetValue();
			var index = poolIndex.GetIndex();
			this.valueToPoolIndexDict[value] = index;
		}
	}
}