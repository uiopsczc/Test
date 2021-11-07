using System;

namespace CsCat
{
    public class DataInputStream : InputStream
    {
        private readonly InputStream inputStream;
        private readonly byte[] buffer = new byte[8];

        public DataInputStream(InputStream inputStream)
        {
            this.inputStream = inputStream;
        }


        public override void Close()
        {
            inputStream.Close();
        }

        public override int CurrentPosition()
        {
            return inputStream.CurrentPosition();
        }

        public override bool Eof()
        {
            return inputStream.Eof();
        }

        public override byte[] GetBuffer()
        {
            return inputStream.GetBuffer();
        }

        public override int GetLength()
        {
            return inputStream.GetLength();
        }

        public override void Peek(byte[] buffer, int offset, int length)
        {
            inputStream.Peek(buffer, offset, length);
        }

        public override void Read(byte[] buffer, int offset, int length)
        {
            inputStream.Read(buffer, offset, length);
        }

        public override void Reset()
        {
            base.Reset();
            inputStream?.Reset();
        }

        public override void Seek(int position)
        {
            inputStream.Seek(position);
        }

        public override void Skip(int length)
        {
            inputStream.Skip(length);
        }


        public InputStream GetInputStream()
        {
            return inputStream;
        }

        public uint ReadARGBInt()
        {
            return ReadUInt32();
        }

        public bool ReadBool()
        {
            inputStream.Read(buffer, 0, 1);
            return buffer[0] > 0;
        }

        public sbyte ReadByte()
        {
            inputStream.Read(buffer, 0, 1);
            return (sbyte) buffer[0];
        }

        public byte[] ReadBytes()
        {
            var num = ReadInt32();
            if (num > 99999999 || num < 0)
            {
                LogCat.LogErrorFormat("ReadBytes error! length={0}", num);
                return null;
            }

            var array = new byte[num];
            Read(array, 0, num);
            return array;
        }

        public double ReadDouble()
        {
            inputStream.Read(buffer, 0, 8);
            Array.Reverse(buffer, 0, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        public float ReadFloat()
        {
            inputStream.Read(buffer, 0, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        public short ReadInt16()
        {
            inputStream.Read(buffer, 0, 2);
            return (short) (buffer[1] | (buffer[0] << 8));
        }

        public int ReadInt32()
        {
            inputStream.Read(buffer, 0, 4);
            return buffer[3] | (buffer[2] << 8) | (buffer[1] << 16) | (buffer[0] << 24);
        }

        public long ReadInt64()
        {
            inputStream.Read(buffer, 0, 8);
            var value1 = (ulong) (buffer[3] | (buffer[2] << 8) | (buffer[1] << 16) | (buffer[0] << 24));
            var value2 = (uint) (buffer[7] | (buffer[6] << 8) | (buffer[5] << 16) | (buffer[4] << 24));
            return (long) ((value1 << 32) | value2);
        }

        public ushort ReadUInt16()
        {
            inputStream.Read(buffer, 0, 2);
            return (ushort) (buffer[1] | (buffer[0] << 8));
        }

        public uint ReadUInt32()
        {
            inputStream.Read(buffer, 0, 4);
            return (uint) (buffer[3] | (buffer[2] << 8) | (buffer[1] << 16) | (buffer[0] << 24));
        }

        public ulong ReadUInt64()
        {
            inputStream.Read(buffer, 0, 8);
            var value1 = (ulong) (buffer[3] | (buffer[2] << 8) | (buffer[1] << 16) | (buffer[0] << 24));
            var value2 = (uint) (buffer[7] | (buffer[6] << 8) | (buffer[5] << 16) | (buffer[4] << 24));
            return (value1 << 32) | value2;
        }

        public string ReadUTF8String()
        {
            var num = ReadInt32();
            if (num == 0) return string.Empty;
            if (num > 9999999 || num < 0)
            {
                LogCat.LogErrorFormat("ReadUTF8String error! length={0}", num);
                return string.Empty;
            }

            var array = new byte[num];
            inputStream.Read(array, 0, num);
            return ByteUtil.ToString(array);
        }
    }
}