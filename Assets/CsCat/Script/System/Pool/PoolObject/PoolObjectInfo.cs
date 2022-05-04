using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolObjectInfo
	{
		private IPoolCat _pool;
		private int _indexInPool;

		public PoolObjectInfo(IPoolCat pool, int indexInPool)
		{
			this._pool = pool;
			this._indexInPool = indexInPool;
		}

		public IPoolObject GetPoolObject()
		{
			return this._pool.GetPoolObjectAtIndex(this._indexInPool);
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
			PoolObjectInfo otherPoolObjectInfo = other as PoolObjectInfo;
			if (otherPoolObjectInfo == null)
				return false;
			return otherPoolObjectInfo.GetPool() == this._pool &&
			       otherPoolObjectInfo.GetIndexInPool() == this._indexInPool;
		}
	}
}