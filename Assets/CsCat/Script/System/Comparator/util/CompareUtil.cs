using System;
using System.Collections.Generic;

namespace CsCat
{
  public static class CompareUtil
  {
    public static int CompareWithRules<T>(T data1, T data2,params Comparison<T>[] compare_rules)
    {
      foreach (var compare_rule in compare_rules)
      {
        if (compare_rule == null)
          continue;
        int result = compare_rule(data1, data2);
        if (result == 0)
          continue;
        return result;
      }
      return 0;
    }
  }
}