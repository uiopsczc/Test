using System;
using NPOI.SS.Formula.Functions;
using UnityEngine;

namespace CsCat
{
  public static class FloatExtension
  {
    /// <summary>
    ///   比较两个float是否相等，两者相差FloatConst.EPSILON则判断为相等，否则判断为不相等
    /// </summary>
    /// <param name="self"></param>
    /// <param name="f2"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public static bool EqualsEPSILON(this float self, float f2, float epsilon = FloatConst.Epsilon)
    {
      return Math.Abs(self - f2) < epsilon;
    }

    /// <summary>
    ///   转为bytes
    /// </summary>
    /// <param name="self"></param>
    /// <param name="is_net_order">是否是网络顺序，网络顺序是相反的</param>
    /// <returns></returns>
    public static byte[] ToBytes(this float self, bool is_net_order = false)
    {
      var data = BitConverter.GetBytes(self);
      if (is_net_order)
        Array.Reverse(data);
      return data;
    }



    //是否是defalut, 默认是与float.MaxValue比较
    public static bool IsDefault(this float value, bool isMin = false)
    {
      if (isMin)
        return value == float.MinValue;
      else
        return value == float.MaxValue;
    }

    //得到百分比
    public static float GetPercent(this float value, float minValue, float maxValue, bool isClamp = true)
    {
      if (isClamp)
      {
        if (value < minValue)
          value = minValue;
        else if (value > maxValue)
          value = maxValue;
      }

      float offset = value - minValue;
      return offset / (maxValue - minValue);
    }

    public static bool IsInRange(this float value, float minValue, float maxValue, bool isMinValueInclude = false,
      bool isMaxValueInclude = false)
    {
      if (value < minValue || value > maxValue)
        return false;
      if (value == minValue && !isMinValueInclude)
        return false;
      if (value == maxValue && !isMaxValueInclude)
        return false;
      return true;
    }

    /// <summary>
    /// 百分比  输入0.1,输出10%
    /// </summary>
    /// <param name="pct"></param>
    /// <returns></returns>
    public static string ToPctString(this float pct)
    {
      return string.Format("{0}%", pct * 100);
    }

    //将v Round四舍五入snap_soze的倍数的值
    //Rounds value to the closest multiple of snap_size.
    public static float Snap(this float v, float snap_size)
    {
      return Mathf.Round(v / snap_size) * snap_size;
    }

    public static float Snap2(this float v, float snap_size)
    {
      return Mathf.Round(v * snap_size) / snap_size;
    }

    public static float Minimum(this float self, float minimum)
    {
      return Math.Max(self, minimum);
    }

    public static float Maximum(this float self, float maximum)
    {
      return Math.Min(self, maximum);
    }

  }
}