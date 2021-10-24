using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
    [Serializable]
    public class Circle3D : Polygon3D
    {
        #region field

        /// <summary>
        /// 半径
        /// </summary>
        public float radius;

        #endregion


        #region ctor

        /// <summary>
        /// 默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="worldOffset"></param>
        /// <param name="radius"></param>
        /// <param name="matrix"></param>
        public Circle3D(Vector3 worldOffset, float radius, Matrix4x4 matrix)
        {
            AddWorldOffset(worldOffset);
            MultiplyMatrix(matrix);
            this.radius = radius;
        }

        /// <summary>
        /// 默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="worldOffset"></param>
        /// <param name="radius"></param>
        public Circle3D(Vector3 worldOffset, float radius) : this(worldOffset, radius, Matrix4x4.identity)
        {
        }

        /// <summary>
        /// 默认Matrix4x4.identity是向上
        /// </summary>
        /// <param name="radius"></param>
        public Circle3D(float radius) : this(Vector3.zero, radius, Matrix4x4.identity)
        {
        }

        #endregion

        #region  operator

        public static Circle3D operator +(Circle3D circle, Vector3 vector)
        {
            Circle3D clone = CloneUtil.CloneDeep(circle);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Circle3D operator -(Circle3D circle, Vector3 vector)
        {
            Circle3D clone = CloneUtil.CloneDeep(circle);
            clone.AddWorldOffset(-vector);
            return clone;
        }

        #endregion

        #region public method

        public override bool Equals(object obj)
        {
            if (!(obj is Circle3D other))
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

        public override List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
        {
            List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>();


            float eachDegree = 4;
            int segmentNum = (int) Mathf.Ceil(360 / eachDegree);
            for (int i = 0; i <= segmentNum; i++)
            {
                float preDegree = i * eachDegree;
                float nextDegree = (i + 1) * eachDegree;
                Vector3 prePoint =
                    ToWorldSpace(new Vector3(Mathf.Cos(preDegree * Mathf.Deg2Rad), 0,
                                     Mathf.Sin(preDegree * Mathf.Deg2Rad)) *
                                 radius);
                Vector3 nextPoint =
                    ToWorldSpace(new Vector3(Mathf.Cos(nextDegree * Mathf.Deg2Rad), 0,
                                     Mathf.Sin(nextDegree * Mathf.Deg2Rad)) *
                                 radius);
                result.Add(new KeyValuePair<Vector3, Vector3>(prePoint, nextPoint));
            }

            return result;
        }

        #endregion
    }
}