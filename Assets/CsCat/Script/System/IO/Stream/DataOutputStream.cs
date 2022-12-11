using System;

namespace CsCat
{
	public class DataOutputStream : OutputStream
	{
		private static readonly byte[] _buffer = new byte[8];
		private readonly OutputStream _outputStream;

		public DataOutputStream(OutputStream outputStream)
		{
			this._outputStream = outputStream;
		}


		public override void Close()
		{
			_outputStream.Close();
		}

		public override int CurrentPosition()
		{
			return _outputStream.CurrentPosition();
		}

		public override void Flush()
		{
			_outputStream.Flush();
		}

		public override byte[] GetBuffer()
		{
			return _outputStream.GetBuffer();
		}

		public override void Reset()
		{
			base.Reset();
			_outputStream?.Reset();
		}

		public override void Seek(int position)
		{
			_outputStream.Seek(position);
		}

		public override void Skip(int length)
		{
			_outputStream.Skip(length);
		}

		public override bool Write(byte[] buffer, int offset, int length)
		{
			return _outputStream.Write(buffer, offset, length);
		}


		public OutputStream GetOutputStream()
		{
			return _outputStream;
		}

		public void WriteBool(bool buf)
		{
			_buffer[0] = (byte)(buf ? 1 : 0);
			_outputStream.Write(_buffer, 0, 1);
		}

		public void WriteByte(byte buf)
		{
			_buffer[0] = buf;
			_outputStream.Write(_buffer, 0, 1);
		}

		public void WriteBytes(byte[] buf)
		{
			WriteInt32(buf.Length);
			_outputStream.Write(buf, 0, buf.Length);
		}

		public void WriteDouble(double value)
		{
			var bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes, 0, bytes.Length);
			_outputStream.Write(bytes, 0, bytes.Length);
		}

		public void WriteFloat(float value)
		{
			var bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes, 0, bytes.Length);
			_outputStream.Write(bytes, 0, bytes.Length);
		}

		public void WriteInt16(short buf)
		{
			_buffer[1] = (byte)buf;
			_buffer[0] = (byte)(buf >> 8);
			_outputStream.Write(_buffer, 0, 2);
		}

		public void WriteInt32(int value)
		{
			_buffer[3] = (byte)value;
			_buffer[2] = (byte)(value >> 8);
			_buffer[1] = (byte)(value >> 16);
			_buffer[0] = (byte)(value >> 24);
			_outputStream.Write(_buffer, 0, 4);
		}

		public void WriteInt64(long value)
		{
			_buffer[7] = (byte)value;
			_buffer[6] = (byte)(value >> 8);
			_buffer[5] = (byte)(value >> 16);
			_buffer[4] = (byte)(value >> 24);
			_buffer[3] = (byte)(value >> 32);
			_buffer[2] = (byte)(value >> 40);
			_buffer[1] = (byte)(value >> 48);
			_buffer[0] = (byte)(value >> 56);
			_outputStream.Write(_buffer, 0, 8);
		}

		public void WriteUInt16(ushort buf)
		{
			_buffer[1] = (byte)buf;
			_buffer[0] = (byte)(buf >> 8);
			_outputStream.Write(_buffer, 0, 2);
		}

		public void WriteUInt32(uint value)
		{
			_buffer[3] = (byte)value;
			_buffer[2] = (byte)(value >> 8);
			_buffer[1] = (byte)(value >> 16);
			_buffer[0] = (byte)(value >> 24);
			_outputStream.Write(_buffer, 0, 4);
		}

		public void WriteUInt64(ulong value)
		{
			_buffer[7] = (byte)value;
			_buffer[6] = (byte)(value >> 8);
			_buffer[5] = (byte)(value >> 16);
			_buffer[4] = (byte)(value >> 24);
			_buffer[3] = (byte)(value >> 32);
			_buffer[2] = (byte)(value >> 40);
			_buffer[1] = (byte)(value >> 48);
			_buffer[0] = (byte)(value >> 56);
			_outputStream.Write(_buffer, 0, 8);
		}

		public void WriteUTF8String(string buf)
		{
			var array = buf.ToBytes();
			if (array.Length != 0)
			{
				WriteInt32(array.Length);
				_outputStream.Write(array, 0, array.Length);
				return;
			}

			WriteInt32(0);
		}
	}
}