using UnityEngine;

namespace CsCat
{
    public class BoxColliderUtil
    {
        /// <summary>
        /// 世界坐标（中点）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldCenter(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return transform.position + transform.rotation * transform.lossyScale.Multiply(boxCollider.center);
        }

        /// <summary>
        /// 世界坐标（前面 左上角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldFrontTopLeft(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(-boxCollider.size.x / 2, boxCollider.size.y / 2,
                       boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（前面  右上角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldFrontTopRight(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(boxCollider.size.x / 2, boxCollider.size.y / 2,
                       boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（前面  左下角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldFrontBottomLeft(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(-boxCollider.size.x / 2, -boxCollider.size.y / 2,
                       boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（前面  左下角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldFrontBottomRight(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(boxCollider.size.x / 2, -boxCollider.size.y / 2,
                       boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（后面 左上角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldBackTopLeft(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(-boxCollider.size.x / 2, boxCollider.size.y / 2,
                       -boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（后面 右上角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldBackTopRight(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(boxCollider.size.x / 2, boxCollider.size.y / 2,
                       -boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（后面 左下角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldBackBottomLeft(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(-boxCollider.size.x / 2, -boxCollider.size.y / 2,
                       -boxCollider.size.z / 2));
        }

        /// <summary>
        /// 世界坐标（后面 右下角）
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3 WorldBackBottomRight(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return boxCollider.WorldCenter() + transform.rotation *
                   transform.lossyScale.Multiply(new Vector3(boxCollider.size.x / 2, -boxCollider.size.y / 2,
                       -boxCollider.size.z / 2));
        }

        public static Vector3 WorldSize(BoxCollider boxCollider)
        {
            Transform transform = boxCollider.transform;
            return transform.lossyScale.Multiply(boxCollider.size);
        }

        /// <summary>
        /// 所有的世界坐标
        /// </summary>
        /// <param name="boxCollider"></param>
        /// <returns></returns>
        public static Vector3[] WorldPoints(BoxCollider boxCollider)
        {
            Vector3[] points = new Vector3[8];

            points[0] = boxCollider.WorldFrontTopLeft();
            points[1] = boxCollider.WorldFrontTopRight();
            points[2] = boxCollider.WorldFrontBottomLeft();
            points[3] = boxCollider.WorldFrontBottomRight();

            points[4] = boxCollider.WorldBackTopLeft();
            points[5] = boxCollider.WorldBackTopRight();
            points[6] = boxCollider.WorldBackBottomLeft();
            points[7] = boxCollider.WorldBackBottomRight();

            return points;
        }
    }
}