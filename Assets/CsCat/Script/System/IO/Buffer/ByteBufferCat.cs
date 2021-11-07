using System;
using System.IO;

namespace CsCat
{
    public class ByteBufferCat : MemoryStream
    {
        private readonly bool isInfinite;


        /// <summary>
        ///   检查是否超出边界
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private void CheckBounds(int offset, int length, int size)
        {
            if ((offset | length | (offset + length) | (size - (offset + length))) < 0)
                throw new IndexOutOfRangeException();

            if (length > Remaining() && isInfinite == false)
                throw new IndexOutOfRangeException();
        }


        public ByteBufferCat(int size)
            : base(new byte[size])
        {
        }

        public ByteBufferCat()
        {
            isInfinite = true;
        }


        /// <summary>
        ///   清除buf
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            SetLength(0);
        }

        /// <summary>
        ///   将初始位置设置为0
        /// </summary>
        /// <returns></returns>
        public void Flip()
        {
            var position = Position;
            SetLength(position);
            Position = 0;
        }

        /// <summary>
        ///   还有多少是剩余的
        /// </summary>
        /// <returns></returns>
        public int Remaining()
        {
            return (int) (Length - Position);
        }

        /// <summary>
        ///   是否还有剩余
        /// </summary>
        /// <returns></returns>
        public bool IsHasRemaining()
        {
            return Remaining() > 0;
        }

        /// <summary>
        ///   添加byte（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public ByteBufferCat Put(byte b)
        {
            CheckBounds(0, 1, 1);
            WriteByte(b);
            return this;
        }

        /// <summary>
        ///   添加一个int类型的数据（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public ByteBufferCat Put(int b)
        {
            return Put((byte) b);
        }

        /// <summary>
        ///   在index位置添加byte（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public ByteBufferCat Put(int index, byte b)
        {
            var tmpPosition = Position;
            Seek(index, SeekOrigin.Begin);
            CheckBounds(0, 1, 1);
            WriteByte(b);
            //this.Position = tmp_position;
            Seek(tmpPosition, SeekOrigin.Begin);
            return this;
        }

        /// <summary>
        ///   添加ByteBuffer（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public ByteBufferCat Put(ByteBufferCat src)
        {
            if (src == this)
                throw new ArgumentException();
            var n = src.Remaining();
            if (n > Remaining() && isInfinite == false)
                throw new IndexOutOfRangeException();
            for (var i = 0; i < n; i++)
                Put(src.Get());
            return this;
        }

        /// <summary>
        ///   添加bytes输出位置为offset，长度为length的subbytes（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public ByteBufferCat Put(byte[] src, int offset, int length)
        {
            CheckBounds(offset, length, src.Length);
            var end = offset + length;
            for (var i = offset; i < end; i++)
                Put(src[i]);
            return this;
        }

        /// <summary>
        ///   添加bytes（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public ByteBufferCat Put(byte[] src)
        {
            return Put(src, 0, src.Length);
        }

        /// <summary>
        ///   在index添加bytes（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public ByteBufferCat Put(int index, byte[] src)
        {
            foreach (var t in src)
                Put(index++, t);
            return this;
        }

        /// <summary>
        ///   添加一个char类型的数据（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutChar(char value)
        {
            var data = value.ToBytes();
            return Put(data);
        }

        /// <summary>
        ///   在index位置添加一个char类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutChar(int index, char value)
        {
            var data = value.ToBytes();
            return Put(index, data);
        }

        /// <summary>
        ///   添加一个short类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutShort(short value)
        {
            var data = value.ToBytes();
            return Put(data);
        }

        public ByteBufferCat PutShort(int value)
        {
            return PutShort((short) value);
        }

        /// <summary>
        ///   在index位置添加一个short类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutShort(int index, short value)
        {
            var data = value.ToBytes();
            return Put(index, data);
        }

        /// <summary>
        ///   添加一个int类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutInt(int value)
        {
            var data = value.ToBytes();
            return Put(data);
        }

        /// <summary>
        ///   在index位置添加一个int类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutInt(int index, int value)
        {
            var data = value.ToBytes();
            return Put(index, data);
        }

        /// <summary>
        ///   添加一个long类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutLong(long value)
        {
            var data = value.ToBytes();
            return Put(data);
        }

        /// <summary>
        ///   在index位置添加一个long类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutLong(int index, long value)
        {
            var data = value.ToBytes();
            return Put(index, data);
        }

        /// <summary>
        ///   添加一个float类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutFloat(float value)
        {
            var data = value.ToBytes();
            return Put(data);
        }

