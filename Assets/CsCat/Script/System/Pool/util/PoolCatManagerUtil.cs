using System;
using System.Collections.Generic;

namespace CsCat
{
    public static partial class PoolCatManagerUtil
    {
        public static PoolCat AddPool(string poolName, PoolCat pool)
        {
            return PoolCatManager.instance.AddPool(poolName, pool);
        }

        public static void RemovePool(string poolName)
        {
            PoolCatManager.instance.RemovePool(poolName);
        }

        public static PoolCat GetPool(string poolName)
        {
            return PoolCatManager.instance.GetPool(poolName);
        }

        public static PoolCat GetPool<T>()
        {
            return PoolCatManager.instance.GetPool<T>();
        }

        public static bool IsContainsPool(string name)
        {
            return PoolCatManager.instance.IsContainsPool(name);
        }

        public static PoolCat GetOrAddPool(Type poolType, params object[] poolConstructArgs)
        {
            return PoolCatManager.instance.GetOrAddPool(poolType, poolConstructArgs);
        }

        public static T GetOrAddPool<T>(params object[] poolConstructArgs) where T : PoolCat
        {
            return PoolCatManager.instance.GetOrAddPool<T>(poolConstructArgs);
        }


        public static void Despawn(object despawn, string poolName)
        {
            PoolCatManager.instance.Despawn(despawn, poolName);
        }

        public static void Despawn(object o)
        {
            PoolCatManager.instance.Despawn(o);
        }

        public static void DespawnAll(string poolName)
        {
            PoolCatManager.instance.DespawnAll(poolName);
        }

        public static object Spawn(Type type, string poolName = null, Action<object> onSpawnCallback = null)
        {
            return PoolCatManager.instance.Spawn(type, poolName, onSpawnCallback);
        }

        public static T Spawn<T>(string poolName = null, Action<T> onSpawnCallback = null)
        {
            if (onSpawnCallback == null)
                return PoolCatManager.instance.Spawn<T>(poolName);
            return PoolCatManager.instance.Spawn<T>(poolName, obj => onSpawnCallback(obj));
        }


        public static object Spawn(Func<object> spawnFunc, string poolName, Action<object> onSpawnCallback = null)
        {
            return PoolCatManager.instance.Spawn(spawnFunc, poolName, onSpawnCallback);
        }

        public static T Spawn<T>(Func<object> spawnFunc, string poolName = null, Action<T> onSpawnCallback = null)
        {
            return PoolCatManager.instance.Spawn<T>(spawnFunc, poolName, onSpawnCallback);
        }
    }
}