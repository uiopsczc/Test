using System;

namespace CsCat
{
  public class UintUtil
  {
    public static bool IsContains(uint value, uint be_contained_value)
    {
      return (value & be_contained_value) == be_contained_value;
    }
  }
}