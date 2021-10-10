using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace CsCat
{
    public static class IDictionaryKVExtension
    {
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
        public static V GetOrAddDefault<K, V>(this IDictionary<K, V> self, K key, Func<V> defaultValueFunc = null)
        {
            if (self.ContainsKey(key)) return self[key];
            self[key] = defaultValueFunc == null ? default : defaultValueFunc();
            return self[key];
        }

        /// <summary>
        /// 没有的时候返回dv，不会设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="dv"></param>
        /// <returns></returns>
        public static V GetOrGetDefault<K, V>(this IDictionary<K, V> self, K key, Func<V> defaultValueFunc = null)
        {
            return self == null || !self.ContainsKey(key)
                ? defaultValueFunc == null ? default : defaultValueFunc()
                : self[key];
        }
        

        public static void RemoveByFunc<K, V>(this IDictionary<K, V> self, Func<K, V, bool> func)
        {
            List<K> toRemoveKeyList = new List<K>();
            foreach (var key in self.Keys)
            {
                if (func(key, self[key]))
                    toRemoveKeyList.Add(key);
            }

            foreach (var toRemoveKey in toRemoveKeyList)
                self.Remove(toRemoveKey);
        }

        public static void RemoveByValue<K, V>(this IDictionary<K, V> self, V value)
        {
            List<K> toRemoveKeyList = new List<K>();
            foreach (var key in self.Keys)
            {
                if (ObjectUtil.Equals(self[key], value))
                    toRemoveKeyList.Add(key);
            }

            foreach (var toRemoveKey in toRemoveKeyList)
                self.Remove(toRemoveKey);
        }

        public static void RemoveAllAndClear<K, V>(this IDictionary<K, V> self, Action<K, V> onRemoveAction)
        {
            foreach (var key in self.Keys)
                onRemoveAction(key, self[key]);
            self.Clear();
        }


        public static V Remove2<K,V>(this IDictionary<K,V> self, K key)
        {
            if (!self.ContainsKey(key))
                return default;

            V result = self[key];
            self.Remove(key);
            return result;
        }

        


        public static void Combine<K,V>(this IDictionary<K,V> self, IDictionary<K,V> another)
        {
            foreach (var anotherKey in another.Keys)
            {
                if (!self.ContainsKey(anotherKey))
                    self[anotherKey] = another[anotherKey];
            }
        }


        public static List<T> RandomList<T>(this IDictionary<T, float> self, int outCount, bool isUnique,
            RandomManager randomManager = null)
        {
            randomManager = randomManager ?? Client.instance.randomManager;
            return randomManager.RandomList(self, outCount, isUnique);
        }

        public static T Random<T>(this IDictionary<T, float> self, RandomManager randomManager = null)
        {
            return self.RandomList(1, false, randomManager)[0];
        }

        public static T Get<T>(this IDictionary self, object key)
        {
            return self.Contains(key) ? self[key].To<T>() : default;
        }


        

        public static K FindKey<K, V>(this IDictionary<K, V> self, K key)
        {
            foreach (var k in self.Keys)
                if (k.Equals(key))
                    return k;

            return default;
        }
    }
}