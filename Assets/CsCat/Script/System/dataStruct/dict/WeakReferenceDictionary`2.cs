using System.Collections.Generic;
using System;

namespace CsCat
{
  /// <summary>
  /// WeakReferenceDictionary
  /// </summary>
  /// <typeparam name="K"></typeparam>
  /// <typeparam name="V"></typeparam>
  public class WeakReferenceDictionary<K, V>
  {
    #region field

    private Dictionary<K, WeakReference> dict = new Dictionary<K, WeakReference>();

    #endregion

    #region property

    public ICollection<K> Keys
    {
      get { return this.dict.Keys; }
    }

    public ICollection<WeakReference> ReferenceValues
    {
      get { return this.dict.Values; }
    }

    public List<V> Values
    {
      get
      {
        List<V> list = new List<V>();
        foreach (K current in this.dict.Keys)
        {
          V v;
          if (TryGetValue(current, out v))
            list.Add(v);
        }

        return list;
      }
    }

    public V this[K key]
    {
      get
      {
        V v;
        if (TryGetValue(key, out v))
        {
          return v;
        }

        return default(V);
      }
      set { this.Add(key, value); }
    }

    #endregion

    #region public method

    public void Add(K key, V value)
    {
      if (!this.dict.ContainsKey(key))
      {
        this.dict.Add(key, new WeakReference(value));
        return;
      }

      this.dict[key] = new WeakReference(value);
    }

    public void Clear()
    {
      this.dict.Clear();
    }

    public bool ContainsKey(K key)
    {
      return this.dict.ContainsKey(key);
    }

    public bool Remove(K key)
    {
      return this.dict.Remove(key);
    }

    public bool TryGetValue(K key, out V value)
    {
      if (this.dict.ContainsKey(key))
      {
        var valueResult = this.dict[key].GetValueResult<V>();
        value = valueResult.value;
        return valueResult.is_has_value;
      }

      value = default(V);
      return false;
    }

    public void GC()
    {
      List<K> to_remove_list = new List<K>();
      foreach (var e in dict.Keys)
      {
        if (!dict[e].IsAlive)
          to_remove_list.Add(e);
      }

      if (to_remove_list.Count > 0)
      {
        foreach (var e in to_remove_list)
          dict.Remove(e);
        System.GC.Collect();
      }
    }

    #endregion
  }
}