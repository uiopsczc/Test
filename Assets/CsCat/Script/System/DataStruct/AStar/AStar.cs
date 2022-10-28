using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//每个格子的垂直或者横向的估值是10，不是1
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public class AStar
	{
		protected BinaryHeap<AStarNode> openHeap; // 开放列表

		protected Dictionary<Vector2Int, AStarNode>
			handledDict = new Dictionary<Vector2Int, AStarNode>(); // 在关闭或者开放列表中

		protected List<AStarNode> neighborList = new List<AStarNode>();
		protected List<Vector2Int> neighborOffsetList = new List<Vector2Int>();

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
			openHeap = new BinaryHeap<AStarNode>(size, true, AStarNode.Compare);
		}

		private void Reset()
		{
			foreach (var node in handledDict)
				node.Despawn();
			openHeap.Clear();
			handledDict.Clear();
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

		protected void SetNeighborList(Vector2Int basePoint)
		{
			this.neighborList.Clear();
			neighborOffsetList.Clear();
			SetNeighborOffsetList();
			for (var i = 0; i < neighborOffsetList.Count; i++)
			{
				var neighborOffset = neighborOffsetList[i];
				AddNeighbor(this.neighborList, basePoint, neighborOffset.x, neighborOffset.y);
			}
		}

		protected virtual void SetNeighborOffsetList()
		{
			neighborOffsetList.Add(Vector2IntConst.Top); //上方邻居节点
			neighborOffsetList.Add(Vector2IntConst.RightTop); //右上角邻居节点
			neighborOffsetList.Add(Vector2IntConst.Right); //右侧邻居节点
			neighborOffsetList.Add(Vector2IntConst.RightBottom); //右下角邻居节点
			neighborOffsetList.Add(Vector2IntConst.Bottom); //下方邻居节点
			neighborOffsetList.Add(Vector2IntConst.LeftBottom); //左下角的邻居节点
			neighborOffsetList.Add(Vector2IntConst.Left); //左侧邻居节点
			neighborOffsetList.Add(Vector2IntConst.LeftTop); //左上角邻居节点
		}

		protected void AddNodeToOpenList(AStarNode node)
		{
			node.astarInListType = AStarNodeInListType.Open_List;
			openHeap.Push(node);
			handledDict[node.pos] = node;
		}


		protected void AddNodeToCloseList(AStarNode node)
		{
			node.astarInListType = AStarNodeInListType.Close_List;
			handledDict[node.pos] = node;
		}


		protected virtual float GetG(Vector2Int p1, Vector2Int p2)
		{
			return 0;
		}

		protected virtual float GetH(Vector2Int nPoint, Vector2Int goalPoint)
		{
			return 0;
		}

		protected void AddNeighbor(List<AStarNode> neighborList, Vector2Int basePoint, int dx, int dy)
		{
			int newX = basePoint.x + dx;
			int newY = basePoint.y + dy;

			// 测试边界
			if (!IsInRange(newX, newY))
				return;

			// 跳过不能通过的障碍
			if (!CanPass(newX, newY))
				return;

			// 当前点(p.x,p.y)与该检测邻居点(new_x,new_y)如果是斜线的话， 垂直于当前点(p.x,p.y)与该检测邻居点(new_x,new_y)对角线的两个点中其中一个是阻挡的,则该检测邻居点忽略，不考虑
			// 判断左上角邻居节点
			if (dx == -1 && dy == 1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), newX, newY))
				return;

			// 判断左下角邻居节点
			if (dx == -1 && dy == -1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), newX, newY))
				return;

			// 判断右上角邻居节点
			if (dx == 1 && dy == 1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), newX, newY))
				return;

			// 判断右下角邻居节点
			if (dx == 1 && dy == -1 && IsSkiped(DirectionInfoUtil.GetDirectionInfo(dx, dy), newX, newY))
				return;

			var neighbor_node = PoolCatManagerUtil.Spawn<AStarNode>(null, astarNode => astarNode.Init(newX, newY));
			neighborList.Add(neighbor_node);
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

		protected virtual bool CanPass(int xWithoutOffset, int yWithoutOffset)
		{
			return true;
		}

		//////////////////////////////////////////////////////////////////////////////////////////////
		public List<Vector2Int> Find(int startX, int startY, int goalX, int goalY)
		{
			return Find(new Vector2Int(startX, startY), new Vector2Int(goalX, goalY));
		}

		public List<Vector2Int> Find(Vector2Int startPoint, Vector2Int goalPoint)
		{
			Reset();
			// 为起点赋初值
			AStarNode startNode =
				PoolCatManagerUtil.Spawn<AStarNode>(null, astarNode => astarNode.Init(startPoint.x, startPoint.y));
			startNode.h = GetH(startPoint, goalPoint);
			startNode.f = startNode.h + startNode.g;
			AddNodeToOpenList(startNode);

			while (openHeap.Size > 0)
			{
				// 寻找开启列表中F值最低的格子。我们称它为当前格
				AStarNode checkNode = openHeap.Pop();

				// 把目标格添加进了开启列表，这时候路径被找到
				if (checkNode.pos.Equals(goalPoint))
					return Solve(checkNode);

				// 获得当前附近的节点集合
				SetNeighborList(checkNode.pos);
				foreach (var neighborNode in neighborList)
				{
					float neighborG = checkNode.g + GetG(checkNode.pos, neighborNode.pos);
					if (handledDict.ContainsKey(neighborNode.pos))
					{
						var oldNeighborNode = handledDict[neighborNode.pos];
						if (neighborG < oldNeighborNode.g)
						{
							switch (handledDict[neighborNode.pos].astarInListType)
							{
								case AStarNodeInListType.Close_List:
									neighborNode.parent = checkNode;
									neighborNode.g = neighborG;
									neighborNode.h = GetH(neighborNode.pos, goalPoint);
									neighborNode.f = neighborNode.g + neighborNode.h;
									//更新neighbor_node的值
									AddNodeToOpenList(neighborNode);
									ObjectExtension.Despawn(oldNeighborNode);
									break;
								case AStarNodeInListType.Open_List:
									neighborNode.parent = checkNode;
									neighborNode.g = neighborG;
									neighborNode.h = GetH(neighborNode.pos, goalPoint);
									neighborNode.f = neighborNode.g + neighborNode.h;
									//更新neighbor_node的值
									openHeap.Remove(oldNeighborNode);
									AddNodeToOpenList(neighborNode);
									ObjectExtension.Despawn(oldNeighborNode);
									break;
							}
						}
						else
						{
							//舍弃的进行回收
							ObjectExtension.Despawn(neighborNode);
						}
					}
					else
					{
						neighborNode.parent = checkNode;
						neighborNode.g = neighborG;
						neighborNode.h = GetH(neighborNode.pos, goalPoint);
						neighborNode.f = neighborNode.g + neighborNode.h;
						AddNodeToOpenList(neighborNode); // 排序插入
					}
				}

				// 把当前格切换到关闭列表
				AddNodeToCloseList(checkNode);
			}

			return null;
		}
	}
}