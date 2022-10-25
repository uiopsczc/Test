using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolObjectIndex
	{
		private readonly IPoolCat _pool;
		private readonly int _indexInPool;

		public PoolObjectIndex(IPoolCat pool, int indexInPool)
		{
			this._pool = pool;
			this._indexInPool = indexInPool;
		}

		public IPoolItem GetPoolObject()
		{
			return this._pool.InvokeMethod<IPoolItem>("GetPoolItemAtIndex", false, this._indexInPool);
		}

		public T GetValue<T>()
		{
			return GetPoolObject().InvokeGenericMethod<T>("GetValue", new[] {typeof(T)});
		}

		public int GetIndexInPool()
		{
			return this._indexInPool;
		}

		public IPoolCat GetPool()
		{
			return this._pool;
		}

		public override int GetHashCode()
		{
			return this._pool.GetHashCode() ^ this._indexInPool.GetHashCode();
		}

		public override bool Equals(object other)
		{
			PoolObjectIndex otherPoolObjectIndex = other as PoolObjectIndex;
			if (otherPoolObjectIndex == null)
				return false;
			return otherPoolObjectIndex.GetPool() == this._pool &&
			       otherPoolObjectIndex.GetIndexInPool() == this._indexInPool;
		}
	}
}