using System;
using UnityEngine;

namespace CsCat
{
    [Serializable]
    public partial class Line : Polygon
    {
        #region property

        /// <summary>
        /// 起始点
        /// </summary>
        public Vector2 pointA => ToWorldSpace(localVertexList[0]);

        /// <summary>
        /// 终点
        /// </summary>
        public Vector2 pointB => ToWorldSpace(localVertexList[1]);

        #endregion

        #region ctor

        public Line(Vector2 start, Vector2 end) : base(start, end)
        {
        }

        #endregion

        #region static method

        public static Line operator +(Line line, Vector2 vector)
        {
            Line clone = CloneUtil.CloneDeep(line);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Line operator -(Line line, Vector2 vector)
        {
            Line clone = CloneUtil.CloneDeep(line);
            clone.AddWorldOffset(-vector);
            return clone;
        }

        #endregion

        #region override method

        #region ToString

        public override string ToString()
        {
            return string.Format("A:{0} B:{1}", this.pointA, this.pointB);
        }

        #endregion

        #endregion

        #region public method

        #region 通用方法

        /// <summary>
        /// 线段的方向
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDirection()
        {
            Vector2 direction = pointB - pointA;
            return direction.normalized;
        }

        /// <summary>
        /// 线段的法向量
        /// </summary>
        /// <returns></returns>
        public Vector2 GetNormal()
        {
            Vector2 dir = GetDirection();
            float oldYValue = dir.y;
            Vector2 normal;
            normal.y = dir.x;
            normal.x = -oldYValue;
            return normal;
        }

        /// <summary>
        /// 给定点到直线的带符号距离，从a点朝向b点，左向为正，右向为负
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public float SignedDistance(Vector2 p)
        {
            Vector2 ap = p - pointA;
            return Vector2.Dot(ap, this.GetNormal());
        }

        #endregion

        #region 线段和点的关系

        public override bool Contains(Vector2 point)
        {
            if (point.EqualsEPSILON(this.pointA) || point.EqualsEPSILON(this.pointB))
                return true;
            Vector2 dir1 = point - this.pointA;
            dir1.Normalize();
            Vector2 dir2 = point - this.pointB;
            dir2.Normalize();
            return SignedDistance(point).EqualsEpsilon(0) && Vector2.Dot(dir1, dir2) < 0;
        }

        /// <summary>
        /// 判断点与直线的关系，假设你站在a点朝向b点，
        /// </summary>
        /// <param name="p"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public PointClassification ClassifyPoint(Vector2 p, float epsilon = FloatConst.Epsilon)
        {
            if (this.Contains(p))
                return PointClassification.OnSegment;

            float distance = SignedDistance(p);

            return distance > epsilon ? PointClassification.LeftSide :
                distance < -epsilon ? PointClassification.RightSide : PointClassification.OnLine;
        }

        #endregion

        #region 线段和线段的关系

        public override bool Contains(Line line2)
        {
            return this.Contains(line2.pointA) && this.Contains(line2.pointB);
        }

        public LineClassification Intersection_Line(Line line2, out Vector2? intersectPoint)
        {
            Vector2 P1 = line2.pointA;
            Vector2 P2 = line2.pointB;
            float extend = 10000;
            Line line2Line = new Line(P1 - line2.GetDirection() * extend, P2 + line2.GetDirection() * extend);
            return Intersection(line2Line, out intersectPoint);
        }

        /// <summary>
        /// 判断两个直线关系 intersectPoint交点
        /// http://blog.163.com/caty_nuaa/blog/static/90390720104252210791/
        /// </summary>
        /// <param name="line2"></param>
        /// <param name="intersectPoint"></param>
        /// <returns></returns>
        public LineClassification Intersection(Line line2, out Vector2? intersectPoint)
        {
            Vector2 P1 = this.pointA;
            Vector2 P2 = this.pointB;
            Vector2 Q1 = line2.pointA;
            Vector2 Q2 = line2.pointB;
            intersectPoint = null;
            //快速排斥试验
            float minX1 = Math.Min(P1.x, P2.x);
            float maxX1 = Math.Max(P1.x, P2.x);
            float minY1 = Math.Min(P1.y, P2.y);
            float maxY1 = Math.Max(P1.y, P2.y);
            float minX2 = Math.Min(Q1.x, Q2.x);
            float maxX2 = Math.Max(Q1.x, Q2.x);
            float minY2 = Math.Min(Q1.y, Q2.y);
            float maxY2 = Math.Max(Q1.y, Q2.y);
            if (minX1 > maxX2 || maxX1 < minX2 || minY1 > maxY2 || maxY1 < minY2)
                return LineClassification.SegmentsNotIntersect;

            //跨立实验
            //P1P2跨立Q1Q2: ( P1 - Q1 ) × ( Q2 - Q1 ) * ( Q2 - Q1 ) × ( P2 - Q1 ) >= 0
            float f1 = (P1 - Q1).Cross(Q2 - Q1) * (Q2 - Q1).Cross(P2 - Q1);
            //Q1Q2跨立P1P2:( Q1 - P1 ) × ( P2 - P1 ) * ( P2 - P1 ) × ( Q2 - P1 ) >= 0
            float f2 = (Q1 - P1).Cross(P2 - P1) * (P2 - P1).Cross(Q2 - P1);


            if (f1 >= 0 && f2 >= 0)
            {
                //求交点http://blog.csdn.net/dgq8211/article/details/7952825
                float s1 = MathUtil.Area(P1, P2, Q1);
                float s2 = MathUtil.Area(P1, P2, Q2);

                if (s1.EqualsEpsilon(0) && s2.EqualsEpsilon(0)) //重叠
                {
                    //判断端点相同的情况
                    if (line2.pointA.EqualsEPSILON(this.pointA))
                    {
                        bool isSameDir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
                        if (isSameDir)
                            return LineClassification.Collinear;
                        intersectPoint = line2.pointA;
                        return LineClassification.SegmentsIntersect;
                    }

                    if (line2.pointA.EqualsEPSILON(this.pointB))
                    {
                        bool isSameDir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
                        if (!isSameDir)
                            return LineClassification.Collinear;
                        intersectPoint = line2.pointA;
                        return LineClassification.SegmentsIntersect;
                    }

                    if (line2.pointB.EqualsEPSILON(this.pointA))
                    {
                        bool isSameDir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
                        if (!isSameDir)
                            return LineClassification.Collinear;
                        intersectPoint = line2.pointB;
                        return LineClassification.SegmentsIntersect;
                    }

                    if (line2.pointB.EqualsEPSILON(this.pointB))
                    {
                        bool isSameDir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
                        if (isSameDir)
                            return LineClassification.Collinear;
                        intersectPoint = line2.pointB;
                        return LineClassification.SegmentsIntersect;
                    }

                    return LineClassification.Collinear;
                }

                float x = (Q2.x * s1 + Q1.x * s2) / (s1 + s2);
                float y = (Q2.y * s1 + Q1.y * s2) / (s1 + s2);
                intersectPoint = new Vector2(x, y);
                return LineClassification.SegmentsIntersect;
            }

            return LineClassification.SegmentsNotIntersect;
        }

        #endregion

        #region 该线段离指定点最近的点

        /// <summary>
        /// 到线段最近的点
        /// 搜索“计算集合常用算法概览”中的“计算点到线段的最近点”
        /// </summary>
        /// <param name="p2"></param>
        /// <returns></returns>
        public Vector2 GetClosestPoint(Vector2 p2)
        {
            Vector2 tmp;
            Vector2 dir1 = this.GetDirection();

            if (this.Contains(p2))
                return p2;


            if (dir1.EqualsEPSILON(Vector2.right) || dir1.EqualsEPSILON(-Vector2.right)) //水平方向
                tmp = new Vector2(p2.x, this.pointA.y);
            else if (dir1.EqualsEPSILON(Vector2.up) || dir1.EqualsEPSILON(-Vector2.up)) //垂直方向
                tmp = new Vector2(this.pointA.x, p2.y);
            else //该线段不平行于X轴也不平行于Y轴
            {
                float k = (this.pointB.y - this.pointA.y) / (this.pointB.x - this.pointA.x);
                float x = (k * k * this.pointA.x + k * (p2.y - this.pointA.y) + p2.x) / (k * k + 1);
                float y = k * (x - this.pointA.x) + this.pointA.y;
                tmp = new Vector2(x, y);
            }

            var closestPoint = CalculateClosestPoint(tmp);
            return closestPoint;
        }

        #endregion

        #endregion

        #region private method

        /// <summary>
        /// 如果点在线段上，返回该点,否则返回到该点最近的端点
        /// </summary>
        /// <param name="p2"></param>
        /// <returns></returns>
        private Vector2 CalculateClosestPoint(Vector2 p2)
        {
            if (this.Contains(p2))
                return p2;
            float d1 = (p2 - this.pointA).sqrMagnitude;
            float d2 = (p2 - this.pointB).sqrMagnitude;
            return d1 < d2 ? this.pointA : this.pointB;
        }

        #endregion
    }
}