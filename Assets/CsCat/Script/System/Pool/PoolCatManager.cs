using System;
using System.Collections.Generic;
using XLua;

namespace CsCat
{
	public partial class PoolCatManager
	{
		private readonly Dictionary<string, IPoolCat> poolDict = new Dictionary<string, IPoolCat>();
		

		public IPoolCat AddPool(string poolName, IPoolCat pool)
		{
			poolDict[poolName] = pool;
			pool.SetPoolManager(this);
			return pool;
		}

		public void RemovePool(string poolName)
		{
			if (poolDict.TryGetValue(poolName, out var pool))
			{
				pool.Destroy();
				poolDict.Remove(poolName);
			}
		}

		public IPoolCat GetPool(string poolName)
		{
			return poolDict[poolName];
		}

		public PoolCat<T> GetPool<T>(string poolName = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			return this.GetPool(poolName) as PoolCat<T>;
		}

		public bool TryGetPool(string poolName, out IPoolCat pool)
		{
			return this.poolDict.TryGetValue(poolName, out pool);
		}

		public bool IsContainsPool(string poolName)
		{
			return poolDict.ContainsKey(poolName);
		}

		public IPoolCat GetOrAddPool(Type poolType, params object[] poolConstructArgs)
		{
			if (poolConstructArgs.Length == 0)
				poolConstructArgs = new[] {poolType.FullName};
			string poolName = (string)poolConstructArgs[0];
			if (this.TryGetPool(poolName, out var pool))
				return pool;
			pool = poolType.CreateInstance(poolConstructArgs) as IPoolCat;
			AddPool(poolName, pool);
			return pool;
		}

		public void DespawnAll(string poolName)
		{
			if (poolDict.TryGetValue(poolName, out var pool))
			{
				pool.DespawnAll();
			}
		}
		

		public IPoolObject Spawn(Type spawnType, string poolName = null)
		{
			poolName = poolName ?? spawnType.FullName;
			if (!poolDict.TryGetValue(poolName, out var pool))
			{
				pool = this.InvokeGenericMethod("Spawn", new[] { spawnType }, false, poolName, null, null) as IPoolCat;
				this.AddPool(poolName, pool);
			}
			var poolObject = pool.InvokeMethod<IPoolObject>("Spawn");
			return poolObject;
		}


		public PoolObject<T> Spawn<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			if (!poolDict.TryGetValue(poolName, out var pool))
			{
				pool = new PoolCat<T>(poolName, spawnFunc);
				this.AddPool(poolName, pool);
			}
			var poolObject = ((PoolCat<T>)pool).Spawn(onSpawnCallback);
			return poolObject;
		}
	}
}