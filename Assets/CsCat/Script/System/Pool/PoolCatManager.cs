using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class PoolCatManager : ISingleton
  {
    private readonly Dictionary<string, PoolCat> pool_dict = new Dictionary<string, PoolCat>();
    public static PoolCatManager instance => SingletonFactory.instance.Get<PoolCatManager>();

    public PoolCat AddPool(string pool_name, PoolCat pool)
    {
      pool_dict[pool_name] = pool;
      return pool;
    }

    public void RemovePool(string pool_name)
    {
      if (pool_dict.ContainsKey(pool_name))
      {
        pool_dict[pool_name].Destroy();
        pool_dict.Remove(pool_name);
      }
    }

    public PoolCat GetPool(string pool_name)
    {
      return pool_dict[pool_name];
    }

    public PoolCat GetPool<T>()
    {
      return pool_dict[typeof(T).ToString()];
    }

    public bool IsContainsPool(string pool_name)
    {
      return pool_dict.ContainsKey(pool_name);
    }

    public PoolCat GetOrAddPool(Type pool_type, params object[] pool_construct_args)
    {
      string pool_name = pool_construct_args[0] as string;
      if (!IsContainsPool(pool_name))
        AddPool(pool_name, pool_type.CreateInstance(pool_construct_args) as PoolCat);
      return GetPool(pool_name);
    }

    public T GetOrAddPool<T>(params object[] pool_construct_args) where T : PoolCat
    {
      return (T)GetOrAddPool(typeof(T), pool_construct_args);
    }


    public void Despawn(object despawn_object, string pool_name = null)
    {
      pool_name = pool_name??despawn_object.GetType().Name;
      if (!pool_dict.ContainsKey(pool_name))
        return;
      pool_dict[pool_name].Despawn(despawn_object);
    }

    public void DespawnAll(string pool_name)
    {
      if (!pool_dict.ContainsKey(pool_name))
        return;
      pool_dict[pool_name].DespawnAll();
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


      foreach (var key in pool_dict.Keys)
      {
        var pool = pool_dict[key];
        pool.Trim();
      }
    }

    public object Spawn(Type spawn_type,string pool_name = null, Action<object> on_spawn_callback = null)
    {
      pool_name = pool_name??spawn_type.ToString();
      if (!pool_dict.ContainsKey(pool_name))
        pool_dict[pool_name] = new PoolCat(pool_name, spawn_type);
      var pool = pool_dict[pool_name];
      var spawn = pool.Spawn(on_spawn_callback);
      return spawn;
    }
    

    public T Spawn<T>(string pool_name = null,Action < T> on_spawn_callback = null)
    {
      if (on_spawn_callback == null)
        return (T)Spawn(typeof(T), pool_name);
      return (T)Spawn(typeof(T), pool_name, obj => on_spawn_callback((T)obj));
    }
  }
}