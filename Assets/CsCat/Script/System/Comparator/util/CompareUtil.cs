using System;
using System.Collections.Generic;

namespace CsCat
{
  public static class CompareUtil
  {

    public static void SortListWithCompareRules<T>(List<T> to_sort_list,params Comparison<T>[] compare_rules)
    {
      to_sort_list.Sort((a,b)=> __Sort(a,b,compare_rules));
    }

    public static T[] SortArrayWithCompareRules<T>(T[] to_sort_arrays, params Comparison<T>[] compare_rules)
    {
      if (to_sort_arrays == null || to_sort_arrays.Length == 0)
        return to_sort_arrays;
      var __tmp_list = new List<T>(to_sort_arrays);
      SortListWithCompareRules(__tmp_list);
      for (int i = 0; i < __tmp_list.Count; i++)
        to_sort_arrays[i] = __tmp_list[i];
      return to_sort_arrays;
    }

    static int __Sort<T>(T a, T b, params Comparison<T>[] compare_rules)
    {
      foreach (var compare_rule in compare_rules)
      {
        if (compare_rule == null)
          continue;
        int result = compare_rule(a, b);
        if (result != 0)
          return result;
      }
      return 0;
    }
  }
}