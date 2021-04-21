

using System;
using UnityEngine;

namespace CsCat
{
  public static class Vector2Extension

  {

    public static string ToStringDetail(this Vector2 self, string separator = ",")
    {
      return Vector2Util.ToStringDetail(self, separator);
    }



    /// <summary>
    /// 叉乘
    /// </summary>
    /// <param name="self"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static float Cross(this Vector2 self, Vector2 v2)
    {
      return Vector2Util.Cross(self, v2);
    }

    /// <summary>
    /// 变成Vector3
    /// </summary>
    /// <param name="self"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 self, string format = "x,y,0")
    {
      return Vector2Util.ToVector3(self, format);
    }

    /// <summary>
    /// 两个值是否相等 小于或等于
    /// </summary>
    /// <param name="self"></param>
    /// <param name="v2"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public static bool EqualsEPSILON(this Vector2 self, Vector2 v2, float epsilon = FloatConst.Epsilon)
    {
      return Vector2Util.EqualsEPSILON(self, v2, epsilon);
    }

    public static Vector2 Average(this Vector2[] selfs)
    {
      Vector2 total = Vector2.zero;
      foreach (Vector2 v in selfs)
      {
        total += v;
      }

      return selfs.Length == 0 ? Vector2.zero : total / selfs.Length;
    }

    public static Vector2 SetX(this Vector2 self, float args)
    {
      return self.Set("x", args);
    }

    public static Vector2 SetY(this Vector2 self, float args)
    {
      return self.Set("y", args);
    }

    public static Vector2 AddX(this Vector2 self, float args)
    {
      return self.Set("x", self.x + args);
    }

    public static Vector2 AddY(this Vector2 self, float args)
    {
      return self.Set("y", self.y + args);
    }

    public static Vector2 Set(this Vector2 self, string format, params float[] args)
    {
      string[] formats = format.Split('|');
      float x = self.x;
      float y = self.y;

      int i = 0;
      foreach (string f in formats)
      {
        if (f.ToLower().Equals("x"))
        {
          x = args[i];
          i++;
        }

        if (f.ToLower().Equals("y"))
        {
          y = args[i];
          i++;
        }

      }

      return new Vector2(x, y);
    }

    public static Vector2 Abs(this Vector2 self)
    {
      return new Vector2(Math.Abs(self.x), Math.Abs(self.y));
    }

    //将v Round四舍五入snap_size的倍数的值
    //Rounds value to the closest multiple of snap_size.
    public static Vector2 Snap(this Vector2 self, Vector2 snap_size)
    {
      return Vector2Util.Snap(self, snap_size);
    }

    public static Vector2 Snap2(this Vector2 self, Vector2 snap_size)
    {
      return Vector2Util.Snap2(self, snap_size);
    }

    public static Vector2 ConvertElement(this Vector2 self, Func<float, float> convert_element_func)
    {
      return Vector2Util.ConvertElement(self, convert_element_func);
    }

    public static Vector2Int ToVector2Int(this Vector2 self)
    {
      return new Vector2Int((int)self.x, (int)self.y);
    }

    public static string ToStringOrDefault(this Vector2 self, string to_default_string = null,
      Vector2 default_value = default(Vector2))
    {
      if (ObjectUtil.Equals(self, default_value))
        return to_default_string;
      return self.ToString();
    }

    public static bool IsDefault(this Vector2 self, bool is_min = false)
    {
      if (is_min)
        return self == Vector2Const.Default_Min;
      else
        return self == Vector2Const.Default_Max;
    }


    public static bool IsZero(this Vector2 self)
    {
      if (self.Equals(Vector2.zero))
        return true;
      return false;
    }


    public static bool IsOne(this Vector2 self)
    {
      if (self.Equals(Vector2.one))
        return true;
      return false;
    }
  }
}