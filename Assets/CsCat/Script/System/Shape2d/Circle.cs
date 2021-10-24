using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
    [Serializable]
    public class Circle : Polygon
    {
        #region field

        /// <summary>
        /// 半径
        /// </summary>
        public float radius;

        #endregion


        #region ctor

        public Circle(Vector2 worldOffset, float radius)
        {
            AddWorldOffset(worldOffset);
            this.radius = radius;
        }

        #endregion

        #region  operator

        public static Circle operator +(Circle circle, Vector2 vector)
        {
            Circle clone = CloneUtil.CloneDeep(circle);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Circle operator -(Circle circle, Vector2 vector)
        {
            Circle clone = CloneUtil.CloneDeep(circle);
            clone.AddWorldOffset(-vector);
            return clone;
        }

        #endregion

        #region public method

        public override bool Equals(object obj)
        {
            if (!(obj is Circle other))
                return false;
            if (radius != other.radius)
                return false;
            if (worldOffset != other.worldOffset)
                return false;
            return other.matrix.Equals(this.matrix) && base.Equals(obj);
        }

        public override string ToString()
        {
            return string.Format("Center:{0},radius:{1}", center, radius);
        }

        public override List<KeyValuePair<Vector2, Vector2>> GetDrawLineList()
        {
            List<KeyValuePair<Vector2, Vector2>> result = new List<KeyValuePair<Vector2, Vector2>>();


            float eachDegree = 4;
            int segmentNum = (int) Mathf.Ceil(360 / eachDegree);
            for (int i = 0; i <= segmentNum; i++)
            {
                float preDegree = i * eachDegree;
                float nextDegree = (i + 1) * eachDegree;
                Vector2 prePoint =
                    ToWorldSpace(new Vector2(Mathf.Cos(preDegree * Mathf.Deg2Rad),
                                     Mathf.Sin(preDegree * Mathf.Deg2Rad)) *
                                 radius);
                Vector2 nextPoint =
                    ToWorldSpace(new Vector2(Mathf.Cos(nextDegree * Mathf.Deg2Rad),
                                     Mathf.Sin(nextDegree * Mathf.Deg2Rad)) *
                                 radius);
                result.Add(new KeyValuePair<Vector2, Vector2>(prePoint, nextPoint));
            }

            return result;
        }

        public override bool IsIntersect(Circle circle2)
        {
            float d1 = (center - circle2.center).sqrMagnitude;
            float d2 = (radius + circle2.radius) * (radius + circle2.radius);
            return d1 <= d2;
        }

        //https://www.zhihu.com/question/24251545
        public override bool IsIntersect(Rectangle rectangle)
        {
            Vector2 localCenterCircleOfRectangle = rectangle.ToLocalSpace(center);

            Vector2 v = new Vector2(Math.Abs(localCenterCircleOfRectangle.x),
                Math.Abs(localCenterCircleOfRectangle.y));
            Vector2 h = rectangle.rightTop;
            Vector2 u = v - h;
            u = new Vector2(Math.Max(u.x, 0), Math.Max(u.y, 0));
            return Vector2.Dot(u, u) <= radius * radius;
        }

        #endregion
    }
}