using System;
using System.Collections.Generic;

namespace CsCat
{
  public class PoolCat
  {
    /// <summary>
    /// 存放object的数组
    /// </summary>
    protected Stack<object> despawned_object_stack = new Stack<object>();

    protected List<object> spawned_object_list = new List<object>();
    private Type spawn_type;
    public string pool_name;
    private Func<object> spawn_func;
    


    public PoolCat(string pool_name, Type spawn_type)
    {
      this.pool_name = pool_name;
      this.spawn_type = spawn_type;
    }

    public PoolCat(string pool_name, Func<object> spawn_func)
    {
      this.pool_name = pool_name;
      this.spawn_func = spawn_func;
    }

    public void InitPool(int init_count = 1, Action<object> on_spawn_callback = null)
    {
      for (int i = 0; i < init_count; i++)
        Despawn(Spawn(on_spawn_callback));
    }


    #region virtual method

    /// <summary>
    /// 子类中重写spawn中需要用到的newObject
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected virtual object __Spawn()
    {
//      LogCat.warn(pool_name);
      return spawn_func != null ? spawn_func() : Activator.CreateInstance(spawn_type);
    }

    #endregion

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual object Spawn(Action<object> on_spawn_callback = null)
    {
      object spawn = null;
      spawn = despawned_object_stack.Count > 0 ? despawned_object_stack.Pop() : __Spawn();
      on_spawn_callback?.Invoke(spawn);
      spawned_object_list.Add(spawn);
      return spawn;
    }

    public T Spawn<T>(Action<object> on_spawn_callback = null)
    {
      return (T) Spawn(on_spawn_callback);
    }

    public virtual void Despawn(object obj)
    {
      if (obj == null)
        return;
      if (!spawned_object_list.Contains(obj))
      {
        LogCat.error(string.Format("pool: {0} not contained::{1}",pool_name, obj));
        return;
      }
        
      despawned_object_stack.Push(obj);
      spawned_object_list.Remove(obj);
      if (obj is IDespawn spawnable)
        spawnable.OnDespawn();
    }

    public virtual void Trim()
    {
      foreach (var despawned_object in despawned_object_stack)
        __Trim(despawned_object);
      despawned_object_stack.Clear();
    }

    protected virtual void __Trim(object despawned_object)
    {
    }

    public void DespawnAll()
    {
      for(int i =spawned_object_list.Count-1;i>=0;i--)
        Despawn(spawned_object_list[i]);
    }

    public bool IsEmpty()
    {
      if (this.spawned_object_list.Count == 0&& despawned_object_stack.Count==0)
        return true;
      return false;
    }

    public virtual void Destroy()
    {
      DespawnAll();
      Trim();

      spawn_type = null;
      pool_name = null;
      spawn_func = null;

      despawned_object_stack.Clear();
      spawned_object_list.Clear();
    }
  }
}