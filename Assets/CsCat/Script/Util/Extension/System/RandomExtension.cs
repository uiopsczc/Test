using System;

namespace CsCat
{
    public static class RandomExtension
    {
        /// <summary>
        ///   随机长整数
        /// </summary>
        public static long RandomLong(this Random self, DigitSign sign = DigitSign.All)
        {
            var bytes = new byte[8];
            self.NextBytes(bytes);
            if (sign == DigitSign.Positive)
                bytes[0] = 0; //非负
            else if (sign == DigitSign.Negative)
                bytes[0] = 1; //非负
            return ByteUtil.ToLong(bytes);
        }

        public static bool RandomBool(this Random self)
        {
            return self.Next(2) != 0;
        }
    }
}