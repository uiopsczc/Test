using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;

#endif

namespace CsCat
{
    public static class ListExtension
    {
        public static List<T> EmptyIfNull<T>(this List<T> self)
        {
            if (self == null)
                return new List<T>();
            return self;
        }


        public static int FindIndex<T>(this IList<T> self, Func<T, bool> match)
        {
            using (var scope = self.GetEnumerator().Scope())
            {
                var curIndex = -1;
                while (scope.iterator.MoveNext(ref curIndex))
                    if (match(scope.iterator.Current))
                        return curIndex;
                return curIndex;
            }
        }

        public static void RemoveEmpty<T>(this List<T> self)
        {
            for (var i = self.Count - 1; i >= 0; i--)
                if (self[i] == null)
                    self.RemoveAt(i);
        }

        /// <summary>
        ///   将list[index1]和list[index2]交换
        /// </summary>
        public static void Swap<T>(this IList<T> self, int index1, int index2)
        {
            ListUtil.Swap(self, index1, self, index2);
        }

        //超过index或者少于0的循环index表获得
        public static T GetLooped<T>(this IList<T> self, int index)
        {
            while (index < 0) index += self.Count;
            if (index >= self.Count) index %= self.Count;
            return self[index];
        }

        //超过index或者少于0的循环index表设置
        public static void SetLooped<T>(this IList<T> self, int index, T value)
        {
            while (index < 0) index += self.Count;
            if (index >= self.Count) index %= self.Count;
            self[index] = value;
        }

        public static bool ContainsIndex(this IList self, int index)
        {
            return index < self.Count && index >= 0;
        }

        /// <summary>
        ///   使其内元素单一
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<T> Unique<T>(this List<T> self)
        {
            var uniqueList = new List<T>();
            foreach (var element in self)
            {
                if (!uniqueList.Contains(element))
                    uniqueList.Add(element);
            }

            return uniqueList;
        }


        public static List<T> Combine<T>(this IList<T> self, IList<T> another, bool isUnique = false)
        {
            var result = new List<T>(self);
            result.AddRange(another);
            if (isUnique)
                result = result.Unique();
            return result;
        }

        public static void Push<T>(this IList<T> self, T t)
        {
            self.Add(t);
        }

        public static T Peek<T>(this IList<T> self)
        {
            return self.Last();
        }

        public static T Pop<T>(this IList<T> self)
        {
            return self.RemoveLast();
        }


        #region 查找

        /// <summary>
        ///   第一个item
        ///   用linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T First<T>(this IList<T> self)
        {
            return self[0];
        }

        /// <summary>
        ///   最后一个item
        ///   用linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T Last<T>(this IList<T> self)
        {
            return self[self.Count - 1];
        }

