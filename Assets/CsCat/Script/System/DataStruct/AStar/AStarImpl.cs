using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CsCat
{
	//https://blog.csdn.net/wf471859778/article/details/103608623
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	// 里面的point是没有offset的
	public class AStarImpl : AStar
	{
		protected AStarHType astarHType;
		protected AStarMapPath astarMapPath; // 地图数组
		protected int[] canPassObstacleTypes; // 可通过障碍表
		protected int[] canPassTerrainTypes; // 可通过地形表

		public AStarImpl(AStarMapPath astarMapPath, AStarHType astarHType, int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes)
		{
			this.astarMapPath = astarMapPath;
			SetAStarHType(astarHType);
			SetCanPassType(canPassObstacleTypes, canPassTerrainTypes);
			SetRange(0, 0, astarMapPath.Height() - 1, astarMapPath.Width() - 1);
		}

		public void SetCanPassType(int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			this.canPassObstacleTypes = canPassObstacleTypes;
			this.canPassTerrainTypes = canPassTerrainTypes;
		}

		protected override bool CanPass(int xWithoutOffset, int yWithoutOffset)
		{
			if (!AStarUtil.CanPass(astarMapPath.GetFinalGrids(), xWithoutOffset, yWithoutOffset, canPassObstacleTypes, canPassTerrainTypes))
				return false;
			return true;
		}

		int GetHeight()
		{
			return astarMapPath.Height();
		}

		int GetWidth()
		{
			return astarMapPath.Width();
		}

		public void SetAStarHType(AStarHType astarHType)
		{
			this.astarHType = astarHType;
		}

		//返回的是不带offset的point
		public Vector2Int GetRandomCanPassPoint(RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			var canPassPointList = new List<Vector2Int>();
			for (int x = 0; x < astarMapPath.Width(); x++)
				for (int y = 0; y < astarMapPath.Height(); y++)
				{
					if (CanPass(x, y))
						canPassPointList.Add(new Vector2Int(x, y));
				}

			int randomIndex = randomManager.RandomInt(0, canPassPointList.Count);
			return canPassPointList[randomIndex];
		}

		protected override float GetG(Vector2Int p1, Vector2Int p2)
		{
			int dx = Math.Abs(p1.x - p2.x);
			int dy = Math.Abs(p1.y - p2.y);
			var terrainType = AStarUtil.GetTerrainType(astarMapPath.GetFinalGrids()[p2.x][p2.y]);
			float cost = AStarConst.AStarTerrainType_Dict[terrainType].cost;
			if (dx == 0 || dy == 0) //非斜线方向
				return cost;
			else //斜线方向
				return cost * 1.414f;
		}


		protected override float GetH(Vector2Int nPoint, Vector2Int goalPoint)
		{
			switch (astarHType)
			{
				case AStarHType.Euclid_Distance:
					return (float)Math.Sqrt((nPoint.x - goalPoint.x) * (nPoint.x - goalPoint.x) + (nPoint.y - goalPoint.y) * (nPoint.y - goalPoint.y));
				case AStarHType.Euclid_SqureDistance:
					return (nPoint.x - goalPoint.x) * (nPoint.x - goalPoint.x) + (nPoint.y - goalPoint.y) * (nPoint.y - goalPoint.y);
				case AStarHType.Manhattan_Distance:
					return Math.Abs(nPoint.x - goalPoint.x) + Math.Abs(nPoint.y - goalPoint.y);
				case AStarHType.Diagonal_Distance:
					var hDiagonal = Math.Min(Math.Abs(nPoint.x - goalPoint.x), Math.Abs(nPoint.y - goalPoint.y));
					var hStraight = Math.Abs(nPoint.x - goalPoint.x) + Math.Abs(nPoint.y - goalPoint.y);
					return 1.414f * hDiagonal + (hStraight - 2 * hDiagonal);
			}

			return 0;
		}

		protected override void SetNeighborOffsetList()
		{
			switch (astarHType)
			{
				case AStarHType.Manhattan_Distance:
					neighborOffsetList.Add(Vector2IntConst.Top); //上方邻居节点
					neighborOffsetList.Add(Vector2IntConst.Right); //右侧邻居节点
					neighborOffsetList.Add(Vector2IntConst.Bottom); //下方邻居节点
					neighborOffsetList.Add(Vector2IntConst.Left); //左侧邻居节点
					break;
				default:
					base.SetNeighborOffsetList();
					break;
			}
		}

	}
}
