using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Scene
	{
		////////////////////////////物品容器////////////////////////
		public Item[] GetItems(string id = null, string belong = null, bool is_not_include_child_scene = false)
		{
			List<Item> result = new List<Item>();
			if (belong == null)
			{
				Item[] current_scene_item_list = o_sceneItems.GetItems(id);
				result.AddRange(current_scene_item_list);
				if (!is_not_include_child_scene)
					foreach (var child_scene in GetChildScenes())
						result.AddRange(child_scene.GetItems(id, null, is_not_include_child_scene));
			}
			else
			{
				foreach (Item item in GetItems(id, null, is_not_include_child_scene))
				{
					if (belong.Equals(item.GetBelong()))
						result.Add(item);
				}
			}

			return result.ToArray();
		}



		public int GetItemCount(string id = null, string belong = null, bool is_not_include_child_scene = false)
		{
			return GetItems(id, belong, is_not_include_child_scene).Length;
		}

		public Item GetItem(string id_or_rid, string belong = null, bool is_not_include_child_scene = false)
		{
			foreach (var item in this.GetItems(null, belong, is_not_include_child_scene))
			{
				if (IdUtil.IsIdOrRidEquals(id_or_rid, item.GetId(), item.GetRid()))
					return item;
			}

			return null;
		}



		//添加物品到指定坐标
		public void AddItem(Vector2Int pos, Item item)
		{
			item.SetEnv(this);
			item.SetPos(pos);

			o_sceneItems.GetItemDict_ToEdit()[item.GetRid()] = item;

			// 触发进入事件
			DoEnter(item);
		}

		//删除物品
		public void RemoveItem(Item item)
		{
			if (!o_sceneItems.GetItemDict_ToEdit().Contains(item.GetRid()))
				return;
			o_sceneItems.GetItemDict_ToEdit().Remove(item.GetRid());


			// 触发离开事件
			DoLeave(item);
			item.SetEnv(null);
		}

		//删除物品
		public Item RemoveItem(string rid)
		{
			if (!o_sceneItems.GetItemDict_ToEdit().Contains(rid))
				return null;
			var result = o_sceneItems.GetItemDict_ToEdit()[rid] as Item;
			RemoveItem(result);
			return result;
		}

		//将物品移到指定位置
		public void MoveItem(Item item, Vector2Int to_pos, List<Vector2Int> track_list, int type)
		{
			Vector2Int from_pos = item.GetPos();
			item.SetPos(to_pos);

			// 触发移动事件
			DoMove(item, from_pos, to_pos, track_list, type);
		}

		//将物品转移到指定子场景的指定位置
		public void ShiftItem(Item item, Scene child_scene, Vector2Int to_pos, int type)
		{
			Vector2Int from_pos = item.GetPos();
			o_sceneItems.GetItemDict_ToEdit().Remove(item.GetRid());
			item.SetEnv(child_scene);
			item.SetPos(to_pos);
			child_scene.o_sceneItems.GetItemDict_ToEdit()[item.GetRid()] = item;
			DoShift(item, from_pos, child_scene, to_pos, type);
		}

		// 清除所有物品
		public void ClearItems(bool is_not_include_child_scene = false)
		{
			foreach (Item item in o_sceneItems.GetItemDict_ToEdit().Values)
			{
				// 触发离开事件
				DoLeave(item);
				item.SetEnv(null);
				item.Destruct();
			}

			o_sceneItems.GetItemDict_ToEdit().Clear();
			if (!is_not_include_child_scene)
			{
				foreach (var child_scene in this.GetChildScenes())
					child_scene.ClearItems(is_not_include_child_scene);
			}
		}

		//获得指定范围的物品（仅供父级场景调用）
		public Item[] GetRangeItems(AStarRange range, string belong = null, bool is_not_include_child_scene = false)
		{
			List<Item> result = new List<Item>();
			foreach (var item in GetItems(null, belong, is_not_include_child_scene))
			{
				Vector2Int pos = item.GetEnv<Scene>().ToParentPos(item.GetPos(), this);
				if (range.IsInRange(pos))
					result.Add(item);
			}

			return result.ToArray();
		}

		//获得指定范围的物品（仅供父级场景调用）
		public Item[] GetAroundItems(Vector2Int compare_pos, int radius, string belong = null,
		  bool is_not_include_child_scene = false)
		{
			List<Item> result = new List<Item>();
			foreach (var item in GetItems(null, belong, is_not_include_child_scene))
			{
				Vector2Int pos = item.GetEnv<Scene>().ToParentPos(item.GetPos(), this);
				if (AStarUtil.IsInAround(pos, compare_pos, radius))
					result.Add(item);
			}

			return result.ToArray();
		}

		//获得指定扇形的物品（仅供父级场景调用）
		public Item[] GetSectorItems(Vector2Int sector_center_pos, Vector2 sector_dir, int sector_radius,
		  float sector_half_degrees, string belong = null, bool is_not_include_child_scene = false)
		{
			List<Item> result = new List<Item>();
			foreach (var item in GetItems(null, belong, is_not_include_child_scene))
			{
				Vector2Int pos = item.GetEnv<Scene>().ToParentPos(item.GetPos(), this);
				if (AStarUtil.IsInSector(pos, sector_center_pos, sector_dir, sector_radius, sector_half_degrees))
					result.Add(item);
			}

			return result.ToArray();
		}


		public Item[] GetGroupItems(string group, string belong = null, bool is_not_include_child_scene = false)
		{
			List<Item> result = new List<Item>();
			foreach (Item item in GetItems(null, belong, is_not_include_child_scene))
			{
				if (item.GetGroup().Equals(group))
					result.Add(item);
			}

			return result.ToArray();
		}

		public int GetGroupItemCount(string group, string belong = null, bool is_not_include_child_scene = false)
		{
			return GetGroupItems(group, belong, is_not_include_child_scene).Length;
		}
	}
}