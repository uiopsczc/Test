

using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class Vector3Util
  {
    #region ZeroX/Y/Z;

    /// <summary>
    /// X的值为0
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 ZeroX(Vector3 vector3)
    {
      vector3.x = 0;
      return vector3;
    }

    /// <summary>
    /// y的值为0
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 ZeroY(Vector3 vector3)
    {
      vector3.y = 0;
      return vector3;
    }

    /// <summary>
    /// Z的值为0
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 ZeroZ(Vector3 vector3)
    {
      vector3.z = 0;
      return vector3;
    }

    #endregion

    #region 各种To ToXXX

    public static Vector2 ToVector2(Vector3 vector3, string format = "x,y")
    {
      string[] formats = format.Split(',');
      float x = GetFormat(vector3, formats[0]);
      float y = GetFormat(vector3, formats[1]);
      return new Vector2(x, y);
    }

    /// <summary>
    /// Vector3.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
    /// </summary>
    public static string ToStringDetail(Vector3 vector3, string separator = ",")
    {
      return vector3.x + separator + vector3.y + separator + vector3.z;
    }

    /// <summary>
    /// 将逗号改成对应的separator
    /// </summary>
    public static string ToStringReplaceSeparator(Vector3 vector3, string separator = ",")
    {
      return vector3.ToString().Replace(",", separator);
    }

    public static Dictionary<string, float> ToDictionary(Vector3 vector3) //
    {
      Dictionary<string, float> ret = new Dictionary<string, float>();
      ret["x"] = vector3.x;
      ret["y"] = vector3.y;
      ret["z"] = vector3.z;
      return ret;
    }

    #endregion

    /// <summary>
    /// v1乘v2
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public static Vector3 Multiply(Vector3 v1, Vector3 v2)
    {
      return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

    //将v Round四舍五入snap_size的倍数的值
    //Rounds value to the closest multiple of snap_size.
    public static Vector3 Snap(Vector3 v, Vector3 snap_size)
    {
      return new Vector3(v.x.Snap(snap_size.x), v.y.Snap(snap_size.y), v.z.Snap(snap_size.z));
    }

    public static Vector3 Snap2(Vector3 v, Vector3 snap_size)
    {
      return new Vector3(v.x.Snap2(snap_size.x), v.y.Snap2(snap_size.y), v.z.Snap2(snap_size.z));
    }

    public static Vector3 ConvertElement(Vector3 v, Func<float, float> convert_element_func)
    {
      return new Vector3(convert_element_func(v.x), convert_element_func(v.y), convert_element_func(v.z));
    }

    #region Other

    public static float GetFormat(Vector3 vector3, string format)
    {
      format = format.ToLower();
      if (format.Equals("x"))
        return vector3.x;
      else if (format.Equals("y"))
        return vector3.y;
      else if (format.Equals("z"))
        return vector3.z;
      else
      {
        float result;
        bool flag = float.TryParse(format, out result);
        if (flag)
        {
          return result;
        }
        else
        {
          throw new Exception("错误的格式");
        }

      }
    }

    #endregion


  }
}