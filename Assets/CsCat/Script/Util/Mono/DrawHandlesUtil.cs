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
            Vector3 offsetVector = new Vector3(offset, offset, offset);
            //前面
            Handles.DrawLine(boxCollider.WorldFrontTopLeft() + offsetVector,
                boxCollider.WorldFrontTopRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontTopRight() + offsetVector,
                boxCollider.WorldFrontBottomRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontBottomRight() + offsetVector,
                boxCollider.WorldFrontBottomLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontBottomLeft() + offsetVector,
                boxCollider.WorldFrontTopLeft() + offsetVector);

            //后面
            Handles.DrawLine(boxCollider.WorldBackTopLeft() + offsetVector,
                boxCollider.WorldBackTopRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackTopRight() + offsetVector,
                boxCollider.WorldBackBottomRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackBottomRight() + offsetVector,
                boxCollider.WorldBackBottomLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackBottomLeft() + offsetVector,
                boxCollider.WorldBackTopLeft() + offsetVector);

            //左侧面
            Handles.DrawLine(boxCollider.WorldFrontTopLeft() + offsetVector,
                boxCollider.WorldBackTopLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackTopLeft() + offsetVector,
                boxCollider.WorldBackBottomLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackBottomLeft() + offsetVector,
                boxCollider.WorldFrontBottomLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontBottomLeft() + offsetVector,
                boxCollider.WorldFrontTopLeft() + offsetVector);

            //右侧面
            Handles.DrawLine(boxCollider.WorldFrontTopRight() + offsetVector,
                boxCollider.WorldBackTopRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackTopRight() + offsetVector,
                boxCollider.WorldBackBottomRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackBottomRight() + offsetVector,
                boxCollider.WorldFrontBottomRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontBottomRight() + offsetVector,
                boxCollider.WorldFrontTopRight() + offsetVector);

            //上面
            Handles.DrawLine(boxCollider.WorldFrontTopLeft() + offsetVector,
                boxCollider.WorldFrontTopRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontTopRight() + offsetVector,
                boxCollider.WorldBackTopRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackTopRight() + offsetVector,
                boxCollider.WorldBackTopLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackTopLeft() + offsetVector,
                boxCollider.WorldFrontTopLeft() + offsetVector);

            //下面
            Handles.DrawLine(boxCollider.WorldFrontBottomLeft() + offsetVector,
                boxCollider.WorldFrontBottomRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldFrontBottomRight() + offsetVector,
                boxCollider.WorldBackBottomRight() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackBottomRight() + offsetVector,
                boxCollider.WorldBackBottomLeft() + offsetVector);
            Handles.DrawLine(boxCollider.WorldBackBottomLeft() + offsetVector,
                boxCollider.WorldFrontBottomLeft() + offsetVector);
        }
    }
}
#endif