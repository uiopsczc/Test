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
        private readonly Dictionary<Type, object> _dict = new Dictionary<Type, object>();

        /// <summary>
        /// Mono类的单例集合
        /// </summary>
        public Dictionary<Type, GameObject> monoDict = new Dictionary<Type, GameObject>();


        private static SingletonFactory _instance;


        public static SingletonFactory instance => _instance ?? (_instance = new SingletonFactory());


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
            Type type = typeof(T);
            if (!_dict.ContainsKey(type))
                _dict[type] = SingletonUtil.GetInstance(default(T));
            return (T) _dict[type];
        }

        /// <summary>
        /// Mono类的单例集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetMono<T>() where T : MonoBehaviour, ISingleton
        {
            Type type = typeof(T);
            if (!monoDict.ContainsKey(type) || monoDict[type] == null)
                monoDict[type] = SingletonUtil.GetInstanceMono(default(T)).gameObject;

            return monoDict[type].GetComponent<T>();
        }
    }
}