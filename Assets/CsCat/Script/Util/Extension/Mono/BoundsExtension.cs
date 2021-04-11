using UnityEngine;

namespace CsCat
{
  public static class BoundsExtension
  {

    /// <summary>
    /// 世界坐标（前面 左上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 FrontTopLeft(this Bounds self)
    {
      return BoundsUtil.FrontTopLeft(self);
    }

    /// <summary>
    /// 世界坐标（前面  右上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 FrontTopRight(this Bounds self)
    {
      return BoundsUtil.FrontTopRight(self);
    }

    /// <summary>
    /// 世界坐标（前面  左下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 FrontBottomLeft(this Bounds self)
    {
      return BoundsUtil.FrontBottomLeft(self);
    }

    /// <summary>
    /// 世界坐标（前面  左下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 FrontBottomRight(this Bounds self)
    {
      return BoundsUtil.FrontBottomRight(self);
    }

    /// <summary>
    /// 世界坐标（后面 左上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 BackTopLeft(this Bounds self)
    {
      return BoundsUtil.BackTopLeft(self);
    }

    /// <summary>
    /// 世界坐标（后面 右上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 BackTopRight(this Bounds self)
    {
      return BoundsUtil.BackTopRight(self);
    }

    /// <summary>
    /// 世界坐标（后面 左下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 BackBottomLeft(this Bounds self)
    {
      return BoundsUtil.BackBottomLeft(self);
    }

    /// <summary>
    /// 世界坐标（后面 右下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 BackBottomRight(this Bounds self)
    {
      return BoundsUtil.BackBottomRight(self);
    }

    public static Vector3[] CornerPoints(this Bounds self)
    {
      return BoundsUtil.CornerPoints(self);
    }
  }
}