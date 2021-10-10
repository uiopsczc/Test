using System;
using System.Collections.Generic;
using System.Linq;

namespace CsCat
{
    public static class ArrayTExtension
    {
        /// <summary>
        /// 将数组转化为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] self)
        {
            if (self == null)
                return null;
            if (self.Length == 0)
                return new List<T>();
            var list = new List<T>(self.Length);
            foreach (var element in self)
                list.Add(element);
            return list;
        }

        public static T[] EmptyIfNull<T>(this T[] self)
        {
            return self ?? new T[0];
        }

        public static T[] RemoveEmpty<T>(this T[] self)
        {
            int[] remainIndexes = new int[self.Length];
            int remainCount = 0;
            for (int i = 0; i < self.Length; i++)
            {
                if (self[i] != null)
                {
                    remainIndexes[remainCount] = i;
                    remainCount++;
                }
            }

            T[] result = new T[remainCount];
            for (int i = 0; i < remainCount; i++)
            {
                var remainIndex = remainIndexes[i];
                result[i] = self[remainIndex];
            }

            return result;
        }

        public static void Swap<T>(this T[] self, int index1, int index2)
        {
            ArrayTUtil.Swap(self, index1, self, index2);
        }

        //超过index或者少于0的循环index表获得
        public static T GetByLoopIndex<T>(this T[] self, int index)
        {
            while (index < 0) index += self.Length;
            if (index >= self.Length) index %= self.Length;
            return self[index];
        }

        //超过index或者少于0的循环index表设置
        public static void SetByLoopIndex<T>(this T[] self, int index, T value)
        {
            while (index < 0) index += self.Length;
            if (index >= self.Length) index %= self.Length;
            self[index] = value;
        }

        public static bool Contains<T>(this T[] self, T target)
        {
            return self.IndexOf(target) != -1;
        }

        public static bool ContainsIndex<T>(this T[] self, int index)
        {
            return index >= 0 && index < self.Length;
        }

        /// <summary>
        /// 使其内元素单一
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <returns></returns>
        public static T[] Unique<T>(this T[] self)
        {
            HashSet<T> hashSet = new HashSet<T>();
            int[] addIndexes = new int[self.Length];
            int addCount = 0;
            for (int i = 0; i < self.Length; i++)
            {
                if (hashSet.Add(self[i]))
                {
                    addIndexes[addCount] = i;
                    addCount++;
                }
            }

            T[] result = new T[addCount];
            for (int i = 0; i < addCount; i++)
            {
                var addIndex = addIndexes[i];
                result[i] = self[addIndex];
            }

            return result;
        }

        public static T[] Combine<T>(this T[] self, T[] another)
        {
            T[] result = new T[self.Length + another.Length];
            Array.Copy(self, result, self.Length);
            Array.Copy(another, 0, result, self.Length, another.Length);
            result = result.Unique();
            return result;
        }

        /// <summary>
        /// 将多个数组合成一个数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="arrs"></param>
        /// <returns></returns>
        public static T[] Combine<T>(this T[] self, params T[][] arrs)
        {
            var arrsCount = arrs.Length;
            var totalElementCount = self.Length;
            for (int i = 0; i < arrsCount; i++)
                totalElementCount += arrs[i].Length;
            T[] result = new T[totalElementCount];
            Array.Copy(self, result, self.Length);
            int curIndex = self.Length;
            for (int i = 0; i < arrsCount; i++)
            {
                var arr = arrs[i];
                Array.Copy(arr, 0, result, curIndex, arr.Length);
                curIndex += arr.Length;
            }

            result = result.Unique();
            return result;
        }

        public static T[] Push<T>(this T[] self, T t)
        {
            return self.Add(t);
        }

        public static T Peek<T>(this T[] self)
        {
            return self.Last();
        }

        public static (T element, T[] array) Pop<T>(this T[] self)
        {
            return self.RemoveLast2();
        }

        public static T First<T>(this T[] self)
        {
            return self[0];
        }

        public static T Last<T>(this T[] self)
        {
            return self[self.Length - 1];
        }

