using System;
using UnityEngine;

namespace CsCat
{
  public class MemoryOutputStream : OutputStream
  {
    #region public method

    public void Reset(byte[] inBuf, int length)
    {
      data = inBuf;
      base.length = length;
      pos = 0;
    }

    #endregion

    #region field

    private byte[] data;
    private int inc_len;

    #endregion


    #region ctor

    public MemoryOutputStream(int length, int inc_len)
    {
      this.inc_len = inc_len;
      data = new byte[length];
      base.length = length;
      pos = 0;
    }

    public MemoryOutputStream(byte[] buf, int inc_len = 0)
    {
      this.inc_len = inc_len;
      data = buf;
      length = buf.Length;
      pos = 0;
    }

    #endregion


    #region override method

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

    public override bool Write(byte[] buf, int offset, int length)
    {
      if (pos + length > base.length)
      {
        if (inc_len <= 0)
        {
          inc_len = 128;
          LogCat.LogError("KMemoryOutputStream write error with 0 increase length");
        }

        var num = inc_len;
        var num2 = base.length + num;
        while (pos + length >= num2) num2 += num;
        var dst = new byte[num2];
        Buffer.BlockCopy(data, 0, dst, 0, base.length);
        data = dst;
        base.length = num2;
      }

      Buffer.BlockCopy(buf, offset, data, pos, length);
      pos += length;
      return true;
    }

    #endregion
  }
}