using System;
using System.Collections.Generic;
using XLua;

namespace CsCat
{
	public partial class PoolCatManager
	{
		private static PoolCatManager _default;
		public static PoolCatManager Default => _default ?? (_default = new PoolCatManager());
		private readonly Dictionary<string, IPoolCat> _poolDict = new Dictionary<string, IPoolCat>();
		

		public IPoolCat AddPool(string poolName, IPoolCat pool)
		{
			_poolDict[poolName] = pool;
			pool.SetPoolManager(this);
			return pool;
		}

		public void RemovePool(string poolName)
		{
			if (_poolDict.TryGetValue(poolName, out var pool))
			{
				pool.Destroy();
				_poolDict.Remove(poolName);
			}
		}

		public IPoolCat GetPool(string poolName)
		{
			return _poolDict[poolName];
		}

		public PoolCat<T> GetPool<T>(string poolName = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			return this.GetPool(poolName) as PoolCat<T>;
		}

		public bool TryGetPool(string poolName, out IPoolCat pool)
		{
			return this._poolDict.TryGetValue(poolName, out pool);
		}

		public bool TryGetPool<T>(string poolName, out PoolCat<T> pool)
		{
			if (_poolDict.TryGetValue(poolName, out var tmpPool))
			{
				pool = tmpPool as PoolCat<T>;
				return true;
			}
			pool = null;
			return false;
		}

		public bool IsContainPool(string poolName)
		{
			return _poolDict.ContainsKey(poolName);
		}

		public bool IsContainPool<T>()
		{
			return IsContainPool(typeof(T).FullName);
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
			if (_poolDict.TryGetValue(poolName, out var pool))
				pool.DespawnAll();
		}
		

		public (IPoolItem poolItem, IPoolItemIndex poolItemIndex) Spawn(Type spawnType, string poolName = null)
		{
			return this.InvokeGenericMethod<(IPoolItem, IPoolItemIndex)>("Spawn", new[] {spawnType}, false, poolName, null, null);
		}

		public object SpawnValue(Type spawnType, string poolName = null)
		{
			return this.InvokeGenericMethod<object>("SpawnValue", new[] { spawnType }, false, poolName, null, null);
		}


		public (PoolItem<T> poolItem, PoolItemIndex<T> poolItemIndex) Spawn<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			if (!_poolDict.TryGetValue(poolName, out var pool))
			{
				pool = new PoolCat<T>(poolName, spawnFunc);
				this.AddPool(poolName, pool);
			}
			var (poolItem, poolItemIndex) = ((PoolCat<T>)pool).Spawn(onSpawnCallback);
			return (poolItem, poolItemIndex);
		}

		public T SpawnValue<T>(string poolName, Func<T> spawnFunc, Action<T> onSpawnCallback = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			if (!_poolDict.TryGetValue(poolName, out var pool))
			{
				pool = new PoolCat<T>(poolName, spawnFunc);
				this.AddPool(poolName, pool);
			}
			var value = ((PoolCat<T>)pool).SpawnValue(onSpawnCallback);
			return value;
		}

		public void DespawnValue<T>(T value, string poolName = null)
		{
			poolName = poolName ?? typeof(T).FullName;
			var pool = _poolDict[poolName] as PoolCat<T>;
			pool.DespawnValue(value);
		}
	}
}