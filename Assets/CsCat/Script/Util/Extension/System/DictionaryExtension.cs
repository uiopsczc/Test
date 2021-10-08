using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace CsCat
{
    public static class DictionaryExtension
    {
        public static Dictionary<K, V> EmptyIfNull<K, V>(this Dictionary<K, V> self)
        {
            return self ?? new Dictionary<K, V>();
        }

        //删除值为null值、0数值、false逻辑值、空字符串、空集合等数据项
        public static void Trim(this IDictionary self)
        {
            List<object> toRemoveKeyList = new List<object>();
            foreach (var key in self.Keys)
            {
                var value = self[key];
                switch (value)
                {
                    //删除值为null的数值
                    case null:
                        toRemoveKeyList.Add(key);
                        break;
                    default:
                    {
                        if (value.IsNumber() && value.To<double>() == 0) //删除值为0的数值
                            toRemoveKeyList.Add(key);
                        else if (value.IsBool() && (bool) value == false) //删除值为false的逻辑值
                            toRemoveKeyList.Add(key);
                        else if (value.IsString() && ((string) value).IsNullOrWhiteSpace()) //删除值为空的字符串
                            toRemoveKeyList.Add(key);
                        else if (value is ICollection collection && collection.Count == 0) //删除为null的collection
                            toRemoveKeyList.Add(key);
                        else if (value is IDictionary dictionary)
                            Trim(dictionary);
                        break;
                    }
                }
            }

            foreach (var toRemoveKey in toRemoveKeyList)
                self.Remove(toRemoveKey);
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
        public static T GetOrAddDefault<T>(this IDictionary self, object key, Func<T> defaultValueFunc = null)
        {
            if (self.Contains(key)) return (T) self[key];
            self[key] = defaultValueFunc == null ? default : defaultValueFunc();
            return (T) self[key];
        }

        /// <summary>
        /// 没有的时候返回dv，不会设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="dv"></param>
        /// <returns></returns>
        public static T GetOrGetDefault<T>(this IDictionary self, object key, Func<T> defaultValueFunc = null)
        {
            return self == null || !self.Contains(key)
                ? defaultValueFunc != null ? default : defaultValueFunc()
                : (T) self[key];
        }

        public static void RemoveByFunc(this IDictionary self, Func<object, object, bool> func)
        {
            List<object> toRemoveKeysList = new List<object>();
            foreach (var key in self.Keys)
            {
                if (func(key, self[key]))
                    toRemoveKeysList.Add(key);
            }

            foreach (var toRemoveKey in toRemoveKeysList)
                self.Remove(toRemoveKey);
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
            self.RemoveByFunc((k, v) => ObjectUtil.Equals(value, v));
        }

        public static void RemoveAllAndClear<K, V>(this IDictionary<K, V> self, Action<K, V> onRemoveAction)
        {
            foreach (var key in self.Keys)
                onRemoveAction(key, self[key]);
            self.Clear();
        }


        public static void Remove2(this IDictionary self, object key)
        {
            Remove2<object>(self, key);
        }

        public static T Remove2<T>(this IDictionary self, object key)
        {
            if (!self.Contains(key))
                return default;

            T result = (T) self[key];
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
        /// <param name="combineCallback">其中带一个参数是key，第二个参数是source，第三个参数是antoher，（返回一个V，用于发生冲突时的替换策略）</param>
        /// <returns></returns>
        public static Dictionary<K, V> Combine<K, V>(this Dictionary<K, V> self, Dictionary<K, V> another,
            Func<K, Dictionary<K, V>, Dictionary<K, V>, V> combineCallback = null)
        {
            foreach (var anotherKey in another.Keys)
            {
                if (!self.ContainsKey(anotherKey))
                    self[anotherKey] = another[anotherKey];
                else //重复
                {
                    if (combineCallback != null)
                        self[anotherKey] = combineCallback(anotherKey, self, another);
                }
            }

            return self;
        }


        public static void Combine(this IDictionary self, IDictionary another)
        {
            foreach (var anotherKey in another.Keys)
            {
                if (!self.Contains(anotherKey))
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


        //////////////////////////////////////////////////////////////////////
        // Diff相关
        //////////////////////////////////////////////////////////////////////
        // 必须和ApplyDiff使用
        // 以new为基准，获取new相对于old不一样的部分
        // local diff = table.GetDiff(old, new)
        //  table.ApplyDiff(old, diff)
        // 这样old的就变成和new一模一样的数据
        public static LinkedHashtable GetDiff(this IDictionary oldDict, IDictionary newDict)
        {
            return DictionaryUtil.GetDiff(oldDict, newDict);
        }

        // table.ApplyDiff(old, diff)
        // 将diff中的东西应用到old中
        public static void ApplyDiff(this IDictionary oldDict, LinkedHashtable diffDict)
        {
            DictionaryUtil.ApplyDiff(oldDict, diffDict);
        }

        // 必须和ApplyDiff使用
        // 以new为基准，获取new中有，但old中没有的
        // local diff = table.GetNotExist(old, new)
        // table.ApplyDiff(old, diff)
        // 这样old就有new中的字段
        public static LinkedHashtable GetNotExist(this IDictionary oldDict, IDictionary newDict)
        {
            return DictionaryUtil.GetNotExist(oldDict, newDict);
        }

        //两个table是否不一样
        public static bool IsDiff(this IDictionary oldDict, IDictionary newDict)
        {
            return DictionaryUtil.IsDiff(oldDict, newDict);
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