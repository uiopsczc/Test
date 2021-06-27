
using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class SortUtil
  {

    public static int CompareWithRules<T>(T data1, T data2,Comparison<T>[] compare_rules)
    {
      foreach (var rule in compare_rules)
      {
        int result = rule(data1, data2);
        if (result == 0)
          continue;
        return result;
      }
      return 0;
    }
    //////////////////////////////////////////////////////////////////////
    // 冒泡排序
    // O(n^2)
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

    public static void BubbleSortWithCompareRules<T>(IList<T> list, params Comparison<T>[] compare_rules)
    {
      BubbleSort(list, (a, b) => CompareWithRules(a, b, compare_rules) < 0);
    }
    //////////////////////////////////////////////////////////////////////////////
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
    
    public static void BubbleSortWithCompareRules(IList list, params Comparison<object>[] compare_rules)
    {
      BubbleSort(list, (a, b) => CompareWithRules(a, b, compare_rules) < 0);
    }

    //////////////////////////////////////////////////////////////////////
    // 归并排序
    // https://www.youtube.com/watch?v=TzeBrDU-JaY
    // O(N* logN)
    // 需要另外的N空间来进行排序处理，但稳定 最好的情况需要时间为N*logN,最坏的情况需要时间为N* logN
    //////////////////////////////////////////////////////////////////////
    //如：list.BubbleSort(list, (a, b)=>return a.count <= b.count)
    //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
    public static void MergeSort<T>(IList<T> list, Func<T, T, bool> func)
    {
      __MergeSort(list, 0, list.Count - 1, func);
    }

    public static void MergeSortWithCompareRules<T>(IList<T> list, params Comparison<T>[] compare_rules)
    {
      MergeSort(list, (a, b) =>
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

    private static void __MergeSort<T>(IList<T> list,int left, int right, Func<T, T, bool> func)
    {
      if (left >= right)
        return;
      int middle = (int)Math.Floor((double)(left + right));
      __MergeSort(list, left, middle, func);
      __MergeSort(list, middle+1, right, func);
      __Merge(list, left, middle, right, func);
    }

    private static void __Merge<T>(IList<T> list, int left, int middle, int right, Func<T, T, bool> func)
    {
      List<T> new_list = new List<T>(right-left);//新的数组(用于存放排序后的元素)
      int left_list_cur_index = left;//左边的数组的当前index
      int right_list_cur_index = middle + 1;//右边的数组的当前index
      while (left_list_cur_index <= middle && right_list_cur_index <= right)
      {
        if (!func(list[left_list_cur_index], list[right_list_cur_index]))
        {
          new_list.Add(list[left_list_cur_index]);
          left_list_cur_index++;
        }
        else
        {
          new_list.Add(list[right_list_cur_index]);
          right_list_cur_index++;
        }
      }
      //left_array_cur_index <= mid and right_array_cur_index <= right 其中之一不成立的情况
      //处理没有被完全处理的数组
      for(; left_list_cur_index<= middle; left_list_cur_index++)
        new_list.Add(list[left_list_cur_index]);
      for (; right_list_cur_index <= right; right_list_cur_index++)
        new_list.Add(list[right_list_cur_index]);

      //重新赋值给list
      for (int i = 0; i <= new_list.Count; i++)
        list[left + i] = new_list[i];
    }

    //////////////////////////////////////////////////////////////////////////////
    public static void MergeSort(IList list, Func<object, object, bool> func)
    {
      __MergeSort(list, 0, list.Count - 1, func);
    }

    public static void MergeSortWithCompareRules(IList list, params Comparison<object>[] compare_rules)
    {
      MergeSort(list, (a, b) => CompareWithRules(a, b, compare_rules) < 0);
    }

    private static void __MergeSort(IList list, int left, int right, Func<object, object, bool> func)
    {
      if (left >= right)
        return;
      int middle = (int)Math.Floor((double)(left + right));
      __MergeSort(list, left, middle, func);
      __MergeSort(list, middle + 1, right, func);
      __Merge(list, left, middle, right, func);
    }

    private static void __Merge(IList list, int left, int middle, int right, Func<object, object, bool> func)
    {
      List<object> new_list = new List<object>(right - left);//新的数组(用于存放排序后的元素)
      int left_list_cur_index = left;//左边的数组的当前index
      int right_list_cur_index = middle + 1;//右边的数组的当前index
      while (left_list_cur_index <= middle && right_list_cur_index <= right)
      {
        if (!func(list[left_list_cur_index], list[right_list_cur_index]))
        {
          new_list.Add(list[left_list_cur_index]);
          left_list_cur_index++;
        }
        else
        {
          new_list.Add(list[right_list_cur_index]);
          right_list_cur_index++;
        }
      }
      //left_array_cur_index <= mid and right_array_cur_index <= right 其中之一不成立的情况
      //处理没有被完全处理的数组
      for (; left_list_cur_index <= middle; left_list_cur_index++)
        new_list.Add(list[left_list_cur_index]);
      for (; right_list_cur_index <= right; right_list_cur_index++)
        new_list.Add(list[right_list_cur_index]);

      //重新赋值给list
      for (int i = 0; i <= new_list.Count; i++)
        list[left + i] = new_list[i];
    }


    //////////////////////////////////////////////////////////////////////
    // 快速排序
    // https://www.youtube.com/watch?v=COk73cpQbFQ
    // O(N* logN)
    // 不需要另外的N空间来进行排序处理，但不稳定 最好的情况需要时间为N*logN,最坏的情况需要时间为N^2
    //////////////////////////////////////////////////////////////////////
    //如：list.QuickSort_Array(list, (a, b)=>return a.count <= b.count)
    //则是将count由小到大排序，注意比较大小时不要漏掉等于号，否则相等时也进行排序，则排序不稳定
    public static void QuickSort(IList list, Func<object, object, bool> func, int? left = null, int? right = null)
    {
      int _left = left.GetValueOrDefault(0);
      int _right = right.GetValueOrDefault(list.Count - 1);
      if (_left >= _right)
        return;
      //在partition_index左边的都比index的value小，右边的都比index的value大
      var partition_index = Partition(list, func, _left, _right);
      //对左边单元进行排序
      QuickSort(list, func, left, partition_index - 1);
      //对右边单元进行排序
      QuickSort(list, func, partition_index + 1, right);
    }

    public static void QuickSortWithCompareRules(IList list, params Comparison<object>[] compare_rules)
    {
      QuickSort(list, (a, b) => CompareWithRules(a, b, compare_rules) < 0);
    }

    private static int Partition(IList list, Func<object, object, bool> func, int left, int right)
    {
      var pivot_value = list[right];
      int partition_index = left;
      object temp;
      for (int i = 0; i < right - 1; i++)
      {
        if (!func(list[i], pivot_value))
        {
          //swap list[i] and list[partition_index]
          temp = list[i];
          list[i] = list[partition_index];
          list[partition_index] = temp;
          partition_index++;
        }
      }

      //swap list[partition_index] and list[right]
      temp = list[partition_index];
      list[right] = list[partition_index];
      list[partition_index] = temp;

      return partition_index;
    }

    ///////////////////////////////////////////////////////////////////////////
    public static void QuickSort<T>(IList<T> list, Func<T, T, bool> func, int? left = null, int? right = null)
    {
      int _left = left.GetValueOrDefault(0);
      int _right = right.GetValueOrDefault(list.Count - 1);
      if (_left >= _right)
        return;
      //在partition_index左边的都比index的value小，右边的都比index的value大
      var partition_index = Partition(list, func, _left, _right);
      //对左边单元进行排序
      QuickSort(list, func, left, partition_index - 1);
      //对右边单元进行排序
      QuickSort(list, func, partition_index + 1, right);
    }

    public static void QuickSortWithCompareRules<T>(IList<T> list, params Comparison<T>[] compare_rules)
    {
      QuickSort(list, (a, b) => CompareWithRules(a, b, compare_rules) < 0);
    }

    private static int Partition<T>(IList<T> list, Func<T, T, bool> func, int left, int right)
    {
      var pivot_value = list[right];
      int partition_index = left;
      T temp;
      for (int i = 0; i < right - 1; i++)
      {
        if (!func(list[i], pivot_value))
        {
          //swap list[i] and list[partition_index]
          temp = list[i];
          list[i] = list[partition_index];
          list[partition_index] = temp;
          partition_index++;
        }
      }

      //swap list[partition_index] and list[right]
      temp = list[partition_index];
      list[right] = list[partition_index];
      list[partition_index] = temp;

      return partition_index;
    }
  }
}
