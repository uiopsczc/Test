using System;
using UnityEngine;

namespace CsCat
{
    public class Vector2Util

    {
        /// <summary>
        /// Vector2.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
        /// </summary>
        public static string ToStringDetail(Vector2 vector2, string separator = StringConst.String_Comma)
        {
            return vector2.x + separator + vector2.y;
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Cross(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.y - v2.x * v1.y;
        }

        /// <summary>
        /// 变成Vector3
        /// </summary>
        /// <param name="v"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector2 v, string format = StringConst.String_x_y_0)
        {
            string[] formats = format.Split(CharConst.Char_Comma);
            float x = Vector3Util.GetFormat(v, formats[0]);
            float y = Vector3Util.GetFormat(v, formats[1]);
            float z = Vector3Util.GetFormat(v, formats[2]);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// 两个值是否相等 小于或等于
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool EqualsEPSILON(Vector2 v1, Vector2 v2, float epsilon = FloatConst.Epsilon)
        {
            return (v1.x.EqualsEPSILON(v2.x, epsilon)) && (v1.y.EqualsEPSILON(v2.y, epsilon));
        }

        //将v Round四舍五入snap_size的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static Vector2 Snap(Vector2 v, Vector2 snapSize)
        {
            return new Vector2(v.x.Snap(snapSize.x), v.y.Snap(snapSize.y));
        }

        public static Vector2 Snap2(Vector2 v, Vector2 snapSize)
        {
            return new Vector2(v.x.Snap2(snapSize.x), v.y.Snap2(snapSize.y));
        }

        public static Vector2 ConvertElement(Vector2 v, Func<float, float> convertElementFunc)
        {
            return new Vector2(convertElementFunc(v.x), convertElementFunc(v.y));
        }
    }
}