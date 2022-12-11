using System;

namespace CsCat
{
	public partial class PoolCat<T>
	{
		protected virtual T _Spawn()
		{
			return _spawnFunc != null ? _spawnFunc() : (T)Activator.CreateInstance(typeof(T));
		}

		public virtual (PoolItem<T> poolItem, PoolItemIndex<T> poolItemIndex) Spawn()
		{
			return this.Spawn(null);
		}

		public virtual (PoolItem<T> poolItem, PoolItemIndex<T> poolItemIndex) Spawn(Action<T> onSpawnCallback = null)
		{
			PoolItem<T> poolItem;
			PoolItemIndex<T> poolItemIndex;
			for (var i = 0; i < _poolItemList.Count; i++)
			{
				poolItem = _poolItemList[i];
				if (poolItem.IsDespawned())
				{
					poolItem.SetIsDespawned(false);
					onSpawnCallback?.Invoke(poolItem.GetValue());
					poolItemIndex = new PoolItemIndex<T>(this, i);
					return (poolItem, poolItemIndex);
				}
			}
			int index = _poolItemList.Count;
			T value = _Spawn();
			poolItem = new PoolItem<T>(value, false);
			onSpawnCallback?.Invoke(poolItem.GetValue());
			_poolItemList.Add(poolItem);
			poolItemIndex = new PoolItemIndex<T>(this, index);
			return (poolItem, poolItemIndex);
		}

		public virtual T SpawnValue(Action<T> onSpawnCallback = null)
		{
			var (poolItem, poolItemIndex) = this.Spawn(onSpawnCallback);
			_OnSpawnValue(poolItem, poolItemIndex);
			return poolItem.GetValue();
		}

		protected void _OnSpawnValue(PoolItem<T> poolItem, PoolItemIndex<T> poolItemIndex)
		{
			var value = poolItem.GetValue();
			var index = poolItemIndex.GetIndex();
			this.valueToPoolItemIndexDict[value] = index;
		}
	}
}