        /// <summary>
        ///   在self中找subArray的开始位置
        /// </summary>
        /// <returns>-1表示没找到</returns>
        public static int IndexOfSub<T>(this T[] self, T[] subArray)
        {
            var resultFromIndex = -1; //sublist在list中的开始位置
            for (var i = 0; i < self.Length; i++)
            {
                object o = self[i];
                if (!ObjectUtil.Equals(o, subArray[0])) continue;
                var isEquals = true;
                for (var j = 1; j < subArray.Length; j++)
                {
                    var o1 = subArray[j];
                    var o2 = i + j > self.Length - 1 ? default : self[i + j];
                    if (ObjectUtil.Equals(o1, o2)) continue;
                    isEquals = false;
                    break;
                }

                if (!isEquals) continue;
                resultFromIndex = i;
                break;
            }

            return resultFromIndex;
        }

        /// <summary>
        ///   在self中只保留subArray中的元素
        /// </summary>
        public static T[] RetainElementsOfSub<T>(this T[] self, T[] subArray)
        {
            int[] remainIndexes = new int[self.Length];
            int remainCount = 0;
            for (var i = 0; i < self.Length; i++)
                if (subArray.Contains(self[i]))
                {
                    remainIndexes[remainCount] = i;
                    remainCount++;
                }

            T[] result = new T[remainCount];
            for (int i = 0; i < remainCount; i++)
            {
                var remainIndex = remainIndexes[i];
                result[i] = self[remainIndex];
            }

            return result;
        }

        /// <summary>
        /// </summary>
        public static T[] Sub<T>(this T[] self, int fromIndex, int length)
        {
            length = Math.Min(length, self.Length - fromIndex + 1);
            return self.Clone(fromIndex, length);
        }

        /// <summary>
        /// 包含fromIndx到末尾
        /// </summary>
        public static T[] Sub<T>(this T[] self, int fromIndex)
        {
            return self.Sub(fromIndex, self.Length - fromIndex + 1);
        }

        /// <summary>
        /// 当set来使用，保持只有一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        public static T[] Add<T>(this T[] self, T element, bool isUnique = false)
        {
            if (isUnique && self.Contains(element))
                return self;
            var result = self.AddCapacity(1);
            result[result.Length - 1] = element;
            return result;
        }

        //当isUnique==true的情况下,默认toAddArray里面的元素是不重复的
        public static T[] AddRange<T>(this T[] self, T[] toAddArray, bool isUnique = false)
        {
            var selfLength = self.Length;
            T[] result;
            if (isUnique)
            {
                int[] toAddIndexes = new int[toAddArray.Length];
                int addCount = 0;
                for (int i = 0; i < toAddArray.Length; i++)
                {
                    var element = toAddArray[i];
                    if (self.Contains(element))
                        continue;
                    toAddIndexes[addCount] = i;
                    addCount++;
                }

                result = self.AddCapacity(addCount);
                for (int i = 0; i < addCount; i++)
                {
                    var toAddIndex = toAddIndexes[i];
                    result[selfLength + i] = toAddArray[toAddIndex];
                }

                return result;
            }

            result = self.AddCapacity(toAddArray.Length);
            for (int i = 0; i < toAddArray.Length; i++)
                result[selfLength + i] = toAddArray[i];
            return result;
        }

        public static T[] AddRange<T>(this T[] self, List<T> toAddList, bool isUnique = false)
        {
            var selfLength = self.Length;
            T[] result;
            if (isUnique)
            {
                int[] toAddIndexes = new int[toAddList.Count];
                int addCount = 0;
                for (int i = 0; i < toAddList.Count; i++)
                {
                    var element = toAddList[i];
                    if (self.Contains(element))
                        continue;
                    toAddIndexes[addCount] = i;
                    addCount++;
                }

                result = self.AddCapacity(addCount);
                for (int i = 0; i < addCount; i++)
                {
                    var toAddIndex = toAddIndexes[i];
                    result[selfLength + i] = toAddList[toAddIndex];
                }

                return result;
            }

            result = self.AddCapacity(toAddList.Count);
            for (int i = 0; i < toAddList.Count; i++)
                result[selfLength + i] = toAddList[i];
            return result;
        }


