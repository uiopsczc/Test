using System;
using Object = UnityEngine.Object;

namespace CsCat
{
	public static partial class PoolCatManagerUtil
	{
		public static CustomPoolCat AddCustomPool(string poolName, Func<object> spawnFunc)
		{
			var pool = new CustomPoolCat(poolName, spawnFunc);
			PoolCatManager.instance.AddPool(poolName, pool);
			return pool;
		}

		public static CustomPoolCat GetCustomPool(string poolName)
		{
			return PoolCatManager.instance.GetPool(poolName) as CustomPoolCat;
		}

		public static CustomPoolCat GetOrAddCustomPool(string poolName, Func<object> spawnFunc)
		{
			return PoolCatManager.instance.GetOrAddPool(typeof(CustomPoolCat), poolName, spawnFunc) as CustomPoolCat;
		}
	}
}