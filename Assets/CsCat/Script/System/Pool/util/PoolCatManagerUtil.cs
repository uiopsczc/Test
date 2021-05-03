using System;
using System.Collections.Generic;

namespace CsCat
{
  public static partial class PoolCatManagerUtil
  {
    public static PoolCat AddPool(string pool_name, PoolCat pool)
    {
      return PoolCatManager.instance.AddPool(pool_name, pool);
    }

    public static void RemovePool(string pool_name)
    {
      PoolCatManager.instance.RemovePool(pool_name);
    }

    public static PoolCat GetPool(string pool_name)
    {
      return PoolCatManager.instance.GetPool(pool_name);
    }

    public static PoolCat GetPool<T>()
    {
      return PoolCatManager.instance.GetPool<T>();
    }

    public static bool IsContainsPool(string name)
    {
      return PoolCatManager.instance.IsContainsPool(name);
    }

    public static PoolCat GetOrAddPool(Type pool_type, params object[] pool_construct_args)
    {
      return PoolCatManager.instance.GetOrAddPool(pool_type, pool_construct_args);
    }

    public static T GetOrAddPool<T>(params object[] pool_construct_args) where T : PoolCat
    {
      return PoolCatManager.instance.GetOrAddPool<T>(pool_construct_args);
    }


    public static void Despawn(object despawn, string pool_name)
    {
      PoolCatManager.instance.Despawn(despawn, pool_name);
    }

    public static void Despawn(object o)
    {
      PoolCatManager.instance.Despawn(o);
    }

    public static void DespawnAll(string pool_name)
    {
      PoolCatManager.instance.DespawnAll(pool_name);
    }

    public static object Spawn(Type type,string pool_name = null, Action<object> on_spawn_callback = null)
    {
      return PoolCatManager.instance.Spawn(type, pool_name,on_spawn_callback);
    }

    public static T Spawn<T>(string pool_name = null, Action<T> on_spawn_callback = null)
    {
      if (on_spawn_callback == null)
        return PoolCatManager.instance.Spawn<T>(pool_name);
      return PoolCatManager.instance.Spawn<T>(pool_name,obj => on_spawn_callback((T)obj));
    }

    public static EventName SpawnEventName()
    {
      if (!PoolCatManager.instance.TryGetPool(PoolNameConst.EventName, out var pool))
      {
        pool = new EventNamePoolCat();
        PoolCatManager.instance.AddPool(PoolNameConst.EventName, pool);
      }

      return pool.Spawn<EventName>();
    }

  }
}