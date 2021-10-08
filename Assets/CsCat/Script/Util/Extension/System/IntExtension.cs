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
            return self.ToString(StringConst.String_X);
        }

        #endregion

        #region bytes

        /// <summary>
        ///   将数字转化为bytes
        /// </summary>
        public static byte[] ToBytes(this int self, bool isNetOrder = false)
        {
            return ByteUtil.ToBytes(self & 0xFFFFFFFF, 4, isNetOrder);
        }

        #endregion

        /// <summary>
        ///   随机一个total以内的队列（队列里面的元素不会重复）
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isIncludeTotal">是否包括total</param>
        /// <param name="isZeroBase">是否从0开始</param>
        /// <returns></returns>
        public static List<int> Random(this int self, float outCount, bool isUnique, bool isIncludeTotal = false,
            bool isZeroBase = true, RandomManager randomManager = null)
        {
            randomManager = randomManager ?? Client.instance.randomManager;
            var result = new List<int>();
            var toRandomList = new List<int>(); //要被随机的List

            for (var i = isZeroBase ? 0 : 1; i < (isIncludeTotal ? self + 1 : self); i++)
                toRandomList.Add(i);

            for (var i = 0; i < outCount; i++)
            {
                var index = randomManager.RandomInt(0, toRandomList.Count);
                result.Add(isUnique ? toRandomList.RemoveAt2(index) : toRandomList[index]);
            }

            return result;
        }


        public static T ToEnum<T>(this int self)
        {
            return (T) Enum.ToObject(typeof(T), self);
        }

        //是否是defalut, 默认是与float.MaxValue比较
        public static bool IsDefault(this int self, bool isMin = false)
        {
            return isMin ? self == int.MinValue : self == int.MaxValue;
        }

        public static bool IsInRange(this int self, int minValue, int maxValue, bool isMinValueIncluded = false,
            bool isMaxValueIncluded = false)
        {
            return self >= minValue && self <= maxValue &&
                   ((self != minValue || isMinValueIncluded) && (self != maxValue || isMaxValueIncluded));
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
            return string.Format(StringConst.String_Format_NumberWithComma, self);
        }
    }
}