        /// <summary>
        ///   在list中找sublist的开始位置
        /// </summary>
        /// <returns>-1表示没找到</returns>
        public static int IndexOfSub<T>(this IList<T> self, IList<T> subList)
        {
            var resultFromIndex = -1; //sublist在list中的开始位置
            for (var i = 0; i < self.Count; i++)
            {
                object o = self[i];
                if (!ObjectUtil.Equals(o, subList[0])) continue;
                var isEquals = true;
                for (var j = 1; j < subList.Count; j++)
                {
                    var o1 = subList[j];
                    var o2 = i + j > self.Count - 1 ? default : self[i + j];
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
        ///   在list中只保留sublist中的元素
        /// </summary>
        public static bool RetainElementsOfSub<T>(this IList<T> self, IList<T> subList)
        {
            var isModify = false;
            for (var i = self.Count - 1; i >= 0; i--)
                if (!subList.Contains(self[i]))
                {
                    self.RemoveAt(i);
                    isModify = true;
                }

            return isModify;
        }

        /// <summary>
        ///   包含fromIndx，但不包含toIndx，到toIndex前一位
        /// </summary>
        public static List<T> Sub<T>(this IList<T> self, int fromIndex, int toIndex)
        {
            var list = new List<T>();
            for (var i = fromIndex; i <= toIndex; i++)
                list.Add(self[i]);
            return list;
        }


        /// <summary>
        ///   包含fromIndx到末尾
        /// </summary>
        public static List<T> Sub<T>(this IList<T> self, int fromIndex)
        {
            return self.Sub(fromIndex, self.Count - 1);
        }

        #endregion

        #region 插入删除操作

        /// <summary>
        ///   当set来使用，保持只有一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        public static List<T> Add<T>(this List<T> self, T element, bool isUnique = false)
        {
            if (isUnique && CheckIsDuplicate(self, element))
                return self;
            self.Add(element);
            return self;
        }

        public static List<T> AddRange<T>(this List<T> self, IEnumerable<T> collection, bool isUnique = false)
        {
            foreach (var element in collection)
            {
                if (isUnique && self.Contains(element))
                    continue;
                self.Add(element);
            }

            return self;
        }

        private static bool CheckIsDuplicate<T>(this IList<T> self, T element)
        {
            return self.Contains(element);
        }


        public static System.Collections.Generic.List<T> AddFirst<T>(this List<T> self, T o,
            bool isUnique = false)
        {
            if (isUnique && CheckIsDuplicate(self, o))
                return self;
            self.Insert(0, o);
            return self;
        }

        public static List<T> AddLast<T>(this List<T> self, T o,
            bool isUnique = false)
        {
            if (isUnique && CheckIsDuplicate(self, o))
                return self;
            self.Insert(self.Count, o);
            return self;
        }


        public static List<T> AddUnique<T>(this List<T> self, T o)
        {
            return AddLast(self, o, true);
        }


        public static T RemoveFirst<T>(this IList<T> self)
        {
            var t = self[0];
            self.RemoveAt(0);
            return t;
        }


        public static T RemoveLast<T>(this IList<T> self)
        {
            var o = self[self.Count - 1];
            self.RemoveAt(self.Count - 1);
            return o;
        }

        /// <summary>
        ///   跟RemoveAt一样，只是有返回值
        /// </summary>
        public static T RemoveAt2<T>(this List<T> self, int index)
        {
            var t = self[index];
            self.RemoveAt(index);
            return t;
        }


        /// <summary>
        ///   跟Remove一样，只是有返回值(是否删除掉)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool Remove2<T>(this IList<T> self, T o)
        {
            if (!self.Contains(o))
                return false;
            self.Remove(o);
            return true;
        }

        /// <summary>
        ///   删除list中的所有元素
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static void RemoveAll<T>(this IList<T> self)
        {
            self.RemoveRange2(0, self.Count);
        }

        /// <summary>
        ///   删除list中的subList（subList必须要全部在list中）
        /// </summary>
        public static bool RemoveSub<T>(this IList<T> self, IList<T> subList)
        {
            var fromIndex = self.IndexOfSub(subList);
            if (fromIndex == -1)
                return false;
            var isModify = false;
            for (var i = fromIndex + subList.Count - 1; i >= fromIndex; i--)
            {
                self.RemoveAt(i);
                if (!isModify)
                    isModify = true;
            }

            return isModify;
        }

        /// <summary>
        ///   跟RemoveRange一样，但返回删除的元素List
        /// </summary>
        public static List<T> RemoveRange2<T>(this IList<T> self, int index, int length)
        {
            var result = new List<T>();
            var lastIndex = index + length - 1 <= self.Count - 1 ? index + length - 1 : self.Count - 1;
            for (var i = lastIndex; i >= index; i--)
            {
                result.Add(self[i]);
                self.RemoveAt(i);
            }

            result.Reverse();

            return result;
        }


        /// <summary>
        ///   在list中删除subList中出现的元素
        /// </summary>
        public static bool RemoveElementsOfSub<T>(this IList<T> self, IList<T> subList)
        {
            var isModify = false;
            for (var i = self.Count - 1; i >= 0; i++)
                if (subList.Contains(self[i]))
                {
                    self.Remove(self[i]);
                    isModify = true;
                }

            return isModify;
        }

        #endregion

        #region Random 随机

        public static List<T> RandomList<T>(this List<T> self, int outCount, bool isUnique,
            RandomManager randomManager = null, params float[] weights)
        {
            randomManager = randomManager ?? Client.instance.randomManager;
            return randomManager.RandomList(self, outCount, isUnique, weights.ToList());
        }

        public static T Random<T>(this List<T> self, RandomManager randomManager = null, params float[] weights)
        {
            return self.RandomList(1, false, randomManager, weights)[0];
        }

        #endregion


        //如：list.BubbleSort((a, b)=>return a.count <= b.count)
        //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
        public static void BubbleSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
        {
            SortUtil.BubbleSort(self, compareFunc);
        }

        public static void BubbleSort(this IList self, Func<object, object, bool> compareFunc)
        {
            SortUtil.BubbleSort(self, compareFunc);
        }

        public static void BubbleSortWithCompareRules<T>(this IList<T> self, params Comparison<T>[] compareRules)
        {
            SortUtil.BubbleSortWithCompareRules(self, compareRules);
        }

        public static void BubbleSortWithCompareRules(this IList self, params Comparison<object>[] compareRules)
        {
            SortUtil.BubbleSortWithCompareRules(self, compareRules);
        }


        //如：list.MergeSort((a, b)=>return a.count <= b.count)
        //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
        public static void MergeSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
        {
            SortUtil.MergeSort(self, compareFunc);
        }

        public static void MergeSort(this IList self, Func<object, object, bool> compareFunc)
        {
            SortUtil.MergeSort(self, compareFunc);
        }

        public static void MergeSortWithCompareRules<T>(this IList<T> self, params Comparison<T>[] compareRules)
        {
            SortUtil.MergeSortWithCompareRules(self, compareRules);
        }

        public static void MergeSortWithCompareRules(this IList self, params Comparison<object>[] compareRules)
        {
            SortUtil.MergeSortWithCompareRules(self, compareRules);
        }

        //如：list.QuickSort((a, b)=>return a.count <= b.count)
        //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
        public static void QuickSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
        {
            SortUtil.QuickSort(self, compareFunc);
        }

        public static void QuickSort(this IList self, Func<object, object, bool> compareFunc)
        {
            SortUtil.QuickSort(self, compareFunc);
        }

        public static void QuickSortWithCompareRules<T>(this IList<T> self, params Comparison<T>[] compareRules)
        {
            SortUtil.QuickSortWithCompareRules(self, compareRules);
        }

        public static void QuickSortWithCompareRules(this IList self, params Comparison<object>[] compareRules)
        {
            SortUtil.QuickSortWithCompareRules(self, compareRules);
        }

        //////////////////////////////////////////////////////////////////////
        // Diff相关
        //////////////////////////////////////////////////////////////////////
        // 必须和ApplyDiff使用
        // 以new为基准，获取new相对于old不一样的部分
        // local diff = table.GetDiff(old, new)
        //  table.ApplyDiff(old, diff)
        // 这样old的就变成和new一模一样的数据
        public static LinkedHashtable GetDiff(this IList oldList, IList newList)
        {
            return ListUtil.GetDiff(oldList, newList);
        }

        // table.ApplyDiff(old, diff)
        // 将diff中的东西应用到old中
        // 重要：当为Array的时候，需要重新赋值；List的时候，可以不需要重新赋值
        public static IList ApplyDiff(this IList oldList, LinkedHashtable diffDict)
        {
            return ListUtil.ApplyDiff(oldList, diffDict);
        }

        // 必须和ApplyDiff使用
        // 以new为基准，获取new中有，但old中没有的
        // local diff = table.GetNotExist(old, new)
        // table.ApplyDiff(old, diff)
        // 这样old就有new中的字段
        public static LinkedHashtable GetNotExist(this IList oldList, IList newList)
        {
            return ListUtil.GetNotExist(oldList, newList);
        }

        //两个table是否不一样
        public static bool IsDiff(this IList oldList, IList newList)
        {
            return ListUtil.IsDiff(oldList, newList);
        }

        public static void CopyTo<T>(this IList<T> self, IList<T> destList, params object[] constructArgs)
            where T : ICopyable
        {
            destList.Clear();
            foreach (var element in self)
            {
                var destElement = typeof(T).CreateInstance<T>(constructArgs);
                destList.Add(destElement);
                element.CopyTo(destElement);
            }
        }

        public static void CopyFrom<T>(this IList<T> self, IList<T> sourceList, params object[] constructArgs)
            where T : ICopyable
        {
            self.Clear();
            foreach (var element in sourceList)
            {
                var selfElement = typeof(T).CreateInstance<T>(constructArgs);
                self.Add(selfElement);
                selfElement.CopyFrom(element);
            }
        }

        public static ArrayList DoSaveList<T>(this IList<T> self, Action<T, Hashtable> doSaveCallback)
        {
            ArrayList result = new ArrayList();
            foreach (var element in self)
            {
                Hashtable elementDict = new Hashtable();
                result.Add(elementDict);
                doSaveCallback(element, elementDict);
            }

            return result;
        }

        public static void DoRestoreList<T>(this IList<T> self, ArrayList arrayList,
            Func<Hashtable, T> doRestoreCallback)
        {
            foreach (var element in arrayList)
            {
                var elementDict = element as Hashtable;
                T elementT = doRestoreCallback(elementDict);
                self.Add(elementT);
            }
        }

        public static void SortWithCompareRules<T>(this List<T> self, params Comparison<T>[] compareRules)
        {
            SortUtil.MergeSortWithCompareRules(self, compareRules);
        }

        public static int BinarySearchCat<T>(this IList<T> self, T target_value,
            IndexOccurType indexOccurType = IndexOccurType.Any_Index, params Comparison<T>[] compareRules)
        {
            return SortedListSerachUtil.BinarySearchCat(self, target_value, indexOccurType, compareRules);
        }

        public static ListSorttedType GetListSortedType<T>(this IList<T> self, Comparison<T>[] compareRules)
        {
            T firstValue = self[0];
            T lastValue = self[self.Count - 1];
            return CompareUtil.CompareWithRules(firstValue, lastValue, compareRules) <= 0
                ? ListSorttedType.Increace
                : ListSorttedType.Decrease;
        }

        #region 各种To ToXXX

        /// <summary>
        ///   变为对应的ArrayList
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(this IList self)
        {
            var list = new ArrayList();
            foreach (var o in self)
                list.Add(o);
            return list;
        }

        public static T[] ToArray<T>(this ICollection<T> self)
        {
            var result = new T[self.Count];
            self.CopyTo(result, 0);
            return result;
        }
#if UNITY_EDITOR
        public static void ToReorderableList(this IList toReorderList, ref ReorderableList reorderableList)
        {
            ReorderableListUtil.ToReorderableList(toReorderList, ref reorderableList);
        }
#endif

        #endregion
    }
}