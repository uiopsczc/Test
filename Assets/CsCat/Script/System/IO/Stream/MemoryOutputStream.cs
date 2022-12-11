using System;
using UnityEngine;

namespace CsCat
{
	public class MemoryOutputStream : OutputStream
	{
		private byte[] _data;
		private int _incLen;

		public void Reset(byte[] inBuffer, int length)
		{
			_data = inBuffer;
			this._length = length;
			_pos = 0;
		}


		public MemoryOutputStream(int length, int incLen)
		{
			this._incLen = incLen;
			_data = new byte[length];
			this._length = length;
			_pos = 0;
		}

		public MemoryOutputStream(byte[] buf, int incLen = 0)
		{
			this._incLen = incLen;
			_data = buf;
			_length = buf.Length;
			_pos = 0;
		}


		public override void Flush()
		{
		}

		public override byte[] GetBuffer()
		{
			return _data;
		}

		public override void Seek(int length)
		{
			Debug.Assert(length <= base._length, "out of output stream length");
			_pos = length;
		}

		public override void Skip(int length)
		{
			Debug.Assert(_pos + length <= base._length, "out of output stream length");
			_pos += length;
		}

		public override bool Write(byte[] buffer, int offset, int length)
		{
			if (_pos + length > base._length)
			{
				if (_incLen <= 0)
				{
					_incLen = 128;
					LogCat.LogError("KMemoryOutputStream write error with 0 increase length");
				}

				var num = _incLen;
				var num2 = this._length + num;
				while (_pos + length >= num2) num2 += num;
				var dst = new byte[num2];
				Buffer.BlockCopy(_data, 0, dst, 0, base._length);
				_data = dst;
				base._length = num2;
			}

			Buffer.BlockCopy(buffer, offset, _data, _pos, length);
			_pos += length;
			return true;
		}
	}
}