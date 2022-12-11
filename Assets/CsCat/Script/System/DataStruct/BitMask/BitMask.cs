using System;
using System.Text;
using UnityEngine;

namespace CsCat
{
	public class BitMask
	{
		private readonly uint[] _bytes;
		private const int _byteWidth = 32; //uint是32位


		public bool this[int index]
		{
			get
			{
				var bytesIndex = (uint)(index / _byteWidth);
				var bitIndex = index % _byteWidth;
				var b = (_bytes[bytesIndex] >> bitIndex) & 1u;
				return Convert.ToBoolean(b);
			}
			set
			{
				var bytesIndex = (uint)(index / _byteWidth);
				var bitIndex = index % _byteWidth;
				var b = 1u << bitIndex;
				if (value)
					_bytes[bytesIndex] = _bytes[bytesIndex] | b;
				else
					_bytes[bytesIndex] = _bytes[bytesIndex] & ~b;
			}
		}


		/// <param name="size">szie需要，会保存在_bytes中，_bytes每个数字是unit，每个unit保存32位，如果该位为0表示被标记，该位为0表示未标记</param>
		public BitMask(int size)
		{
			_bytes = new uint[(size - 1) / _byteWidth + 1];
		}

		public BitMask(BitMask mask)
		{
			_bytes = new uint[mask._bytes.Length];
			for (var i = 0; i < mask._bytes.Length; i++) _bytes[i] = mask._bytes[i];
		}


		#region static method

		public static bool operator ==(BitMask m1, BitMask m2)
		{
			return _isEqual(m1, m2);
		}

		public static bool operator !=(BitMask m1, BitMask m2)
		{
			return !_isEqual(m1, m2);
		}

		#region Equals

		private static bool _isEqual(BitMask m1, BitMask m2)
		{
			// BitMasks need to be the same size
			if (m1._bytes.Length != m2._bytes.Length) return false;

			// Compare all elements in each BitMask's _byte array
			for (var i = 0; i < m1._bytes.Length; i++)
				if (m1._bytes[i] != m2._bytes[i])
					return false;

			return true;
		}

		#endregion

		#endregion

		#region override method

		public override bool Equals(object obj)
		{
			// If parameter is null return false.

			// If parameter cannot be cast to BitMask return false.
			var m = obj as BitMask;
			if (m == null)
				return false;

			return _isEqual(this, m);
		}

		public override int GetHashCode()
		{
			unchecked // Overflow is fine, just wrap
			{
				var hash = 17;
				hash = hash * 23 + _bytes.GetHashCode();
				return hash;
			}
		}

		#region ToString

		public override string ToString()
		{
			var sb = new StringBuilder(_bytes.Length * _byteWidth);
			for (var i = _bytes.Length - 1; i >= 0; i--)
				sb.Append(Convert.ToString(_bytes[i], 2).PadLeft(_byteWidth, '0'));
			return sb.ToString();
		}

		#endregion

		#endregion

		#region public method

		public BitMask And(BitMask mask)
		{
			for (var i = 0; i < mask._bytes.Length; i++) _bytes[i] = _bytes[i] & mask._bytes[i];
			return this;
		}

		public BitMask Or(BitMask mask)
		{
			Debug.Assert(_bytes.Length == mask._bytes.Length, "BitMasks must be the same size");
			for (var i = 0; i < mask._bytes.Length; i++) _bytes[i] = _bytes[i] | mask._bytes[i];
			return this;
		}

		public BitMask Xor(BitMask mask)
		{
			Debug.Assert(_bytes.Length == mask._bytes.Length, "BitMasks must be the same size");
			for (var i = 0; i < mask._bytes.Length; i++) _bytes[i] = _bytes[i] ^ mask._bytes[i];
			return this;
		}

		public BitMask Not()
		{
			for (var i = 0; i < _bytes.Length; i++) _bytes[i] = ~_bytes[i];
			return this;
		}

		public void SetAll(bool b)
		{
			var num = b ? uint.MaxValue : 0u;
			for (var i = 0; i < _bytes.Length; i++) _bytes[i] = num;
		}

		#endregion
	}
}