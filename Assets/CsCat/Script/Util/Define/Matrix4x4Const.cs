using UnityEngine;

namespace CsCat
{
  public static class Matrix4x4Const
  {
    /// <summary>
    ///   xy平面到xz平面的旋转矩阵
    /// </summary>
    public static Matrix4x4 xy_to_xz_matrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

    /// <summary>
    ///   xz平面到xy平面的旋转矩阵
    /// </summary>
    public static Matrix4x4 xz_to_xy_matrix = Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0));

    /// <summary>
    ///   xy平面到yz平面的旋转矩阵
    /// </summary>
    public static Matrix4x4 xy_to_yz_matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0));

    /// <summary>
    ///   yz平面到xy平面的旋转矩阵
    /// </summary>
    public static Matrix4x4 yz_to_xy_matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0));
  }
}