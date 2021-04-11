using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CsCat
{
  public static class IndexUtil
  {
    //按高,行,列来填写
    //scales从1开始计算
    //indexes从0开始计算
    public static int GetCombinedIndex(int[] scales, int[] indexes)
    {
      int result = 0;
      for (int i = 0; i < indexes.Length; i++)
        if (i != indexes.Length - 1)
          result += indexes[i] + scales[i + 1];
        else
          result += indexes[i];
      return result;
    }

    //按高,行,列来填写
    //scales从1开始计算
    //indexes从0开始计算
    public static int GetCombinedIndex((int, int) scales, (int, int) indexes)
    {
      return indexes.Item1 * scales.Item2 + indexes.Item2 * 1;
    }

    //按高,行,列来填写
    //scales从1开始计算
    //indexes从0开始计算
    public static int GetCombinedIndex((int, int, int) scales, (int, int, int) indexes)
    {
      return indexes.Item1 * scales.Item2 + indexes.Item2 * scales.Item3 + indexes.Item3 * 1;
    }

    //按高,行,列来填写
    //scales从1开始计算
    //indexes从0开始计算
    public static int GetCombinedIndex((int, int, int, int) scales, (int, int, int, int) indexes)
    {
      return indexes.Item1 * scales.Item2 + indexes.Item2 * scales.Item3 + indexes.Item3 * scales.Item4 +
             indexes.Item4 * 1;
    }

    //按高,行,列来填写
    //scales从1开始计算
    //indexes从0开始计算
    public static int GetCombinedIndex((int, int, int, int, int) scales, (int, int, int, int, int) indexes)
    {
      return indexes.Item1 * scales.Item2 + indexes.Item2 * scales.Item3 + indexes.Item3 * scales.Item4 +
             indexes.Item4 * scales.Item5 + indexes.Item5 * 1;
    }
  }
}