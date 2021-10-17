using System;
using UnityEngine;

namespace CsCat
{
    public static class Vector3IntExtension
    {
        public static string ToStringOrDefault(this Vector3Int self, string toDefaultString = null,
            Vector3Int defaultValue = default)
        {
            return ObjectUtil.Equals(self, defaultValue) ? toDefaultString : self.ToString();
        }

        public static bool IsDefault(this Vector3Int self, bool isMin = false)
        {
            return isMin ? self == Vector3IntConst.Default_Min : self == Vector3IntConst.Default_Max;
        }


        public static Vector3Int Abs(this Vector3Int self)
        {
            return new Vector3Int(Math.Abs(self.x), Math.Abs(self.y), Math.Abs(self.z));
        }


        public static bool IsZero(this Vector3Int self)
        {
            return self.Equals(Vector3Int.zero);
        }

        public static bool IsOne(this Vector3Int self)
        {
            return self.Equals(Vector3Int.one);
        }
    }
}