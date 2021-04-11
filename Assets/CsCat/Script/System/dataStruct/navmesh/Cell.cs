using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
  public class Cell : Triangle, IComparable<Cell>
  {
    #region field

    /// <summary>
    /// 在数组中的索引值
    /// </summary>
    public int index;

    /// <summary>
    /// 与该三角型连接的三角型索引， -1表示改边没有连接
    /// </summary>
    public List<int> link_list;

    public int session_id;
    public float f;
    public float h;
    public float g;
    public bool is_open;
    public Cell parent;

    /// <summary>
    /// 入/出边的index，与cell中的SIDE_AB，SIDE_BC，SIDE_CA
    /// </summary>
    public int arrival_wall;

    /// <summary>
    /// 每条边的中点
    /// </summary>
    public List<Vector2> wall_middle_point_list;

    /// <summary>
    /// 每两条边的中点距离(0-1,1-2,2-0)
    /// </summary>
    public List<float> wall_distance_list;

    #endregion

    #region ctor

    public Cell(Vector2 p1, Vector2 p2, Vector2 p3)
      : base(p1, p2, p3)
    {
      Init();
    }

    #endregion

    #region public method

    public int GetWallDistance(int in_index)
    {
      int out_index = arrival_wall;

      if (in_index == 0)
      {
        if (out_index == 1)
          return 0;
        else if (out_index == 2)
          return 2;
      }
      else if (in_index == 1)
      {
        if (out_index == 0)
          return 0;
        else if (out_index == 2)
          return 1;
      }
      else if (in_index == 2)
      {
        if (out_index == 0)
          return 2;
        else if (out_index == 1)
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
      if (GetLink(Side_AB) == -1 && cellB.RequestLink(point_A, point_B, this))
      {
        SetLink(Side_AB, cellB);
      }
      else if (GetLink(Side_BC) == -1 && cellB.RequestLink(point_B, point_C, this))
      {
        SetLink(Side_BC, cellB);
      }
      else if (GetLink(Side_CA) == -1 && cellB.RequestLink(point_C, point_A, this))
      {
        SetLink(Side_CA, cellB);
      }
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
      if (index == link_list[0])
      {
        arrival_wall = 0;
        return 0;
      }
      else if (index == link_list[1])
      {
        arrival_wall = 1;
        return 1;
      }
      else if (index == link_list[2])
      {
        arrival_wall = 2;
        return 2;
      }

      return -1;
    }



    public int CompareTo(Cell other)
    {
      if (f < other.f)
        return -1;
      else if (f > other.f)
        return 1;
      else
        return 0;
    }

    #endregion

    #region private method

    private void Init()
    {
      link_list = new List<int>();
      link_list.Add(-1);
      link_list.Add(-1);
      link_list.Add(-1);

      wall_middle_point_list = new List<Vector2>();
      wall_distance_list = new List<float>();
      // 计算中心点
      /*
          wallMidPoints[0] = this.PointA/2+this.PointB/2;
          wallMidPoints[1] = this.PointB/2+this.PointC/2;
          wallMidPoints[2] = this.PointC/2+this.PointA/2;
           * */
      wall_middle_point_list.Add(this.point_A / 2 + this.point_B / 2);
      wall_middle_point_list.Add(this.point_B / 2 + this.point_C / 2);
      wall_middle_point_list.Add(this.point_C / 2 + this.point_A / 2);

      // 计算每两条边的中点距离
      /*
          wallDistances[0] = (wallMidPoints[0]-wallMidPoints[1]).Length();
          wallDistances[1] = (wallMidPoints[1] - wallMidPoints[2]).Length();
          wallDistances[2] = (wallMidPoints[2] - wallMidPoints[0]).Length();
           * */
      wall_distance_list.Add((wall_middle_point_list[0] - wall_middle_point_list[1]).magnitude);
      wall_distance_list.Add((wall_middle_point_list[1] - wall_middle_point_list[2]).magnitude);
      wall_distance_list.Add((wall_middle_point_list[2] - wall_middle_point_list[0]).magnitude);
    }

    //获得两个点的相邻三角型
    private bool RequestLink(Vector2 pA, Vector2 pB, Cell caller)
    {
      if (this.point_A.Equals(pA))
      {
        if (this.point_B.Equals(pB))
        {
          link_list[Side_AB] = caller.index;
          return true;
        }
        else if (this.point_C.Equals(pB))
        {
          link_list[Side_CA] = caller.index;
          return true;
        }
      }
      else if (this.point_B.Equals(pA))
      {
        if (this.point_A.Equals(pB))
        {
          link_list[Side_AB] = caller.index;
          return true;
        }
        else if (this.point_C.Equals(pB))
        {
          link_list[Side_BC] = caller.index;
          return true;
        }
      }
      else if (this.point_C.Equals(pA))
      {
        if (this.point_A.Equals(pB))
        {
          link_list[Side_CA] = caller.index;
          return (true);
        }
        else if (this.point_B.Equals(pB))
        {
          link_list[Side_BC] = caller.index;
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
      link_list[side] = cell.index;
    }

    /// <summary>
    /// 取得指定边的相邻三角型的索引
    /// </summary>
    /// <param name="side"></param>
    /// <returns></returns>
    private int GetLink(int side)
    {
      return link_list[side];
    }

    #endregion
  }
}

