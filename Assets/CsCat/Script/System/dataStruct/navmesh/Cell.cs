using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
    public class Cell : Triangle
    {
        /// <summary>
        /// 在数组中的索引值
        /// </summary>
        public int index;

        /// <summary>
        /// 与该三角型连接的三角型索引， -1表示改边没有连接
        /// </summary>
        public List<int> linkList;

        public int sessionId;
        public float f;
        public float h;
        public float g;
        public bool isOpen;
        public Cell parent;

        /// <summary>
        /// 入/出边的index，与cell中的SIDE_AB，SIDE_BC，SIDE_CA
        /// </summary>
        public int arrivalWall;

        /// <summary>
        /// 每条边的中点
        /// </summary>
        public List<Vector2> wallMiddlePointList;

        /// <summary>
        /// 每两条边的中点距离(0-1,1-2,2-0)
        /// </summary>
        public List<float> wallDistanceList;


        public Cell(Vector2 p1, Vector2 p2, Vector2 p3)
            : base(p1, p2, p3)
        {
            Init();
        }


        public int GetWallDistance(int inIndex)
        {
            int outIndex = arrivalWall;

            if (inIndex == 0)
            {
                if (outIndex == 1)
                    return 0;
                if (outIndex == 2)
                    return 2;
            }
            else if (inIndex == 1)
            {
                if (outIndex == 0)
                    return 0;
                if (outIndex == 2)
                    return 1;
            }
            else if (inIndex == 2)
            {
                if (outIndex == 0)
                    return 2;
                if (outIndex == 1)
                    return 1;
            }

            return -1;
        }

        /// <summary>
        /// 检查并设置当前三角型与cellB的连接关系（方法会同时设置cellB与该三角型的连接）
        /// </summary>
        /// <param name="cellB"></param>
        public void CheckAndLink(Cell cellB)
        {
            if (GetLink(Side_AB) == -1 && cellB.RequestLink(pointA, pointB, this))
                SetLink(Side_AB, cellB);
            else if (GetLink(Side_BC) == -1 && cellB.RequestLink(pointB, pointC, this))
                SetLink(Side_BC, cellB);
            else if (GetLink(Side_CA) == -1 && cellB.RequestLink(pointC, pointA, this))
                SetLink(Side_CA, cellB);
        }

        /// <summary>
        /// 计算估价（G）增长
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public float ComputeGIncrease(Vector2 p1, Vector2 p2)
        {
            return (p1 - p2).magnitude;
        }

        /// <summary>
        /// 计算估价（h） p1进入边的中点（如果是第一个cell的，就是起始点或终点）, goal目标点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="goal"></param>
        public void ComputeH(Vector2 p1, Vector2 goal)
        {
            h = (p1 - goal).magnitude;
        }

        /// <summary>
        /// 计算估价（h）
        /// </summary>
        public void ComputeF()
        {
            f = g + h;
        }

        /// <summary>
        /// 记录路径从上一个节点进入该节点的边（如果从终点开始寻路即为穿出边）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int SetAndGetArrivalWall(int index)
        {
            if (index == linkList[0])
            {
                arrivalWall = 0;
                return 0;
            }

            if (index == linkList[1])
            {
                arrivalWall = 1;
                return 1;
            }

            if (index == linkList[2])
            {
                arrivalWall = 2;
                return 2;
            }

            return -1;
        }


        public static int Compare(Cell data1, Cell data2)
        {
            if (data1.f < data2.f)
                return -1;
            if (data1.f > data2.f)
                return 1;
            return 0;
        }


        private void Init()
        {
            linkList = new List<int> {-1, -1, -1};

            wallMiddlePointList = new List<Vector2>();
            wallDistanceList = new List<float>();
            // 计算中心点
            /*
                wallMidPoints[0] = this.PointA/2+this.PointB/2;
                wallMidPoints[1] = this.PointB/2+this.PointC/2;
                wallMidPoints[2] = this.PointC/2+this.PointA/2;
                 * */
            wallMiddlePointList.Add(this.pointA / 2 + this.pointB / 2);
            wallMiddlePointList.Add(this.pointB / 2 + this.pointC / 2);
            wallMiddlePointList.Add(this.pointC / 2 + this.pointA / 2);

            // 计算每两条边的中点距离
            /*
                wallDistances[0] = (wallMidPoints[0]-wallMidPoints[1]).Length();
                wallDistances[1] = (wallMidPoints[1] - wallMidPoints[2]).Length();
                wallDistances[2] = (wallMidPoints[2] - wallMidPoints[0]).Length();
                 * */
            wallDistanceList.Add((wallMiddlePointList[0] - wallMiddlePointList[1]).magnitude);
            wallDistanceList.Add((wallMiddlePointList[1] - wallMiddlePointList[2]).magnitude);
            wallDistanceList.Add((wallMiddlePointList[2] - wallMiddlePointList[0]).magnitude);
        }

        //获得两个点的相邻三角型
        private bool RequestLink(Vector2 pA, Vector2 pB, Cell caller)
        {
            if (this.pointA.Equals(pA))
            {
                if (this.pointB.Equals(pB))
                {
                    linkList[Side_AB] = caller.index;
                    return true;
                }

                if (this.pointC.Equals(pB))
                {
                    linkList[Side_CA] = caller.index;
                    return true;
                }
            }
            else if (this.pointB.Equals(pA))
            {
                if (this.pointA.Equals(pB))
                {
                    linkList[Side_AB] = caller.index;
                    return true;
                }

                if (this.pointC.Equals(pB))
                {
                    linkList[Side_BC] = caller.index;
                    return true;
                }
            }
            else if (this.pointC.Equals(pA))
            {
                if (this.pointA.Equals(pB))
                {
                    linkList[Side_CA] = caller.index;
                    return (true);
                }

                if (this.pointB.Equals(pB))
                {
                    linkList[Side_BC] = caller.index;
                    return (true);
                }
            }

            return false;
        }

        /// <summary>
        /// 设置side指定的边的连接到caller的索引
        /// </summary>
        /// <param name="side"></param>
        /// <param name="cell"></param>
        private void SetLink(int side, Cell cell)
        {
            linkList[side] = cell.index;
        }

        /// <summary>
        /// 取得指定边的相邻三角型的索引
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        private int GetLink(int side)
        {
            return linkList[side];
        }
    }
}