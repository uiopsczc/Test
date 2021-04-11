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


    public static string ToStringOrDefault(this Matrix4x4 self, string to_default_string = null,
      Matrix4x4 default_value = default(Matrix4x4))
    {
      if (ObjectUtil.Equals(self, default_value))
        return to_default_string;
      return self.ToString();
    }

  }
}
