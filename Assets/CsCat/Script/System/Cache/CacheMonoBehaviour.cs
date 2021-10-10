
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class CacheMonoBehaviour : MonoBehaviour
  {
    public Dictionary<string, object> dict = new Dictionary<string, object>();

    public void Set(object obj, string key = null)
    {
      if (key == null)
        key = obj.GetType().FullName;
      dict[key] = obj;
    }

    public void Set(object obj, string key, string sub_key)
    {
      if (key == null)
        key = obj.GetType().FullName;
      Dictionary<string, object> sub_dict = dict.GetOrAddDefault2(key, () => new Dictionary<string, object>());
      sub_dict[sub_key] = obj;
    }

    public T Get<T>(string key = null)
    {
      if (key == null)
        key = typeof(T).FullName;
      return (T)dict[key];
    }

    public T Get<T>(string key, string sub_key)
    {
      Dictionary<string, object> sub_dict = Get<Dictionary<string, object>>(key);
      return (T)sub_dict[key];
    }

    public T GetOrAdd<T>(string key, Func<T> defaultFunc)
    {
      if (key == null)
        key = typeof(T).FullName;
      if (!dict.ContainsKey(key))
        dict[key] = defaultFunc();
      return (T)dict[key];
    }

    public T GetOrAdd<T>(string key, string sub_key, Func<T> defaultFunc)
    {
      Dictionary<string, object> sub_dict = GetOrAdd(key, () => new Dictionary<string, object>());
      return sub_dict.GetOrAddDefault2(sub_key, defaultFunc);
    }

  }
}