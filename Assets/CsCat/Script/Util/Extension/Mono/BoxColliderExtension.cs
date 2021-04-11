using System.Collections;
using UnityEngine;

namespace CsCat
{
  public  static partial class BoxColliderExtension
  {
    /// <summary>
    /// 世界坐标（中点）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldCenter(this BoxCollider self)
    {
      return BoxColliderUtil.WorldCenter(self);
    }

    /// <summary>
    /// 世界坐标（前面 左上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldFrontTopLeft(this BoxCollider self)
    {
      return BoxColliderUtil.WorldFrontTopLeft(self);
    }

    /// <summary>
    /// 世界坐标（前面  右上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldFrontTopRight(this BoxCollider self)
    {
      return BoxColliderUtil.WorldFrontTopRight(self);
    }

    /// <summary>
    /// 世界坐标（前面  左下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldFrontBottomLeft(this BoxCollider self)
    {
      return BoxColliderUtil.WorldFrontBottomLeft(self);
    }

    /// <summary>
    /// 世界坐标（前面  左下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldFrontBottomRight(this BoxCollider self)
    {
      return BoxColliderUtil.WorldFrontBottomRight(self);
    }

    /// <summary>
    /// 世界坐标（后面 左上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldBackTopLeft(this BoxCollider self)
    {
      return BoxColliderUtil.WorldBackTopLeft(self);
    }

    /// <summary>
    /// 世界坐标（后面 右上角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldBackTopRight(this BoxCollider self)
    {
      return BoxColliderUtil.WorldBackTopRight(self);
    }

    /// <summary>
    /// 世界坐标（后面 左下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldBackBottomLeft(this BoxCollider self)
    {
      return BoxColliderUtil.WorldBackBottomLeft(self);
    }

    /// <summary>
    /// 世界坐标（后面 右下角）
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3 WorldBackBottomRight(this BoxCollider self)
    {
      return BoxColliderUtil.WorldBackBottomRight(self);
    }

    /// <summary>
    /// 所有的世界坐标
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Vector3[] WorldPoints(this BoxCollider self)
    {
      return BoxColliderUtil.WorldPoints(self);
    }

    public static Vector3 WorldSize(this BoxCollider self)
    {
      return BoxColliderUtil.WorldSize(self);
    }
    


#if UNITY_EDITOR
    /// <summary>
    /// 画BoxCollider
    /// </summary>
    /// <param name="self"></param>
    /// <param name="color">颜色</param>
    /// <param name="offset">偏移</param>
    public static void Draw(this BoxCollider self, Color color, float offset = 0)
    {
      DrawHandlesUtil.Draw(self, color, offset);

    }
#endif





  }
}