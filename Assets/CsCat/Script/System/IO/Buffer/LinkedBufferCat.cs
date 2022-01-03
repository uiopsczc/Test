using System;
using System.Collections.Generic;

namespace CsCat
{
	public class LinkedBufferCat : BufferCat
	{
		private readonly List<byte[]> bfs = new List<byte[]>();
		private readonly int minBufferSize = 1024;
		private int capacity;
		private int firstOffset;
		private int length;


		/// <summary>
		///   ctor
		/// </summary>
		/// <param name="minBufferUnitSize">最小缓冲区块的大小，默认为1024</param>
		/// <returns></returns>
		public LinkedBufferCat(int minBufferUnitSize)
		{
			if (minBufferUnitSize <= 0)
				throw new ArgumentException();
			minBufferSize = minBufferUnitSize;
		}

		public LinkedBufferCat()
		{
		}

		/// <summary>
		///   获得当前可用缓冲区大小
		/// </summary>
		/// <returns></returns>
		public sealed override int Capacity()
		{
			return capacity - firstOffset;
		}

		/// <summary>
		///   获得当前数据长度
		/// </summary>
		/// <returns></returns>
		public sealed override int Length()
		{
			return length;
		}

		/// <summary>
		///   清空
		/// </summary>
		/// <returns></returns>
		public sealed override void Clear()
		{
			length = 0;
			firstOffset = 0;
			CheckFreeBuffer();
		}

		/// <summary>
		///   删除前面的从firstoffset开始len长度的数据
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public sealed override void Remove(int length)
		{
			if (length < 0)
				throw new ArgumentException();
			if (length > this.length)
				throw new IndexOutOfRangeException();
			firstOffset += length;
			this.length -= length;
			CheckFreeBuffer();
		}

		/// <summary>
		///   删除firstoffset+length开始往前数的len长度的数据
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public sealed override void Truncate(int length)
		{
			if (length < 0)
				throw new ArgumentException();
			if (length > this.length)
				throw new IndexOutOfRangeException();
			this.length -= length;
			CheckFreeBuffer();
		}

		/// <summary>
		///   整理缓冲区，使缓冲区大小与数据大小相同，整理后只剩一个缓冲区块
		/// </summary>
		/// <returns></returns>
		public sealed override void TrimBuffer()
		{
			var all = GetAll();
			bfs.Clear();
			bfs.Add(all);
			capacity = all.Length;
		}

		/// <summary>
		///   将data的从offset开始len长度的subdata添加到末尾
		/// </summary>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public override void Append(byte[] data, int offset, int length)
		{
			if (length < 0)
				throw new ArgumentException();
			if (offset < 0 || offset + length > data.Length)
				throw new IndexOutOfRangeException();


			var need = length - (Capacity() - this.length);
			if (need > 0)
				ExtendBuffer(need);
			Set(this.length, data, offset, length);
			this.length += length;
		}

		/// <summary>
		///   将ByteBuffer添加到末尾
		/// </summary>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public override void Append(ByteBufferCat bbf)
		{
			var len = bbf.Remaining();
			var need = len - (Capacity() - length);
			if (need > 0)
				ExtendBuffer(need);
			Set(length, bbf);
			length += len;
		}

		/// <summary>
		///   获取len长度的bytes,放到buf的offset位置中
		/// </summary>
		/// <param name="buf"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public override void Pop(byte[] buf, int offset, int length)
		{
			Get(0, buf, offset, length);
			firstOffset += length;
			this.length -= length;
			CheckFreeBuffer();
		}

		/// <summary>
		///   获取ByteBuffer
		/// </summary>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public override void Pop(ByteBufferCat bbf)
		{
			var len = bbf.Remaining();
			Get(0, bbf);
			firstOffset += len;
			length -= len;

			CheckFreeBuffer();
		}

		/// <summary>
		///   将data的从offset开始len长度的subdata添加到pos位置
		/// </summary>
		/// <param name="pos">指定位置</param>
		/// <param name="data">数据</param>
		/// <param name="offset">data的开始位置</param>
		/// <param name="length">data的长度</param>
		/// <returns></returns>
		public override void Set(int pos, byte[] data, int offset, int length)
		{
			if (length < 0)
				throw new ArgumentException();
			if (pos < 0 || offset < 0 || offset + length > data.Length || pos + length > Capacity())
				throw new IndexOutOfRangeException();
			if (length == 0)
				return;
			if (bfs.Count == 0)
				throw new IndexOutOfRangeException();

			var first = bfs.First();
			if (firstOffset + pos >= first.Length)
			{
				pos -= first.Length - firstOffset;
			}
			else
			{
				var copyPos = firstOffset + pos; // listi中要copy的位置
				var copyLen = Math.Min(length, first.Length - (firstOffset + pos)); // listi中要copy的长度
				Array.Copy(data, offset, first, copyPos, copyLen);
				offset += copyLen;
				length -= copyLen;
				pos = 0;

				if (length <= 0)
					return;
			}

			for (var i = 1; i < bfs.Count; i++)
			{
				var listi = bfs[i];
				if (pos >= listi.Length)
				{
					pos -= listi.Length;
				}
				else
				{
					var copyPos = pos; // listi中要copy的位置
					var copyLen = Math.Min(length, listi.Length - pos); // listi中要copy的长度
					Array.Copy(data, offset, listi, copyPos, copyLen);
					offset += copyLen;
					length -= copyLen;
					pos = 0;

					if (length <= 0)
						break;
				}
			}
		}

