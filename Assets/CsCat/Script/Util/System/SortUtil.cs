
using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class SortUtil
  {
    //////////////////////////////////////////////////////////////////////
    // 冒泡排序
    //////////////////////////////////////////////////////////////////////
    //如：list.BubbleSort(list, (a, b)=>return a.count <= b.count)
    //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
    public static void BubbleSort<T>(IList<T> list, Func<T, T, bool> func)
    {
      int count = list.Count;
      for (int i = 0; i < count - 1; i++)
      {
        for (int j = 0; j < count - i - 1; j++)
          if (!func(list[j], list[j + 1]))
          {
            var tmp = list[j];
            list[j] = list[j + 1];
            list[j + 1] = tmp;
          }
      }
    }

    public static void BubbleSort(IList list, Func<object, object, bool> func)
    {
      int count = list.Count;
      for (int i = 0; i < count - 1; i++)
      {
        for (int j = 0; j < count - i - 1; j++)
          if (!func(list[j], list[j + 1]))
          {
            var tmp = list[j];
            list[j] = list[j + 1];
            list[j + 1] = tmp;
          }
      }
    }

    public static void BubbleSortWithCompareRules<T>(IList<T> list, params Comparison<T>[] compare_rules)
    {
      BubbleSort(list, (a, b) =>
      {
        foreach (var compare_rule in compare_rules)
        {
          if (compare_rule == null)
            continue;
          int result = compare_rule(a, b);
          if (result == 0)
            continue;
          return result < 0;
        }
        return true;
      });
    }

    public static void BubbleSortWithCompareRules(IList list, params Comparison<object>[] compare_rules)
    {
      BubbleSort(list, (a, b) =>
      {
        foreach (var compare_rule in compare_rules)
        {
          if (compare_rule == null)
            continue;
          int result = compare_rule(a, b);
          if (result == 0)
            continue;
          return result < 0;
        }
        return true;
      });
    }


    //////////////////////////////////////////////////////////////////////
    // 快速排序
    //////////////////////////////////////////////////////////////////////
    //如：list.QuickSort_Array(list, (a, b)=>return a.count <= b.count)
    //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
    //https://baike.baidu.com/item/%E5%BF%AB%E9%80%9F%E6%8E%92%E5%BA%8F%E7%AE%97%E6%B3%95/369842?fr=aladdin#4_11
    public static void QuickSort(IList list, Func<object, object, bool> func, int? left = null, int? right = null)
    {
      int _left = left.GetValueOrDefault(0);
      int _right = right.GetValueOrDefault(list.Count - 1);
      if (_left >= _right)
        return;
      //完成一次单元排序
      var index = __QuickSort(list, func, _left, _right);
      //对左边单元进行排序
      QuickSort(list, func, left, index - 1);
      //对右边单元进行排序
      QuickSort(list, func, index + 1, right);
    }

    private static int __QuickSort(IList list, Func<object, object, bool> func, int left, int right)
    {
      var key = list[left];
      while (left < right)
      {
        //从后向前搜索比key小的值
        while (func(key, list[right]) && right > left)
          right = right - 1;
        //比key小的放左边
        list[left] = list[right];
        //从前向后搜索比key大的值，比key大的放右边
        while (func(list[left], key) && right > left)
          left = left + 1;
        //比key大的放右边
        list[right] = list[left];
      }

      //左边都比key小，右边都比key大。 将key放在游标当前位置。 此时left等于right
      list[left] = key;
      return right;
    }

    public static void QuickSort<T>(IList<T> list, Func<T, T, bool> func, int? left = null, int? right = null)
    {
      int _left = left.GetValueOrDefault(0);
      int _right = right.GetValueOrDefault(list.Count - 1);
      if (_left >= _right)
        return;
      //完成一次单元排序
      var index = __QuickSort(list, func, _left, _right);
      //对左边单元进行排序
      QuickSort(list, func, left, index - 1);
      //对右边单元进行排序
      QuickSort(list, func, index + 1, right);
    }

    private static int __QuickSort<T>(IList<T> list, Func<T, T, bool> func, int left, int right)
    {
      var key = list[left];
      while (left < right)
      {
        //从后向前搜索比key小的值
        while (func(key, list[right]) && right > left)
          right = right - 1;
        //比key小的放左边
        list[left] = list[right];
        //从前向后搜索比key大的值，比key大的放右边
        while (func(list[left], key) && right > left)
          left = left + 1;
        //比key大的放右边
        list[right] = list[left];
      }

      //左边都比key小，右边都比key大。 将key放在游标当前位置。 此时left等于right
      list[left] = key;
      return right;
    }

    public static void QuickSortWithCompareRules(IList list, params Comparison<object>[] compare_rules)
    {
      QuickSort(list, (a, b) =>
      {
        foreach (var compare_rule in compare_rules)
        {
          if (compare_rule == null)
            continue;
          int result = compare_rule(a, b);
          if (result == 0)
            continue;
          return result < 0;
        }
        return true;
      });
    }

    public static void QuickSortWithCompareRules<T>(IList<T> list, params Comparison<T>[] compare_rules)
    {
      QuickSort(list, (a, b) =>
      {
        foreach (var compare_rule in compare_rules)
        {
          if (compare_rule == null)
            continue;
          int result = compare_rule(a, b);
          if (result == 0)
            continue;
          return result < 0;
        }
        return true;
      });
    }
  }
}
