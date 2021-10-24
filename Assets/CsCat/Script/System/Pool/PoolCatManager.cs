using System;
using System.Collections.Generic;

namespace CsCat
{
    public partial class PoolCatManager : ISingleton
    {
        private readonly Dictionary<string, PoolCat> poolDict = new Dictionary<string, PoolCat>();
        public static PoolCatManager instance => SingletonFactory.instance.Get<PoolCatManager>();

        public void SingleInit()
        {
        }

        public PoolCat AddPool(string poolName, PoolCat pool)
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

        public PoolCat GetPool(string poolName)
        {
            return poolDict[poolName];
        }

        public bool TryGetPool(string poolName, out PoolCat pool)
        {
            return this.poolDict.TryGetValue(poolName, out pool);
        }

        public PoolCat GetPool<T>()
        {
            return poolDict[typeof(T).FullName];
        }

        public bool IsContainsPool(string poolName)
        {
            return poolDict.ContainsKey(poolName);
        }

        public PoolCat GetOrAddPool(Type poolType, params object[] poolConstructArgs)
        {
            string poolName = poolConstructArgs[0] as string;
            if (!IsContainsPool(poolName))
                AddPool(poolName, poolType.CreateInstance(poolConstructArgs) as PoolCat);
            return GetPool(poolName);
        }

        public T GetOrAddPool<T>(params object[] poolConstructArgs) where T : PoolCat
        {
            return (T) GetOrAddPool(typeof(T), poolConstructArgs);
        }


        public void Despawn(object despawnObject, string poolName = null)
        {
            poolName = poolName ?? despawnObject.GetType().FullName;
            if (!poolDict.ContainsKey(poolName))
                return;
            poolDict[poolName].Despawn(despawnObject);
        }

        public void DespawnAll(string poolName)
        {
            if (!poolDict.ContainsKey(poolName))
                return;
            poolDict[poolName].DespawnAll();
        }

        public void Trim()
        {
            //      List<string> to_remove_list = new List<string>();
            //      foreach (var key in pool_dict.Keys)
            //      {
            //        var pool = pool_dict[key];
            //        pool.Trim();
            //        if (pool.IsEmpty())
            //          to_remove_list.Add(key);
            //      }
            //
            //      foreach (var to_remove_key in to_remove_list)
            //        pool_dict.Remove(to_remove_key);


            foreach (var key in poolDict.Keys)
            {
                var pool = poolDict[key];
                pool.Trim();
            }
        }

        public object Spawn(Type spawnType, string poolName = null, Action<object> onSpawnCallback = null)
        {
            poolName = poolName ?? spawnType.FullName;
            if (!poolDict.TryGetValue(poolName, out var pool))
            {
                pool = new PoolCat(poolName, spawnType);
                poolDict[poolName] = pool;
            }

            var spawn = pool.Spawn(onSpawnCallback);
            return spawn;
        }

        public T Spawn<T>(string poolName = null, Action<T> onSpawnCallback = null)
        {
            if (onSpawnCallback == null)
                return (T) Spawn(typeof(T), poolName);
            return (T) Spawn(typeof(T), poolName, obj => onSpawnCallback((T) obj));
        }


        public object Spawn(Func<object> spawnFunc, string poolName, Action<object> onSpawnCallback = null)
        {
            if (!poolDict.TryGetValue(poolName, out var pool))
            {
                pool = new PoolCat(poolName, spawnFunc);
                poolDict[poolName] = pool;
            }

            var spawn = pool.Spawn(onSpawnCallback);
            return spawn;
        }

        public T Spawn<T>(Func<object> spawnFunc, string poolName = null, Action<T> onSpawnCallback = null)
        {
            if (onSpawnCallback == null)
                return (T) Spawn(spawnFunc, poolName ?? typeof(T).FullName);
            return (T) Spawn(spawnFunc, poolName ?? typeof(T).FullName, obj => onSpawnCallback((T) obj));
        }
    }
}