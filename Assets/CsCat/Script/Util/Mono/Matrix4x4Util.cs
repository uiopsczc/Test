using UnityEngine;

namespace CsCat
{
  /// <summary>
  /// http://chengdudahonghua.lofter.com/post/117105_3b843c
  /// </summary>
  public static class Matrix4x4Util
  {
    /// <summary>
    /// 通过矩阵获取Rotation
    /// </summary>
    /// <param name="matrix4x4"></param>
    /// <returns></returns>
    public static Quaternion GetRotation(Matrix4x4 matrix4x4)
    {
      float qw = Mathf.Sqrt(1f + matrix4x4.m00 + matrix4x4.m11 + matrix4x4.m22) / 2;
      float w = 4 * qw;
      float qx = (matrix4x4.m21 - matrix4x4.m12) / w;
      float qy = (matrix4x4.m02 - matrix4x4.m20) / w;
      float qz = (matrix4x4.m10 - matrix4x4.m01) / w;
      return new Quaternion(qx, qy, qz, qw);
    }

    /// <summary>
    /// 通过矩阵获取Position
    /// </summary>
    /// <param name="matrix4x4"></param>
    /// <returns></returns>
    public static Vector3 GetPosition(Matrix4x4 matrix4x4)
    {
      float x = matrix4x4.m03;
      float y = matrix4x4.m13;
      float z = matrix4x4.m23;
      return new Vector3(x, y, z);
    }

    /// <summary>
    /// 通过矩阵获取Scale
    /// </summary>
    /// <param name="matrix4x4"></param>
    /// <returns></returns>
    public static Vector3 GetScale(Matrix4x4 matrix4x4)
    {
      float x = Mathf.Sqrt(
        matrix4x4.m00 * matrix4x4.m00 + matrix4x4.m01 * matrix4x4.m01 + matrix4x4.m02 * matrix4x4.m02);
      float y = Mathf.Sqrt(
        matrix4x4.m10 * matrix4x4.m10 + matrix4x4.m11 * matrix4x4.m11 + matrix4x4.m12 * matrix4x4.m12);
      float z = Mathf.Sqrt(
        matrix4x4.m20 * matrix4x4.m20 + matrix4x4.m21 * matrix4x4.m21 + matrix4x4.m22 * matrix4x4.m22);
      return new Vector3(x, y, z);
    }









  }




}
