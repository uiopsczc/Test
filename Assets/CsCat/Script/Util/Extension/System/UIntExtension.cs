using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class UIntExtension
  {
    public static bool IsContains(this uint value, uint be_contained_value)
    {
      return UintUtil.IsContains(value, be_contained_value);
    }
    
  }
}