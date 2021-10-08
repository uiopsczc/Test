using System;
using System.Collections.Generic;

namespace CsCat
{
    public static class ArrayExtension
    {
        public static T[] EmptyIfNull<T>(this T[] self)
        {
            return self ?? new T[0];
        }

        /// <summary>
        /// 将数组转化为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] self)
        {
            var list = new List<T>();
            if (self.IsNullOrEmpty()) return list;
            foreach (var element in self)
                list.Add(element);

            return list;
        }

        public static void Reverse<T>(this T[] self)
        {
            Array.Reverse(self);
        }

        public static void Foreach<T>(this T[] self, Action<T> action)
        {
            foreach (var element in self)
                action(element);
        }

        public static T[] Clone<T>(this T[] self)
        {
            T[] clone = new T[self.Length];
            self.CopyTo(clone, 0);
            return clone;
        }


        /// <summary>
        /// 数组中target的Index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] self, T target)
        {
            for (int i = 0; i < self.Length; i++)
            {
                if (target.Equals(self.GetValue(i)))
                    return i;
            }

            return -1;
        }


        public static T[] CopyTo<T>(this T[] self, int startIndex, int len)
        {
            var length = Math.Min(len, self.Length - startIndex);
            var target = new T[length];
            for (int i = 0; i < length; i++)
                target[i] = self[startIndex + i];

            return target;
        }

        /// <summary>
        ///扩展数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static T[] EnlargeCapacity<T>(this T[] self, float enlargeFactor = 2f)
        {
            int orgLength = self.Length;
            int newLength = (int) (orgLength * enlargeFactor);
            T[] newObjects = new T[newLength];
            Array.Copy(self, newObjects, orgLength);
            return newObjects;
        }

        /// <summary>
        /// 包含fromIndx，但不包含toIndx，到toIndex前一位
        /// </summary>
        public static T[] Sub<T>(this T[] self, int fromIndex, int toIndex)
        {
            return self.ToList().Sub(fromIndex, toIndex).ToArray();
        }

        /// <summary>
        /// 包含fromIndx到末尾
        /// </summary>
        public static T[] Sub<T>(this T[] self, int fromIndex)
        {
            return self.Sub(fromIndex, self.Length - 1);
        }

        /// <summary>
        /// 当set来使用，保持只有一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        public static T[] Add<T>(this T[] self, T element, bool isUnique = false)
        {
            return self.ToList().Add(element, isUnique).ToArray();
        }

        public static T[] AddRange<T>(this T[] self, IEnumerable<T> collection, bool isUnique = false)
        {
            return self.ToList().AddRange(collection, isUnique).ToArray();
        }


        public static T[] AddFirst<T>(this T[] self, T element)
        {
            return self.ToList().AddFirst(element).ToArray();
        }

        public static T[] AddLast<T>(this T[] self, T element)
        {
            return self.ToList().AddLast(element).ToArray();
        }

        public static T RemoveFirst<T>(this T[] self)
        {
            List<T> list = self.ToList();
            T result = list.RemoveFirst();
            return result;
        }

        public static T[] Insert<T>(this T[] self, int index, T element)
        {
            if (index == self.Length)
                return self.Add(element);
            var list = new List<T>(self);
            list.Insert(index, element);
            return list.ToArray();
        }

        public static T[] InsertRange<T>(this T[] self, int index, IEnumerable<T> collection)
        {
            if (index == self.Length)
                return self.AddRange(collection);
            var list = new List<T>(self);
            list.InsertRange(index, collection);
            return list.ToArray();
        }


        public static T RemoveLast<T>(this T[] self)
        {
            var list = self.ToList();
            T result = list.RemoveLast();
            return result;
        }


        /// <summary>
        /// 跟RemoveAt一样，只是有返回值
        /// </summary>
        public static T RemoveAt2<T>(this T[] self, int index)
        {
            List<T> list = self.ToList();
            T result = list.RemoveAt2(index);
            return result;
        }


        /// <summary>
        /// 删除list中的subList（subList必须要全部在list中）
        /// </summary>
        public static bool RemoveSub<T>(this T[] self, T[] subArray)
        {
            List<T> list = self.ToList();
            bool result = list.RemoveSub(subArray);
            return result;
        }


        /// <summary>
        /// 跟RemoveRange一样，但返回删除的元素List
        /// </summary>
        public static T[] RemoveRange2<T>(this T[] self, int index, int length)
        {
            var list = self.ToList();
            var result = list.RemoveRange2(index, length).ToArray();
            return result;
        }

