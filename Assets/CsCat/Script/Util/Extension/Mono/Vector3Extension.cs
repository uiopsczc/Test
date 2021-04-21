using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class Vector3Extension
  {
    #region ZeroX/Y/Z;

    /// <summary>
    /// X的值为0
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 SetZeroX(this Vector3 self)
    {
      return Vector3Util.ZeroX(self);
    }

    /// <summary>
    /// y的值为0
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 SetZeroY(this Vector3 self)
    {
      return Vector3Util.ZeroY(self);
    }

    /// <summary>
    /// Z的值为0
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 SetZeroZ(this Vector3 self)
    {
      return Vector3Util.ZeroZ(self);
    }

    #endregion

    public static Vector2 XY(this Vector3 self)
    {
      return new Vector2(self.x, self.y);
    }


    public static Vector2 YZ(this Vector3 self)
    {
      return new Vector2(self.y, self.z);
    }


    public static Vector2 XZ(this Vector3 self)
    {
      return new Vector2(self.x, self.z);
    }

    #region 各种To ToXXX

    public static Vector2 ToVector2(this Vector3 self, string format = "x,y")
    {
      return Vector3Util.ToVector2(self, format);
    }

    /// <summary>
    /// Vector3.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
    /// </summary>
    public static string ToStringDetail(this Vector3 self, string separator = ",")
    {
      return Vector3Util.ToStringDetail(self, separator);
    }

    /// <summary>
    /// 将逗号改成对应的separator
    /// </summary>
    public static string ToStringReplaceSeparator(this Vector3 self, string separator = ",")
    {
      return Vector3Util.ToStringReplaceSeparator(self, separator);
    }

    public static Dictionary<string, float> ToDictionary(this Vector3 self) //
    {
      return Vector3Util.ToDictionary(self);
    }

    #endregion

    /// <summary>
    /// v1乘v2
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static Vector3 Multiply(this Vector3 self, Vector3 v2)
    {
      return Vector3Util.Multiply(self, v2);
    }


    #region Other

    public static float GetFormat(Vector3 self, string format)
    {
      return Vector3Util.GetFormat(self, format);
    }

    #endregion

    public static Vector3 Average(this Vector3[] selfs)
    {
      Vector3 total = Vector3.zero;
      foreach (Vector3 v in selfs)
      {
        total += v;
      }

      return selfs.Length == 0 ? Vector3.zero : total / selfs.Length;
    }


    public static Vector3 SetX(this Vector3 self, float args)
    {
      return self.Set("x", args);
    }

    public static Vector3 SetY(this Vector3 self, float args)
    {
      return self.Set("y", args);
    }

    public static Vector3 SetZ(this Vector3 self, float args)
    {
      return self.Set("z", args);
    }

    public static Vector3 AddX(this Vector3 self, float args)
    {
      return self.Set("x", self.x + args);
    }

    public static Vector3 AddY(this Vector3 self, float args)
    {
      return self.Set("y", self.y + args);
    }

    public static Vector3 AddZ(this Vector3 self, float args)
    {
      return self.Set("z", self.z + args);
    }


    public static Vector3 Set(this Vector3 self, string format, params float[] args)
    {
      string[] formats = format.Split('|');
      float x = self.x;
      float y = self.y;
      float z = self.z;

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

        if (f.ToLower().Equals("z"))
        {
          z = args[i];
          i++;
        }
      }

      return new Vector3(x, y, z);
    }

    public static Vector3 Abs(this Vector3 self)
    {
      return new Vector3(Math.Abs(self.x), Math.Abs(self.y), Math.Abs(self.z));
    }


    public static bool IsDefault(this Vector3 self, bool is_min = false)
    {
      if (is_min)
        return self == Vector3Const.Default_Min;
      else
        return self == Vector3Const.Default_Max;
    }


    public static Vector3 Clamp(this Vector3 self, Vector3 min_position, Vector3 max_position)
    {
      return new Vector3(Mathf.Clamp(self.x, min_position.x, max_position.x),
        Mathf.Clamp(self.z, min_position.z, max_position.z), Mathf.Clamp(self.z, min_position.z, max_position.z));
    }

    public static Vector3Position ToVector3Position(this Vector3 self)
    {
      return new Vector3Position(self);
    }

    //将v Round四舍五入snap_size的倍数的值
    //Rounds value to the closest multiple of snap_size.
    public static Vector3 Snap(this Vector3 self, Vector3 snap_size)
    {
      return Vector3Util.Snap(self, snap_size);
    }

    public static Vector3 Snap2(this Vector3 self, Vector3 snap_size)
    {
      return Vector3Util.Snap2(self, snap_size);
    }

    public static Vector3 ConvertElement(this Vector3 self, Func<float, float> convert_element_func)
    {
      return Vector3Util.ConvertElement(self, convert_element_func);
    }


    public static Vector3Int ToVector3Int(this Vector3 self)
    {
      return new Vector3Int((int)self.x, (int)self.y, (int)self.z);
    }


    public static string ToStringOrDefault(this Vector3 self, string to_default_string = null,
      Vector3 default_value = default(Vector3))
    {
      if (ObjectUtil.Equals(self, default_value))
        return to_default_string;
      return self.ToString();
    }

    public static bool IsZero(this Vector3 self)
    {
      if (self.Equals(Vector3.zero))
        return true;
      return false;
    }

    public static bool IsOne(this Vector3 self)
    {
      if (self.Equals(Vector3.one))
        return true;
      return false;
    }
  }
}