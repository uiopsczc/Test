using System;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  /// 缓存
  /// </summary>
  public class Cache : IDespawn
  {
    #region field

    public Dictionary<object, object> dict = new Dictionary<object, object>();

    #endregion

    public object this[object key]
    {
      get { return dict[key]; }
      set { dict[key] = value; }
    }

    public void Remove(object key)
    {
      this.dict.Remove(key);
    }

    public object Get(object key)
    {
      return this.dict[key];
    }

    public T Get<T>(object key)
    {
      return (T)this.dict[key];
    }

    public bool ContainsKey(object key)
    {
      if (this.dict.ContainsKey(key))
        return true;
      return false;
    }

    public bool ContainsValue(object value)
    {
      if (this.dict.ContainsValue(value))
        return true;
      return false;
    }


    public T GetOrAddDefault<T>(object key, Func<T> dvFunc = null)
    {
      return dict.GetOrAddDefault<T>(key, dvFunc);
    }

    public T GetOrAddDefault<T>(Func<T> dvFunc = null)
    {
      return dict.GetOrAddDefault<T>(typeof(T).ToString(), dvFunc);
    }


    public T GetOrGetDefault<T>(object key, Func<T> dvFunc = null)
    {
      return dict.GetOrGetDefault<T>(key, dvFunc);
    }

    public object Remove2(object key)
    {
      return Remove2<object>(key);
    }

    public T Remove2<T>(object key)
    {
      return dict.Remove2<T>(key);
    }

    public void Clear()
    {
      dict.Clear();
    }

    public void OnDespawn()
    {
      Clear();
    }

  }
}