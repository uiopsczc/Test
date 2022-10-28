using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolIndex<T>:IPoolIndex
	{
		private PoolCat<T> _pool;
		//在pool中的index
		private int _index;
		public PoolIndex(PoolCat<T> pool, int index)
		{
			this._pool = pool;
			this._index = index;
		}

		//在pool中的PoolItem
		public PoolItem<T> GetPoolItem()
		{
			return this._pool.GetPoolItemAtIndex(this._index);
		}

		//在pool中的PoolItem的value
		public T GetValue()
		{
			return this.GetPoolItem().GetValue();
		}

		//在pool中的index
		public int GetIndex()
		{
			return this._index;
		}

		public void Despawn()
		{
			this._pool.Despawn(this.GetPoolItem());
		}

		object IPoolIndex.GetValue()
		{
			return this.GetValue();
		}
	}
}