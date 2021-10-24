using UnityEngine;

namespace CsCat
{
    public partial class DrawUtil
    {
        public static bool Is_Gizmos_Enable = true;

        #region 画箭头 

        /// <summary>
        ///   画箭头
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public static void GizmosArrow(Vector3 position, Vector3 direction)
        {
            GizmosArrow(position, direction, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画箭头
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="color"></param>
        public static void GizmosArrow(Vector3 position, Vector3 direction, Color color)
        {
            if (!Is_Gizmos_Enable)
                return;
            using (new GizmosColorScope(color))
            {
                Gizmos.DrawRay(position, direction);
                GizmosCone(position + direction, direction.normalized, 15, color);
            }
        }

        #endregion

        #region 画bounds 

        /// <summary>
        ///   画bounds
        /// </summary>
        /// <param name="bounds"></param>
        public static void GizmosBounds(Bounds bounds)
        {
            GizmosBounds(bounds, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画bounds
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="color"></param>
        public static void GizmosBounds(Bounds bounds, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var cube = new Cube3D(bounds.center, bounds.size);
                cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画Capsule 

        public static void GizmosCapsule(Vector3 start, Vector3 end, float radius)
        {
            GizmosCapsule(start, end, radius, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        public static void GizmosCapsule(Vector3 start, Vector3 end, float radius, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                GizmosCylinder(start, end, radius, color);
                GizmosSphere(start, radius, color);
                GizmosSphere(end, radius, color);
            }
        }

        #endregion

        #region 画圆 

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        public static void GizmosCircle(Vector3 position, float radius)
        {
            GizmosCircle(position, radius, Matrix4x4.identity, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="mutiplyMatrix"></param>
        public static void GizmosCircle(Vector3 position, float radius, Matrix4x4 mutiplyMatrix)
        {
            GizmosCircle(position, radius, mutiplyMatrix, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画圆  默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="color"></param>
        public static void GizmosCircle(Vector3 position, float radius, Matrix4x4 mutiplyMatrix, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var circle = new Circle3D(position, radius, mutiplyMatrix);

                circle.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion


        #region 画圆锥 

        public static void GizmosCone(Vector3 endPoint, Vector3 forward)
        {
            GizmosCone(endPoint, forward, 45, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        public static void GizmosCone(Vector3 endPoint, Vector3 forward, float angle)
        {
            GizmosCone(endPoint, forward, angle, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        /// </summary>
        /// <param name="endPoint">箭头的终点</param>
        /// <param name="forward">箭头的方向</param>
        /// <param name="angle">角度</param>
        /// <param name="color"></param>
        public static void GizmosCone(Vector3 endPoint, Vector3 forward, float angle, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var slerpRight = Vector3.Slerp(-forward, forward, 0.5f);
                var slerpUp = Vector3.Cross(forward, slerpRight).normalized * forward.magnitude;

                var angleSlerpRight = Vector3.Slerp(forward, slerpRight, angle / 90);
                var angleSlerpLeft = Vector3.Slerp(forward, -slerpRight, angle / 90);

                var angleSlerpUp = Vector3.Slerp(forward, slerpUp, angle / 90);
                var angleSlerpDown = Vector3.Slerp(forward, -slerpUp, angle / 90);

                var plane = new Plane(forward, endPoint - forward);
                var ray = new Ray(endPoint, -angleSlerpRight);
                plane.Raycast(ray, out var distance);

                Gizmos.DrawRay(endPoint, (-angleSlerpRight).normalized * distance);
                Gizmos.DrawRay(endPoint, (-angleSlerpLeft).normalized * distance);
                Gizmos.DrawRay(endPoint, (-angleSlerpUp).normalized * distance);
                Gizmos.DrawRay(endPoint, (-angleSlerpDown).normalized * distance);

                GizmosCircle(endPoint - forward, (forward + -angleSlerpRight.normalized * distance).magnitude,
                    Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color);
                GizmosCircle(endPoint - forward / 2,
                    (forward + -angleSlerpRight.normalized * distance).magnitude / 2,
                    Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color);
            }
        }

        #endregion


        #region 画圆柱体 

        /// <summary>
        ///   画圆柱体
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="radius"></param>
        public static void GizmosCylinder(Vector3 start, Vector3 end, float radius)
        {
            GizmosCylinder(start, end, radius, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画圆柱体
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        public static void GizmosCylinder(Vector3 start, Vector3 end, float radius, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var upDir = (end - start).normalized;

                var toUpMatrix = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, upDir));
                //顶部的圆形
                GizmosCircle(start, radius, toUpMatrix, color);
                ////底部的圆形
                GizmosCircle(end, radius, toUpMatrix, color);

                GizmosLine(start, end, Color.yellow);

                //四个矩形
                var rectCenter = (start + end) / 2;
                var size = new Vector2(radius * 2, (start - end).magnitude);

                var toRightMatrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

                GizmosCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0)), color);
                GizmosCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45)), color);
                GizmosCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), color);
                GizmosCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 135)), color);
            }
        }

        #endregion


        #region 画LocalCube 

        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public static void GizmosLocalCube(Transform transform, Vector3 offset, Vector3 size)
        {
            GizmosLocalCube(transform, offset, size, DrawUtilConst.GizmosDraw_DefaultColor);
        }


        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public static void GizmosLocalCube(Transform transform, Vector3 offset, Vector3 size, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var cube = new Cube3D(Vector3.zero, Vector3.one);
                cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
                cube.PreMultiplyMatrix(transform.localToWorldMatrix);
                cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public static void GizmosLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size)
        {
            GizmosLocalCube(matrix, offset, size, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public static void GizmosLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var cube = new Cube3D(Vector3.zero, Vector3.one);
                cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
                cube.PreMultiplyMatrix(matrix);
                cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        public static void GizmosCube(Vector3 min, Vector3 max, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var cube = new Cube3D((min + max) / 2, (max - min).Abs());
                cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #region 画点 

        /// <summary>
        ///   画点
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        public static void GizmosPoint(Vector3 position, float scale = 1)
        {
            GizmosPoint(position, DrawUtilConst.GizmosDraw_DefaultColor, scale);
        }

        /// <summary>
        ///   画点
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        public static void GizmosPoint(Vector3 position, Color color, float scale = 1)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                Gizmos.DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale);
                Gizmos.DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale);
                Gizmos.DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale);
            }
        }

