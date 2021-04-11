

using UnityEngine;

namespace CsCat
{
  public class RectUtil
  {
    /// <summary>
    /// 缩放Rect
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="scale_factor"></param>
    /// <param name="pivot_point_offset">中心点偏移，默认(0,0)是在中心</param>
    /// <returns></returns>
    public static Rect ScaleBy(Rect rect, float scale_factor, Vector2 pivot_point_offset = default(Vector2))
    {
      Rect result = rect;
      result.x -= pivot_point_offset.x;
      result.y -= pivot_point_offset.y;
      result.xMin *= scale_factor;
      result.xMax *= scale_factor;
      result.yMin *= scale_factor;
      result.yMax *= scale_factor;
      result.x += pivot_point_offset.x;
      result.y += pivot_point_offset.y;
      return result;
    }
  }
}