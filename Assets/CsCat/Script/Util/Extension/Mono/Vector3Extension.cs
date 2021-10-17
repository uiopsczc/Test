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

        public static Vector2 ToVector2(this Vector3 self, string format = StringConst.String_x_y)
        {
            return Vector3Util.ToVector2(self, format);
        }

        /// <summary>
        /// Vector3.ToString只保留小数后2位，看起来会卡，所以需要ToStringDetail
        /// </summary>
        public static string ToStringDetail(this Vector3 self, string separator = StringConst.String_Comma)
        {
            return Vector3Util.ToStringDetail(self, separator);
        }

        /// <summary>
        /// 将逗号改成对应的separator
        /// </summary>
        public static string ToStringReplaceSeparator(this Vector3 self, string separator = StringConst.String_Comma)
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
            return self.Set(StringConst.String_x, args);
        }

        public static Vector3 SetY(this Vector3 self, float args)
        {
            return self.Set(StringConst.String_y, args);
        }

        public static Vector3 SetZ(this Vector3 self, float args)
        {
            return self.Set(StringConst.String_z, args);
        }

        public static Vector3 AddX(this Vector3 self, float args)
        {
            return self.Set(StringConst.String_x, self.x + args);
        }

        public static Vector3 AddY(this Vector3 self, float args)
        {
            return self.Set(StringConst.String_y, self.y + args);
        }

        public static Vector3 AddZ(this Vector3 self, float args)
        {
            return self.Set(StringConst.String_z, self.z + args);
        }


        public static Vector3 Set(this Vector3 self, string format, params float[] args)
        {
            string[] formats = format.Split(CharConst.Char_Vertical);
            float x = self.x;
            float y = self.y;
            float z = self.z;

            int i = 0;
            foreach (string f in formats)
            {
                switch (f.ToLower())
                {
                    case StringConst.String_x:
                        x = args[i];
                        break;
                    case StringConst.String_y:
                        y = args[i];
                        break;
                    case StringConst.String_z:
                        z = args[i];
                        break;
                }
                i++;
            }

            return new Vector3(x, y, z);
        }

        public static Vector3 Abs(this Vector3 self)
        {
            return new Vector3(Math.Abs(self.x), Math.Abs(self.y), Math.Abs(self.z));
        }


        public static bool IsDefault(this Vector3 self, bool isMin = false)
        {
            return isMin ? self == Vector3Const.Default_Min : self == Vector3Const.Default_Max;
        }


        public static Vector3 Clamp(this Vector3 self, Vector3 minPosition, Vector3 maxPosition)
        {
            return new Vector3(Mathf.Clamp(self.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(self.z, minPosition.z, maxPosition.z),
                Mathf.Clamp(self.z, minPosition.z, maxPosition.z));
        }

        public static Vector3Position ToVector3Position(this Vector3 self)
        {
            return new Vector3Position(self);
        }

        //将v Round四舍五入snap_size的倍数的值
        //Rounds value to the closest multiple of snap_size.
        public static Vector3 Snap(this Vector3 self, Vector3 snapSize)
        {
            return Vector3Util.Snap(self, snapSize);
        }

        public static Vector3 Snap2(this Vector3 self, Vector3 snapSize)
        {
            return Vector3Util.Snap2(self, snapSize);
        }

        public static Vector3 ConvertElement(this Vector3 self, Func<float, float> convertElementFunc)
        {
            return Vector3Util.ConvertElement(self, convertElementFunc);
        }


        public static Vector3Int ToVector3Int(this Vector3 self)
        {
            return new Vector3Int((int) self.x, (int) self.y, (int) self.z);
        }


        public static string ToStringOrDefault(this Vector3 self, string toDefaultString = null,
            Vector3 defaultValue = default)
        {
            return ObjectUtil.Equals(self, defaultValue) ? toDefaultString : self.ToString();
        }

        public static bool IsZero(this Vector3 self)
        {
            return self.Equals(Vector3.zero);
        }

        public static bool IsOne(this Vector3 self)
        {
            return self.Equals(Vector3.one);
        }
    }
}