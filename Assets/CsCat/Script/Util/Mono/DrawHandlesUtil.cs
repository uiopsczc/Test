#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
  public class DrawHandlesUtil
  {
    /// <summary>
    /// 画BoxCollider
    /// </summary>
    /// <param name="boxCollider"></param>
    /// <param name="color">颜色</param>
    /// <param name="offset">偏移</param>
    public static void Draw(BoxCollider boxCollider, Color color, float offset = 0)
    {

      Handles.color = color;
      Vector3 offset_vector = new Vector3(offset, offset, offset);
      //前面
      Handles.DrawLine(boxCollider.WorldFrontTopLeft() + offset_vector,
        boxCollider.WorldFrontTopRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontTopRight() + offset_vector,
        boxCollider.WorldFrontBottomRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontBottomRight() + offset_vector,
        boxCollider.WorldFrontBottomLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontBottomLeft() + offset_vector,
        boxCollider.WorldFrontTopLeft() + offset_vector);

      //后面
      Handles.DrawLine(boxCollider.WorldBackTopLeft() + offset_vector, boxCollider.WorldBackTopRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackTopRight() + offset_vector,
        boxCollider.WorldBackBottomRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackBottomRight() + offset_vector,
        boxCollider.WorldBackBottomLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackBottomLeft() + offset_vector,
        boxCollider.WorldBackTopLeft() + offset_vector);

      //左侧面
      Handles.DrawLine(boxCollider.WorldFrontTopLeft() + offset_vector, boxCollider.WorldBackTopLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackTopLeft() + offset_vector,
        boxCollider.WorldBackBottomLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackBottomLeft() + offset_vector,
        boxCollider.WorldFrontBottomLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontBottomLeft() + offset_vector,
        boxCollider.WorldFrontTopLeft() + offset_vector);

      //右侧面
      Handles.DrawLine(boxCollider.WorldFrontTopRight() + offset_vector,
        boxCollider.WorldBackTopRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackTopRight() + offset_vector,
        boxCollider.WorldBackBottomRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackBottomRight() + offset_vector,
        boxCollider.WorldFrontBottomRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontBottomRight() + offset_vector,
        boxCollider.WorldFrontTopRight() + offset_vector);

      //上面
      Handles.DrawLine(boxCollider.WorldFrontTopLeft() + offset_vector,
        boxCollider.WorldFrontTopRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontTopRight() + offset_vector,
        boxCollider.WorldBackTopRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackTopRight() + offset_vector, boxCollider.WorldBackTopLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackTopLeft() + offset_vector, boxCollider.WorldFrontTopLeft() + offset_vector);

      //下面
      Handles.DrawLine(boxCollider.WorldFrontBottomLeft() + offset_vector,
        boxCollider.WorldFrontBottomRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldFrontBottomRight() + offset_vector,
        boxCollider.WorldBackBottomRight() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackBottomRight() + offset_vector,
        boxCollider.WorldBackBottomLeft() + offset_vector);
      Handles.DrawLine(boxCollider.WorldBackBottomLeft() + offset_vector,
        boxCollider.WorldFrontBottomLeft() + offset_vector);
    }
  }
}
#endif