        public static T[] AddFirst<T>(this T[] self, T element, bool isUnique = false)
        {
            T[] result;
            if (isUnique && self.Contains(element))
            {
                result = new T[self.Length];
                Array.Copy(self, result, self.Length);
                return result;
            }

            result = self.AddCapacity(1, false);
            result[0] = element;
            return result;
        }

        public static T[] AddLast<T>(this T[] self, T element, bool isUnique = false)
        {
            T[] result;
            if (isUnique && self.Contains(element))
            {
                result = new T[self.Length];
                Array.Copy(self, result, self.Length);
                return result;
            }

            result = self.AddCapacity(1);
            result[result.Length - 1] = element;
            return result;
        }

        public static T[] AddUnique<T>(this T[] self, T o)
        {
            return AddLast(self, o, true);
        }

        public static T[] RemoveFirst<T>(this T[] self)
        {
            return RemoveFirst2(self).array;
        }

        public static (T element, T[] array) RemoveFirst2<T>(this T[] self)
        {
            var element = self[0];
            var array = new T[self.Length - 1];
            if (array.Length > 0)
                Array.Copy(self, 1, array, 0, self.Length - 1);
            return (element, array);
        }

        public static T[] RemoveLast<T>(this T[] self)
        {
            return RemoveLast2(self).array;
        }

        public static (T element, T[] array) RemoveLast2<T>(this T[] self)
        {
            var element = self[self.Length - 1];
            var array = new T[self.Length - 1];
            if (array.Length > 0)
                Array.Copy(self, 0, array, 0, self.Length - 1);
            return (element, array);
        }

        public static (T element, T[] array) RemoveAt2<T>(this T[] self, int removeIndex)
        {
            var element = self[removeIndex];
            var array = new T[self.Length - 1];
            int removeIndexMinus1 = removeIndex - 1;
            int removeIndexPlus1 = removeIndex + 1;
            if (removeIndexMinus1 >= 0)
                Array.Copy(self, 0, array, 0, removeIndexMinus1 + 1);
            if (removeIndexPlus1 < self.Length)
                Array.Copy(self, removeIndexPlus1, array, removeIndex, self.Length - removeIndex - 1);
            return (element, array);
        }

        public static T[] RemoveAt<T>(this T[] self, int removeIndex)
        {
            return self.RemoveAt2(removeIndex).array;
        }

        /// <summary>
        ///   删除list中的subList（subList必须要全部在list中）
        /// </summary>
        public static T[] RemoveSub<T>(this T[] self, T[] subArray)
        {
            var fromIndex = self.IndexOfSub(subArray);
            T[] result;
            if (fromIndex == -1)
            {
                result = new T[self.Length];
                Array.Copy(self, result, self.Length);
                return result;
            }

            result = self.RemoveRange(fromIndex, subArray.Length);
            return result;
        }

        //elements:移除掉的元素
        //array:self被移除后的数组
        public static (T[] elements, T[] array) RemoveRange2<T>(this T[] self, int removeFromIndex)
        {
            var length = self.Length - removeFromIndex + 1;
            return RemoveRange2(self, removeFromIndex, length);
        }

        //elements:移除掉的元素
        //array:self被移除后的数组
        public static (T[] elements, T[] array) RemoveRange2<T>(this T[] self, int removeFromIndex, int length)
        {
            length = Math.Min(self.Length - removeFromIndex + 1, length);
            var elements = new T[length];
            Array.Copy(self, removeFromIndex, elements, 0, elements.Length);
            var array = new T[self.Length - length];
            int removeFromIndexMinus1 = removeFromIndex - 1;
            int removeEndIndexPlus1 = removeFromIndex + length;
            if (removeFromIndexMinus1 >= 0)
                Array.Copy(self, 0, array, 0, removeFromIndexMinus1 + 1);
            if (removeEndIndexPlus1 < self.Length)
                Array.Copy(self, removeEndIndexPlus1, array, removeFromIndex, self.Length - removeFromIndex - length);
            return (elements, array);
        }

        public static T[] RemoveRange<T>(this T[] self, int removeFromIndex)
        {
            var length = self.Length - removeFromIndex + 1;
            return RemoveRange(self, removeFromIndex, length);
        }

