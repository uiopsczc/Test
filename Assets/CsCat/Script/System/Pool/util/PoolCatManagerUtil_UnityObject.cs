using System;
using Object = UnityEngine.Object;

namespace CsCat
{
	public static partial class PoolCatManagerUtil
	{
		public static UnityObjectPoolCat AddUnityObjectPool(string poolName, Object prefab, string category = null)
		{
			var pool = new UnityObjectPoolCat(poolName, prefab, category);
			PoolCatManager.instance.AddPool(poolName, pool);
			return pool;
		}

		public static UnityObjectPoolCat GetUnityObjectPool(string poolName)
		{
			return PoolCatManager.instance.GetPool(poolName) as UnityObjectPoolCat;
		}

		public static UnityObjectPoolCat GetOrAddUnityObjectPool(string poolName, Object prefab,
			string category = null)
		{
			return PoolCatManager.instance.GetOrAddPool(typeof(UnityObjectPoolCat), poolName, prefab, category) as
				UnityObjectPoolCat;
		}
	}
}