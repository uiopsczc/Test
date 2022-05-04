using System;
using System.Collections.Generic;

namespace CsCat
{
	public static partial class PoolCatManagerUtil
	{
		public static IPoolCat AddPool(string poolName, IPoolCat pool)
		{
			return PoolCatManager.instance.AddPool(poolName, pool);
		}

		public static void RemovePool(string poolName)
		{
			PoolCatManager.instance.RemovePool(poolName);
		}

		public static IPoolCat GetPool(string poolName)
		{
			return PoolCatManager.instance.GetPool(poolName);
		}

		public static bool TryGetPool(string poolName, out IPoolCat pool)
		{
			return PoolCatManager.instance.TryGetPool(poolName, out pool);
		}

		public static IPoolCat GetPool<T>(string poolName = null)
		{
			return PoolCatManager.instance.GetPool<T>(poolName);
		}

		public static bool IsContainsPool(string poolName)
		{
			return PoolCatManager.instance.IsContainsPool(poolName);
		}

		public static IPoolCat GetOrAddPool(Type poolType, params object[] poolConstructArgs)
		{
			return PoolCatManager.instance.GetOrAddPool(poolType, poolConstructArgs);
		}

		public static PoolCat<T> GetOrAddPool<T>(params object[] poolConstructArgs)
		{
			return PoolCatManager.instance.GetOrAddPool<T>(poolConstructArgs);
		}

		public static void DeSpawnAll(string poolName)
		{
			PoolCatManager.instance.DeSpawnAll(poolName);
		}

		public static IPoolObject Spawn(Type type, string poolName = null)
		{
			return PoolCatManager.instance.Spawn(type, poolName);
		}

		public static PoolObject<T> Spawn<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			return PoolCatManager.instance.Spawn(poolName, spawnFunc, onSpawnCallback);
		}
	}
}