        public static T[] RemoveRange<T>(this T[] self, int removeFromIndex, int length)
        {
            length = Math.Min(self.Length - removeFromIndex + 1, length);
            var array = new T[self.Length - length];
            int removeFromIndexMinus1 = removeFromIndex - 1;
            int removeEndIndexPlus1 = removeFromIndex + length;
            if (removeFromIndexMinus1 >= 0)
                Array.Copy(self, 0, array, 0, removeFromIndexMinus1 + 1);
            if (removeEndIndexPlus1 < self.Length)
                Array.Copy(self, removeEndIndexPlus1, array, removeFromIndex, self.Length - removeFromIndex - length);
            return array;
        }

        /// <summary>
        /// 在list中删除subList中出现的元素
        /// </summary>
        public static T[] RemoveElements<T>(this T[] self, HashSet<T> hashSet)
        {
            int[] remainIndexes = new int[self.Length];
            int remainCount = 0;
            for (int i = 0; i < self.Length; i++)
            {
                if (!hashSet.Contains(self[i]))
                {
                    remainIndexes[remainCount] = i;
                    remainCount++;
                }
            }

            T[] result = new T[remainCount];
            for (int i = 0; i < remainCount; i++)
            {
                var remainIndex = remainIndexes[i];
                result[i] = self[remainIndex];
            }

            return result;
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
                if (target.Equals(self[i]))
                    return i;
            }

            return -1;
        }


        public static T[] Clone<T>(this T[] self, int startIndex, int len)
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

        public static T[] AddCapacity<T>(this T[] self, int add, bool isAppend = true)
        {
            int selfLength = self.Length;
            int newLength = selfLength + add;
            T[] newObjects = new T[newLength];
            if (isAppend)
                Array.Copy(self, newObjects, selfLength);
            else
                Array.Copy(self, 0, newObjects, add, selfLength);
            return newObjects;
        }


        public static T[] Insert<T>(this T[] self, int insertIndex, T element)
        {
            var result = new T[self.Length + 1];
            result[insertIndex] = element;
            int insertIndexMinus1 = insertIndex - 1;
            int insertIndexPlus1 = insertIndex + 1;
            if (insertIndexMinus1 >= 0)
                Array.Copy(self, 0, result, 0, insertIndexMinus1 + 1);
            if (insertIndexPlus1 < result.Length)
                Array.Copy(self, insertIndex, result, insertIndexPlus1, self.Length - insertIndex);
            return result;
        }

        public static T[] InsertRange<T>(this T[] self, int insertIndex, T[] toAddArray)
        {
            var toAddLength = toAddArray.Length;
            var result = new T[self.Length + toAddLength];
            Array.Copy(toAddArray, 0, result, insertIndex, toAddLength);
            int insertFromIndexMinus1 = insertIndex - 1;
            int insertEndIndexPlus = insertIndex + toAddLength;
            if (insertFromIndexMinus1 >= 0)
                Array.Copy(self, 0, result, 0, insertFromIndexMinus1 + 1);
            if (insertEndIndexPlus < result.Length)
                Array.Copy(self, insertIndex, result, insertEndIndexPlus, self.Length - insertIndex);
            return result;
        }

        public static T[] InsertRange<T>(this T[] self, int insertIndex, List<T> toAddList)
        {
            var toAddLength = toAddList.Count;
            var result = new T[self.Length + toAddLength];
            for (int i = 0; i < toAddList.Count; i++)
                result[insertIndex + i] = toAddList[i];
            int insertFromIndexMinus1 = insertIndex - 1;
            int insertEndIndexPlus1 = insertIndex + toAddLength;
            if (insertFromIndexMinus1 >= 0)
                Array.Copy(self, 0, result, 0, insertFromIndexMinus1 + 1);
            if (insertEndIndexPlus1 < result.Length)
                Array.Copy(self, insertIndex, result, insertEndIndexPlus1, self.Length - insertIndex);
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

        public static void SortWithCompareRules<T>(this T[] self, params Comparison<T>[] compareRules)
        {
            SortUtil.MergeSortWithCompareRules(self, compareRules);
        }
    }
}