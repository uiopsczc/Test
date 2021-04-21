using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public static class DictionaryExtension
  {

    public static Dictionary<K, V> EmptyIfNull<K, V>(this Dictionary<K, V> self)
    {
      if (self == null)
        return new Dictionary<K, V>();
      return self;
    }

    //删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
    public static void Trim(this IDictionary self)
    {
      List<object> to_remove_key_list = new List<object>();
      foreach (var key in self.Keys)
      {
        var value = self[key];
        if (value == null) //删除值为null的数值
          to_remove_key_list.Add(key);
        else if (value.IsNumber() && value.To<double>() == 0) //删除值为0的数值
          to_remove_key_list.Add(key);
        else if (value.IsBool() && (bool)value == false) //删除值为false的逻辑值
          to_remove_key_list.Add(key);
        else if (value.IsString() && ((string)value).IsNullOrWhiteSpace()) //删除值为空的字符串
          to_remove_key_list.Add(key);
        else if (value is ICollection && ((ICollection)value).Count == 0) //删除为null的collection
          to_remove_key_list.Add(key);
        else if (value is IDictionary)
          Trim((IDictionary)value);
      }

      foreach (var to_remove_key in to_remove_key_list)
        self.Remove(to_remove_key);
    }

    public static Hashtable ToHashtable(this IDictionary self)
    {
      Hashtable result = new Hashtable();
      foreach (var key in self.Keys)
        result[key] = self[key];
      return result;
    }


    /// <summary>
    ///例子
    ///Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
    ///dict.GetOrAddNew<List<string>>("kk").Add("chenzhongmou");
    ///采用延迟调用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <param name="dv"></param>
    /// <returns></returns>
    public static T GetOrAddDefault<T>(this IDictionary self, object key, Func<T> dvFunc = null)
    {
      if (!self.Contains(key))
      {
        if (dvFunc == null)
          self[key] = default(T);
        else
          self[key] = dvFunc();
      }

      return (T)self[key];
    }

    /// <summary>
    /// 没有的时候返回dv，不会设置值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <param name="dv"></param>
    /// <returns></returns>
    public static T GetOrGetDefault<T>(this IDictionary self, object key, Func<T> dvFunc = null)
    {
      if (self == null || !self.Contains(key))
      {
        if (dvFunc == null)
          return default(T);
        else
          return dvFunc();
      }

      return (T)self[key];
    }

    public static void RemoveByFunc(this IDictionary self, Func<object, object, bool> func)
    {
      List<object> to_remove_list = new List<object>();
      foreach (var key in self.Keys)
      {
        if (func(key, self[key]))
          to_remove_list.Add(key);
      }

      foreach (var to_remove_key in to_remove_list)
      {
        self.Remove(to_remove_key);
      }
    }

    public static void RemoveByFunc<K, V>(this IDictionary<K, V> self, Func<K, V, bool> func)
    {
      List<K> to_remove_list = new List<K>();
      foreach (var key in self.Keys)
      {
        if (func(key, self[key]))
          to_remove_list.Add(key);
      }

      foreach (var to_remove_key in to_remove_list)
      {
        self.Remove(to_remove_key);
      }
    }

    public static void Remove2(this IDictionary self, object key)
    {
      Remove2<object>(self, key);
    }

    public static T Remove2<T>(this IDictionary self, object key)
    {
      if (!self.Contains(key))
        return default(T);

      T result = (T)self[key];
      self.Remove(key);
      return result;
    }

    /// <summary>
    /// 合并两个Dictionary
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="self"></param>
    /// <param name="another"></param>
    /// <param name="combine_callback">其中带一个参数是key，第二个参数是source，第三个参数是antoher，（返回一个V，用于发生冲突时的替换策略）</param>
    /// <returns></returns>
    public static Dictionary<K, V> Combine<K, V>(this Dictionary<K, V> self, Dictionary<K, V> another,
      Func<K, Dictionary<K, V>, Dictionary<K, V>, V> combine_callback = null)
    {
      foreach (K another_key in another.Keys)
      {
        if (!self.ContainsKey(another_key))
        {
          self[another_key] = another[another_key];
        }
        else //重复
        {
          if (combine_callback != null)
            self[another_key] = combine_callback(another_key, self, another);
        }
      }

      return self;
    }


    public static void Combine(this IDictionary self, IDictionary another)
    {
      foreach (var another_key in another.Keys)
      {
        if (!self.Contains(another_key))
        {
          self[another_key] = another[another_key];
        }
      }
    }


    public static List<T> RandomList<T>(this IDictionary<T, float> self, int out_count, bool is_unique,
      RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      return randomManager.RandomList(self, out_count, is_unique);
    }

    public static T Random<T>(this IDictionary<T, float> self, RandomManager randomManager = null)
    {
      return self.RandomList(1, false, randomManager)[0];
    }

    public static T Get<T>(this IDictionary self, object key)
    {
      if (self.Contains(key))
        return self[key].To<T>();
      else
        return default(T);
    }


    //////////////////////////////////////////////////////////////////////
    // Diff相关
    //////////////////////////////////////////////////////////////////////
    // 必须和ApplyDiff使用
    // 以new为基准，获取new相对于old不一样的部分
    // local diff = table.GetDiff(old, new)
    //  table.ApplyDiff(old, diff)
    // 这样old的就变成和new一模一样的数据
    public static LinkedHashtable GetDiff(this IDictionary old_dict, IDictionary new_dict)
    {
      return DictionaryUtil.GetDiff(old_dict, new_dict);
    }

    // table.ApplyDiff(old, diff)
    // 将diff中的东西应用到old中
    public static void ApplyDiff(this IDictionary old_dict, LinkedHashtable diff_dict)
    {
      DictionaryUtil.ApplyDiff(old_dict, diff_dict);
    }

    // 必须和ApplyDiff使用
    // 以new为基准，获取new中有，但old中没有的
    // local diff = table.GetNotExist(old, new)
    // table.ApplyDiff(old, diff)
    // 这样old就有new中的字段
    public static LinkedHashtable GetNotExist(this IDictionary old_dict, IDictionary new_dict)
    {

      return DictionaryUtil.GetNotExist(old_dict, new_dict);
    }

    //两个table是否不一样
    public static bool IsDiff(this IDictionary old_dict, IDictionary new_dict)
    {
      return DictionaryUtil.IsDiff(old_dict, new_dict);
    }

    public static K FindKey<K, V>(this IDictionary<K, V> self, K key)
    {
      foreach (var k in self.Keys)
      {
        if (k.Equals(key))
          return k;
      }
      return default(K);
    }
  }
}