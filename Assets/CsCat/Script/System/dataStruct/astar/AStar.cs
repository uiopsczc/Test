using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//每个格子的垂直或者横向的估值是10，不是1
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public class AStar
	{
		protected BinaryHeap<AStarNode> open_heap; // 开放列表

		protected Dictionary<Vector2Int, AStarNode>
			handled_dict = new Dictionary<Vector2Int, AStarNode>(); // 在关闭或者开放列表中

		protected List<AStarNode> neighbor_list = new List<AStarNode>();
		protected List<Vector2Int> neighbor_offset_list = new List<Vector2Int>();

		protected int left;
		protected int right;
		protected int top;
		protected int bottom;


		public virtual void SetRange(int left, int bottom, int right, int top)
		{
			this.left = Math.Min(left, right);
			this.bottom = Math.Min(bottom, top);
			this.right = Math.Max(left, right);
			this.top = Math.Max(bottom, top);

			int size = (right - left) * (top - bottom);
			open_heap = new BinaryHeap<AStarNode>(size, true, AStarNode.Compare);
		}

		private void Reset()
		{
			foreach (var node in handled_dict)
				node.Despawn();
			open_heap.Clear();
			handled_dict.Clear();
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

		protected void SetNeighborList(Vector2Int base_point)
		{
			this.neighbor_list.Clear();
			neighbor_offset_list.Clear();
			SetNeighborOffsetList();
			foreach (var neighbor_offset in neighbor_offset_list)
				AddNeighbor(this.neighbor_list, base_point, neighbor_offset.x, neighbor_offset.y);
		}

		protected virtual void SetNeighborOffsetList()
		{
			neighbor_offset_list.Add(Vector2IntConst.Top); //上方邻居节点
			neighbor_offset_list.Add(Vector2IntConst.RightTop); //右上角邻居节点
			neighbor_offset_list.Add(Vector2IntConst.Right); //右侧邻居节点
			neighbor_offset_list.Add(Vector2IntConst.RightBottom); //右下角邻居节点
			neighbor_offset_list.Add(Vector2IntConst.Bottom); //下方邻居节点
			neighbor_offset_list.Add(Vector2IntConst.LeftBottom); //左下角的邻居节点
			neighbor_offset_list.Add(Vector2IntConst.Left); //左侧邻居节点
			neighbor_offset_list.Add(Vector2IntConst.LeftTop); //左上角邻居节点
		}

		protected void AddNodeToOpenList(AStarNode node)
		{
			node.astarInListType = AStarNodeInListType.Open_List;
			open_heap.Push(node);
			handled_dict[node.pos] = node;
		}


		protected void AddNodeToCloseList(AStarNode node)
		{
			node.astarInListType = AStarNodeInListType.Close_List;
			handled_dict[node.pos] = node;
		}


		protected virtual float GetG(Vector2Int p1, Vector2Int p2)
		{
			return 0;
		}

		protected virtual float GetH(Vector2Int p, Vector2Int goal)
		{
			return 0;
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
			if (dx == -1 && dy == 1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), new_x, new_y))
				return;

			// 判断左下角邻居节点
			if (dx == -1 && dy == -1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), new_x, new_y))
				return;

			// 判断右上角邻居节点
			if (dx == 1 && dy == 1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), new_x, new_y))
				return;

			// 判断右下角邻居节点
			if (dx == 1 && dy == -1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), new_x, new_y))
				return;

			var neighbor_node = PoolCatManagerUtil.Spawn<AStarNode>(null, astarNode => astarNode.Init(new_x, new_y));
			neighbor_list.Add(neighbor_node);
		}

		protected bool IsSkiped(DirectionInfo directionInfo, int x, int y)
		{
			int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
			// 如果当前位置为左上角，则判断其下方和右侧是否为不可穿过的障碍
			if (directionInfo.Equals(DirectionInfoConst.LeftTopDirectionInfo))
			{
				x1 = x;
				y1 = y - 1;
				x2 = x + 1;
				y2 = y;
			}

			// 如果当前位置为右上角，则判断其下方和左侧是否为不可穿过的障碍
			if (directionInfo.Equals(DirectionInfoConst.RightTopDirectionInfo))
			{
				x1 = x;
				y1 = y - 1;
				x2 = x - 1;
				y2 = y;
			}

			// 如果当前位置为左下角，则判断其上方和右侧是否为不可穿过的障碍
			if (directionInfo.Equals(DirectionInfoConst.LeftBottomDirectionInfo))
			{
				x1 = x;
				y1 = y + 1;
				x2 = x + 1;
				y2 = y;
			}

			// 如果当前位置为右下角，则判断其上方和左侧是否为不可穿过的障碍
			if (directionInfo.Equals(DirectionInfoConst.RightBottomDirectionInfo))
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

		public List<Vector2Int> Find(Vector2Int start_point, Vector2Int goal_point)
		{
			Reset();
			// 为起点赋初值
			AStarNode startNode =
				PoolCatManagerUtil.Spawn<AStarNode>(null, astarNode => astarNode.Init(start_point.x, start_point.y));
			startNode.h = GetH(start_point, goal_point);
			startNode.f = startNode.h + startNode.g;
			AddNodeToOpenList(startNode);

			while (open_heap.Size > 0)
			{
				// 寻找开启列表中F值最低的格子。我们称它为当前格
				AStarNode check_node = open_heap.Pop();

				// 把目标格添加进了开启列表，这时候路径被找到
				if (check_node.pos.Equals(goal_point))
					return Solve(check_node);

				// 获得当前附近的节点集合
				SetNeighborList(check_node.pos);
				foreach (var neighbor_node in neighbor_list)
				{
					float neighbor_g = check_node.g + GetG(check_node.pos, neighbor_node.pos);
					if (handled_dict.ContainsKey(neighbor_node.pos))
					{
						var old_neighbor_node = handled_dict[neighbor_node.pos];
						if (neighbor_g < old_neighbor_node.g)
						{
							switch (handled_dict[neighbor_node.pos].astarInListType)
							{
								case AStarNodeInListType.Close_List:
									neighbor_node.parent = check_node;
									neighbor_node.g = neighbor_g;
									neighbor_node.h = GetH(neighbor_node.pos, goal_point);
									neighbor_node.f = neighbor_node.g + neighbor_node.h;
									//更新neighbor_node的值
									AddNodeToOpenList(neighbor_node);
									old_neighbor_node.Despawn();
									break;
								case AStarNodeInListType.Open_List:
									neighbor_node.parent = check_node;
									neighbor_node.g = neighbor_g;
									neighbor_node.h = GetH(neighbor_node.pos, goal_point);
									neighbor_node.f = neighbor_node.g + neighbor_node.h;
									//更新neighbor_node的值
									open_heap.Remove(old_neighbor_node);
									AddNodeToOpenList(neighbor_node);
									old_neighbor_node.Despawn();
									break;
							}
						}
						else
						{
							//舍弃的进行回收
							neighbor_node.Despawn();
						}
					}
					else
					{
						neighbor_node.parent = check_node;
						neighbor_node.g = neighbor_g;
						neighbor_node.h = GetH(neighbor_node.pos, goal_point);
						neighbor_node.f = neighbor_node.g + neighbor_node.h;
						AddNodeToOpenList(neighbor_node); // 排序插入
					}
				}

				// 把当前格切换到关闭列表
				AddNodeToCloseList(check_node);
			}

			return null;
		}
	}
}