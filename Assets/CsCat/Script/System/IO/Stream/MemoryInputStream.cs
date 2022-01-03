using System;
using System.IO;

namespace CsCat
{
	public class MemoryInputStream : InputStream
	{
		private byte[] _buffer;


		public MemoryInputStream(byte[] inBuffer)
		{
			_buffer = inBuffer;
			length = inBuffer.Length;
		}


		public void Reset(byte[] inBuffer, int length)
		{
			_buffer = inBuffer;
			base.length = length;
			pos = 0;
		}


		private void ClearBuf(byte[] buffer, int offset, int length)
		{
			if (buffer == null) return;
			var num = buffer.Length;
			if (offset < 0) offset = 0;
			if (offset >= num) offset = num - 1;
			if (length < 0) length = 0;
			if (offset + length >= num) length = num - offset - 1;
			for (var i = 0; i < length; i++) buffer[offset + i] = 0;
		}


		public override byte[] GetBuffer()
		{
			return _buffer;
		}

		public override void Peek(byte[] buffer, int offset, int length)
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
					buffer.Length
				);
				var text2 = " --->bytes[";
				for (var i = 0; i < _buffer.Length; i++)
				{
					text2 += StringConst.String_Comma;
					text2 += _buffer[i];
				}

				text += text2;
				text += StringConst.String_RightSquareBrackets;
				ClearBuf(buffer, offset, length);
				throw new IOException(text);
			}

			Buffer.BlockCopy(_buffer, pos, buffer, offset, length);
		}

		public override void Read(byte[] buffer, int offset, int length)
		{
			Peek(buffer, offset, length);
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
	}
}