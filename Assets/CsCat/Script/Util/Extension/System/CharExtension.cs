using System;

namespace CsCat
{
  public static class CharExtension
  {
    /// <summary>
    /// 转化为bytes
    /// </summary>
    /// <param name="self"></param>
    /// <param name="is_net_order">是否是网络顺序</param>
    /// <returns></returns>
    public static byte[] ToBytes(this char self, bool is_net_order = false)
    {
      byte[] data = BitConverter.GetBytes(self);
      if (is_net_order)
        Array.Reverse(data);
      return data;
    }

    public static bool IsUpper(this char self)
    {
      if (self > 'A' && self < 'Z')
        return true;
      else
        return false;
    }

    public static bool IsLower(this char self)
    {
      if (self > 'a' && self < 'z')
        return true;
      else
        return false;
    }
  }
}