using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    public class Vector3Util
    {
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


        public static Vector2 ToVector2(Vector3 vector3, string format = StringConst.String_x_y)
        {
            string[] formats = format.Split(CharConst.Char_Comma);
            float x = GetFormat(vector3, formats[0]);
            float y = GetFormat(vector3, formats[1]);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Vector3.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
        /// </summary>
        public static string ToStringDetail(Vector3 vector3, string separator = StringConst.String_Comma)
        {
            return vector3.x + separator + vector3.y + separator + vector3.z;
        }

        /// <summary>
        /// 将逗号改成对应的separator
        /// </summary>
        public static string ToStringReplaceSeparator(Vector3 vector3, string separator = StringConst.String_Comma)
        {
            return vector3.ToString().Replace(StringConst.String_Comma, separator);
        }

        public static Dictionary<string, float> ToDictionary(Vector3 vector3) //
        {
            Dictionary<string, float> ret = new Dictionary<string, float>
            {
                [StringConst.String_x] = vector3.x,
                [StringConst.String_y] = vector3.y,
                [StringConst.String_z] = vector3.z
            };
            return ret;
        }


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
        public static Vector3 Snap(Vector3 v, Vector3 snapSize)
        {
            return new Vector3(v.x.Snap(snapSize.x), v.y.Snap(snapSize.y), v.z.Snap(snapSize.z));
        }

        public static Vector3 Snap2(Vector3 v, Vector3 snapSize)
        {
            return new Vector3(v.x.Snap2(snapSize.x), v.y.Snap2(snapSize.y), v.z.Snap2(snapSize.z));
        }

        public static Vector3 ConvertElement(Vector3 v, Func<float, float> convertElementFunc)
        {
            return new Vector3(convertElementFunc(v.x), convertElementFunc(v.y), convertElementFunc(v.z));
        }


        public static float GetFormat(Vector3 vector3, string format)
        {
            format = format.ToLower();
            if (format.Equals(StringConst.String_x))
                return vector3.x;
            if (format.Equals(StringConst.String_y))
                return vector3.y;
            if (format.Equals(StringConst.String_z))
                return vector3.z;
            bool flag = float.TryParse(format, out var result);
            return flag ? result : throw new Exception("错误的格式");
        }
    }
}