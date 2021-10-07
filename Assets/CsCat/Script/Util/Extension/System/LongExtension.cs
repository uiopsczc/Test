using System;

namespace CsCat
{
  public static class LongExtension
  {
    /// <summary>
    ///   long转化为指定进制（16进制或者8进制等）
    /// </summary>
    public static string ToString(this long self, int xbase)
    {
      return H2X(self, xbase);
    }

    public static string GetAssetPathByRefId(this long ref_id)
    {
      return AssetPathRefManager.instance.GetAssetPathByRefId(ref_id);
    }

    #region bytes

    /// <summary>
    ///   将数字转化为bytes
    /// </summary>
    public static byte[] ToBytes(this long self, bool is_net_order = false)
    {
      return ByteUtil.ToBytes(self, 8, is_net_order);
    }

    #endregion


    public static long Minimum(this long self, long minimum)
    {
      return Math.Max(self, minimum);
    }

    public static long Maximum(this long self, long maximum)
    {
      return Math.Min(self, maximum);
    }

    public static string ToStringWithComma(this long self)
    {
      return string.Format("{0:N0}", self);
    }

    #region 私有方法

    /// <summary>
    ///   long转化为toBase进制
    /// </summary>
    private static string H2X(long value, int to_base)
    {
      int digit_index;
      var long_positive = Math.Abs(value);
      var radix = to_base;
      var out_digits = new char[63];
      var const_chars = CharUtil.GetDigitsAndCharsBig();

      for (digit_index = 0; digit_index <= 64; digit_index++)
      {
        if (long_positive == 0) break;

        out_digits[out_digits.Length - digit_index - 1] =
          const_chars[long_positive % radix];
        long_positive /= radix;
      }

      return new string(out_digits, out_digits.Length - digit_index, digit_index);
    }

    #endregion
  }
}