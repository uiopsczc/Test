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

    protected Dictionary<object, bool> all_object_dict = new Dictionary<object, bool>();
    private Type spawn_type;
    public string pool_name;
    


    public PoolCat(string pool_name, Type spawn_type)
    {
      this.pool_name = pool_name;
      this.spawn_type = spawn_type;
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
      return Activator.CreateInstance(spawn_type);
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
      all_object_dict[spawn] = true;
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
      if (!all_object_dict.ContainsKey(obj) || all_object_dict[obj] == false)
        return;
      despawned_object_stack.Push(obj);
      all_object_dict[obj] = false;
      if (obj is IDespawn spawnable)
        spawnable.OnDespawn();
    }

    public virtual void Trim()
    {
      foreach (var despawned_object in despawned_object_stack)
      {
        all_object_dict.Remove(despawned_object);
        __Trim(despawned_object);
      }

      despawned_object_stack.Clear();
    }

    protected virtual void __Trim(object despawned_object)
    {
    }

    public void DespawnAll()
    {
      foreach (var obj in all_object_dict.Keys)
      {
        if (!all_object_dict[obj])
          Despawn(obj);
      }
    }

    public bool IsEmpty()
    {
      if (this.all_object_dict.Count == 0)
        return true;
      return false;
    }

    public virtual void Destroy()
    {
      spawn_type = null;
      pool_name = null;

      despawned_object_stack.Clear();
      all_object_dict.Clear();
    }
  }
}