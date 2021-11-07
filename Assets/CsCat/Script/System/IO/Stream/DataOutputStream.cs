using System;

namespace CsCat
{
    public class DataOutputStream : OutputStream
    {
        private static readonly byte[] buffer = new byte[8];
        private readonly OutputStream outputStream;

        public DataOutputStream(OutputStream outputStream)
        {
            this.outputStream = outputStream;
        }


        public override void Close()
        {
            outputStream.Close();
        }

        public override int CurrentPosition()
        {
            return outputStream.CurrentPosition();
        }

        public override void Flush()
        {
            outputStream.Flush();
        }

        public override byte[] GetBuffer()
        {
            return outputStream.GetBuffer();
        }

        public override void Reset()
        {
            base.Reset();
            outputStream?.Reset();
        }

        public override void Seek(int position)
        {
            outputStream.Seek(position);
        }

        public override void Skip(int length)
        {
            outputStream.Skip(length);
        }

        public override bool Write(byte[] buffer, int offset, int length)
        {
            return outputStream.Write(buffer, offset, length);
        }


        public OutputStream GetOutputStream()
        {
            return outputStream;
        }

        public void WriteBool(bool buf)
        {
            buffer[0] = (byte) (buf ? 1 : 0);
            outputStream.Write(buffer, 0, 1);
        }

        public void WriteByte(byte buf)
        {
            buffer[0] = buf;
            outputStream.Write(buffer, 0, 1);
        }

        public void WriteBytes(byte[] buf)
        {
            WriteInt32(buf.Length);
            outputStream.Write(buf, 0, buf.Length);
        }

        public void WriteDouble(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes, 0, bytes.Length);
            outputStream.Write(bytes, 0, bytes.Length);
        }

        public void WriteFloat(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes, 0, bytes.Length);
            outputStream.Write(bytes, 0, bytes.Length);
        }

        public void WriteInt16(short buf)
        {
            buffer[1] = (byte) buf;
            buffer[0] = (byte) (buf >> 8);
            outputStream.Write(buffer, 0, 2);
        }

        public void WriteInt32(int value)
        {
            buffer[3] = (byte) value;
            buffer[2] = (byte) (value >> 8);
            buffer[1] = (byte) (value >> 16);
            buffer[0] = (byte) (value >> 24);
            outputStream.Write(buffer, 0, 4);
        }

        public void WriteInt64(long value)
        {
            buffer[7] = (byte) value;
            buffer[6] = (byte) (value >> 8);
            buffer[5] = (byte) (value >> 16);
            buffer[4] = (byte) (value >> 24);
            buffer[3] = (byte) (value >> 32);
            buffer[2] = (byte) (value >> 40);
            buffer[1] = (byte) (value >> 48);
            buffer[0] = (byte) (value >> 56);
            outputStream.Write(buffer, 0, 8);
        }

        public void WriteUInt16(ushort buf)
        {
            buffer[1] = (byte) buf;
            buffer[0] = (byte) (buf >> 8);
            outputStream.Write(buffer, 0, 2);
        }

        public void WriteUInt32(uint value)
        {
            buffer[3] = (byte) value;
            buffer[2] = (byte) (value >> 8);
            buffer[1] = (byte) (value >> 16);
            buffer[0] = (byte) (value >> 24);
            outputStream.Write(buffer, 0, 4);
        }

        public void WriteUInt64(ulong value)
        {
            buffer[7] = (byte) value;
            buffer[6] = (byte) (value >> 8);
            buffer[5] = (byte) (value >> 16);
            buffer[4] = (byte) (value >> 24);
            buffer[3] = (byte) (value >> 32);
            buffer[2] = (byte) (value >> 40);
            buffer[1] = (byte) (value >> 48);
            buffer[0] = (byte) (value >> 56);
            outputStream.Write(buffer, 0, 8);
        }

        public void WriteUTF8String(string buf)
        {
            var array = buf.ToBytes();
            if (array.Length != 0)
            {
                WriteInt32(array.Length);
                outputStream.Write(array, 0, array.Length);
                return;
            }

            WriteInt32(0);
        }
    }
}