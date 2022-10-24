using System;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class PoolCatManager
	{
		public UnityObjectPoolCat<T> AddUnityObjectPool<T>(string poolName, T prefab) where T : Object
		{
			var pool = new UnityObjectPoolCat<T>(poolName, prefab);
			this.AddPool(poolName, pool);
			return pool;
		}

		public UnityObjectPoolCat<T> GetOrAddUnityObjectPool<T>(string poolName, T prefab) where T : Object
		{
			if (this.TryGetPool(poolName, out var pool))
				return pool as UnityObjectPoolCat<T>;
			return AddUnityObjectPool(poolName, prefab);
		}
	}
}