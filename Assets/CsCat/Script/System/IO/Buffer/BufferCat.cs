using System;

namespace CsCat
{
	public abstract class BufferCat
	{
		/// <summary>
		///   网络字节顺序BIG_ENDIAN
		/// </summary>
		private ByteOrder byteOrder = ByteOrder.BigEndian;

		private readonly byte[] singleByteBuf = new byte[1];

		/// <summary>
		///   是否是网络字节顺序
		/// </summary>
		/// <returns></returns>
		private bool NetOrder()
		{
			return byteOrder.Equals(ByteOrder.BigEndian);
		}


		/// <summary>
		///   容量
		/// </summary>
		/// <returns></returns>
		public abstract int Capacity();

		/// <summary>
		///   长度
		/// </summary>
		/// <returns></returns>
		public abstract int Length();

		/// <summary>
		///   删除前面的从firstoffset开始len长度的数据
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public abstract void Remove(int length);

		/// <summary>
		///   删除firstoffset+length开始往前数的len长度的数据
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public abstract void Truncate(int length);

		/// <summary>
		///   整理缓冲区，使缓冲区大小与数据大小相同，整理后只剩一个缓冲区块
		/// </summary>
		/// <returns></returns>
		public abstract void TrimBuffer();

		/// <summary>
		///   删除整个Buffer中的bytes
		/// </summary>
		/// <returns></returns>
		public abstract void Clear();

		/// <summary>
		///   在pos位置设置data的开始位置为offset，长度len的subdata的bytes
		/// </summary>
		/// <param name="pos">指定位置</param>
		/// <param name="data">数据</param>
		/// <param name="offset">data的开始位置</param>
		/// <param name="length">data的长度</param>
		/// <returns></returns>
		public abstract void Set(int pos, byte[] data, int offset, int length);

		/// <summary>
		///   在pos位置设置ByteBuffer
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public abstract void Set(int pos, ByteBufferCat bbf);

		/// <summary>
		///   获取pos位置开始，buf开始位置为offset，长度为len的的subbuf的bytes，内容返回到buf中
		/// </summary>
		/// <param name="pos">指定位置</param>
		/// <param name="buf">数据缓冲区</param>
		/// <param name="offset">buf的开始存放位置</param>
		/// <param name="length">buf的长度</param>
		/// <returns></returns>
		public abstract void Get(int pos, byte[] buf, int offset, int length);

		/// <summary>
		///   获取pos位置开始的ByteBuffer
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public abstract void Get(int pos, ByteBufferCat bbf);

		/// <summary>
		///   在末尾添加data开始位置为offset，长度为len的subdata
		/// </summary>
		/// <param name="data">要加入缓冲区的数据</param>
		/// <param name="offset">data的开始位置</param>
		/// <param name="length">data的长度</param>
		/// <returns></returns>
		public abstract void Append(byte[] data, int offset, int length);

		/// <summary>
		///   在末尾添加ByteBuffer
		/// </summary>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public abstract void Append(ByteBufferCat bbf);

		/// <summary>
		///   弹出buf开始位置offset长度的为len的subbuf
		/// </summary>
		/// <param name="buf"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public abstract void Pop(byte[] buf, int offset, int length);

		/// <summary>
		///   弹出ByteBuffer
		/// </summary>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public abstract void Pop(ByteBufferCat bbf);


		/// <summary>
		///   设置网络字节顺序
		/// </summary>
		/// <param name="byteOrder"></param>
		/// <returns></returns>
		public void Order(ByteOrder byteOrder)
		{
			if (byteOrder == null)
			{
				this.byteOrder = ByteOrder.BigEndian;
				return;
			}

			this.byteOrder = byteOrder;
		}

		/// <summary>
		///   返回字节顺序
		/// </summary>
		/// <returns></returns>
		public ByteOrder Order()
		{
			return byteOrder;
		}

		/// <summary>
		///   在pos位置设置value的byte
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void Set(int pos, int value)
		{
			singleByteBuf[0] = (byte)value;
			Set(pos, singleByteBuf);
		}

		/// <summary>
		///   在pos位置设置short类型的value
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void SetShort(int pos, short value)
		{
			Set(pos, NetOrder() ? value.ToBytes(true) : value.ToBytes());
		}

		/// <summary>
		///   在pos位置设置int类型的value
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void SetInt(int pos, int value)
		{
			Set(pos, NetOrder() ? value.ToBytes(true) : value.ToBytes());
		}

		/// <summary>
		///   在pos位置设置long类型的value
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void SetLong(int pos, long value)
		{
			Set(pos, NetOrder() ? value.ToBytes(true) : value.ToBytes());
		}

		/// <summary>
		///   在pos位置设置float类型的value
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void SetFloat(int pos, float value)
		{
			SetInt(pos, ByteUtil.ToInt(value.ToBytes()));
		}

		/// <summary>
		///   在pos位置设置double类型的value
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void SetDouble(int pos, double value)
		{
			SetLong(pos, ByteUtil.ToLong(value.ToBytes()));
		}

		/// <summary>
		///   在pos位置设置bytes
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public void Set(int pos, byte[] data)
		{
			Set(pos, data, 0, data.Length);
		}

		/// <summary>
		///   获取pos位置的值
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public int Get(int pos)
		{
			Get(pos, singleByteBuf);
			return singleByteBuf[0];
		}

		/// <summary>
		///   获取pos位置的short值
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public short GetShort(int pos)
		{
			var buf = new byte[2];
			Get(pos, buf);
			return ByteUtil.ToShort(buf, 0, NetOrder());
		}