        /// <summary>
        /// 在list中删除subList中出现的元素
        /// </summary>
        public static bool RemoveElementsOfSub<T>(this T[] self, T[] subArray)
        {
            var list = self.ToList();
            var result = list.RemoveElementsOfSub(subArray);
            return result;
        }

        public static T[] RandomArray<T>(this T[] self, int outCount, bool isUnique,
            RandomManager randomManager = null,
            params float[] weights)
        {
            return self.ToList().RandomList(outCount, isUnique, randomManager, weights).ToArray();
        }

        public static T Random<T>(this T[] self, RandomManager randomManager = null, params float[] weights)
        {
            return self.RandomArray(1, false, randomManager, weights)[0];
        }


        /// <summary>
        /// 使其内元素单一
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <returns></returns>
        public static T[] Unique<T>(this T[] self)
        {
            List<T> list = self.ToList();
            T[] result = list.Unique().ToArray();
            return result;
        }

        public static T[] Combine<T>(this T[] self, T[] another, bool isUnique = false)
        {
            List<T> list = self.ToList();
            T[] result = list.Combine(another, isUnique).ToArray();
            return result;
        }

        /// <summary>
        /// 将多个数组合成一个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="arrs"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T[] self, bool isUnique, params T[][] arrs)
        {
            List<T> result = new List<T>(self);
            foreach (T[] t in arrs)
            foreach (T element in t)
                result.Add(element);
            if (isUnique)
                result = result.Unique();
            return result.ToArray();
        }

        public static bool Contains<T>(this T[] self, T target)
        {
            return self.IndexOf(target) != -1;
        }

        public static bool ContainsIndex<T>(this T[] self, int index)
        {
            return index >= 0 && index < self.Length;
        }

        //将self初始化为[height][width]的数组
        public static T[][] InitArrays<T>(this T[][] self, int height, int width, T defaultValue = default)
        {
            self = new T[height][];
            for (int i = 0; i < height; i++)
                self[i] = new T[width];
            if (ObjectUtil.Equals(defaultValue, default(T))) return self;
            for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                self[i][j] = defaultValue;

            return self;
        }

        //转为左下为原点的坐标系，x增加是向右，y增加是向上（与unity的坐标系一致）
        public static T[][] ToLeftBottomBaseArrays<T>(this T[][] self)
        {
            int selfHeight = self.Length;
            int selfWidth = self[0].Length;
            int resultHeight = selfWidth;
            int resultWidth = selfHeight;
            var result = InitArrays(self, resultHeight, resultWidth);
            for (int i = 0; i < resultWidth; i++)
            for (int j = 0; j < resultHeight; j++)
                result[j][resultWidth - 1 - i] = self[i][j];
            return result;
        }


        public static Array Resize_Array(this Array self, int length)
        {
            Type elementType = self.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, length);
            Array.Copy(self, 0, newArray, 0, Math.Min(self.Length, length));
            return newArray;
        }

        public static Array Insert_Array(this Array self, int index, object value)
        {
            int newArrayLength = index < self.Length ? self.Length + 1 : index + 1;

            Type elementType = self.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, newArrayLength);
            Array.Copy(self, 0, newArray, 0, Math.Min(newArrayLength, self.Length));
            newArray.SetValue(value, index);
            if (index < self.Length)
                Array.Copy(self, index, newArray, index + 1, self.Length - index);
            return newArray;
        }

        public static Array RemoveAt_Array(this Array self, int index)
        {
            Type elementType = self.GetType().GetElementType();
            Array newArray = Array.CreateInstance(elementType, self.Length - 1);
            Array.Copy(self, 0, newArray, 0, index);
            Array.Copy(self, index + 1, newArray, index, self.Length - index - 1);
            return newArray;
        }


        public static void CopyTo<T>(this T[] self, T[] destArray, params object[] constructArgs) where T : ICopyable
        {
            destArray = new T[self.Length];
            for (int i = 0; i < self.Length; i++)
            {
                var dest_element = typeof(T).CreateInstance<T>(constructArgs);
                destArray[i] = dest_element;
                self[i].CopyTo(dest_element);
            }
        }

        public static void CopyFrom<T>(this T[] self, T[] sourceArray, params object[] constructArgs)
            where T : ICopyable
        {
            Array.Resize(ref self, sourceArray.Length);
            for (int i = 0; i < sourceArray.Length; i++)
            {
                var selfElement = typeof(T).CreateInstance<T>(constructArgs);
                selfElement.CopyFrom(sourceArray[i]);
                self[i] = selfElement;
            }
        }

        public static void SortWithCompareRules<T>(this T[] self, params Comparison<T>[] compareRules)
        {
            SortUtil.QuickSortWithCompareRules(self, compareRules);
        }
    }
}