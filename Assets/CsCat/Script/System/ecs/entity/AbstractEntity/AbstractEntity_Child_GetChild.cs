using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {

    public AbstractEntity GetChild(string child_key)
    {
      if (!key_to_child_dict.ContainsKey(child_key))
        return null;
      if (key_to_child_dict[child_key].IsDestroyed())
        return null;
      return key_to_child_dict[child_key];
    }

    public T GetChild<T>(string child_key) where T : AbstractEntity
    {
      return (T)GetChild(child_key);
    }

    public AbstractEntity GetChild(Type child_type)
    {
      foreach (var child in ForeachChild(child_type))
        return child;
      return null;
    }

    public T GetChild<T>() where T : AbstractEntity
    {
      return GetChild(typeof(T)) as T;
    }

    //效率问题引入的
    public AbstractEntity GetChildStrictly(Type child_type)
    {
      if (!this.type_to_childList_dict.ContainsKey(child_type))
        return null;
      foreach (var child in type_to_childList_dict[child_type])
      {
        if (!child.IsDestroyed())
          return child;
      }
      return null;
    }

    public T GetChildStrictly<T>() where T : AbstractEntity
    {
      return GetChildStrictly(typeof(T)) as T;
    }


    public AbstractEntity[] GetChildren(Type child_type)
    {
      List<AbstractEntity> list = PoolCatManagerUtil.Spawn<List<AbstractEntity>>();
      try
      {
        foreach (var child in ForeachChild(child_type))
          list.Add(child);
        return list.ToArray();
      }
      finally
      {
        list.Clear();
        PoolCatManagerUtil.Despawn(list);
      }

    }

    public T[] GetChildren<T>() where T : AbstractEntity
    {
      return (T[])GetChildren(typeof(T));
    }


    public AbstractEntity[] GetChildrenStrictly(Type child_type)
    {
      List<AbstractEntity> list = new List<AbstractEntity>();
      try
      {
        if (!this.type_to_childList_dict.ContainsKey(child_type))
          return list.ToArray();
        foreach (var child in type_to_childList_dict[child_type])
        {
          if (!child.IsDestroyed())
            list.Add(child);
        }
        return list.ToArray();
      }
      finally
      {
        list.Clear();
        PoolCatManagerUtil.Despawn(list);
      }

    }

    public T[] GetChildrenStrictly<T>() where T : AbstractEntity
    {
      return (T[])GetChildrenStrictly(typeof(T));
    }
  }
}