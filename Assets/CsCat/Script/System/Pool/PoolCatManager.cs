using System;
using System.Collections.Generic;
using XLua;

namespace CsCat
{
	public partial class PoolCatManager : ISingleton
	{
		private readonly Dictionary<string, IPoolCat> poolDict = new Dictionary<string, IPoolCat>();
		public static PoolCatManager instance => SingletonFactory.instance.Get<PoolCatManager>();

		public void SingleInit()
		{
		}

		public IPoolCat AddPool(string poolName, IPoolCat pool)
		{
			poolDict[poolName] = pool;
			return pool;
		}

		public void RemovePool(string poolName)
		{
			if (poolDict.ContainsKey(poolName))
			{
				poolDict[poolName].Destroy();
				poolDict.Remove(poolName);
			}
		}

		public IPoolCat GetPool(string poolName)
		{
			return poolDict[poolName];
		}

		public bool TryGetPool(string poolName, out IPoolCat pool)
		{
			return this.poolDict.TryGetValue(poolName, out pool);
		}

		public PoolCat<T> GetPool<T>(string poolName = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			return (PoolCat<T>)poolDict[poolName];
		}

		public bool IsContainsPool(string poolName)
		{
			return poolDict.ContainsKey(poolName);
		}

		public IPoolCat GetOrAddPool(Type poolType, params object[] poolConstructArgs)
		{
			string poolName = poolConstructArgs[0] as string;
			if (!IsContainsPool(poolName))
			{
				var pool = poolType.CreateInstance(poolConstructArgs) as IPoolCat;
				AddPool(poolName, pool);
				return pool;
			}

			poolName = poolName ?? poolType.FullName;
			return GetPool(poolName);
		}

		public PoolCat<T> GetOrAddPool<T>(params object[] poolConstructArgs)
		{
			string poolName = poolConstructArgs[0] as string;
			if (!IsContainsPool(poolName))
			{
				var pool = typeof(PoolCat<T>).CreateInstance(poolConstructArgs) as PoolCat<T>;
				AddPool(poolName, pool);
				return pool;
			}
			return GetPool<T>(poolName);
		}

		public void DeSpawnAll(string poolName)
		{
			if (!poolDict.ContainsKey(poolName))
				return;
			poolDict[poolName].DeSpawnAll();
		}
		

		public IPoolObject Spawn(Type spawnType, string poolName = null)
		{
			poolName = poolName ?? spawnType.FullName;
			if (!poolDict.TryGetValue(poolName, out var pool))
			{
				pool = this.InvokeGenericMethod("Spawn", new[] { spawnType }, false, poolName, null, null) as IPoolCat;
				poolDict[poolName] = pool;
			}
			var poolObject = pool.Spawn();
			return poolObject;
		}


		public PoolObject<T> Spawn<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			if (!poolDict.TryGetValue(poolName, out var pool))
			{
				pool = new PoolCat<T>(poolName, spawnFunc);
				poolDict[poolName] = pool;
			}

			var poolObject = ((PoolCat<T>)pool).Spawn(onSpawnCallback);
			return poolObject;
		}
	}
}