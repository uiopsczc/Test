using System;

namespace CsCat
{
  public class DataOutputStream : OutputStream
  {
    #region ctor

    public DataOutputStream(OutputStream stream)
    {
      outputStream = stream;
    }

    #endregion

    #region field

    private static readonly byte[] buff = new byte[8];


    private readonly OutputStream outputStream;

    #endregion

    #region override method

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
      if (outputStream != null) outputStream.Reset();
    }

    public override void Seek(int position)
    {
      outputStream.Seek(position);
    }

    public override void Skip(int length)
    {
      outputStream.Skip(length);
    }

    public override bool Write(byte[] buf, int offset, int length)
    {
      return outputStream.Write(buf, offset, length);
    }

    #endregion

    #region public method

    public OutputStream GetOutputStream()
    {
      return outputStream;
    }

    public void WriteBool(bool buf)
    {
      buff[0] = (byte) (buf ? 1 : 0);
      outputStream.Write(buff, 0, 1);
    }

    public void WriteByte(byte buf)
    {
      buff[0] = buf;
      outputStream.Write(buff, 0, 1);
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
      buff[1] = (byte) buf;
      buff[0] = (byte) (buf >> 8);
      outputStream.Write(buff, 0, 2);
    }

    public void WriteInt32(int value)
    {
      buff[3] = (byte) value;
      buff[2] = (byte) (value >> 8);
      buff[1] = (byte) (value >> 16);
      buff[0] = (byte) (value >> 24);
      outputStream.Write(buff, 0, 4);
    }

    public void WriteInt64(long value)
    {
      buff[7] = (byte) value;
      buff[6] = (byte) (value >> 8);
      buff[5] = (byte) (value >> 16);
      buff[4] = (byte) (value >> 24);
      buff[3] = (byte) (value >> 32);
      buff[2] = (byte) (value >> 40);
      buff[1] = (byte) (value >> 48);
      buff[0] = (byte) (value >> 56);
      outputStream.Write(buff, 0, 8);
    }

    public void WriteUInt16(ushort buf)
    {
      buff[1] = (byte) buf;
      buff[0] = (byte) (buf >> 8);
      outputStream.Write(buff, 0, 2);
    }

    public void WriteUInt32(uint value)
    {
      buff[3] = (byte) value;
      buff[2] = (byte) (value >> 8);
      buff[1] = (byte) (value >> 16);
      buff[0] = (byte) (value >> 24);
      outputStream.Write(buff, 0, 4);
    }

    public void WriteUInt64(ulong value)
    {
      buff[7] = (byte) value;
      buff[6] = (byte) (value >> 8);
      buff[5] = (byte) (value >> 16);
      buff[4] = (byte) (value >> 24);
      buff[3] = (byte) (value >> 32);
      buff[2] = (byte) (value >> 40);
      buff[1] = (byte) (value >> 48);
      buff[0] = (byte) (value >> 56);
      outputStream.Write(buff, 0, 8);
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

    #endregion
  }
}