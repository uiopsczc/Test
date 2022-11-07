using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolItemIndex<T>:IPoolItemIndex
	{
		private PoolCat<T> _pool;
		//��pool�е�index
		private int _index;
		public PoolItemIndex(PoolCat<T> pool, int index)
		{
			this._pool = pool;
			this._index = index;
		}

		public PoolCat<T> GetPool()
		{
			return this._pool;
		}

		public IPoolCat GetIPool()
		{
			return this._pool;
		}

		//��pool�е�PoolItem
		public PoolItem<T> GetPoolItem()
		{
			return this._pool.GetPoolItemAtIndex(this._index);
		}

		//��pool�е�PoolItem��value
		public T GetValue()
		{
			return this.GetPoolItem().GetValue();
		}

		public T2 GetValue<T2>() where  T2:class 
		{
			return GetValue() as T2;
		}

		//��pool�е�index
		public int GetIndex()
		{
			return this._index;
		}

		public void Despawn()
		{
			this._pool.Despawn(this.GetPoolItem());
		}

		object IPoolItemIndex.GetValue()
		{
			return this.GetValue();
		}
	}
}