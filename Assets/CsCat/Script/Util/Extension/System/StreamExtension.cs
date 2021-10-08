using System.IO;

namespace CsCat
{
    public static class StreamExtension
    {
        public static void WriteChar(this Stream self, char i)
        {
            var data = i.ToBytes();
            Write(self, data);
        }

        public static void WriteShort(this Stream self, short i)
        {
            var data = i.ToBytes();
            Write(self, data);
        }

        public static void WriteInt(this Stream self, int i)
        {
            var data = i.ToBytes();
            Write(self, data);
        }

        public static void WriteLong(this Stream self, long i)
        {
            var data = i.ToBytes();
            Write(self, data);
        }

        public static void WriteFloat(this Stream self, float i)
        {
            var data = i.ToBytes();
            Write(self, data);
        }

        public static void WriteDouble(this Stream self, double i)
        {
            var data = i.ToBytes();
            Write(self, data);
        }

        private static void Write(this Stream self, byte[] data)
        {
            self.Write(data, 0, data.Length);
        }

        private static byte[] Read(this Stream self, byte[] data)
        {
            self.Read(data, 0, data.Length);
            return data;
        }

        public static char ReadChar(this Stream self)
        {
            var data = new byte[2];
            return ByteUtil.ToChar(Read(self, data));
        }

        public static short ReadShort(this Stream stream)
        {
            var data = new byte[2];
            return ByteUtil.ToShort(Read(stream, data));
        }

        public static int ReadInt(this Stream self)
        {
            var data = new byte[4];
            return ByteUtil.ToInt(Read(self, data));
        }

        public static long ReadLong(this Stream stream)
        {
            var data = new byte[8];
            return ByteUtil.ToLong(Read(stream, data));
        }

        public static float ReadFloat(this Stream self)
        {
            var data = new byte[4];
            return ByteUtil.ToFloat(Read(self, data));
        }

        public static double ReadDouble(this Stream self)
        {
            var data = new byte[8];
            return ByteUtil.ToDouble(Read(self, data));
        }

        /// <summary>
        ///   读取stream中的数据到buffer中
        /// </summary>
        /// <param name="self"></param>
        /// <param name="buffer"></param>
        /// <returns>读到的数目</returns>
        public static int ReadBytes(this Stream self, byte[] buffer)
        {
            // Use this method is used to read all bytes from a stream.
            var offset = 0;
            var totalCount = 0;
            var readUnit = buffer.Length > 100 ? 100 : buffer.Length;
            while (true)
            {
                var bytesReadCount = self.Read(buffer, offset,
                    offset + readUnit > buffer.Length ? buffer.Length - offset : readUnit);
                if (bytesReadCount == 0)
                    break;
                offset += bytesReadCount;
                totalCount += bytesReadCount;
            }

            return totalCount;
        }


        /// <summary>
        ///   在Stream读取len长度的数据
        /// </summary>
        /// <param name="self"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(this Stream self, int len)
        {
            var buf = new byte[len];
            len = self.ReadBytes(buf);
            if (len < buf.Length)
                return ByteUtil.SubBytes(buf, 0, len);
            return buf;
        }

        /// <summary>
        ///   读取ins的全部数据
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(this Stream self)
        {
            var outs = new MemoryStream();
            self.CopyToStream(outs);
            return outs.ToArray();
        }

        /// <summary>
        ///   读取ins的全部数据到outs中
        /// </summary>
        /// <param name="self"></param>
        /// <param name="outs"></param>
        /// <returns></returns>
        public static void CopyToStream(this Stream self, Stream outs)
        {
            var data = new byte[4096];
            int len;
            do
            {
                len = self.ReadBytes(data);
                if (len > 0)
                    outs.Write(data, 0, len);
            } while (len >= data.Length); //一般情况下是等于，读完的时候是少于
        }
    }
}