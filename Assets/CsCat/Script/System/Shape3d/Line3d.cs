using System;
using UnityEngine;

namespace CsCat
{
    [Serializable]
    public partial class Line3D : Polygon3D
    {
        #region property

        public Vector3 pointA => ToWorldSpace(localVertexList[0]);

        public Vector3 pointB => ToWorldSpace(localVertexList[1]);

        #endregion

        #region ctor

        public Line3D(Vector3 start, Vector3 end) : base(start, end)
        {
        }

        #endregion

        #region static method

        public static Line3D operator +(Line3D line, Vector3 vector)
        {
            Line3D clone = CloneUtil.CloneDeep(line);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Line3D operator -(Line3D line, Vector3 vector)
        {
            Line3D clone = CloneUtil.CloneDeep(line);
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
    }
}