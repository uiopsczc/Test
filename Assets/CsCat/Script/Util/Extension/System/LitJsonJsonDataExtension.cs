using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class LitJsonJsonDataExtension
  {
    public static Dictionary<K,V> ToDict<K,V>(this LitJson.JsonData self)
    {
      if (self == null)
        return null;
      Dictionary<K, V> result = new Dictionary<K, V>();
      foreach (var key in self.Keys)
        result[key.To<K>()] = self[key].To<V>();
      return result;
    }

    public static List<T> ToList<T>(this LitJson.JsonData self)
    {
      if (self == null)
        return null;
      List<T> result = new List<T>();
      foreach (var value in self)
        result.Add(value.To<T>());
      return result;
    }


    public static T[] ToArray<T>(this LitJson.JsonData self)
    {
      if (self == null)
        return null;
      T[] result = new T[self.Count];
      for (int i = 0; i < self.Count; i++)
        result[i] = self[i].To<T>();
      return result;
    }


  }
}