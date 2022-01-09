using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获得轨迹中可通过的最远点
		public static Vector2Int GetMostPassPoint(AStarMapPath astarMapPath, List<Vector2Int> trackList,
		  int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, bool canOut = false)
		{
			Vector2Int lp = trackList[0];
			Vector2Int tp = lp;
			for (int i = 1; i < trackList.Count; i++)
			{
				Vector2Int p = trackList[i];
				if (!CanPass(astarMapPath, p.x, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
					break;

				DirectionInfo directionInfo = DirectionInfoUtil.GetDirectionInfo(p.x - lp.x, p.y - lp.y);
				//      directionInfo = DirectionConst.GetDirectionInfo(0, 0); // 不再判断临边障碍 2012-10-29
				//      LogCat.log(directionInfo.name);
				if (directionInfo == DirectionInfoConst.LeftTopDirectionInfo) // 左上角
				{

					if (!CanPass(astarMapPath, p.x + 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
					if (!CanPass(astarMapPath, p.x, p.y - 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
				}
				else if (directionInfo == DirectionInfoConst.RightTopDirectionInfo) // 右上角
				{
					if (!CanPass(astarMapPath, p.x - 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
					if (!CanPass(astarMapPath, p.x, p.y - 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
				}
				else if (directionInfo == DirectionInfoConst.RightBottomDirectionInfo) // 右下角
				{
					if (!CanPass(astarMapPath, p.x - 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
					if (!CanPass(astarMapPath, p.x, p.y + 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
				}
				else if (directionInfo == DirectionInfoConst.LeftBottomDirectionInfo) // 左下角
				{
					if (!CanPass(astarMapPath, p.x + 1, p.y, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
					if (!CanPass(astarMapPath, p.x, p.y + 1, canPassObstacleTypes, canPassTerrainTypes, canOut))
						break;
				}

				lp = p;
				tp = lp;
			}

			return tp;
		}

		//获得两点间可通过的最远点
		// can_out  是否允许通过场景外
		public static Vector2Int GetMostLinePassPoint(AStarMapPath astarMapPath, Vector2Int lp, Vector2Int tp,
		  int[] canPassObstacleTypes, int[] canPassTerrainTypes, bool canOut = false)
		{
			if (!canOut && !IsInRange(astarMapPath.GetFinalGrids(), lp))
				return lp;
			List<Vector2Int> pointList = GetLinePointList(lp, tp);
			return GetMostPassPoint(astarMapPath, pointList, canPassObstacleTypes, canPassTerrainTypes, canOut);
		}

	}
}