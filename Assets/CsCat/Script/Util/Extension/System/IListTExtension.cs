using System;
using System.Collections.Generic;

namespace CsCat
{
    public static class IListTExtension
    {
        /// <summary>
        /// 将s从fromIndex开始,到toIndex（不包括toIndex）结束的元素转为字符串连接起来，元素之间用n连接（最后的元素后面不加n）
        /// 例如：object[] s={"aa","bb","cc"} split="DD" return "aaDDbbDDcc"
        /// </summary>
        public static string Concat<T>(this IList<T> self, int fromIndex, int toIndex, string separator)
        {
            if (fromIndex < 0 || toIndex > self.Count || toIndex - fromIndex < 0) throw new IndexOutOfRangeException();
            using (var scope = new StringBuilderScope())
            {
                if (toIndex - fromIndex <= 0)
                    return scope.stringBuilder.ToString();
                for (int i = fromIndex; i < self.Count && i <= toIndex; i++)
                {
                    string value = self[i].ToString();
                    if (i == fromIndex)
                        scope.stringBuilder.Append(value);
                    else
                        scope.stringBuilder.Append(separator + value);
                }
                return scope.stringBuilder.ToString();
            }
        }

        public static string Concat<T>(this IList<T> self,string separator)
        {
            return self.Concat(0, self.Count - 1, separator);
        }

        public static void SortWithCompareRules<T>(this List<T> self, IList<Comparison<T>> compareRules)
        {
            SortUtil.MergeSortWithCompareRules(self, compareRules);
        }


        public static void BubbleSortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
        {
            SortUtil.BubbleSortWithCompareRules(self, compareRules);
        }


        //如：list.MergeSort((a, b)=>return a.count <= b.count)
        //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
        public static void MergeSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
        {
            SortUtil.MergeSort(self, compareFunc);
        }


        public static void MergeSortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
        {
            SortUtil.MergeSortWithCompareRules(self, compareRules);
        }


        //如：list.QuickSort((a, b)=>return a.count <= b.count)
        //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
        public static void QuickSort<T>(this IList<T> self, Func<T, T, bool> compareFunc)
        {
            SortUtil.QuickSort(self, compareFunc);
        }


        public static void QuickSortWithCompareRules<T>(this IList<T> self, IList<Comparison<T>> compareRules)
        {
            SortUtil.QuickSortWithCompareRules(self, compareRules);
        }

        public static int BinarySearchCat<T>(this IList<T> self, T targetValue,
            IndexOccurType indexOccurType = IndexOccurType.Any_Index, IList<Comparison<T>> compareRules = null)
        {
            return SortedListSearchUtil.BinarySearchCat(self, targetValue, indexOccurType, compareRules);
        }

        public static ListSortedType GetListSortedType<T>(this IList<T> self, IList<Comparison<T>> compareRules)
        {
            T firstValue = self[0];
            T lastValue = self[self.Count - 1];
            return CompareUtil.CompareWithRules(firstValue, lastValue, compareRules) <= 0
                ? ListSortedType.Increase
                : ListSortedType.Decrease;
        }
    }
}