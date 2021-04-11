using System;
using UnityEngine;

namespace CsCat
{
  public static class DoubleExtension
  {
    public static byte[] ToBytes(this double self, bool is_net_order = false)
    {
      byte[] data = BitConverter.GetBytes(self);
      if (is_net_order)
        Array.Reverse(data);
      return data;
    }


    //是否是defalut, 默认是与float.MaxValue比较
    public static bool IsDefault(this double self, bool is_min = false)
    {
      if (is_min)
        return self == double.MinValue;
      else
        return self == double.MaxValue;
    }

    //得到百分比
    public static double GetPercent(this double self, double min_value, double max_value, bool is_clamp = true)
    {
      if (is_clamp)
      {
        if (self < min_value)
          self = min_value;
        else if (self > max_value)
          self = max_value;
      }

      double offset = self - min_value;
      return offset / (max_value - min_value);
    }

    public static bool IsInRange(this double self, double min_value, double max_value,
      bool is_min_value_included = false,
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

    //将v Round四舍五入snap_soze的倍数的值
    //Rounds value to the closest multiple of snap_soze.
    public static double Snap(this double v, double snap_size)
    {
      return Math.Round(v / snap_size) * snap_size;
    }

    public static double Snap2(this double v, double snap_size)
    {
      return Math.Round(v * snap_size) / snap_size;
    }

    public static double Minimum(this double self, double minimum)
    {
      return Math.Max(self, minimum);
    }

    public static double Maximum(this double self, double maximum)
    {
      return Math.Min(self, maximum);
    }
  }
}