using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsCat
{
    public static class CollectionExtension
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
        /// 将s从fromIndex开始,到toIndex（不包括toIndex）结束的元素转为字符串连接起来，元素之间用n连接（最后的元素后面不加n）
        /// 例如：object[] s={"aa","bb","cc"} split="DD" return "aaDDbbDDcc"
        /// </summary>
        public static string Join<T>(this ICollection<T> self, int fromIndex, int toIndex, string separator)
        {
            if (fromIndex < 0 || toIndex > self.Count || toIndex - fromIndex < 0) throw new IndexOutOfRangeException();
            using (var scope = new StringBuilderScope())
            {
                if (toIndex - fromIndex <= 0)
                    return scope.stringBuilder.ToString();
                using (var enumeratorScope = self.GetEnumerator().Scope())
                {
                    int i = 0;
                    while (enumeratorScope.iterator.MoveNext())
                    {
                        string value = enumeratorScope.iterator.Current.ToString();
                        if (i == fromIndex)
                            scope.stringBuilder.Append(value);
                        else if (i > fromIndex && i <= toIndex)
                            scope.stringBuilder.Append(separator + value);
                        i++;
                    }

                    return scope.stringBuilder.ToString();
                }
            }
        }

        public static string Join<T>(this ICollection<T> self, string sep)
        {
            return self.Join(0, self.Count, sep);
        }


        /// <summary>
        /// 用于不同类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this ICollection self)
        {
            List<T> result = new List<T>();
            int curIndex = -1;
            var iterator = self.GetEnumerator();
            while (iterator.MoveNext(ref curIndex))
                result.Add((T) iterator.Current);
            return result;
        }

        /// <summary>
        /// 用于不同类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ICollection self)
        {
            T[] result = new T[self.Count];
            int curIndex = -1;
            var iterator = self.GetEnumerator();
            while (iterator.MoveNext(ref curIndex))
                result[curIndex] = (T) iterator.Current;
            return result;
        }


        public static T[] ToArray<U, T>(this ICollection<U> collection, Func<U, T> func)
        {
            T[] result = new T[collection.Count];
            int curIndex = -1;
            using (var scope = collection.GetEnumerator().Scope())
            {
                while (scope.iterator.MoveNext(ref curIndex))
                    result[curIndex] = func(scope.iterator.Current);
                return result;
            }
        }

        public static void AddRange<T>(this ICollection<T> self, params T[] values)
        {
            foreach (var value in values)
                self.Add(value);
        }

        public static bool IsNullOrEmpty(this ICollection self)
        {
            return self == null || self.Count == 0;
        }


        #region ToStrign2

        public static string ToString2(this ICollection self, bool isFillStringWithDoubleQuote = false)
        {
            bool isFirst = true;
            using (var scope = new StringBuilderScope())
            {
                switch (self)
                {
                    case Array _:
                        scope.stringBuilder.Append(StringConst.String_RightRoundBrackets);
                        break;
                    case IList _:
                        scope.stringBuilder.Append(StringConst.String_RightSquareBrackets);
                        break;
                    case IDictionary _:
                        scope.stringBuilder.Append(StringConst.String_RightCurlyBrackets);
                        break;
                }

                if (self is IDictionary dictionary)
                {
                    foreach (var key in dictionary.Keys)
                    {
                        if (isFirst)
                            isFirst = false;
                        else
                            scope.stringBuilder.Append(StringConst.String_Comma);
                        scope.stringBuilder.Append(key.ToString2(isFillStringWithDoubleQuote));
                        scope.stringBuilder.Append(StringConst.String_Colon);
                        object value = dictionary[key];
                        scope.stringBuilder.Append(value.ToString2(isFillStringWithDoubleQuote));
                    }
                }
                else //list
                {
                    foreach (var o in self)
                    {
                        if (isFirst)
                            isFirst = false;
                        else
                            scope.stringBuilder.Append(StringConst.String_Comma);
                        scope.stringBuilder.Append(o.ToString2(isFillStringWithDoubleQuote));
                    }
                }

                switch (self)
                {
                    case Array _:
                        scope.stringBuilder.Append(StringConst.String_RightRoundBrackets);
                        break;
                    case IList _:
                        scope.stringBuilder.Append(StringConst.String_RightSquareBrackets);
                        break;
                    case IDictionary _:
                        scope.stringBuilder.Append(StringConst.String_RightCurlyBrackets);
                        break;
                }

                return scope.stringBuilder.ToString();
            }
        }

        #endregion
    }
}