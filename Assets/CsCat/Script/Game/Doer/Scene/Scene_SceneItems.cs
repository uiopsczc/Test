using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Scene
	{
		////////////////////////////物品容器////////////////////////
		public Item[] GetItems(string id = null, string belong = null, bool isNotIncludeChildScene = false)
		{
			List<Item> result = new List<Item>();
			if (belong == null)
			{
				Item[] currentSceneItemList = oSceneItems.GetItems(id);
				result.AddRange(currentSceneItemList);
				if (!isNotIncludeChildScene)
					foreach (var childScene in GetChildScenes())
						result.AddRange(childScene.GetItems(id, null, isNotIncludeChildScene));
			}
			else
			{
				var items = GetItems(id, null, isNotIncludeChildScene);
				for (var i = 0; i < items.Length; i++)
				{
					Item item = items[i];
					if (belong.Equals(item.GetBelong()))
						result.Add(item);
				}
			}

			return result.ToArray();
		}



		public int GetItemCount(string id = null, string belong = null, bool isNotIncludeChildScene = false)
		{
			return GetItems(id, belong, isNotIncludeChildScene).Length;
		}

		public Item GetItem(string idOrRid, string belong = null, bool isNotIncludeChildScene = false)
		{
			var items = this.GetItems(null, belong, isNotIncludeChildScene);
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
				if (IdUtil.IsIdOrRidEquals(idOrRid, item.GetId(), item.GetRid()))
					return item;
			}

			return null;
		}



		//添加物品到指定坐标
		public void AddItem(Vector2Int pos, Item item)
		{
			item.SetEnv(this);
			item.SetPos(pos);

			oSceneItems.GetItemDict_ToEdit()[item.GetRid()] = item;

			// 触发进入事件
			DoEnter(item);
		}

		//删除物品
		public void RemoveItem(Item item)
		{
			if (!oSceneItems.GetItemDict_ToEdit().Contains(item.GetRid()))
				return;
			oSceneItems.GetItemDict_ToEdit().Remove(item.GetRid());


			// 触发离开事件
			DoLeave(item);
			item.SetEnv(null);
		}

		//删除物品
		public Item RemoveItem(string rid)
		{
			if (!oSceneItems.GetItemDict_ToEdit().Contains(rid))
				return null;
			var result = oSceneItems.GetItemDict_ToEdit()[rid] as Item;
			RemoveItem(result);
			return result;
		}

		//将物品移到指定位置
		public void MoveItem(Item item, Vector2Int toPos, List<Vector2Int> trackList, int type)
		{
			Vector2Int fromPos = item.GetPos();
			item.SetPos(toPos);

			// 触发移动事件
			DoMove(item, fromPos, toPos, trackList, type);
		}

		//将物品转移到指定子场景的指定位置
		public void ShiftItem(Item item, Scene childScene, Vector2Int toPos, int type)
		{
			Vector2Int fromPos = item.GetPos();
			oSceneItems.GetItemDict_ToEdit().Remove(item.GetRid());
			item.SetEnv(childScene);
			item.SetPos(toPos);
			childScene.oSceneItems.GetItemDict_ToEdit()[item.GetRid()] = item;
			DoShift(item, fromPos, childScene, toPos, type);
		}

		// 清除所有物品
		public void ClearItems(bool isNotIncludeChildScene = false)
		{
			foreach (Item item in oSceneItems.GetItemDict_ToEdit().Values)
			{
				// 触发离开事件
				DoLeave(item);
				item.SetEnv(null);
				item.Destruct();
			}

			oSceneItems.GetItemDict_ToEdit().Clear();
			if (!isNotIncludeChildScene)
			{
				var scenes = this.GetChildScenes();
				for (var i = 0; i < scenes.Length; i++)
				{
					var childScene = scenes[i];
					childScene.ClearItems(isNotIncludeChildScene);
				}
			}
		}

		//获得指定范围的物品（仅供父级场景调用）
		public Item[] GetRangeItems(AStarRange range, string belong = null, bool isNotIncludeChildScene = false)
		{
			List<Item> result = new List<Item>();
			var items = GetItems(null, belong, isNotIncludeChildScene);
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
				Vector2Int pos = item.GetEnv<Scene>().ToParentPos(item.GetPos(), this);
				if (range.IsInRange(pos))
					result.Add(item);
			}

			return result.ToArray();
		}

		//获得指定范围的物品（仅供父级场景调用）
		public Item[] GetAroundItems(Vector2Int comparePos, int radius, string belong = null,
		  bool isNotIncludeChildScene = false)
		{
			List<Item> result = new List<Item>();
			var items = GetItems(null, belong, isNotIncludeChildScene);
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
				Vector2Int pos = item.GetEnv<Scene>().ToParentPos(item.GetPos(), this);
				if (AStarUtil.IsInAround(pos, comparePos, radius))
					result.Add(item);
			}

			return result.ToArray();
		}

		//获得指定扇形的物品（仅供父级场景调用）
		public Item[] GetSectorItems(Vector2Int sectorCenterPos, Vector2 sectorDir, int sectorRadius,
		  float sectorHalfDegrees, string belong = null, bool isNotIncludeChildScene = false)
		{
			List<Item> result = new List<Item>();
			var items = GetItems(null, belong, isNotIncludeChildScene);
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
				Vector2Int pos = item.GetEnv<Scene>().ToParentPos(item.GetPos(), this);
				if (AStarUtil.IsInSector(pos, sectorCenterPos, sectorDir, sectorRadius, sectorHalfDegrees))
					result.Add(item);
			}

			return result.ToArray();
		}


		public Item[] GetGroupItems(string group, string belong = null, bool isNotIncludeChildScene = false)
		{
			List<Item> result = new List<Item>();
			var items = GetItems(null, belong, isNotIncludeChildScene);
			for (var i = 0; i < items.Length; i++)
			{
				Item item = items[i];
				if (item.GetGroup().Equals(@group))
					result.Add(item);
			}

			return result.ToArray();
		}

		public int GetGroupItemCount(string group, string belong = null, bool isNotIncludeChildScene = false)
		{
			return GetGroupItems(group, belong, isNotIncludeChildScene).Length;
		}
	}
}