using System;
using UnityEngine;

namespace CsCat
{
    [Serializable]
    public class Triangle3D : Polygon3D
    {
        #region field

        public const int Side_AB = 0;
        public const int Side_BC = 1;
        public const int Side_CA = 2;

        #endregion

        #region property

        public Vector3 pointA => ToWorldSpace(localVertexList[0]);

        public Vector3 pointB => ToWorldSpace(localVertexList[1]);

        public Vector3 pointC => ToWorldSpace(localVertexList[2]);

        #endregion


        #region ctor

        /// <summary>
        /// 点按顺时针存放
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public Triangle3D(Vector3 v1, Vector3 v2, Vector3 v3)
        {
        }

        #endregion


        #region static method

        public static Triangle3D operator +(Triangle3D triangle, Vector3 vector)
        {
            Triangle3D clone = CloneUtil.CloneDeep(triangle);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Triangle3D operator -(Triangle3D triangle, Vector3 vector)
        {
            Triangle3D clone = CloneUtil.CloneDeep(triangle);
            clone.AddWorldOffset(-vector);
            return clone;
        }

        #endregion


        #region public method

        public override string ToString()
        {
            return string.Format("A:{0},B:{1},C:{2}", pointA, pointB, pointC);
        }

        #endregion
    }
}