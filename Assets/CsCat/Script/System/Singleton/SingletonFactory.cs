using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  /// 所有单例都从这里出来
  /// </summary>
  public class SingletonFactory : ISingleton
  {

    /// <summary>
    /// 非mono类的单例集合
    /// </summary>
    public Dictionary<Type, object> dict = new Dictionary<Type, object>();

    /// <summary>
    /// Mono类的单例集合
    /// </summary>
    public Dictionary<Type, GameObject> mono_dict = new Dictionary<Type, GameObject>();



    private static SingletonFactory _instance;


    public static SingletonFactory instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new SingletonFactory();
        }

        return _instance;
      }
    }


      public void SingleInit()
      {
      }


        /// <summary>
        /// 获取非mono类的单例集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : ISingleton, new()
    {
      object result = null;
      Type type = typeof(T);
      if (!dict.ContainsKey(type))
        dict[type] = SingletonUtil.GetInstance((T)result);
      return (T)dict[type];
    }

    /// <summary>
    /// Mono类的单例集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetMono<T>() where T : MonoBehaviour, ISingleton
    {
      object result = null;
      Type type = typeof(T);
      if (!mono_dict.ContainsKey(type) || mono_dict[type] == null)
        mono_dict[type] = SingletonUtil.GetInstnaceMono((T)result).gameObject;

      return mono_dict[type].GetComponent(typeof(T)) as T;
    }



      
  }
}