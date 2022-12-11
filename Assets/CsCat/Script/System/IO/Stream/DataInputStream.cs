using System;

namespace CsCat
{
	public class DataInputStream : InputStream
	{
		private readonly InputStream _inputStream;
		private readonly byte[] _buffer = new byte[8];

		public DataInputStream(InputStream inputStream)
		{
			this._inputStream = inputStream;
		}


		public override void Close()
		{
			_inputStream.Close();
		}

		public override int CurrentPosition()
		{
			return _inputStream.CurrentPosition();
		}

		public override bool Eof()
		{
			return _inputStream.Eof();
		}

		public override byte[] GetBuffer()
		{
			return _inputStream.GetBuffer();
		}

		public override int GetLength()
		{
			return _inputStream.GetLength();
		}

		public override void Peek(byte[] buffer, int offset, int length)
		{
			_inputStream.Peek(buffer, offset, length);
		}

		public override void Read(byte[] buffer, int offset, int length)
		{
			_inputStream.Read(buffer, offset, length);
		}

		public override void Reset()
		{
			base.Reset();
			_inputStream?.Reset();
		}

		public override void Seek(int position)
		{
			_inputStream.Seek(position);
		}

		public override void Skip(int length)
		{
			_inputStream.Skip(length);
		}


		public InputStream GetInputStream()
		{
			return _inputStream;
		}

		public uint ReadARGBInt()
		{
			return ReadUInt32();
		}

		public bool ReadBool()
		{
			_inputStream.Read(_buffer, 0, 1);
			return _buffer[0] > 0;
		}

		public sbyte ReadByte()
		{
			_inputStream.Read(_buffer, 0, 1);
			return (sbyte)_buffer[0];
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
			_inputStream.Read(_buffer, 0, 8);
			Array.Reverse(_buffer, 0, 8);
			return BitConverter.ToDouble(_buffer, 0);
		}

		public float ReadFloat()
		{
			_inputStream.Read(_buffer, 0, 4);
			Array.Reverse(_buffer, 0, 4);
			return BitConverter.ToSingle(_buffer, 0);
		}

		public short ReadInt16()
		{
			_inputStream.Read(_buffer, 0, 2);
			return (short)(_buffer[1] | (_buffer[0] << 8));
		}

		public int ReadInt32()
		{
			_inputStream.Read(_buffer, 0, 4);
			return _buffer[3] | (_buffer[2] << 8) | (_buffer[1] << 16) | (_buffer[0] << 24);
		}

		public long ReadInt64()
		{
			_inputStream.Read(_buffer, 0, 8);
			var value1 = (ulong)(_buffer[3] | (_buffer[2] << 8) | (_buffer[1] << 16) | (_buffer[0] << 24));
			var value2 = (uint)(_buffer[7] | (_buffer[6] << 8) | (_buffer[5] << 16) | (_buffer[4] << 24));
			return (long)((value1 << 32) | value2);
		}

		public ushort ReadUInt16()
		{
			_inputStream.Read(_buffer, 0, 2);
			return (ushort)(_buffer[1] | (_buffer[0] << 8));
		}

		public uint ReadUInt32()
		{
			_inputStream.Read(_buffer, 0, 4);
			return (uint)(_buffer[3] | (_buffer[2] << 8) | (_buffer[1] << 16) | (_buffer[0] << 24));
		}

		public ulong ReadUInt64()
		{
			_inputStream.Read(_buffer, 0, 8);
			var value1 = (ulong)(_buffer[3] | (_buffer[2] << 8) | (_buffer[1] << 16) | (_buffer[0] << 24));
			var value2 = (uint)(_buffer[7] | (_buffer[6] << 8) | (_buffer[5] << 16) | (_buffer[4] << 24));
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
			_inputStream.Read(array, 0, num);
			return ByteUtil.ToString(array);
		}
	}
}