        #endregion

        #region 画球 

        /// <summary>
        ///   画球
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        public static void GizmosSphere(Vector3 position, float radius)
        {
            GizmosSphere(position, radius, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画球
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        public static void GizmosSphere(Vector3 position, float radius, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                var sphere = new Sphere3D(position, radius);
                sphere.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画线 

        public static void GizmosLine(Vector3 start, Vector3 end)
        {
            GizmosLine(start, end, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   画线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void GizmosLine(Vector3 start, Vector3 end, Color color)
        {
            if (!Is_Gizmos_Enable) return;

            using (new GizmosColorScope(color))
            {
                Gizmos.DrawLine(start, end);
            }

            //LogCat.LogErrorFormat("{0} {1}", start, end);
        }

        #endregion

        #region 画多边形 

        public static void GizmosPolygon(params Vector3[] points)
        {
            GizmosPolygon(Matrix4x4.identity, DrawUtilConst.GizmosDraw_DefaultColor, points);
        }

        public static void GizmosPolygon(Color color, params Vector3[] points)
        {
            GizmosPolygon(Matrix4x4.identity, color, points);
        }

        /// <summary>
        ///   画多边形
        /// </summary>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public static void GizmosPolygon(Matrix4x4 mutiplyMatrix, Color color, params Vector3[] points)
        {
            if (!Is_Gizmos_Enable)
                return;

            using (new GizmosColorScope(color))
            {
                var polygon = new Polygon3D(points);
                polygon.MultiplyMatrix(mutiplyMatrix);

                polygon.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画矩形 

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        public static void GizmosCenterRect(Vector3 center, Vector2 size)
        {
            GizmosCenterRect(center, size, Matrix4x4.identity, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="mutiplyMatrix"></param>
        public static void GizmosCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix)
        {
            GizmosCenterRect(center, size, mutiplyMatrix, DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="rect"></param>
        public static void GizmosCenterRect(Rect rect)
        {
            GizmosCenterRect(rect.center.ToVector3(StringConst.String_x_0_y),
                rect.center.ToVector3(StringConst.String_x_0_y), Matrix4x4.identity,
                DrawUtilConst.GizmosDraw_DefaultColor);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="mutiplyMatrix"></param>
        public static void GizmosCenterRect(Rect rect, Matrix4x4 mutiplyMatrix)
        {
            GizmosCenterRect(rect.center.ToVector3(StringConst.String_x_0_y),
                rect.center.ToVector3(StringConst.String_x_0_y), mutiplyMatrix,
                DrawUtilConst.GizmosDraw_DefaultColor);
        }


        /// <summary>
        ///   画中心点在center的矩形
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="color"></param>
        public static void GizmosCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix, Color color)
        {
            using (new GizmosColorScope(color))
            {
                var rectangle = new Rectangle3D(center, size, mutiplyMatrix);
                rectangle.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion
    }
}