

using UnityEngine;

namespace CsCat
{
  public static class RectExtension
  {
    /// <summary>
    /// 缩放Rect
    /// </summary>
    /// <param name="self"></param>
    /// <param name="scale_factor"></param>
    /// <param name="pivot_point_offset">中心点偏移，默认(0,0)是在中心</param>
    /// <returns></returns>
    public static Rect ScaleBy(this Rect self, float scale_factor, Vector2 pivot_point_offset = default(Vector2))
    {
      return RectUtil.ScaleBy(self, scale_factor, pivot_point_offset);
    }

    /// <summary>
    /// 获取两个矩形的交集
    /// </summary>
    public static Rect GetIntersection(this Rect self, Rect other)
    {
      Rect result = new Rect(other.position, other.size);
      if (self.xMin > result.xMin) result.xMin = self.xMin;
      if (self.xMax < result.xMax) result.xMax = self.xMax;
      if (self.yMin > result.yMin) result.yMin = self.yMin;
      if (self.yMax < result.yMax) result.yMax = self.yMax;
      return result;
    }

    #region Padding

    public static Rect Padding(this Rect rect, float left, float right, float top, float bottom)
    {
      rect.position += new Vector2(left, top);
      rect.size -= new Vector2(left + right, top + bottom);
      return rect;
    }

    public static Rect Padding(this Rect rect, Vector2 horizontal, Vector2 vertical)
    {
      return rect.Padding(horizontal.x, horizontal.y, vertical.x, vertical.y);
    }

    public static Rect Padding(this Rect rect, float horizontal, float vertical)
    {
      return rect.Padding(horizontal, horizontal, vertical, vertical);
    }

    public static Rect Padding(this Rect rect, Vector2 padding)
    {
      return rect.Padding(padding.x, padding.x, padding.y, padding.y);
    }

    public static Rect Padding(this Rect rect, float padding)
    {
      return rect.Padding(padding, padding, padding, padding);
    }

    #endregion

  }
}