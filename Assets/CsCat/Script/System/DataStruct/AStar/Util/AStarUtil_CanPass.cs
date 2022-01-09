using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//检测某个点是否可通过
		public static bool CanPass(int[][] grids, int x, int y, int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			int gridType = grids[x][y];
			int gridObstacleType = GetObstacleType(gridType);
			if (gridObstacleType != 0 && canPassObstacleTypes[gridObstacleType] == 0) // 障碍
				return false;
			int gridTerrainType = GetTerrainType(gridType);
			if (gridTerrainType != 0 && canPassTerrainTypes[gridTerrainType] == 0) // 地形
				return false;
			return true;
		}

		//检测某个点是否可通过
		// can_out 是否允许在场景外
		public static bool CanPass(AStarMapPath astarMapPath, int x, int y, int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, bool canOut = false)
		{
			if (!IsInRange(astarMapPath.GetFinalGrids(), x, y))
				return canOut;
			int grid_type = astarMapPath.GetFinalGrids()[x][y]; // 固有地形+障碍
			if (!IsValidObstacleType(grid_type)) // 填充区域
				return canOut;
			if (!CanPass(astarMapPath.GetFinalGrids(), x, y, canPassObstacleTypes, canPassTerrainTypes))
				return false;
			return true;
		}

		//检测轨迹是否可通过
		// can_out 是否允许在场景外
		public static bool CanPass(AStarMapPath astarMapPath, List<Vector2Int> trackList, int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, bool canOut = false)
		{
			if (trackList.Count == 0)
				return true;
			Vector2Int lp = trackList[0];
			if (trackList.Count == 1)
				return CanPass(astarMapPath, lp.x, lp.y, canPassObstacleTypes, canPassTerrainTypes, canOut);
			for (int i = 1; i < trackList.Count; i++)
			{
				Vector2Int p = trackList[i];
				if (!CanPass(astarMapPath, p.x, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
					return false;

				DirectionInfo directionInfo = DirectionInfoUtil.GetDirectionInfo(p.x - lp.x, p.y - lp.y);
				//      directionInfo = DirectionConst.GetDirectionInfo(0, 0);
				if (directionInfo == DirectionInfoConst.LeftTopDirectionInfo) // 左上角
				{
					if (!CanPass(astarMapPath, p.x + 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
					if (!CanPass(astarMapPath, p.x, p.y - 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
				}
				else if (directionInfo == DirectionInfoConst.RightTopDirectionInfo) // 右上角
				{
					if (!CanPass(astarMapPath, p.x - 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
					if (!CanPass(astarMapPath, p.x, p.y - 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
				}
				else if (directionInfo == DirectionInfoConst.RightBottomDirectionInfo) // 右下角
				{
					if (!CanPass(astarMapPath, p.x - 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
					if (!CanPass(astarMapPath, p.x, p.y + 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
				}
				else if (directionInfo == DirectionInfoConst.LeftBottomDirectionInfo) // 左下角
				{
					if (!CanPass(astarMapPath, p.x + 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
					if (!CanPass(astarMapPath, p.x, p.y + 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						return false;
				}

				lp = p;
			}

			return true;
		}

		//检测两点间直线是否可通过
		public static bool CanLinePass(AStarMapPath astarMapPath, Vector2Int pointA, Vector2Int pointB,
		  int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, bool canOut = false)
		{
			if (!canOut && (!IsInRange(astarMapPath.GetFinalGrids(), pointA) ||
							 !IsInRange(astarMapPath.GetFinalGrids(), pointB)))
				return false;
			var linePointList = GetLinePointList(pointA, pointB);
			return CanPass(astarMapPath, linePointList, canPassObstacleTypes, canPassTerrainTypes, canOut);
		}




	}
}