using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolObject<T>: IPoolObject
	{
		private readonly T _value;
		private bool _isDeSpawned;//是否回收了
		private PoolObjectInfo _poolObjectInfo;

		public PoolObject(PoolCat<T> pool, int indexInPool, T value, bool isDeSpawned)
		{
			this._poolObjectInfo = new PoolObjectInfo(pool, indexInPool);
			this._value = value;
			this._isDeSpawned = isDeSpawned;
		}

		public int GetIndexInPool()
		{
			return this._poolObjectInfo.GetIndexInPool();
		}

		public T GetValue()
		{
			return this._value;
		}

		public void SetIsDeSpawned(bool isDeSpawned)
		{
			this._isDeSpawned = isDeSpawned;
		}

		public bool IsDeSpawned()
		{
			return this._isDeSpawned;
		}

		public void DeSpawn()
		{
			this._poolObjectInfo.GetPool().DeSpawn(this);
			this._isDeSpawned = true;
		}
	}
}