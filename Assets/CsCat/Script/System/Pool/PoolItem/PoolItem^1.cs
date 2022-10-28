namespace CsCat
{
	public class PoolItem<T>:IPoolItem
	{
		private readonly T _value;
		private bool _isDespawned;//是否回收了

		public PoolItem(T value, bool isDespawned)
		{
			this._value = value;
			this._isDespawned = isDespawned;
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
	}
}