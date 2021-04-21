using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class IntExtension
  {
    #region 编码

    /// <summary>
    ///   10进制转为16进制字符串
    /// </summary>
    public static string ToHexString(this int self)
    {
      return self.ToString("X");
    }

    #endregion

    #region bytes

    /// <summary>
    ///   将数字转化为bytes
    /// </summary>
    public static byte[] ToBytes(this int self, bool is_net_order = false)
    {
      return ByteUtil.ToBytes(self & 0xFFFFFFFF, 4, is_net_order);
    }

    #endregion

    /// <summary>
    ///   随机一个total以内的队列（队列里面的元素不会重复）
    /// </summary>
    /// <param name="self"></param>
    /// <param name="is_include_total">是否包括total</param>
    /// <param name="is_zero_base">是否从0开始</param>
    /// <returns></returns>
    public static List<int> Random(this int self, float out_count, bool is_unique, bool is_include_total = false,
      bool is_zero_base = true, RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      var result = new List<int>();
      var to_random_list = new List<int>(); //要被随机的List

      for (var i = is_zero_base ? 0 : 1; i < (is_include_total ? self + 1 : self); i++) to_random_list.Add(i);

      var count = to_random_list.Count;

      for (var i = 0; i < out_count; i++)
      {
        var index = randomManager.RandomInt(0, to_random_list.Count);
        if (is_unique)
          result.Add(to_random_list.RemoveAt2(index));
        else
          result.Add(to_random_list[index]);
      }

      return result;
    }


    public static T ToEnum<T>(this int self)
    {
      return (T)Enum.ToObject(typeof(T), self);
    }

    //是否是defalut, 默认是与float.MaxValue比较
    public static bool IsDefault(this int self, bool is_min = false)
    {
      if (is_min)
        return self == int.MinValue;
      else
        return self == int.MaxValue;
    }

    public static bool IsInRange(this int self, int min_value, int max_value, bool is_min_value_included = false,
      bool is_max_value_included = false)
    {
      if (self < min_value || self > max_value)
        return false;
      if (self == min_value && !is_min_value_included)
        return false;
      if (self == max_value && !is_max_value_included)
        return false;
      return true;
    }

    public static int Minimum(this int self, int minimum)
    {
      return Mathf.Max(self, minimum);
    }

    public static int Maximum(this int self, int maximum)
    {
      return Mathf.Min(self, maximum);
    }

    public static string ToStringWithComma(this int self)
    {
      return string.Format("{0:N0}", self);
    }
  }
}