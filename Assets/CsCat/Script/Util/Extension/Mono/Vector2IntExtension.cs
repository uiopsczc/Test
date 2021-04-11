

using System;
using UnityEngine;

namespace CsCat
{
  public static class Vector2IntExtension
  {

    public static bool IsDefault(this Vector2Int self, bool is_min = false)
    {
      if (is_min)
        return self == Vector2IntConst.Default_Min;
      else
        return self == Vector2IntConst.Default_Max;
    }

    public static string ToStringOrDefault(this Vector2Int self, string to_default_string = null,
      Vector2Int default_value = default(Vector2Int))
    {
      if (ObjectUtil.Equals(self, default_value))
        return to_default_string;
      return self.ToString();
    }

    public static Vector2Int Abs(this Vector2Int self)
    {
      return new Vector2Int(Math.Abs(self.x), Math.Abs(self.y));
    }


    public static bool IsZero(this Vector2Int self)
    {
      if (self.Equals(Vector2Int.zero))
        return true;
      return false;
    }


    public static bool IsOne(this Vector2Int self)
    {
      if (self.Equals(Vector2Int.one))
        return true;
      return false;
    }
  }
}