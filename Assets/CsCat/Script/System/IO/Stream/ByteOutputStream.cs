using System;
using System.IO;

namespace CsCat
{
  public class ByteOutputStream : MemoryStream
  {
    #region public method

    public byte[] ToByteArray()
    {
      Position = 0;
      var data = new byte[Length];
      var read_len = this.ReadBytes(data);
      var result = new byte[read_len];
      Array.Copy(data, 0, result, 0, result.Length);
      return result;
    }

    #endregion
  }
}