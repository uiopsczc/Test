using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
	public class NavMesh2d
	{
		// Path finding session ID. This Identifies each pathfinding session
		// so we do not need to clear out old data in the cells from previous sessions.
		/// <summary>
		/// 记录当前节点有没有被访问过，如果访问过，则一定在openList中或closeList中；否则不在openList中和closeList中
		/// </summary>
		private static int pathSessionId = 0;

		private List<Cell> cellList;
		private BinaryHeap<Cell> openList;
		private List<Cell> closedList;


		public NavMesh2d(List<Cell> cellList)
		{
			this.cellList = cellList;
			openList = new BinaryHeap<Cell>(cellList.Count, true, Cell.Compare);
			closedList = new List<Cell>();
		}


		public Cell GetCell(int index)
		{
			return cellList[index];
		}

		//找出给定点所在的三角型
		public Cell FindClosestCell(Vector2 point)
		{
			for (var i = 0; i < cellList.Count; i++)
			{
				Cell cell = cellList[i];
				if (cell.IsPointIn(point))
					return cell;
			}

			return null;
		}

		public List<Vector2> FindPath(Vector2 startPoint, Vector2 endPoint)
		{
			pathSessionId++;

			Vector2 startPos = startPoint;
			Vector2 endPos = endPoint;
			Cell startCell = FindClosestCell(startPos);
			Cell endCell = FindClosestCell(endPos);
			if (startCell == null || endCell == null)
				return null;

			var outPath = Equals(startCell, endCell)
				? new List<Vector2> { startPoint, endPoint }
				: BuildPath(startCell, startPos, endCell, endPos);

			return outPath;
		}

		/// <summary>
		/// 构建路径
		/// </summary>
		/// <param name="startCell"></param>
		/// <param name="startPos"></param>
		/// <param name="endCell"></param>
		/// <param name="endPos"></param>
		/// <returns></returns>
		public List<Vector2> BuildPath(Cell startCell, Vector2 startPos,
			Cell endCell, Vector2 endPos)
		{
			openList.Clear();
			closedList.Clear();

			endCell.g = 0;
			endCell.ComputeH(endPos, startPos);
			endCell.ComputeF();

			endCell.isOpen = false;
			endCell.parent = null;
			endCell.sessionId = pathSessionId;
			openList.Push(endCell);

			bool isFoundPath = false; //是否找到路径
			Cell currNode; //当前节点
			Cell adjacentTmp = null; //当前节点的邻接三角型
			while (openList.Size > 0)
			{
				// 1. 把当前节点从开放列表删除, 加入到封闭列表
				currNode = openList.Pop();
				closedList.Add(currNode);

				//路径是在同一个三角形内
				if (currNode == startCell)
				{
					isFoundPath = true;
					break;
				}

				// 2. 对当前节点相邻的每一个节点依次执行以下步骤:
				//所有邻接三角型
				int adjacentId;
				for (int i = 0; i < 3; i++)
				{
					adjacentId = currNode.linkList[i];
					// 3. 如果该相邻节点不可通行或者该相邻节点已经在封闭列表中,
					//    则什么操作也不执行,继续检验下一个节点;
					if (adjacentId < 0) //不能通过

						continue;
					adjacentTmp = cellList[adjacentId];

					if (adjacentTmp != null)
					{
						if (adjacentTmp.sessionId != pathSessionId)
						{
							// 4. 如果该相邻节点不在开放列表中,则将该节点添加到开放列表中, 
							//    并将该相邻节点的父节点设为当前节点,同时保存该相邻节点的G和F值;
							adjacentTmp.sessionId = pathSessionId;
							adjacentTmp.parent = currNode;
							adjacentTmp.isOpen = true;

							// remember the side this caller is entering from
							adjacentTmp.SetAndGetArrivalWall(currNode.index);

							//计算G值
							//当前的g值+（当前边的进入边的中点-当前边出边的中点）的距离
							var p1 = Equals(currNode, endCell)
								? endPos
								: currNode.lineList[currNode.arrivalWall].center;
							adjacentTmp.g =
								currNode.g + adjacentTmp.ComputeGIncrease(p1, currNode.lineList[i].center);

							//计算H值
							adjacentTmp.ComputeH(adjacentTmp.lineList[adjacentTmp.arrivalWall].center, startPos);
							//计算F值
							adjacentTmp.ComputeF();

							//放入开放列表并排序
							openList.Push(adjacentTmp);
						}
						else
						{
							// 5. 如果该相邻节点在开放列表中, 
							//    则判断若经由当前节点到达该相邻节点的G值是否小于原来保存的G值,
							//    若小于,则将该相邻节点的父节点设为当前节点,并重新设置该相邻节点的G和F值
							if (adjacentTmp.isOpen) //已经在openList中
							{
								//计算G值
								//当前的g值+（当前边的进入边的中点-当前边出边的中点）的距离
								var p1 = Equals(currNode, endCell)
									? endPos
									: currNode.lineList[currNode.arrivalWall].center;
								float increaseG = adjacentTmp.ComputeGIncrease(p1, currNode.lineList[i].center);

								if (currNode.g + increaseG < adjacentTmp.g)
								{
									adjacentTmp.g = currNode.g;
									adjacentTmp.parent = currNode;

									// remember the side this caller is entering from
									adjacentTmp.SetAndGetArrivalWall(currNode.index);

									//重新设置在heap中的位置
									openList.Remove(adjacentTmp);
									openList.Push(adjacentTmp);
								}
							}
							else //已在closeList中
							{
								adjacentTmp = null;
								continue;
							}
						}
					}
				}
			}

			//由网格路径生成路径

			return isFoundPath ? GetPath(startPos, endPos) : null;

			/*返回cell
                List<Vector2> ret = new List<Vector2>();
                foreach (Cell cell in GetCellPath())
                {
                    ret.Add(cell.Center);
                }
                return ret;
                 * */
		}


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
				return null;

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
			if (closedList.IsNullOrEmpty())
				return null;
			Cell st = closedList[closedList.Count - 1];
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
		/// <param name="cellPath"></param>
		/// <param name="endPos"></param>
		/// <returns></returns>
		private WayPoint GetFurthestWayPoint(WayPoint wayPoint, List<Cell> cellPath, Vector2 endPos)
		{
			Vector2 startPoint = wayPoint.position; //当前所在路径点
			Cell cell = wayPoint.cell;
			Cell lastCell = cell;
			int startIndex = cellPath.IndexOf(cell); //开始路点所在的网格索引
			Line outside = cell.lineList[cell.arrivalWall]; //路径线在网格中的穿出边
			Vector2 lastPointA = outside.pointA;
			Vector2 lastPointB = outside.pointB;
			Line lastLineA = new Line(startPoint, lastPointA);
			Line lastLineB = new Line(startPoint, lastPointB);
			Vector2 testPointA, testPointB; //要测试的点

			Cell lastPointCellA = lastCell;
			Cell lastPointCellB = lastCell;


			for (int i = startIndex + 1; i < cellPath.Count; i++)
			{
				cell = cellPath[i];
				outside = cell.lineList[cell.arrivalWall];
				if (i == cellPath.Count - 1)
				{
					testPointA = endPos;
					testPointB = endPos;
				}
				else
				{
					testPointA = outside.pointA;
					testPointB = outside.pointB;
				}

				if (!lastPointA.Equals(testPointA))
				{
					if (lastLineB.ClassifyPoint(testPointA) == PointClassification.RightSide)
						//路点
						return new WayPoint(lastPointCellB, lastPointB);
					if (lastLineA.ClassifyPoint(testPointA) != PointClassification.LeftSide)
					{
						lastPointA = testPointA;
						lastPointCellA = cell;
						//重设直线
						//lastLineA.PointB = lastPtA;
						//lastLineB.PointB = lastPtB;
						lastLineA = new Line(lastLineA.pointA, lastPointA);
						lastLineB = new Line(lastLineB.pointA, lastPointB);
					}
				}

				if (!lastPointB.Equals(testPointB))
				{
					if (lastLineA.ClassifyPoint(testPointB) == PointClassification.LeftSide)
						//路径点
						return new WayPoint(lastPointCellA, lastPointA);

					if (lastLineB.ClassifyPoint(testPointB) != PointClassification.RightSide)
					{
						lastPointB = testPointB;
						lastPointCellB = cell;
						//重设直线
						//lastLineA.PointB = lastPtA;
						//lastLineB.PointB = lastPtB;
						lastLineA = new Line(lastLineA.pointA, lastPointA);
						lastLineB = new Line(lastLineB.pointA, lastPointB);
					}
				}
			}

			return new WayPoint(cellPath[cellPath.Count - 1], endPos); //终点
		}
	}
}