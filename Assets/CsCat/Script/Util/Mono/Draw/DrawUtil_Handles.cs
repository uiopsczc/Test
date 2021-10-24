#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
    public partial class DrawUtil
    {
        public static bool Is_Handles_Enable = true;

        #region 画射线

        public static void DrawRay(Vector3 position, Vector3 direction, Color color)
        {
            using (new HandlesColorScope(color))
            {
                Handles.DrawLine(position, position + direction);
            }
        }

        public static void DrawRay(Vector3 position, Vector3 direction)
        {
            DrawRay(position, direction, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        #endregion

        #region 画箭头 

        /// <summary>
        ///   画箭头
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public static void HandlesArrow(Vector3 position, Vector3 direction)
        {
            HandlesArrow(position, direction, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   画箭头
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="color"></param>
        public static void HandlesArrow(Vector3 position, Vector3 direction, Color color)
        {
            if (!Is_Handles_Enable)
                return;
            using (new HandlesColorScope(color))
            {
                DrawRay(position, direction, color);
                HandlesCone(position + direction, direction.normalized, 15, color);
            }
        }

        #endregion

        #region 画bounds 

        /// <summary>
        ///   画bounds
        /// </summary>
        /// <param name="bounds"></param>
        public static void HandlesBounds(Bounds bounds)
        {
            HandlesBounds(bounds, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   画bounds
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="color"></param>
        public static void HandlesBounds(Bounds bounds, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var cube = new Cube3D(bounds.center, bounds.size);
                cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画Capsule 

        public static void HandlesCapsule(Vector3 start, Vector3 end, float radius)
        {
            HandlesCapsule(start, end, radius, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        public static void HandlesCapsule(Vector3 start, Vector3 end, float radius, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                HandlesCylinder(start, end, radius, color);
                HandlesSphere(start, radius, color);
                HandlesSphere(end, radius, color);
            }
        }

        #endregion

        #region 画圆 

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        public static void HandlesCircle(Vector3 position, float radius)
        {
            HandlesCircle(position, radius, Matrix4x4.identity, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="mutiplyMatrix"></param>
        public static void HandlesCircle(Vector3 position, float radius, Matrix4x4 mutiplyMatrix)
        {
            HandlesCircle(position, radius, mutiplyMatrix, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   画圆
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="color"></param>
        public static void HandlesCircle(Vector3 position, float radius, Matrix4x4 mutiplyMatrix, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var circle = new Circle3D(position, radius, mutiplyMatrix);

                circle.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion


        #region 画圆锥 

        public static void HandlesCone(Vector3 endPoint, Vector3 forward)
        {
            HandlesCone(endPoint, forward, 45, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        public static void HandlesCone(Vector3 endPoint, Vector3 forward, float angle)
        {
            HandlesCone(endPoint, forward, angle, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        /// </summary>
        /// <param name="endPoint">箭头的终点</param>
        /// <param name="forward">箭头的方向</param>
        /// <param name="angle">角度</param>
        /// <param name="color"></param>
        public static void HandlesCone(Vector3 endPoint, Vector3 forward, float angle, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
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

                DrawRay(endPoint, (-angleSlerpRight).normalized * distance, color);
                DrawRay(endPoint, (-angleSlerpLeft).normalized * distance, color);
                DrawRay(endPoint, (-angleSlerpUp).normalized * distance, color);
                DrawRay(endPoint, (-angleSlerpDown).normalized * distance, color);

                HandlesCircle(endPoint - forward, (forward + -angleSlerpRight.normalized * distance).magnitude,
                    Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color);
                HandlesCircle(endPoint - forward / 2,
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
        public static void HandlesCylinder(Vector3 start, Vector3 end, float radius)
        {
            HandlesCylinder(start, end, radius, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   画圆柱体
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        public static void HandlesCylinder(Vector3 start, Vector3 end, float radius, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var upDir = (end - start).normalized;

                var toUpMatrix = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, upDir));
                //顶部的圆形
                HandlesCircle(start, radius, toUpMatrix, color);
                ////底部的圆形
                HandlesCircle(end, radius, toUpMatrix, color);

                HandlesLine(start, end, Color.yellow);

                //四个矩形
                var rectCenter = (start + end) / 2;
                var size = new Vector2(radius * 2, (start - end).magnitude);

                var toRightMatrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

                HandlesCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0)), color);
                HandlesCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45)), color);
                HandlesCenterRect(rectCenter, size,
                    toUpMatrix * toRightMatrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), color);
                HandlesCenterRect(rectCenter, size,
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
        public static void HandlesLocalCube(Transform transform, Vector3 offset, Vector3 size)
        {
            HandlesLocalCube(transform, offset, size, DrawUtilConst.HandlesDraw_DefaultColor);
        }


        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public static void HandlesLocalCube(Transform transform, Vector3 offset, Vector3 size, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var cube = new Cube3D(Vector3.zero, Vector3.one);
                cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
                cube.PreMultiplyMatrix(transform.localToWorldMatrix);
                cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public static void HandlesLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size)
        {
            HandlesLocalCube(matrix, offset, size, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   画LocalCube
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        public static void HandlesLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var cube = new Cube3D(Vector3.zero, Vector3.one);
                cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
                cube.PreMultiplyMatrix(matrix);
                cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画Cube 

        public static void HandlesCube(Vector3 min, Vector3 max, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var cube = new Cube3D((min + max) / 2, (max - min).Abs());
                cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画点 

        /// <summary>
        ///   画点
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        public static void HandlesPoint(Vector3 position, float scale = 1)
        {
            HandlesPoint(position, DrawUtilConst.HandlesDraw_DefaultColor, scale);
        }

        /// <summary>
        ///   画点
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        public static void HandlesPoint(Vector3 position, Color color, float scale = 1)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale, color);
                DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale, color);
                DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale, color);
            }
        }

        #endregion

        #region 画球 

        /// <summary>
        ///   画球
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        public static void HandlesSphere(Vector3 position, float radius)
        {
            HandlesSphere(position, radius, DrawUtilConst.HandlesDraw_DefaultColor);
        }

        /// <summary>
        ///   画球
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="radius"></param>
        public static void HandlesSphere(Vector3 position, float radius, Color color)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                var sphere = new Sphere3D(position, radius);
                sphere.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画线 

        public static void HandlesLine(Vector3 start, Vector3 end, bool isDotted = false)
        {
            HandlesLine(start, end, DrawUtilConst.HandlesDraw_DefaultColor, isDotted);
        }

        /// <summary>
        ///   画线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void HandlesLine(Vector3 start, Vector3 end, Color color, bool isDotted = false)
        {
            if (!Is_Handles_Enable) return;

            using (new HandlesColorScope(color))
            {
                if (!isDotted)
                    Handles.DrawLine(start, end);
                else
                    Handles.DrawDottedLine(start, end, 1);
            }

            //LogCat.LogErrorFormat("{0} {1}", start, end);
        }

        #endregion

        #region 画多边形 

        public static void HandlesPolygon(params Vector3[] points)
        {
            HandlesPolygon(Matrix4x4.identity, DrawUtilConst.HandlesDraw_DefaultColor, points);
        }

        public static void HandlesPolygon(Color color, params Vector3[] points)
        {
            HandlesPolygon(Matrix4x4.identity, color, points);
        }

        /// <summary>
        ///   画多边形
        /// </summary>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="color"></param>
        /// <param name="points"></param>
        public static void HandlesPolygon(Matrix4x4 mutiplyMatrix, Color color, params Vector3[] points)
        {
            if (!Is_Handles_Enable)
                return;

            using (new HandlesColorScope(color))
            {
                var polygon = new Polygon3D(points);
                polygon.MultiplyMatrix(mutiplyMatrix);

                polygon.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
            }
        }

        #endregion

        #region 画矩形 

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="isDotted"></param>
        public static void HandlesCenterRect(Vector3 center, Vector2 size, bool isDotted = false)
        {
            HandlesCenterRect(center, size, Matrix4x4.identity, DrawUtilConst.HandlesDraw_DefaultColor, isDotted);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="isDotted"></param>
        public static void HandlesCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix,
            bool isDotted = false)
        {
            HandlesCenterRect(center, size, mutiplyMatrix, DrawUtilConst.HandlesDraw_DefaultColor, isDotted);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="isDotted"></param>
        public static void HandlesCenterRect(Rect rect, bool isDotted = false)
        {
            HandlesCenterRect(rect.center.ToVector3(StringConst.String_x_0_y), rect.center.ToVector3(StringConst.String_x_0_y), Matrix4x4.identity,
                DrawUtilConst.HandlesDraw_DefaultColor, isDotted);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="isDotted"></param>
        public static void HandlesCenterRect(Rect rect, Matrix4x4 mutiplyMatrix, bool isDotted = false)
        {
            HandlesCenterRect(rect.center.ToVector3(StringConst.String_x_0_y), rect.center.ToVector3(StringConst.String_x_0_y), mutiplyMatrix,
                DrawUtilConst.HandlesDraw_DefaultColor, isDotted);
        }


        /// <summary>
        ///   画中心点在center的矩形
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="color"></param>
        public static void HandlesCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix, Color color,
            bool isDotted = false)
        {
            using (new HandlesColorScope(color))
            {
                var rectangle = new Rectangle3D(center, size, mutiplyMatrix);
                rectangle.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color, isDotted); });
            }
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="faceColor"></param>
        /// <param name="outline_color"></param>
        public static void HandlesCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiplyMatrix, Color faceColor,
            Color outline_color)
        {
            var rectangle = new Rectangle3D(center, size, mutiplyMatrix);
            Handles.DrawSolidRectangleWithOutline(rectangle.vertexeList.ToArray(), faceColor, outline_color);
        }

        /// <summary>
        ///   默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="mutiplyMatrix"></param>
        /// <param name="face_color"></param>
        /// <param name="outline_color"></param>
        public static void HandlesCenterRect(Rect rect, Matrix4x4 mutiplyMatrix, Color face_color, Color outline_color)
        {
            HandlesCenterRect(rect.center.ToVector3(StringConst.String_x_0_y), rect.size.ToVector3(StringConst.String_x_0_y), mutiplyMatrix, face_color,
                outline_color);
        }

        #endregion

        #region Handles自带的Rect

        public static void HandlesDrawSolidRectangleWithOutline(Rect rect, Color face_color, Color outline_color,
            Transform transform = null)
        {
            Vector3[] rectVertexs =
            {
                new Vector3(rect.x, rect.y, 0),
                new Vector3(rect.x + rect.width, rect.y, 0),
                new Vector3(rect.x + rect.width, rect.y + rect.height, 0),
                new Vector3(rect.x, rect.y + rect.height, 0)
            };
            if (transform != null)
            {
                for (int i = 0; i < rectVertexs.Length; i++)
                    rectVertexs[i] = transform.TransformPoint(rectVertexs[i]);
            }

            Handles.DrawSolidRectangleWithOutline(rectVertexs, face_color, outline_color);
        }

        #endregion

        #region DrawString

        public static void HandlesDrawString(Vector3 position, string text, Color? color = null)
        {
            var saveColor = GUI.color;

            if (color.HasValue)
                GUI.color = color.Value;

            //    Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            Handles.Label(position, text);
            GUI.color = saveColor;
            Handles.EndGUI();
        }

        #endregion
    }
}
#endif