using System;
using System.IO;

namespace CsCat
{
  public class MemoryInputStream : InputStream
  {
    #region field

    private byte[] data;

    #endregion

    #region ctor

    public MemoryInputStream(byte[] inBuf)
    {
      data = inBuf;
      length = inBuf.Length;
    }

    #endregion

    #region public method

    public void Reset(byte[] inBuf, int length)
    {
      data = inBuf;
      base.length = length;
      pos = 0;
    }

    #endregion

    #region private method

    private void ClearBuf(byte[] buf, int offset, int length)
    {
      if (buf == null) return;
      var num = buf.Length;
      if (offset < 0) offset = 0;
      if (offset >= num) offset = num - 1;
      if (length < 0) length = 0;
      if (offset + length >= num) length = num - offset - 1;
      for (var i = 0; i < length; i++) buf[offset + i] = 0;
    }

    #endregion

    #region override method

    public override byte[] GetBuffer()
    {
      return data;
    }

    public override void Peek(byte[] buf, int offset, int length)
    {
      if (pos + length > base.length)
      {
        var text = string.Concat(
          "Peek out of stream,want -> mPos:",
          pos,
          ",length: ",
          length,
          ",offset: ",
          offset,
          ", but mLength:",
          base.length,
          ",buf.Length: ",
          buf.Length
        );
        var text2 = " --->bytes[";
        for (var i = 0; i < data.Length; i++)
        {
          text2 += ",";
          text2 += data[i];
        }

        text += text2;
        text += "]";
        ClearBuf(buf, offset, length);
        throw new IOException(text);
      }

      Buffer.BlockCopy(data, pos, buf, offset, length);
    }

    public override void Read(byte[] buf, int offset, int length)
    {
      Peek(buf, offset, length);
      pos += length;
    }

    public override void Seek(int length)
    {
      if (pos + length > base.length)
      {
        LogCat.LogWarningFormat(string.Concat(
          "Seek out of stream, wanted:",
          pos + length,
          ", but:",
          base.length
        ));
        pos = base.length;
        return;
      }

      pos = length;
    }

    public override void Skip(int length)
    {
      if (pos + length > base.length)
      {
        LogCat.LogWarningFormat(string.Concat(
          "Skip out of stream, wanted:",
          pos + length,
          ", but:",
          base.length
        ));
        pos = base.length;
        return;
      }

      pos += length;
    }

    #endregion
  }
}