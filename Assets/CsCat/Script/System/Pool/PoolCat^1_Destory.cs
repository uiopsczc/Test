using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class PoolCat<T>
	{
		protected virtual void OnDestroy(T value)
		{
		}
		public virtual void Destroy()
		{
			DespawnAll();
			for (var i = 0; i < _poolItemList.Count; i++)
			{
				var poolItem = _poolItemList[i];
				OnDestroy(poolItem.GetValue());
			}
			_poolItemList.Clear();
			_poolName = null;
			_spawnFunc = null;
		}
	}
}