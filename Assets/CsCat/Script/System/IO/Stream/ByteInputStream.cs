using System.IO;

namespace CsCat
{
    public class ByteInputStream : MemoryStream
    {
        public ByteInputStream(byte[] buffer, int offset, int length)
            : base(buffer, offset, length)
        {
        }

        public ByteInputStream(byte[] buffer)
            : base(buffer)
        {
        }
    }
}