using System;
using UnityEngine;

namespace CsCat
{
  public static class Vector3IntExtension
  {
    public static string ToStringOrDefault(this Vector3Int self, string to_default_string = null,
      Vector3Int default_value = default(Vector3Int))
    {
      if (ObjectUtil.Equals(self, default_value))
        return to_default_string;
      return self.ToString();
    }

    public static bool IsDefault(this Vector3Int self, bool is_min = false)
    {
      if (is_min)
        return self == Vector3IntConst.Default_Min;
      else
        return self == Vector3IntConst.Default_Max;
    }


    public static Vector3Int Abs(this Vector3Int self)
    {
      return new Vector3Int(Math.Abs(self.x), Math.Abs(self.y), Math.Abs(self.z));
    }


    public static bool IsZero(this Vector3Int self)
    {
      if (self.Equals(Vector3Int.zero))
        return true;
      return false;
    }

    public static bool IsOne(this Vector3Int self)
    {
      if (self.Equals(Vector3Int.one))
        return true;
      return false;
    }
  }
}