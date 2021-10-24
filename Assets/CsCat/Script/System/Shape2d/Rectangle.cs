using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
    /// <summary>
    ///  VertexeList;//定点顺时针为:(leftBottom->leftTop->rightTop->rightBottom),逆时针为:(rightBottom->rightTop->leftTop->leftBottom)
    ///  顶点点列表,外边界用按顺时针，内边界用逆时针，区域在方向右侧
    /// </summary>
    [Serializable]
    public class Rectangle : Polygon
    {
        #region field

        protected Vector2 size;

        #endregion

        #region property

        public Vector2 localLeftBottom => localVertexList[0];

        public Vector2 localLeftTop => localVertexList[1];

        public Vector2 localRightTop => localVertexList[2];

        public Vector2 localRightBottom => localVertexList[3];


        public Vector2 leftBottom => ToWorldSpace(localVertexList[0]);

        public Vector2 leftTop => ToWorldSpace(localVertexList[1]);

        public Vector2 rightTop => ToWorldSpace(localVertexList[2]);

        public Vector2 rightBottom => ToWorldSpace(localVertexList[3]);

        #endregion

        #region ctor

        /// <summary>
        /// leftBottom->leftTop->rightTop->rightBottom
        /// </summary>
        /// <param name="center"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="angle"></param>
        public Rectangle(Vector2 center, Vector2 size)
        {
            MultiplyMatrix(Matrix4x4.Translate(center));
            this.size = size;


            Vector2 localLeftBottom = new Vector2(-size.x / 2, -size.y / 2);
            Vector2 localLeftTop = new Vector2(-size.x / 2, size.y / 2);
            Vector2 localRightTop = new Vector2(size.x / 2, size.y / 2);
            Vector2 localRightBottom = new Vector2(size.x / 2, -size.y / 2);


            localVertexList.Add(localLeftBottom);
            localVertexList.Add(localLeftTop);
            localVertexList.Add(localRightTop);
            localVertexList.Add(localRightBottom);
        }

        public Rectangle(float localLeftTopX, float localLeftTopY, float localRightBottomX,
            float localRightBottomY) : this(
            new Vector2(localLeftTopX + localRightBottomX, localLeftTopY + localRightBottomY) / 2,
            new Vector2(Mathf.Abs(localLeftTopX - localRightBottomX),
                Mathf.Abs(localLeftTopY - localRightBottomY)))
        {
        }

        #endregion

        #region operator

        public static Rectangle operator +(Rectangle rectangle, Vector2 vector)
        {
            Rectangle clone = CloneUtil.CloneDeep(rectangle);
            clone.AddWorldOffset(vector);
            return clone;
        }

        public static Rectangle operator -(Rectangle rectangle, Vector2 vector)
        {
            Rectangle clone = CloneUtil.CloneDeep(rectangle);
            clone.AddWorldOffset(-vector);
            return clone;
        }

        #endregion


        #region public method

        /// <summary>
        /// http://blog.csdn.net/i_dovelemon/article/details/31420749
        /// </summary>
        /// <param name="rectangle1"></param>
        /// <param name="rectangle2"></param>
        /// <param name="isIgnoreV"></param>
        /// <returns></returns>
        public override bool IsIntersect(Rectangle rectangle2)
        {
            Line line1_1 = new Line(vertexList[0], vertexList[1]);
            Line line1_2 = new Line(vertexList[1], vertexList[2]);

            Line line2_1 = new Line(rectangle2.vertexList[0], rectangle2.vertexList[1]);
            Line line2_2 = new Line(rectangle2.vertexList[1], rectangle2.vertexList[2]);

            Vector2 normal1_1 = line1_1.GetNormal();
            Vector2 normal1_2 = line1_2.GetNormal();

            Vector2 normal2_1 = line2_1.GetNormal();
            Vector2 normal2_2 = line2_2.GetNormal();


            List<Vector2> axisList = new List<Vector2> {normal1_1, normal1_2, normal2_1, normal2_2};


            for (var i = 0; i < axisList.Count; i++)
            {
                Vector2 axis = axisList[i];
                Projection p1 = Projection.GetProjection(axis, vertexList);
                Projection p2 = Projection.GetProjection(axis, rectangle2.vertexList);


                if (!Projection.Overlap(p1, p2))
                    return false;
            }

            return true;
        }


        public override bool IsIntersect(Circle circle)
        {
            return circle.IsIntersect(this);
        }


        /// <summary>
        /// 矩形和点的关系
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool Contains(Vector2 p)
        {
            float minX = center.x - size.x / 2;
            float maxX = center.x + size.x / 2;
            float minY = center.y - size.y / 2;
            float maxY = center.y + size.y / 2;

            return p.x >= minX && p.x <= maxX && p.y >= minY && p.y <= maxY;
        }

        #endregion
    }
}