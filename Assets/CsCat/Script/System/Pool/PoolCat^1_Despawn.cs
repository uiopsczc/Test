namespace CsCat
{
	public partial class PoolCat<T>
	{
		public virtual void Despawn(PoolItem<T> poolItem)
		{
			poolItem.SetIsDespawned(false);
			var value = poolItem.GetValue();
			_Despawn(value);
		}

		void _Despawn(T value)
		{
			IDespawn spawnable = value as IDespawn;
			spawnable?.Despawn();
		}

		public virtual void DespawnValue(T value)
		{
			if (_valueToPoolItemIndexDict.TryGetValue(value, out var index))
			{
				var poolItem = this.GetPoolItemAtIndex(index);
				Despawn(poolItem);
			}
		}


		public void DespawnAll()
		{
			for (int i = 0; i < _poolItemList.Count; i++)
			{
				var poolItem = _poolItemList[i];
				if (!poolItem.IsDespawned())
					Despawn(poolItem);
			}
			_valueToPoolItemIndexDict?.Clear();
		}
	}
}