using System;
using System.Collections.Generic;

namespace CsCat
{
  public static class CompareUtil
  {
    public static int CompareWithRules<T>(T data1, T data2,params Comparison<T>[] compareRules)
    {
      foreach (var compareRule in compareRules)
      {
        if (compareRule == null)
          continue;
        var result = compareRule(data1, data2);
        if (result == 0)
          continue;
        return result;
      }
      return 0;
    }
  }
}