        /// <summary>
        ///   在index位置添加一个float类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutFloat(int index, float value)
        {
            var data = value.ToBytes();
            return Put(index, data);
        }

        /// <summary>
        ///   添加一个double类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutDouble(double value)
        {
            var data = value.ToBytes();
            return Put(data);
        }

        /// <summary>
        ///   在index位置添加一个double类型的value（添加后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ByteBufferCat PutDouble(int index, double value)
        {
            var data = value.ToBytes();
            return Put(index, data);
        }

        /// <summary>
        ///   获得一个byte（获取后的初始位置还是原来的位置）
        /// </summary>
        /// <returns></returns>
        public byte Get()
        {
            CheckBounds(0, 1, 1);
            var data = (byte) ReadByte();
            return data;
        }

        /// <summary>
        ///   在index位置获得一个byte（获取后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte Get(int index)
        {
            var tmpPosition = Position;
            Seek(index, SeekOrigin.Begin);
            CheckBounds(0, 1, 1);
            var data = (byte) ReadByte();
            Seek(tmpPosition, SeekOrigin.Begin);
            //this.Position = tmp_position;
            return data;
        }

        /// <summary>
        ///   获取bytes开始位置为offset，长度为length的subbytes（获取后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public ByteBufferCat Get(byte[] dst, int offset, int length)
        {
            CheckBounds(offset, length, dst.Length);
            var end = offset + length;
            for (var i = offset; i < end; i++)
                dst[i] = Get();
            return this;
        }

        /// <summary>
        ///   获取bytes长度（获取后的初始位置还是原来的位置）
        /// </summary>
        /// <param name="dst"></param>
        /// <returns></returns>
        public ByteBufferCat Get(byte[] dst)
        {
            return Get(dst, 0, dst.Length);
        }

        /// <summary>
        ///   获取一个char类型的数据
        /// </summary>
        /// <returns></returns>
        public char GetChar()
        {
            var data = GetData(2);
            return ByteUtil.ToChar(data);
        }

        /// <summary>
        ///   在index位置获取一个char类型的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char GetChar(int index)
        {
            var data = GetData(index, 2);
            return ByteUtil.ToChar(data);
        }

        /// <summary>
        ///   获取一个short类型的数据
        /// </summary>
        /// <returns></returns>
        public short GetShort()
        {
            var data = GetData(2);
            return ByteUtil.ToShort(data);
        }

        /// <summary>
        ///   在index位置获取一个short类型的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public short GetShort(int index)
        {
            var data = GetData(index, 2);
            return ByteUtil.ToShort(data);
        }

        /// <summary>
        ///   获取一个int类型的数据
        /// </summary>
        /// <returns></returns>
        public int GetInt()
        {
            var data = GetData(4);
            return ByteUtil.ToInt(data);
        }

        /// <summary>
        ///   在index位置获取一个int类型的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInt(int index)
        {
            var data = GetData(index, 4);
            return ByteUtil.ToInt(data);
        }

        /// <summary>
        ///   获取一个long类型的数据
        /// </summary>
        /// <returns></returns>
        public long GetLong()
        {
            var data = GetData(8);
            return ByteUtil.ToLong(data);
        }

        /// <summary>
        ///   在index位置获取一个long类型的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public long GetLong(int index)
        {
            var data = GetData(index, 8);
            return ByteUtil.ToInt(data);
        }

        /// <summary>
        ///   获取一个float类型的数据
        /// </summary>
        /// <returns></returns>
        public float GetFloat()
        {
            var data = GetData(4);
            return ByteUtil.ToFloat(data);
        }

        /// <summary>
        ///   在index位置获取一个float类型的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float GetFloat(int index)
        {
            var data = GetData(index, 4);
            return ByteUtil.ToFloat(data);
        }

        /// <summary>
        ///   获取一个double类型的数据
        /// </summary>
        /// <returns></returns>
        public double GetDouble()
        {
            var data = GetData(8);
            return ByteUtil.ToDouble(data);
        }

        /// <summary>
        ///   在index位置获取一个double类型的数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetDouble(int index)
        {
            var data = GetData(index, 8);
            return ByteUtil.ToDouble(data);
        }

        /// <summary>
        ///   获取len长度的bytes
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        private byte[] GetData(int len)
        {
            var data = new byte[len];
            for (var i = 0; i < len; i++)
                data[i] = Get();
            return data;
        }

        /// <summary>
        ///   在index获取len长度的数据
        /// </summary>
        /// <param name="len"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte[] GetData(int index, int len)
        {
            var data = new byte[len];
            for (var i = 0; i < len; i++)
                data[i] = Get(index++);
            return data;
        }
    }
}