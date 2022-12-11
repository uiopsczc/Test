namespace CsCat
{
	public class PoolItemIndex<T>:IPoolItemIndex
	{
		private readonly PoolCat<T> _pool;
		//在pool中的index
		private readonly int _index;
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

		public T2 GetValue<T2>() where  T2:class 
		{
			return GetValue() as T2;
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

		object IPoolItemIndex.GetValue()
		{
			return this.GetValue();
		}
	}
}