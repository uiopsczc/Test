using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		public static List<Vector2Int> GetNeighborList(Vector2Int basePoint)
		{
			List<Vector2Int> neighborList = new List<Vector2Int>();
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y + 1)); // 增加左上角邻居节点
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y)); // 增加左侧邻居节点
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y - 1)); // 增加左下角的邻居节点
			neighborList.Add(new Vector2Int(basePoint.x, basePoint.y + 1)); // 增加上方邻居节点
			neighborList.Add(new Vector2Int(basePoint.x, basePoint.y - 1)); // 增加下方邻居节点
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y + 1)); // 增加右上角邻居节点
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y)); // 增加右侧邻居节点
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y - 1)); // 增加右下角邻居节点
			return neighborList;
		}

		public static List<Vector2Int> GetLeftTopNeighborList(Vector2Int basePoint)
		{
			List<Vector2Int> neighborList = new List<Vector2Int>();
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y + 1)); // 增加左上角邻居节点
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y)); // 增加左侧邻居节点
			neighborList.Add(new Vector2Int(basePoint.x, basePoint.y + 1)); // 增加上方邻居节点
			return neighborList;
		}

		public static List<Vector2Int> GetLeftBottomNeighborList(Vector2Int basePoint)
		{
			List<Vector2Int> neighborList = new List<Vector2Int>();
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y)); // 增加左侧邻居节点
			neighborList.Add(new Vector2Int(basePoint.x - 1, basePoint.y - 1)); // 增加左下角的邻居节点
			neighborList.Add(new Vector2Int(basePoint.x, basePoint.y - 1)); // 增加下方邻居节点
			return neighborList;
		}

		public static List<Vector2Int> GetRightTopNeighborList(Vector2Int basePoint)
		{
			List<Vector2Int> neighborList = new List<Vector2Int>();
			neighborList.Add(new Vector2Int(basePoint.x, basePoint.y + 1)); // 增加上方邻居节点
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y + 1)); // 增加右上角邻居节点
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y)); // 增加右侧邻居节点
			return neighborList;
		}

		public static List<Vector2Int> GetRightBottomNeighborList(Vector2Int basePoint)
		{
			List<Vector2Int> neighborList = new List<Vector2Int>();
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y)); // 增加右侧邻居节点
			neighborList.Add(new Vector2Int(basePoint.x + 1, basePoint.y - 1)); // 增加右下角邻居节点
			neighborList.Add(new Vector2Int(basePoint.x, basePoint.y - 1)); // 增加下方邻居节点
			return neighborList;
		}


		public static List<Vector2Int> GetOppositeNeighborList(Vector2Int o, Vector2Int p)
		{
			List<Vector2Int> list = GetNeighborList(o);
			int dx = p.x > o.x ? 1 : p.x < o.x ? -1 : 0;
			int dy = p.y > o.y ? 1 : p.y < o.y ? -1 : 0;

			list.Remove(new Vector2Int(o.x + dx, o.y + dy));
			if (dx == 0)
			{
				list.Remove(new Vector2Int(o.x + 1, o.y + dy));
				list.Remove(new Vector2Int(o.x - 1, o.y + dy));
			}
			else if (dy == 0)
			{
				list.Remove(new Vector2Int(o.x + dx, o.y + 1));
				list.Remove(new Vector2Int(o.x + dx, o.y - 1));
			}
			else
			{
				list.Remove(new Vector2Int(o.x, o.y + dy));
				list.Remove(new Vector2Int(o.x + dx, o.y));
			}

			return list;
		}


	}
}