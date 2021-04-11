using System;

namespace CsCat
{
  public class DataInputStream : InputStream
  {
    #region ctor

    public DataInputStream(InputStream stream)
    {
      inputStream = stream;
    }

    #endregion

    #region field

    private readonly InputStream inputStream;
    private readonly byte[] buff = new byte[8];

    #endregion

    #region override method

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

    public override void Peek(byte[] buf, int offset, int length)
    {
      inputStream.Peek(buf, offset, length);
    }

    public override void Read(byte[] buf, int offset, int length)
    {
      inputStream.Read(buf, offset, length);
    }

    public override void Reset()
    {
      base.Reset();
      if (inputStream != null) inputStream.Reset();
    }

    public override void Seek(int position)
    {
      inputStream.Seek(position);
    }

    public override void Skip(int length)
    {
      inputStream.Skip(length);
    }

    #endregion

    #region public method

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
      inputStream.Read(buff, 0, 1);
      return buff[0] > 0;
    }

    public sbyte ReadByte()
    {
      inputStream.Read(buff, 0, 1);
      return (sbyte) buff[0];
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
      inputStream.Read(buff, 0, 8);
      Array.Reverse(buff, 0, 8);
      return BitConverter.ToDouble(buff, 0);
    }

    public float ReadFloat()
    {
      inputStream.Read(buff, 0, 4);
      Array.Reverse(buff, 0, 4);
      return BitConverter.ToSingle(buff, 0);
    }

    public short ReadInt16()
    {
      inputStream.Read(buff, 0, 2);
      return (short) (buff[1] | (buff[0] << 8));
    }

    public int ReadInt32()
    {
      inputStream.Read(buff, 0, 4);
      return buff[3] | (buff[2] << 8) | (buff[1] << 16) | (buff[0] << 24);
    }

    public long ReadInt64()
    {
      inputStream.Read(buff, 0, 8);
      var value1 = (ulong) (buff[3] | (buff[2] << 8) | (buff[1] << 16) | (buff[0] << 24));
      var value2 = (uint) (buff[7] | (buff[6] << 8) | (buff[5] << 16) | (buff[4] << 24));
      return (long) ((value1 << 32) | value2);
    }

    public ushort ReadUInt16()
    {
      inputStream.Read(buff, 0, 2);
      return (ushort) (buff[1] | (buff[0] << 8));
    }

    public uint ReadUInt32()
    {
      inputStream.Read(buff, 0, 4);
      return (uint) (buff[3] | (buff[2] << 8) | (buff[1] << 16) | (buff[0] << 24));
    }

    public ulong ReadUInt64()
    {
      inputStream.Read(buff, 0, 8);
      var value1 = (ulong) (buff[3] | (buff[2] << 8) | (buff[1] << 16) | (buff[0] << 24));
      var value2 = (uint) (buff[7] | (buff[6] << 8) | (buff[5] << 16) | (buff[4] << 24));
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

    #endregion
  }
}