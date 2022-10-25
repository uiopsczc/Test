using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PoolCat<T>
	{
		public void Despawn(PoolItem<T> poolItem)
		{
			poolItem.SetIsDespawned(false);
			var value = poolItem.GetValue();
			Despawn(value);
		}

		public virtual void Despawn(T value)
		{
			IDespawn spawnable = value as IDespawn;
			spawnable?.OnDespawn();
		}

		public virtual void DespawnValue(T value)
		{
			int index = this._valueToPoolIndexDict[value];
			var poolItem = this.GetPoolItemAtIndex(index);
			Despawn(poolItem);
		}


		public void DespawnAll()
		{
			for (int i = 0; i < _poolItemList.Count; i++)
			{
				var poolItem = _poolItemList[i];
				if (!poolItem.IsDespawned())
					Despawn(poolItem);
			}
			_valueToPoolIndexDict?.Clear();
		}
	}
}