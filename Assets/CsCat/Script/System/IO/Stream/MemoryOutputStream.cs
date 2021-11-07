using System;
using UnityEngine;

namespace CsCat
{
    public class MemoryOutputStream : OutputStream
    {
        private byte[] data;
        private int incLen;

        public void Reset(byte[] inBuffer, int length)
        {
            data = inBuffer;
            base.length = length;
            pos = 0;
        }


        public MemoryOutputStream(int length, int inc_len)
        {
            this.incLen = inc_len;
            data = new byte[length];
            base.length = length;
            pos = 0;
        }

        public MemoryOutputStream(byte[] buf, int inc_len = 0)
        {
            this.incLen = inc_len;
            data = buf;
            length = buf.Length;
            pos = 0;
        }


        public override void Flush()
        {
        }

        public override byte[] GetBuffer()
        {
            return data;
        }

        public override void Seek(int length)
        {
            Debug.Assert(length <= base.length, "out of output stream length");
            pos = length;
        }

        public override void Skip(int length)
        {
            Debug.Assert(pos + length <= base.length, "out of output stream length");
            pos += length;
        }

        public override bool Write(byte[] buffer, int offset, int length)
        {
            if (pos + length > base.length)
            {
                if (incLen <= 0)
                {
                    incLen = 128;
                    LogCat.LogError("KMemoryOutputStream write error with 0 increase length");
                }

                var num = incLen;
                var num2 = base.length + num;
                while (pos + length >= num2) num2 += num;
                var dst = new byte[num2];
                Buffer.BlockCopy(data, 0, dst, 0, base.length);
                data = dst;
                base.length = num2;
            }

            Buffer.BlockCopy(buffer, offset, data, pos, length);
            pos += length;
            return true;
        }
    }
}