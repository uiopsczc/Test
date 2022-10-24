namespace CsCat
{
	public class PoolObject<T>:IPoolObject
	{
		private readonly T _value;
		private bool _isDespawned;//是否回收了
		private readonly PoolObjectIndex _poolObjectIndex;

		public PoolObject(PoolCat<T> pool, int indexInPool, T value, bool isDespawned)
		{
			this._poolObjectIndex = new PoolObjectIndex(pool, indexInPool);
			this._value = value;
			this._isDespawned = isDespawned;
		}

		public int GetIndexInPool()
		{
			return this._poolObjectIndex.GetIndexInPool();
		}

		public PoolObjectIndex GetPoolObjectIndex()
		{
			return this._poolObjectIndex;
		}

		public T GetValue()
		{
			return this._value;
		}

		public void SetIsDespawned(bool isDespawned)
		{
			this._isDespawned = isDespawned;
		}

		public bool IsDespawned()
		{
			return this._isDespawned;
		}

		public void Despawn()
		{
			this._poolObjectIndex.GetPool().InvokeMethod("Despawn", false, this);
			this._isDespawned = true;
		}
	}
}