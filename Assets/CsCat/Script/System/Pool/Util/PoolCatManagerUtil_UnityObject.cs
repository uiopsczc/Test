using System;
using Object = UnityEngine.Object;

namespace CsCat
{
	public static partial class PoolCatManagerUtil
	{
		public static UnityObjectPoolCat<T> AddUnityObjectPool<T>(string poolName, T prefab) where T : Object
		{
			var pool = new UnityObjectPoolCat<T>(poolName, prefab);
			PoolCatManager.instance.AddPool(poolName, pool);
			return pool;
		}

		public static UnityObjectPoolCat<T> GetOrAddUnityObjectPool<T>(string poolName, T prefab) where T : Object
		{
			if (TryGetPool(poolName, out var pool))
				return pool as UnityObjectPoolCat<T>;
			return AddUnityObjectPool(poolName, prefab);
		}
	}
}