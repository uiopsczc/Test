using System;
using System.Collections.Generic;

namespace CsCat
{
  public class ValueListDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>
  {
    public void Add(TKey key, TValue value, bool is_value_unique = false)
    {
      this.GetOrAddDefault(key, () => new List<TValue>());
      if (is_value_unique && this[key].Contains(value))
        return;
      this[key].Add(value);
    }

    public new void Clear()
    {
      base.Clear();
    }

    public bool Remove(TKey key, TValue value)
    {
      if (!this.ContainsKey(key))
        return false;
      var result = this[key].Remove(value);
      if (result)
        Check(key);
      return result;
    }

    public bool Contains(TKey key, TValue value)
    {
      return this.ContainsKey(key) && this[key].Contains(value);
    }

    public void Check(TKey key)
    {
      if (this.ContainsKey(key)&&this[key].IsNullOrEmpty())
        this.Remove(key);
    }

    public void CheckAll()
    {
      List<TKey> to_remove_key_list = new List<TKey>();
      foreach (var key in this.Keys)
      {
        if (this[key].IsNullOrEmpty())
          to_remove_key_list.Add(key);
      }

      foreach (var to_remove_key in to_remove_key_list)
        this.Remove(to_remove_key);
    }

    public void ForEach(Action<TKey, TValue> action)
    {
      foreach (var key in this.Keys)
      {
        var valueList = this[key];
        foreach (var value in valueList)
          action(key, value);
      }

      CheckAll();
    }

    public void Foreach(TKey key, Action<TValue> action, bool is_ignore_value_null = true)
    {
      if (!this.ContainsKey(key))
        return;
      List<TValue> value_list = this[key];
      if (value_list == null)
        return;

      foreach (TValue value in value_list)
      {
        if (is_ignore_value_null && value == null)
          continue;
        try
        {
          action(value);
        }
        catch (Exception e)
        {
          LogCat.LogError(e);
        }
      }

      CheckAll();
    }
  }
}