using System;
using System.IO;

namespace CsCat
{
	public class ByteOutputStream : MemoryStream
	{
		public byte[] ToByteArray()
		{
			Position = 0;
			var data = new byte[Length];
			var readLength = this.ReadBytes(data);
			var result = new byte[readLength];
			Array.Copy(data, 0, result, 0, result.Length);
			return result;
		}
	}
}