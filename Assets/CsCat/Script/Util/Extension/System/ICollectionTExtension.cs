using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsCat
{
    public static class ICollectionTExtension
    {
        /// <summary>
        /// 转化为ToLinkedHashtable
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static object ToLinkedHashtable2<T>(this ICollection<T> self)
        {
            if (self is IDictionary dictionary)
            {
                LinkedHashtable linkedHashtable = new LinkedHashtable();
                foreach (var key in dictionary.Keys)
                {
                    var value = dictionary[key];
                    linkedHashtable.Put(key, value.ToLinkedHashtable2());
                }

                return linkedHashtable;
            }

            ArrayList list = new ArrayList();
            foreach (object o in self)
                list.Add(o.ToLinkedHashtable2());

            return list;
        }

        /// <summary>
        /// 用于不同类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this ICollection<T> self)
        {
            List<T> result = new List<T>(self.Count);
            result.AddRange(self);
            return result;
        }

        /// <summary>
        /// 用于不同类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ICollection<T> self)
        {
            T[] result = new T[self.Count];
            using (var scope = self.GetEnumerator().Scope())
            {
                int curIndex = -1;
                while (scope.iterator.MoveNext(ref curIndex))
                    result[curIndex] = scope.iterator.Current;
                return result;
            }
        }


        public static T[] ToArray<U, T>(this ICollection<U> collection, Func<U, T> covertElementFunc)
        {
            T[] result = new T[collection.Count];
            using (var scope = collection.GetEnumerator().Scope())
            {
                var curIndex = -1;
                while (scope.iterator.MoveNext(ref curIndex))
                    result[curIndex] = covertElementFunc(scope.iterator.Current);
                return result;
            }
        }
    }
}