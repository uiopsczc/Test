using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //每个格子的垂直或者横向的估值是10，不是1
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public class AStar
  {
    protected AStarType astarType;
    protected BinaryHeap<AStarNode> open_heap = new BinaryHeap<AStarNode>(); // 开放列表
    protected Dictionary<Vector2Int, AStarNode> closed_dict; // 关闭列表

    protected int left;
    protected int right;
    protected int top;
    protected int bottom;

    public AStar(AStarType astarType = default(AStarType))
    {
      SetAStarType(astarType);
    }

    public void SetAStarType(AStarType astarType)
    {
      this.astarType = astarType;
    }

    public virtual void SetRange(int left, int bottom, int right, int top)
    {
      this.left = Math.Min(left, right);
      this.bottom = Math.Min(bottom, top);
      this.right = Math.Max(left, right);
      this.top = Math.Max(bottom, top);

      int size = (right - left) * (top - bottom);
      open_heap.Capacity = size;
      closed_dict = new Dictionary<Vector2Int, AStarNode>(size);
    }

    private void Reset()
    {
      open_heap.Clear();
      closed_dict.Clear();
    }

    protected List<Vector2Int> Solve(AStarNode node)
    {
      List<Vector2Int> result = new List<Vector2Int>();
      result.Add(node.pos);
      while (node.parent != null)
      {
        result.Add(node.parent.pos);
        node = node.parent;
      }

      result.Reverse();
      return result;
    }

    protected List<AStarNode> GetNeighborList(Vector2Int base_point)
    {
      List<AStarNode> neighbor_list = new List<AStarNode>(8);

      // 增加左上角邻居节点
      AddNeighbor(neighbor_list, base_point, -1, 1);

      // 增加左侧邻居节点
      AddNeighbor(neighbor_list, base_point, -1, 0);

      // 增加左下角的邻居节点
      AddNeighbor(neighbor_list, base_point, -1, -1);

      // 增加上方邻居节点
      AddNeighbor(neighbor_list, base_point, 0, 1);

      // 增加下方邻居节点
      AddNeighbor(neighbor_list, base_point, 0, -1);

      // 增加右上角邻居节点
      AddNeighbor(neighbor_list, base_point, 1, 1);

      // 增加右侧邻居节点
      AddNeighbor(neighbor_list, base_point, 1, 0);

      // 增加右下角邻居节点
      AddNeighbor(neighbor_list, base_point, 1, -1);

      return neighbor_list;

    }

    protected void AddNodeToOpenHeap(AStarNode node)
    {
      open_heap.Add(node);
    }

    protected float GetG(Vector2Int p1, Vector2Int p2)
    {
      int dx = Math.Abs(p1.x - p2.x);
      int dy = Math.Abs(p1.y - p2.y);

      if (astarType == AStarType.Best)
      {
        if (dx == 1 && dy == 1)
          return (dx + dy) * AStarConst.Diagonal_Cost + AStarConst.Lineal_Cost;
        else
          return (dx + dy) * AStarConst.Lineal_Cost + AStarConst.Lineal_Cost;
      }
      else // AStarType.Fast
      {
        if (dx == 1 && dy == 1)
          return AStarConst.Diagonal_Cost;
        else
          return AStarConst.Lineal_Cost;
      }
    }

    protected float GetH(Vector2Int p, Vector2Int goal)
    {
      int dx = Math.Abs(p.x - goal.x);
      int dy = Math.Abs(p.y - goal.y);
      return (dx + dy) * AStarConst.Lineal_Cost;
    }

    protected void AddNeighbor(List<AStarNode> neighbor_list, Vector2Int base_point, int dx, int dy)
    {
      int new_x = base_point.x + dx;
      int new_y = base_point.y + dy;

      // 测试边界
      if (!IsInRange(new_x, new_y))
        return;

      // 跳过不能通过的障碍
      if (!CanPass(new_x, new_y))
        return;

      // 当前点(p.x,p.y)与该检测邻居点(new_x,new_y)如果是斜线的话， 垂直于当前点(p.x,p.y)与该检测邻居点(new_x,new_y)对角线的两个点中其中一个是阻挡的,则该检测邻居点忽略，不考虑
      // 判断左上角邻居节点
      if (dx == -1 && dy == 1 && IsSkiped(DirectionConst.GetDirectionInfo(dx, dy), new_x, new_y))
        return;

      // 判断左下角邻居节点
      if (dx == -1 && dy == -1 && IsSkiped(DirectionConst.GetDirectionInfo(dx, dy), new_x, new_y))
        return;

      // 判断右上角邻居节点
      if (dx == 1 && dy == 1 && IsSkiped(DirectionConst.GetDirectionInfo(dx, dy), new_x, new_y))
        return;

      // 判断右下角邻居节点
      if (dx == 1 && dy == -1 && IsSkiped(DirectionConst.GetDirectionInfo(dx, dy), new_x, new_y))
        return;

      neighbor_list.Add(new AStarNode(new_x, new_y));
    }

    protected bool IsSkiped(DirectionInfo directionInfo, int x, int y)
    {
      int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
      // 如果当前位置为左上角，则判断其下方和右侧是否为不可穿过的障碍
      if (directionInfo.Equals(DirectionConst.GetDirectionInfo("left_top")))
      {
        x1 = x;
        y1 = y - 1;
        x2 = x + 1;
        y2 = y;
      }

      // 如果当前位置为右上角，则判断其下方和左侧是否为不可穿过的障碍
      if (directionInfo.Equals(DirectionConst.GetDirectionInfo("right_top")))
      {
        x1 = x;
        y1 = y - 1;
        x2 = x - 1;
        y2 = y;
      }

      // 如果当前位置为左下角，则判断其上方和右侧是否为不可穿过的障碍
      if (directionInfo.Equals(DirectionConst.GetDirectionInfo("left_bottom")))
      {
        x1 = x;
        y1 = y + 1;
        x2 = x + 1;
        y2 = y;
      }

      // 如果当前位置为右下角，则判断其上方和左侧是否为不可穿过的障碍
      if (directionInfo.Equals(DirectionConst.GetDirectionInfo("right_bottom")))
      {
        x1 = x;
        y1 = y + 1;
        x2 = x - 1;
        y2 = y;
      }

      // 根据计算后获得的坐标(x1,y1),(x2,y2)，判断其是否为不可穿越的障碍，如果是，则跳过该邻居节点
      if (!CanPass(x1, y1) || !CanPass(x2, y2))
        return true;
      return false;
    }

    protected bool IsInRange(int x, int y)
    {
      if (x < left || x > right)
        return false;
      if (y < bottom || y > top)
        return false;
      return true;
    }

    protected virtual bool CanPass(int x, int y)
    {
      return true;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    public List<Vector2Int> Find(int start_x, int start_y, int goal_x, int goal_y)
    {
      return Find(new Vector2Int(start_x, start_y), new Vector2Int(goal_x, goal_y));
    }

    public List<Vector2Int> Find(Vector2Int start_pos, Vector2Int goal_pos)
    {
      Reset();
      // 为起点赋初值
      AStarNode startNode = new AStarNode(start_pos.x, start_pos.y);
      startNode.h = GetH(start_pos, goal_pos);
      startNode.f = startNode.h + startNode.g;
      open_heap.Add(startNode);

      while (open_heap.Count > 0)
      {
        // 寻找开启列表中F值最低的格子。我们称它为当前格
        AStarNode check_node = (AStarNode) open_heap.Remove();

        // 把目标格添加进了开启列表，这时候路径被找到
        if (check_node.pos.Equals(goal_pos))
        {
          closed_dict[check_node.pos] = check_node;
          return Solve(check_node);
        }
        else
        {
          // 获得当前附近的节点集合
          List<AStarNode> neighbor_list = GetNeighborList(check_node.pos);
          for (int i = 0; i < neighbor_list.Count; i++)
          {
            // 计算邻居节点的耗费值
            AStarNode neighbor_node = (AStarNode) neighbor_list[i];

            float neighbor_g = check_node.g + GetG(check_node.pos, neighbor_node.pos);
            if (closed_dict.ContainsKey(neighbor_node.pos))
            {
              if (neighbor_g < neighbor_node.g)
              {
                neighbor_node.parent = check_node;
                neighbor_node.g = neighbor_g;
                neighbor_node.h = GetH(neighbor_node.pos, goal_pos);
                neighbor_node.f = neighbor_node.g + neighbor_node.h;
                closed_dict.Remove(neighbor_node.pos);
                AddNodeToOpenHeap(neighbor_node);
              }
            }
            else if (open_heap.Contains(neighbor_node))
            {
              if (neighbor_g < neighbor_node.g)
              {
                neighbor_node.parent = check_node;
                neighbor_node.g = neighbor_g;
                neighbor_node.h = GetH(neighbor_node.pos, goal_pos);
                neighbor_node.f = neighbor_node.g + neighbor_node.h;
                open_heap.Remove(neighbor_node);
                AddNodeToOpenHeap(neighbor_node);
              }
            }
            else
            {
              neighbor_node.parent = check_node;
              neighbor_node.g = neighbor_g;
              neighbor_node.h = GetH(neighbor_node.pos, goal_pos);
              neighbor_node.f = neighbor_node.g + neighbor_node.h;
              AddNodeToOpenHeap(neighbor_node); // 排序插入
            }
          }

          // 把当前格切换到关闭列表
          closed_dict[check_node.pos] = check_node;
        }
      }

      return null;
    }

  }
}