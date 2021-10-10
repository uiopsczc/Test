using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace CsCat
{
    public static class DictionaryKVExtension
    {
        public static Dictionary<K, V> EmptyIfNull<K, V>(this Dictionary<K, V> self)
        {
            return self ?? new Dictionary<K, V>();
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
    }
}