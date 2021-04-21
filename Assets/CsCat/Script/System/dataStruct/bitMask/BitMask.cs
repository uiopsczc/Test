using System;
using System.Text;
using UnityEngine;

namespace CsCat
{
  public class BitMask
  {
    #region property

    #region this[int index]

    public bool this[int index]
    {
      get
      {
        var bytes_index = (uint)(index / byte_width);
        var bit_index = index % byte_width;
        var b = (bytes[bytes_index] >> bit_index) & 1u;
        return Convert.ToBoolean(b);
      }
      set
      {
        var bytes_index = (uint)(index / byte_width);
        var bit_index = index % byte_width;
        var b = 1u << bit_index;
        if (value)
          bytes[bytes_index] = bytes[bytes_index] | b;
        else
          bytes[bytes_index] = bytes[bytes_index] & ~b;
      }
    }

    #endregion

    #endregion

    #region field

    private readonly uint[] bytes;
    private const int byte_width = 32; //uint是32位

    #endregion

    #region ctor

    /// <param name="size">szie需要，会保存在_bytes中，_bytes每个数字是unit，每个unit保存32位，如果该位为0表示被标记，该位为0表示未标记</param>
    public BitMask(int size)
    {
      bytes = new uint[(size - 1) / byte_width + 1];
    }

    public BitMask(BitMask mask)
    {
      bytes = new uint[mask.bytes.Length];
      for (var i = 0; i < mask.bytes.Length; i++) bytes[i] = mask.bytes[i];
    }

    #endregion

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
      if (m1.bytes.Length != m2.bytes.Length) return false;

      // Compare all elements in each BitMask's _byte array
      for (var i = 0; i < m1.bytes.Length; i++)
        if (m1.bytes[i] != m2.bytes[i])
          return false;

      return true;
    }

    #endregion

    #endregion

    #region override method

    public override bool Equals(object obj)
    {
      // If parameter is null return false.
      if (null == obj) return false;

      // If parameter cannot be cast to BitMask return false.
      var m = obj as BitMask;
      if (null == (object)m) return false;

      return _isEqual(this, m);
    }

    public override int GetHashCode()
    {
      unchecked // Overflow is fine, just wrap
      {
        var hash = 17;
        hash = hash * 23 + bytes.GetHashCode();
        return hash;
      }
    }

    #region ToString

    public override string ToString()
    {
      var sb = new StringBuilder(bytes.Length * byte_width);
      for (var i = bytes.Length - 1; i >= 0; i--) sb.Append(Convert.ToString(bytes[i], 2).PadLeft(byte_width, '0'));
      return sb.ToString();
    }

    #endregion

    #endregion

    #region public method

    public BitMask And(BitMask mask)
    {
      for (var i = 0; i < mask.bytes.Length; i++) bytes[i] = bytes[i] & mask.bytes[i];
      return this;
    }

    public BitMask Or(BitMask mask)
    {
      Debug.Assert(bytes.Length == mask.bytes.Length, "BitMasks must be the same size");
      for (var i = 0; i < mask.bytes.Length; i++) bytes[i] = bytes[i] | mask.bytes[i];
      return this;
    }

    public BitMask Xor(BitMask mask)
    {
      Debug.Assert(bytes.Length == mask.bytes.Length, "BitMasks must be the same size");
      for (var i = 0; i < mask.bytes.Length; i++) bytes[i] = bytes[i] ^ mask.bytes[i];
      return this;
    }

    public BitMask Not()
    {
      for (var i = 0; i < bytes.Length; i++) bytes[i] = ~bytes[i];
      return this;
    }

    public void SetAll(bool b)
    {
      var num = b ? uint.MaxValue : 0u;
      for (var i = 0; i < bytes.Length; i++) bytes[i] = num;
    }

    #endregion
  }
}