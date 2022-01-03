using System;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//是否有效障碍
		public static bool IsValidObstacleType(int grid_type)
		{
			return (grid_type & 0xff) != AStarMapPathConst.Invalid_Obstacle_Types;
		}

		public static int ToGridType(int field, int terrain_type, int obstacle_type)
		{
			return (field << 8) + (terrain_type << 3) + obstacle_type;
		}

		// 障碍类型，取后3位, 数值范围[0,7]
		public static int GetObstacleType(int grid_type)
		{
			return (grid_type & 0x7);
		}

		// 地形类型，取后三位向前的5位 数值范围[0,31]
		public static int GetTerrainType(int grid_type)
		{
			return (grid_type & 0xff) >> 3;
		}

		//区块编号值 ,基于grid_type的低16位移除低8位的值
		public static int GetField(int grid_type)
		{
			return (grid_type & 0xff00) >> 8; //>>8去掉低8位
		}



		//是否是同一个区块编号值,基于grid_type的低16位移除低8位的值
		public static bool IsSameField(int v1, int v2)
		{
			return GetField(v1) == GetField(v2);
		}

		// 获取区块的Point，基于Client_View_Width_Grid_Count，Client_View_Height_Grid_Count
		public static Vector2Int GetBlockPoint(Vector2Int p)
		{
			int x = p.x / AStarMapPathConst.Client_View_Width_Grid_Count;
			int y = p.y / AStarMapPathConst.Client_View_Height_Grid_Count;
			return new Vector2Int(x, y);
		}

		//是否在同一区块，基于Client_View_Width_Grid_Count，Client_View_Height_Grid_Count
		public static bool IsSameBlock(Vector2Int p1, Vector2Int p2)
		{
			return GetBlockPoint(p1) == GetBlockPoint(p2);
		}

		//是否在相邻区块，block是基于Client_View_Width_Grid_Count，Client_View_Height_Grid_Count
		public static bool IsNeighborBlock(Vector2Int p1, Vector2Int p2)
		{
			return Math.Abs(p1.x / AStarMapPathConst.Client_View_Width_Grid_Count -
							p2.x / AStarMapPathConst.Client_View_Width_Grid_Count) <=
				   1
				   && Math.Abs(
					 p1.y / AStarMapPathConst.Client_View_Height_Grid_Count -
					 p2.y / AStarMapPathConst.Client_View_Height_Grid_Count) <= 1;
		}

		//是否在相邻区块，block是基于Client_View_Width_Grid_Count，Client_View_Height_Grid_Count
		public static bool IsNeighborBlock(int x1, int y1, Vector2Int block_point2)
		{
			return IsNeighborBlock(new Vector2Int(x1, y1), block_point2);
		}

		//是否在相邻区块，block是基于Client_View_Width_Grid_Count，Client_View_Height_Grid_Count
		public static bool IsNeighborBlock(Vector2Int block_point1, int x2, int y2)
		{
			return IsNeighborBlock(block_point1, new Vector2Int(x2, y2));
		}

		//是否在相邻区块，block是基于Client_View_Width_Grid_Count，Client_View_Height_Grid_Count
		public static bool IsNeighborBlock(int x1, int y1, int x2, int y2)
		{
			return IsNeighborBlock(new Vector2Int(x1, y1), new Vector2Int(x2, y2));
		}
	}
}