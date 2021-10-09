using System;
using System.Text;

namespace CsCat
{
    public class ByteUtil
    {
        /// <summary>
        ///   将data全部设为0
        /// </summary>
        public static void ZeroBytes(byte[] data)
        {
            ZeroBytes(data, 0);
        }

        /// <summary>
        ///   将data从offset位置开始设置为0
        /// </summary>
        public static void ZeroBytes(byte[] data, int offset)
        {
            var len = data.Length - offset;
            ZeroBytes(data, offset, len);
        }

        /// <summary>
        ///   将data从offset开始，长度为len设为0
        /// </summary>
        public static void ZeroBytes(byte[] data, int offset, int len)
        {
            const byte value = 0;
            SetBytes(data, offset, len, value);
        }

        /// <summary>
        ///   将data全部设置为value
        /// </summary>
        public static void SetBytes(byte[] data, byte value)
        {
            SetBytes(data, 0, value);
        }

        /// <summary>
        ///   将data从offset开始设置为value
        /// </summary>
        public static void SetBytes(byte[] data, int offset, byte value)
        {
            var len = data.Length - offset;
            SetBytes(data, offset, len, value);
        }

        /// <summary>
        ///   将data从offset开始,长度为len设置为value
        /// </summary>
        public static void SetBytes(byte[] data, int offset, int len, byte value)
        {
            for (var i = offset; i < len + offset; i++)
                data[i] = value;
        }


        /// <summary>
        ///   截取data从offset开始
        /// </summary>
        public static byte[] SubBytes(byte[] data, int offset)
        {
            return SubBytes(data, offset, data.Length - offset);
        }

        /// <summary>
        ///   截取data从offset开始，长度为len,如果len还有空余，则用0填充
        /// </summary>
        public static byte[] SubBytes(byte[] data, int offset, int len)
        {
            var bb = new byte[len];
            if (data.Length - offset >= len)
            {
                Array.Copy(data, offset, bb, 0, len);
            }
            else
            {
                Array.Copy(data, offset, bb, 0, data.Length - offset);
                ZeroBytes(bb, data.Length - offset);
            }

            return bb;
        }

        /// <summary>
        ///   截取data从offset开始，copy到bb中，如果bb的长度还有空余，则用0填充
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="bb"></param>
        /// <returns></returns>
        public static byte[] SubBytes(byte[] data, int offset, byte[] bb)
        {
            var len = bb.Length;
            if (data.Length - offset >= len)
            {
                Array.Copy(data, offset, bb, 0, len);
            }
            else
            {
                Array.Copy(data, offset, bb, 0, data.Length - offset);
                ZeroBytes(bb, data.Length - offset);
            }

            return bb;
        }


        /// <summary>
        ///   将bs1和bs2连接起来
        /// </summary>
        public static byte[] JoinBytes(byte[] bs1, byte[] bs2)
        {
            return JoinBytes(bs1, 0, bs1.Length, bs2, 0, bs2.Length);
        }

        /// <summary>
        ///   将bs1从offset1开始，长度为len1和bs2从offset2开始，长度为len2连接起来
        /// </summary>
        public static byte[] JoinBytes(byte[] bs1, int offset1, int len1, byte[] bs2, int offset2, int len2)
        {
            var bs = new byte[len1 + len2];
            BytesCopy(bs1, offset1, bs, 0, len1);
            BytesCopy(bs2, offset2, bs, len1, len2);
            return bs;
        }


        /// <summary>
        ///   将src复制到dst中，长度为len
        /// </summary>
        public static void BytesCopy(byte[] src, byte[] dst, int len)
        {
            Array.Copy(src, 0, dst, 0, len);
        }

        /// <summary>
        ///   将src从srcOffset开始复制len长度到dst的destOffset位置
        /// </summary>
        public static void BytesCopy(byte[] src, int src_offset, byte[] dst, int dst_offset, int len)
        {
            Array.Copy(src, src_offset, dst, dst_offset, len);
        }


        /// <summary>
        ///   b1和b2是否相等
        /// </summary>
        public static bool BytesEquals(byte[] b1, byte[] b2)
        {
            return BytesEquals(b1, 0, b2, 0);
        }

        /// <summary>
        ///   b1从offset1开始和b2从offset2开始是否相等
        /// </summary>
        public static bool BytesEquals(byte[] b1, int offset1, byte[] b2, int offset2)
        {
            return BytesEquals(b1, offset1, b2, offset2, b1.Length - offset1);
        }

        /// <summary>
        ///   b1从offset1开始和b2从offset2开始，长度为len是否相等
        /// </summary>
        public static bool BytesEquals(byte[] b1, int offset1, byte[] b2, int offset2, int len)
        {
            if (b1 != b2 && (b1 == null || b2 == null))
                return false;
            if (b1.Length < offset1 + len || b2.Length < offset2 + len)
                return false;
            for (var i = 0; i < len; i++)
                if (b1[offset1 + i] != b2[offset2 + i])
                    return false;
            return true;
        }


        public static bool ToBoolean(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToBoolean(value, startIndex);
        }

        public static char ToChar(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToChar(value, startIndex);
        }


        public static short ToShort(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToInt16(value, startIndex);
        }


        public static int ToInt(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToInt32(value, startIndex);
        }


        public static long ToLong(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToInt64(value, startIndex);
        }


        public static float ToFloat(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToSingle(value, startIndex);
        }


        public static double ToDouble(byte[] value, int startIndex = 0, bool isNetOrder = false)
        {
            if (isNetOrder)
                Array.Reverse(value);
            return BitConverter.ToDouble(value, startIndex);
        }

        /// <summary>
        ///   将数字转化为bytes
        /// </summary>
        public static byte[] ToBytes(long value, int len, bool isNetOrder = false)
        {
            if (isNetOrder)
            {
                var bb = new byte[len];
                //一个byte是8位的，所以8个byte就是64位，
                // 网络顺序的存储方式是bb[0]bb[1]bb[2]bb[3]bb[4]bb[5]bb[6]bb[7]bb[8],从i=0开始处理,每次将value右移(len-i-1)*8位,然后&0xFF取最后8位
                for (var i = 0; i < len; i++)
                    bb[i] = (byte) (int) ((value >> ((len - i - 1) * 8)) & 0xFF);
                return bb;
            }
            else
            {
                //一个byte是8位的，所以8个byte就是64位，
                //主机顺序的存储方式是bb[8]bb[7]bb[6]bb[5]bb[4]bb[3]bb[2]bb[1]bb[0],从i=0开始处理,每次右移i*8位,然后&0xFF取最后8位
                var bb = new byte[len];
                for (var i = 0; i < len; i++)
                    bb[i] = (byte) (int) ((value >> (i * 8)) & 0xFF);
                return bb;
            }
        }


        /// <summary>
        ///   将bb转为string
        /// </summary>
        /// <param name="bb"></param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string ToString(byte[] bb, Encoding encoding = null)
        {
            if (bb == null)
                return null;
            var i = 0;
            var isFound = false; //是否匹配到
            for (; i < bb.Length; i++)
                if (bb[i] == 0)
                {
                    isFound = true;
                    break;
                }

            var len = isFound ? i + 1 : bb.Length;
            return ToString(bb, 0, len, encoding);
        }

        public static string ToString(byte[] data, int offset, int len, Encoding encoding = null)
        {
            var bb = data.Clone(offset, len);
            return EncodingUtil.GetString(bb, offset, len, encoding);
        }
    }
}