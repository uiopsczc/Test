using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//里面的point都是没有grid_offset的，以0开始
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public class AStarMapPath
	{
		private int[][] finalGrids;
		public int[][] grids;
		public int[][] projectGrids; //别的地方投影到该mapPath的grids,调用UpdateFinalGrids进行混合
		public int gridOffsetX;
		public int gridOffsetY;

		public AStarMapPath(string jsonConent)
		{
			Hashtable dict = MiniJson.JsonDecode(jsonConent).To<Hashtable>();
			int minGridX = dict["minGridX"].To<int>();
			int minGridY = dict["minGridY"].To<int>();
			int maxGridX = dict["maxGridX"].To<int>();
			int maxGridY = dict["maxGridY"].To<int>();
			Dictionary<Vector2Int, int> dataDict = new Dictionary<Vector2Int, int>();
			Hashtable _dataDict = dict["dataDict"].To<Hashtable>();
			foreach (var _key in _dataDict.Keys)
			{
				Vector2 v = _key.To<string>().ToVector2();
				Vector2Int key = new Vector2Int((int)v.x, (int)v.y);
				int value = _dataDict[_key].To<int>();
				dataDict[key] = value;
			}

			int[][] grids = null;
			grids = grids
				.InitArrays(maxGridY - minGridY + 1, maxGridX - minGridX + 1,
					AStarConst.Default_Data_Value).ToLeftBottomBaseArrays();
			gridOffsetX = minGridX; //用于astarBehaviour的非0的偏移
			gridOffsetY = minGridY; //用于astarBehaviour的非0的偏移

			foreach (var key in dataDict.Keys)
				grids[key.x - minGridX][key.y - minGridY] = dataDict[key];

			Init(grids);
		}

		public AStarMapPath(int[][] grids)
		{
			Init(grids);
		}

		void Init(int[][] grids)
		{
			this.grids = grids;
			this.projectGrids = grids.InitArrays(Height(), Width());
		}

		public Vector2Int GetPointWithoutOffset(Vector2Int pointWithOffset)
		{
			return new Vector2Int(GetPointXWithoutOffset(pointWithOffset.x),
				GetPointYWithoutOffset(pointWithOffset.y));
		}

		public int GetPointXWithoutOffset(int xWithOffset)
		{
			return xWithOffset - gridOffsetX;
		}

		public int GetPointYWithoutOffset(int yWithOffset)
		{
			return yWithOffset - gridOffsetY;
		}

		public Vector2Int GetOffset(int yWithOffset)
		{
			return new Vector2Int(gridOffsetX, gridOffsetY);
		}


		public int Width()
		{
			return grids?[0].Length ?? 0;
		}

		public int Height()
		{
			return grids?.Length ?? 0;
		}

		public int[][] GetFinalGrids()
		{
			if (finalGrids == null)
				UpdateFinalGrids();
			return finalGrids;
		}

		public void UpdateFinalGrids()
		{
			this.finalGrids = grids.InitArrays(Height(), Width());
			for (int i = 0; i < grids.Length; i++)
			{
				for (int j = 0; j < grids[0].Length; j++)
				{
					int gridType = grids[i][j];
					int projectGridType = projectGrids[i][j];
					if (projectGridType == 0) //没有project_grid_type，则用grid_type
						finalGrids[i][j] = gridType;
					else
					{
						int field = AStarUtil.GetField(gridType); //用grid_type的field
						int terrainType = AStarUtil.GetTerrainType(projectGridType) != 0
							? AStarUtil.GetTerrainType(projectGridType)
							: AStarUtil.GetTerrainType(gridType); //覆盖关系
						int obstacleType = AStarUtil.GetObstacleType(projectGridType) != 0
							? AStarUtil.GetObstacleType(projectGridType)
							: AStarUtil.GetObstacleType(gridType); //覆盖关系

						finalGrids[i][j] = AStarUtil.ToGridType(field, terrainType, obstacleType);
					}
				}
			}
		}

		//先对角线查找，再直角查找
		public List<Vector2Int> DirectFindPath(Vector2Int pointA,
			Vector2Int pointB, int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			return AStarMapPathUtil.DirectFindPath(this, pointA, pointB, canPassObstacleTypes,
				canPassTerrainTypes);
		}

		//直角寻路(先横向再纵向寻路)
		public List<Vector2Int> BorderFindPath(Vector2Int pointA, Vector2Int pointB, int[] canPassObstacleTypes,
			int[] canPassTerrainTypes)
		{
			return AStarMapPathUtil.BorderFindPath(this, pointA, pointB, canPassObstacleTypes,
				canPassTerrainTypes);
		}


		//对角线寻路
		public List<Vector2Int> DiagonallyFindPath(Vector2Int pointA, Vector2Int pointB,
			int[] canPassObstacleTypes,
			int[] canPassTerrainTypes)
		{
			return AStarMapPathUtil.DiagonallyFindPath(this, pointA, pointB, canPassObstacleTypes,
				canPassTerrainTypes);
		}


		//获取P点四周为+-out_count的可以通过的点列表
		public List<Vector2Int> GetAroundFreePointList(Vector2Int basePoint, int outCount,
			int[] canPassObstacleTypes,
			int[] canPassTerrainTypes)
		{
			return AStarUtil.GetAroundFreePointList(this, basePoint, outCount, canPassObstacleTypes,
				canPassObstacleTypes);
		}

		//获取P点四周为+-out_count（包含边界）以内的可以通过的点
		public Vector2Int? FindAroundFreePoint(Vector2Int basePoint, int outCount, List<Vector2Int> exceptPointList,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			return AStarUtil.FindAroundFreePoint(this, basePoint, outCount, exceptPointList,
				canPassObstacleTypes,
				canPassObstacleTypes, randomManager);
		}

		//获取P点四周为+-out_count的可以通过的点
		public Vector2Int? FindAroundFreePoint(Vector2Int basePoint, int outCount, int[] canPassObstacleTypes,
			int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			return AStarUtil.FindAroundFreePoint(this, basePoint, outCount, canPassObstacleTypes,
				canPassObstacleTypes, randomManager);
		}

		//获得轨迹中可通过的最远点
		public Vector2Int GetMostPassPoint(List<Vector2Int> trackList,
			int[] canPassObstacleTypes,
			int[] canPassTerrainTypes, bool isCanOut = false)
		{
			return AStarUtil.GetMostPassPoint(this, trackList,
				canPassObstacleTypes,
				canPassTerrainTypes, isCanOut);
		}

		//获得两点间可通过的最远点
		// can_out  是否允许通过场景外
		public Vector2Int GetMostLinePassPoint(Vector2Int lp, Vector2Int tp,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes, bool isCanOut = false)
		{
			return AStarUtil.GetMostLinePassPoint(this, lp, tp, canPassObstacleTypes, canPassTerrainTypes,
				isCanOut);
		}

		//获取离a,b最近的点
		public Vector2Int GetNearestPoint(Vector2Int pointA, Vector2Int pointB,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			return AStarUtil.GetNearestPoint(this, pointA, pointB, canPassObstacleTypes, canPassTerrainTypes);
		}

		public Vector2Int? GetRandomMovePoint(Vector2Int basePoint, Vector2Int goalPoint,
			int maxRadiusBetweenTargetPointAndGoalPoint, int[] canPassObstacleTypes,
			int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			return AStarUtil.GetRandomMovePoint(this, basePoint, goalPoint,
				maxRadiusBetweenTargetPointAndGoalPoint,
				canPassObstacleTypes, canPassTerrainTypes, randomManager);
		}


		//获取range范围内的可以通过的格子列表
		public List<Vector2Int> GetRangeFreePointList(int x1, int y1, int x2, int y2,
			List<Vector2Int> exceptPointList,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			return AStarUtil.GetRangeFreePointList(this, x1, y1, x2,
				y2, exceptPointList, canPassObstacleTypes, canPassTerrainTypes);
		}


		//获取range范围内的可以通过的格子
		public Vector2Int? FindRangeFreePoint(int x1, int y1, int x2, int y2,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			return FindRangeFreePoint(x1, y1, x2, y2, null,
				canPassObstacleTypes, canPassTerrainTypes, randomManager);
		}


		//获取range范围内的可以通过的格子
		public Vector2Int? FindRangeFreePoint(int x1, int y1, int x2, int y2,
			List<Vector2Int> exceptPointList,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			return AStarUtil.FindRangeFreePoint(this, x1, y1, x2, y2, exceptPointList, canPassObstacleTypes,
				canPassTerrainTypes, randomManager);
		}

		//检测某个点是否可通过
		// can_out 是否允许在场景外
		public bool CanPass(int x, int y, int[] canPassObstacleTypes,
			int[] canPassTerrainTypes, bool isCanOut = false)
		{
			return AStarUtil.CanPass(this, x, y, canPassObstacleTypes, canPassTerrainTypes, isCanOut);
		}

		//检测轨迹是否可通过
		// can_out 是否允许在场景外
		public bool CanPass(List<Vector2Int> trackList, int[] canPassObstacleTypes,
			int[] canPassTerrainTypes, bool isCanOut = false)
		{
			return AStarUtil.CanPass(this, trackList, canPassObstacleTypes, canPassTerrainTypes, isCanOut);
		}

		//检测两点间直线是否可通过
		public bool CanLinePass(Vector2Int pointA, Vector2Int pointB, int[] canPassObstacleTypes,
			int[] canPassTerrainTypes, bool isCanOut = false)
		{
			return AStarUtil.CanLinePass(this, pointA, pointB, canPassObstacleTypes, canPassTerrainTypes,
				isCanOut);
		}

		//是否有效地图坐标（不含填充区域）
		public bool IsValidPoint(int x, int y)
		{
			if (!AStarUtil.IsInRange(grids, x, y))
				return false;
			return AStarUtil.IsValidObstacleType(grids[x][y]);
		}
	}
}