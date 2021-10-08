using System;

namespace CsCat
{
    public static class CharExtension
    {
        /// <summary>
        /// 转化为bytes
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isNetOrder">是否是网络顺序</param>
        /// <returns></returns>
        public static byte[] ToBytes(this char self, bool isNetOrder = false)
        {
            byte[] data = BitConverter.GetBytes(self);
            if (isNetOrder)
                Array.Reverse(data);
            return data;
        }

        public static bool IsUpper(this char self)
        {
            return self >= CharConst.Char_A && self <= CharConst.Char_Z;
        }

        public static bool IsLower(this char self)
        {
            return self >= CharConst.Char_a && self <= CharConst.Char_z;
        }
    }
}