		/// <summary>
		///   在pos位置设置bbf
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public override void Set(int pos, ByteBufferCat bbf)
		{
			if (pos < 0 || pos + bbf.Remaining() > Capacity())
				throw new IndexOutOfRangeException();
			if (bbf.Remaining() == 0)
				return;
			if (bfs.Count == 0)
				throw new IndexOutOfRangeException();

			var first = bfs.First();
			if (firstOffset + pos > first.Length)
			{
				pos -= first.Length - firstOffset;
			}
			else
			{
				var getPos = firstOffset + pos; // bbf从get_pos放数据到listi中
				var getLen = Math.Min(bbf.Remaining(), first.Length - (firstOffset + pos)); // bbf放get_len长度的数据到listi中
				bbf.Get(first, getPos, getLen);
				pos = 0;
				if (!bbf.IsHasRemaining())
					return;
			}

			for (var i = 1; i < bfs.Count; i++)
			{
				var listi = bfs[i];
				if (pos >= listi.Length)
					pos -= listi.Length;
				else
				{
					var getPos = pos; // bbf从get_pos放数据到listi中
					var getLen = Math.Min(bbf.Remaining(), listi.Length - pos); // bbf放get_len长度的数据到listi中
					bbf.Get(listi, getPos, getLen);
					pos = 0;
					if (!bbf.IsHasRemaining())
						break;
				}
			}
		}

		/// <summary>
		///   获取len长度的bytes,放到data的offset位置中
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public override void Get(int pos, byte[] data, int offset, int length)
		{
			if (length < 0)
				throw new ArgumentException();
			if (pos < 0 || offset < 0 || offset + length > data.Length || pos + length > this.length)
				throw new IndexOutOfRangeException();
			if (length == 0)
				return;
			if (bfs.Count == 0)
				throw new IndexOutOfRangeException();

			var first = bfs.First();
			if (firstOffset + pos > first.Length)
				pos -= first.Length - firstOffset;
			else
			{
				var copyPos = offset; // data从copy_pos开始复制
				var copyLen = Math.Min(length, first.Length - (firstOffset + pos)); // data复制copy_len长度
				Array.Copy(first, firstOffset + pos, data, copyPos, copyLen);
				offset += copyLen;
				length -= copyLen;
				pos = 0;

				if (length <= 0)
					return;
			}

			for (var i = 1; i < bfs.Count; i++)
			{
				var listi = bfs[i];
				if (pos >= listi.Length)
					pos -= listi.Length;
				else
				{
					var copyPos = offset; // data从copy_pos开始复制
					var copyLen = Math.Min(length, listi.Length - pos); // data复制copy_len长度
					Array.Copy(listi, pos, data, copyPos, copyLen);
					offset += copyLen;
					length -= copyLen;
					pos = 0;

					if (length <= 0)
						break;
				}
			}
		}

		/// <summary>
		///   在pos位置获取ByteBuffer
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="bbf"></param>
		/// <returns></returns>
		public override void Get(int pos, ByteBufferCat bbf)
		{
			if (pos < 0 || pos + bbf.Remaining() > length)
				throw new IndexOutOfRangeException();
			if (bbf.Remaining() == 0)
				return;
			if (bfs.Count == 0)
				throw new IndexOutOfRangeException();

			var first = bfs.First();
			if (firstOffset + pos >= first.Length)
				pos -= first.Length - firstOffset;
			else
			{
				var putPos = firstOffset + pos; // listi从get_pos放数据到bbf中
				var putLen = Math.Min(bbf.Remaining(), first.Length - (firstOffset + pos));
				// listi从get_pos放get_len长度的数据到bbf中
				bbf.Put(first, putPos, putLen);
				pos = 0;

				if (!bbf.IsHasRemaining())
					return;
			}

			for (var i = 1; i < bfs.Count; i++)
			{
				var listi = bfs[i];
				if (pos >= listi.Length)
					pos -= listi.Length;
				else
				{
					var putPos = pos; // listi从get_pos放数据到bbf中
					var putLen = Math.Min(bbf.Remaining(), listi.Length - pos); // listi从get_pos放get_len长度的数据到bbf中
					bbf.Put(listi, putPos, putLen);
					pos = 0;
					if (!bbf.IsHasRemaining())
						break;
				}
			}
		}


		/// <summary>
		///   清除多余的空闲空间
		/// </summary>
		/// <returns></returns>
		private void CheckFreeBuffer()
		{
			// 清空firstoffset之前的在bfs中的element;但不清firstoffset当前所在的element之前的元素
			byte[] data;
			if (bfs.Count > 0)
				while (firstOffset >= (data = bfs.First()).Length)
				{
					bfs.RemoveFirst();
					firstOffset -= data.Length;
					capacity -= data.Length;
					if (bfs.Count == 0)
						break;
				}

			// 清除最后面的多余的空间
			if (bfs.Count > 0)
				// 还有的空间=capacity-(offset+lenghth)
				for (var i = Capacity() - length; i > (data = bfs.Last()).Length;)
				{
					bfs.RemoveLast();
					i -= data.Length;
					capacity -= data.Length;
					if (bfs.Count == 0)
						break;
				}
		}

		/// <summary>
		///   增加len的容量
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		private void ExtendBuffer(int len)
		{
			if (len > 0)
			{
				len = Math.Max(len * 2, minBufferSize); // 增加传进来的长度的2倍,如果不够minBufferSize,则增加minBufferSize
				var extendedBytes = new byte[len];
				bfs.Add(extendedBytes);
				capacity += len;
			}
		}
	}
}