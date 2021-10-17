using UnityEngine;

namespace CsCat
{
    public static class Matrix4x4Extensions
    {
        /// <summary>
        /// 通过矩阵获取Rotation
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Quaternion GetRotation(this Matrix4x4 self)
        {
            return Matrix4x4Util.GetRotation(self);
        }

        /// <summary>
        /// 通过矩阵获取Position
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector3 GetPosition(this Matrix4x4 self)
        {
            return Matrix4x4Util.GetPosition(self);
        }

        /// <summary>
        /// 通过矩阵获取Scale
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector3 GetScale(this Matrix4x4 self)
        {
            return Matrix4x4Util.GetScale(self);
        }


        public static string ToStringOrDefault(this Matrix4x4 self, string toDefaultString = null,
            Matrix4x4 defaultValue = default)
        {
            return ObjectUtil.Equals(self, defaultValue) ? toDefaultString : self.ToString();
        }
    }
}