		/// <summary>
		///   获取pos位置的int值
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public int GetInt(int pos)
		{
			var buf = new byte[4];
			Get(pos, buf);
			return ByteUtil.ToInt(buf, 0, NetOrder());
		}

		/// <summary>
		///   获取pos位置的long值
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public long GetLong(int pos)
		{
			var buf = new byte[8];
			Get(pos, buf);
			return ByteUtil.ToLong(buf, 0, NetOrder());
		}

		/// <summary>
		///   获取pos位置的float值
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public float GetFloat(int pos)
		{
			return ByteUtil.ToFloat(GetInt(pos).ToBytes());
		}

		/// <summary>
		///   获取pos位置的double值
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public double GetDouble(int pos)
		{
			return ByteUtil.ToDouble(GetLong(pos).ToBytes());
		}

		/// <summary>
		///   获取全部的bytes
		/// </summary>
		public byte[] GetAll()
		{
			return Get(0, Length());
		}

		/// <summary>
		///   获取pos位置开始，长度为len的bytes
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public byte[] Get(int pos, int length)
		{
			if (length < 0)
				throw new ArgumentException("len must >0");
			if (pos < 0 || pos + length > Length())
				throw new IndexOutOfRangeException();
			var buf = new byte[length];
			Get(pos, buf);
			return buf;
		}

		/// <summary>
		///   获取pos位置开始，buf长度的的bytes，内容返回到buf中
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="buf"></param>
		/// <returns></returns>
		public void Get(int pos, byte[] buf)
		{
			Get(pos, buf, 0, buf.Length);
		}

		/// <summary>
		///   在末尾添加value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public void Append(int value)
		{
			singleByteBuf[0] = (byte)value;
			Append(singleByteBuf);
		}

		/// <summary>
		///   在末尾添加short类型的value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public void AppendShort(short value)
		{
			Append(ByteUtil.ToBytes(value, 0, NetOrder()));
		}

		/// <summary>
		///   在末尾添加int类型的value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public void AppendInt(int value)
		{
			Append(ByteUtil.ToBytes(value, 0, NetOrder()));
		}

		/// <summary>
		///   在末尾添加long类型的value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public void AppendLong(long value)
		{
			Append(ByteUtil.ToBytes(value, 0, NetOrder()));
		}

		/// <summary>
		///   在末尾添加float类型的value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public void AppendFloat(float value)
		{
			AppendInt(ByteUtil.ToInt(value.ToBytes()));
		}

		/// <summary>
		///   在末尾添加double类型的value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public void AppendDouble(double value)
		{
			AppendLong(ByteUtil.ToLong(value.ToBytes()));
		}

		/// <summary>
		///   在末尾添加bytes
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public void Append(byte[] data)
		{
			Append(data, 0, data.Length);
		}

		/// <summary>
		///   弹出一个byte
		/// </summary>
		/// <returns></returns>
		public int Pop()
		{
			Pop(singleByteBuf);
			return singleByteBuf[0];
		}

		/// <summary>
		///   弹出一个short
		/// </summary>
		/// <returns></returns>
		public short PopShort()
		{
			var buf = new byte[2];
			Pop(buf);
			return ByteUtil.ToShort(buf, 0, NetOrder());
		}

		/// <summary>
		///   弹出一个int
		/// </summary>
		/// <returns></returns>
		public int PopInt()
		{
			var buf = new byte[4];
			Pop(buf);
			return ByteUtil.ToInt(buf, 0, NetOrder());
		}

		/// <summary>
		///   弹出一个long
		/// </summary>
		/// <returns></returns>
		public long PopLong()
		{
			var buf = new byte[8];
			Pop(buf);
			return ByteUtil.ToLong(buf, 0, NetOrder());
		}

		/// <summary>
		///   弹出一个float
		/// </summary>
		/// <returns></returns>
		public float PopFloat()
		{
			return ByteUtil.ToFloat(PopInt().ToBytes());
		}

		/// <summary>
		///   弹出一个double
		/// </summary>
		/// <returns></returns>
		public double PopDouble()
		{
			return ByteUtil.ToDouble(PopLong().ToBytes());
		}

		/// <summary>
		///   弹出全部
		/// </summary>
		/// <returns></returns>
		public byte[] PopAll()
		{
			return Pop(Length());
		}

		/// <summary>
		///   弹出len长度的bytes
		/// </summary>
		/// <returns></returns>
		public byte[] Pop(int len)
		{
			if (len < 0)
				throw new ArgumentException("len must >0");
			if (len > Length())
				throw new IndexOutOfRangeException();
			var buf = new byte[len];
			Pop(buf);
			return buf;
		}

		/// <summary>
		///   弹出buf长度的bytes
		/// </summary>
		/// <param name="buf"></param>
		/// <returns></returns>
		public void Pop(byte[] buf)
		{
			Pop(buf, 0, buf.Length);
		}

		/// <summary>
		///   弹出一行
		/// </summary>
		/// <returns></returns>
		public byte[] PopLine()
		{
			return PopLine(true);
		}

		/// <summary>
		///   从缓冲区前面弹出一行数据，返回数据中包含回车换行符
		/// </summary>
		/// <param name="forAll">为true时，不足一行截取全部数据；为false时，不足一行返回null</param>
		/// <returns></returns>
		public byte[] PopLine(bool forAll)
		{
			if (Length() == 0)
			{
				if (forAll)
					return new byte[0];
				return null;
			}

			int i;
			for (i = 0; i < Length(); i++)
				if (Get(i) == 10) //换行符
					break;

			if (i >= Length() && !forAll)
				return null;
			var len = i + 1;
			var buf = new byte[len]; // pop的长度,包括换行符,所以要i+1
			Pop(buf);
			return buf;
		}
	}
}