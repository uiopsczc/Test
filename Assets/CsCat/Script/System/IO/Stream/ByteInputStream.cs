using System.IO;

namespace CsCat
{
  public class ByteInputStream : MemoryStream
  {
    #region ctor

    public ByteInputStream(byte[] buf, int offset, int length)
      : base(buf, offset, length)
    {
    }

    public ByteInputStream(byte[] buf)
      : base(buf)
    {
    }

    #endregion
  }
}