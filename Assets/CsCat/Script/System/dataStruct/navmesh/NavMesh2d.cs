using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  public class NavMesh2d
  {
    #region field

    // Path finding session ID. This Identifies each pathfinding session
    // so we do not need to clear out old data in the cells from previous sessions.
    /// <summary>
    /// 记录当前节点有没有被访问过，如果访问过，则一定在openList中或closeList中；否则不在openList中和closeList中
    /// </summary>
    private static int path_session_id = 0;

    private List<Cell> cell_list;
    private BinaryHeap<Cell> open_list;
    private List<Cell> closed_list;

    #endregion

    #region ctor

    public NavMesh2d(List<Cell> cell_list)
    {
      this.cell_list = cell_list;
      open_list = new BinaryHeap<Cell>(cell_list.Count,true, Cell.Compare);
      closed_list = new List<Cell>();
    }

    #endregion

    #region public method

    public Cell GetCell(int index)
    {
      return cell_list[index];
    }

    //找出给定点所在的三角型
    public Cell FindClosestCell(Vector2 point)
    {
      foreach (Cell cell in cell_list)
      {
        if (cell.IsPointIn(point))
        {
          return cell;
        }
      }

      return null;
    }

    public List<Vector2> FindPath(Vector2 start_point, Vector2 end_point)
    {
      path_session_id++;

      Vector2 start_pos = start_point;
      Vector2 end_pos = end_point;
      Cell start_cell = FindClosestCell(start_pos);
      Cell end_cell = FindClosestCell(end_pos);
      if (start_cell == null || end_cell == null)
      {
        return null;
      }

      List<Vector2> outPath;

      if (start_cell == end_cell)
      {
        outPath = new List<Vector2> { start_point, end_point };
      }
      else
      {
        outPath = BuildPath(start_cell, start_pos, end_cell, end_pos);
      }

      return outPath;
    }

    /// <summary>
    /// 构建路径
    /// </summary>
    /// <param name="start_cell"></param>
    /// <param name="start_pos"></param>
    /// <param name="end_cell"></param>
    /// <param name="end_pos"></param>
    /// <returns></returns>
    public List<Vector2> BuildPath(Cell start_cell, Vector2 start_pos,
      Cell end_cell, Vector2 end_pos)
    {
      open_list.Clear();
      closed_list.Clear();

      end_cell.g = 0;
      end_cell.ComputeH(end_pos, start_pos);
      end_cell.ComputeF();

      end_cell.is_open = false;
      end_cell.parent = null;
      end_cell.session_id = path_session_id;
      open_list.Push(end_cell);

      bool is_found_path = false; //是否找到路径
      Cell curr_node; //当前节点
      Cell adjacent_tmp = null; //当前节点的邻接三角型
      while (open_list.Size > 0)
      {
        // 1. 把当前节点从开放列表删除, 加入到封闭列表
        curr_node = open_list.Pop();
        closed_list.Add(curr_node);

        //路径是在同一个三角形内
        if (curr_node == start_cell)
        {
          is_found_path = true;
          break;
        }

        // 2. 对当前节点相邻的每一个节点依次执行以下步骤:
        //所有邻接三角型
        int adjacent_id;
        for (int i = 0; i < 3; i++)
        {
          adjacent_id = curr_node.link_list[i];
          // 3. 如果该相邻节点不可通行或者该相邻节点已经在封闭列表中,
          //    则什么操作也不执行,继续检验下一个节点;
          if (adjacent_id < 0) //不能通过
          {
            continue;
          }
          else
          {
            adjacent_tmp = cell_list[adjacent_id];
          }

          if (adjacent_tmp != null)
          {
            if (adjacent_tmp.session_id != path_session_id)
            {
              // 4. 如果该相邻节点不在开放列表中,则将该节点添加到开放列表中, 
              //    并将该相邻节点的父节点设为当前节点,同时保存该相邻节点的G和F值;
              adjacent_tmp.session_id = path_session_id;
              adjacent_tmp.parent = curr_node;
              adjacent_tmp.is_open = true;

              // remember the side this caller is entering from
              adjacent_tmp.SetAndGetArrivalWall(curr_node.index);

              //计算G值
              //当前的g值+（当前边的进入边的中点-当前边出边的中点）的距离
              Vector2 p1;
              if (curr_node == end_cell)
                p1 = end_pos;
              else
                p1 = curr_node.line_list[curr_node.arrival_wall].center;
              adjacent_tmp.g = curr_node.g + adjacent_tmp.ComputeGIncrease(p1, curr_node.line_list[i].center);

              //计算H值
              adjacent_tmp.ComputeH(adjacent_tmp.line_list[adjacent_tmp.arrival_wall].center, start_pos);
              //计算F值
              adjacent_tmp.ComputeF();

              //放入开放列表并排序
              open_list.Push(adjacent_tmp);


            }
            else
            {
              // 5. 如果该相邻节点在开放列表中, 
              //    则判断若经由当前节点到达该相邻节点的G值是否小于原来保存的G值,
              //    若小于,则将该相邻节点的父节点设为当前节点,并重新设置该相邻节点的G和F值
              if (adjacent_tmp.is_open) //已经在openList中
              {
                //计算G值
                //当前的g值+（当前边的进入边的中点-当前边出边的中点）的距离
                Vector2 p1;
                if (curr_node == end_cell)
                  p1 = end_pos;
                else
                  p1 = curr_node.line_list[curr_node.arrival_wall].center;
                float increase_g = adjacent_tmp.ComputeGIncrease(p1, curr_node.line_list[i].center);

                if (curr_node.g + increase_g < adjacent_tmp.g)
                {
                  adjacent_tmp.g = curr_node.g;
                  adjacent_tmp.parent = curr_node;

                  // remember the side this caller is entering from
                  adjacent_tmp.SetAndGetArrivalWall(curr_node.index);

                  //重新设置在heap中的位置
                  open_list.Remove(adjacent_tmp);
                  open_list.Push(adjacent_tmp);
                }
              }
              else //已在closeList中
              {
                adjacent_tmp = null;
                continue;
              }
            }
          }
        }
      }

      //由网格路径生成路径

      if (is_found_path)
      {
        return GetPath(start_pos, end_pos);
      }
      else
      {
        return null;
      }

      /*返回cell
          List<Vector2> ret = new List<Vector2>();
          foreach (Cell cell in GetCellPath())
          {
              ret.Add(cell.Center);
          }
          return ret;
           * */
    }

    #endregion

    #region private method

    /// <summary>
    /// 根据经过的三角形返回路径点(下一个拐角点法)
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private List<Vector2> GetPath(Vector2 start, Vector2 end)
    {
      //经过的三角形
      List<Cell> cellPath = GetCellPath();
      //没有路径
      if (cellPath == null || cellPath.Count == 0)
      {
        return null;
      }

      //保存最终的路径
      List<Vector2> path = new List<Vector2>();

      //开始点
      path.Add(start);
      //起点与终点在同一三角形中
      if (cellPath.Count == 1)
      {
        path.Add(end); //结束点
        return path;
      }

      //获取路点
      WayPoint wayPoint = new WayPoint(cellPath[0], start);
      while (!wayPoint.position.Equals(end))
      {
        wayPoint = this.GetFurthestWayPoint(wayPoint, cellPath, end);
        path.Add(wayPoint.position);
      }

      return path;
    }

    /// <summary>
    /// 路径经过的网格	
    /// </summary>
    /// <returns></returns>
    private List<Cell> GetCellPath()
    {
      List<Cell> path = new List<Cell>();
      if (closed_list.IsNullOrEmpty())
        return null;
      Cell st = closed_list[closed_list.Count - 1];
      path.Add(st);

      while (st.parent != null)
      {
        path.Add(st.parent);
        st = st.parent;
      }

      return path;
    }

    /// <summary>
    /// 下一个拐点 wayPoint 当前所在路点 cellPath 网格路径 end 终点	
    /// </summary>
    /// <param name="wayPoint"></param>
    /// <param name="cell_path"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private WayPoint GetFurthestWayPoint(WayPoint wayPoint, List<Cell> cell_path, Vector2 end)
    {
      Vector2 start_point = wayPoint.position; //当前所在路径点
      Cell cell = wayPoint.cell;
      Cell last_cell = cell;
      int start_index = cell_path.IndexOf(cell); //开始路点所在的网格索引
      Line outside = cell.line_list[cell.arrival_wall]; //路径线在网格中的穿出边
      Vector2 last_point_A = outside.point_A;
      Vector2 last_point_B = outside.point_B;
      Line last_line_A = new Line(start_point, last_point_A);
      Line last_line_B = new Line(start_point, last_point_B);
      Vector2 test_point_A, test_point_B; //要测试的点

      Cell last_point_cell_A = last_cell;
      Cell last_point_cell_B = last_cell;



      for (int i = start_index + 1; i < cell_path.Count; i++)
      {
        cell = cell_path[i];
        outside = cell.line_list[cell.arrival_wall];
        if (i == cell_path.Count - 1)
        {
          test_point_A = end;
          test_point_B = end;
        }
        else
        {
          test_point_A = outside.point_A;
          test_point_B = outside.point_B;
        }

        if (!last_point_A.Equals(test_point_A))
        {
          if (last_line_B.ClassifyPoint(test_point_A) == PointClassification.RIGHT_SIDE)
          {
            //路点
            return new WayPoint(last_point_cell_B, last_point_B);
          }
          else
          {
            if (last_line_A.ClassifyPoint(test_point_A) != PointClassification.LEFT_SIDE)
            {
              last_point_A = test_point_A;
              last_point_cell_A = cell;
              //重设直线
              //						lastLineA.PointB = lastPtA;
              //						lastLineB.PointB = lastPtB;
              last_line_A = new Line(last_line_A.point_A, last_point_A);
              last_line_B = new Line(last_line_B.point_A, last_point_B);
            }
          }
        }

        if (!last_point_B.Equals(test_point_B))
        {
          if (last_line_A.ClassifyPoint(test_point_B) == PointClassification.LEFT_SIDE)
          {
            //路径点
            return new WayPoint(last_point_cell_A, last_point_A);
          }
          else
          {
            if (last_line_B.ClassifyPoint(test_point_B) != PointClassification.RIGHT_SIDE)
            {
              last_point_B = test_point_B;
              last_point_cell_B = cell;
              //重设直线
              //						lastLineA.PointB = lastPtA;
              //						lastLineB.PointB = lastPtB;
              last_line_A = new Line(last_line_A.point_A, last_point_A);
              last_line_B = new Line(last_line_B.point_A, last_point_B);
            }
          }
        }
      }

      return new WayPoint(cell_path[cell_path.Count - 1], end); //终点
    }

    #endregion




  }
}
