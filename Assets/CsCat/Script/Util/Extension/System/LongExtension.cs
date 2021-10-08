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
            return _H2X(self, xbase);
        }

        public static string GetAssetPathByRefId(this long refId)
        {
            return AssetPathRefManager.instance.GetAssetPathByRefId(refId);
        }

        #region bytes

        /// <summary>
        ///   将数字转化为bytes
        /// </summary>
        public static byte[] ToBytes(this long self, bool isNetOrder = false)
        {
            return ByteUtil.ToBytes(self, 8, isNetOrder);
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
            return string.Format(StringConst.String_Format_NumberWithComma, self);
        }

        #region 私有方法

        /// <summary>
        ///   long转化为toBase进制
        /// </summary>
        private static string _H2X(long value, int toBase)
        {
            int digitIndex;
            var longPositive = Math.Abs(value);
            var radix = toBase;
            var outDigits = new char[63];
            var constChars = CharUtil.GetDigitsAndCharsBig();

            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0) break;

                outDigits[outDigits.Length - digitIndex - 1] =
                    constChars[longPositive % radix];
                longPositive /= radix;
            }

            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }

        #endregion
    }
}