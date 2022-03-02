using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Scene
	{
		void CheckParentCall()
		{
			if (IsChildScene())
				throw new Exception("请使用父级场景调用此方法");
		}

		//清除主场景的子场景投影障碍（仅供父级场景调用）
		public void ClearProjectGrids(Vector2Int baseOnParentPos, Scene childScene)
		{
			CheckParentCall();

			AStarMapPath mapPath = GetMapPath();
			int[][] projectGrids;
			projectGrids = childScene.GetMapType() == 1 ? childScene.GetGrids() : childScene.GetProjectGrids();
			if (projectGrids != null)
			{
				Vector2Int offsetPos = childScene.GetOffsetPos();
				for (int x = 0; x < projectGrids.Length; x++)
				{
					for (int y = 0; y < projectGrids[x].Length; y++)
					{
						int v = projectGrids[x][y];
						if (v == 0 || !AStarUtil.IsValidObstacleType(v)) // 子场景无效区域不投影
							continue;
						int px = ToParentX(baseOnParentPos, offsetPos, x);
						int py = ToParentY(baseOnParentPos, offsetPos, y);
						if (mapPath.IsValidPoint(px, py)) // 父场景无效区域不投影
							mapPath.projectGrids[px][py] = 0;
					}
				}
			}
		}

		// 设置子场景投影障碍到主场景（仅供父级场景调用）
		public void SetProjectGrids(Vector2Int baseOnParentPos, Scene childScene)
		{
			CheckParentCall();

			AStarMapPath mapPath = GetMapPath();
			if (mapPath?.grids == null)
				return;
			var projectGrids = childScene.GetMapType() == 1 ? childScene.GetGrids() : childScene.GetProjectGrids();
			if (projectGrids != null)
			{
				Vector2Int offsetPos = childScene.GetOffsetPos();
				for (int x = 0; x < projectGrids.Length; x++)
				{
					for (int y = 0; y < projectGrids[x].Length; y++)
					{
						int v = projectGrids[x][y];
						if (v == 0 || !AStarUtil.IsValidObstacleType(v)) // 子场景无效区域不投影
							continue;
						int px = ToParentX(baseOnParentPos, offsetPos, x);
						int py = ToParentY(baseOnParentPos, offsetPos, y);
						if (mapPath.IsValidPoint(px, py)) // 父场景无效区域不投影
							mapPath.projectGrids[px][py] = v;
					}
				}
			}
		}


		//清空所有动态障碍（仅供父级场景调用）
		public void ClearAllProjectGrids()
		{
			CheckParentCall();

			AStarMapPath mapPath = GetMapPath();
			if (mapPath?.projectGrids == null)
				return;
			for (int x = 0; x < mapPath.projectGrids.Length; x++)
			{
				for (int y = 0; y < mapPath.projectGrids[x].Length; y++)
				{
					mapPath.projectGrids[x][y] = 0;
				}
			}
		}

		//重置所有动态障碍（仅供父级场景调用）
		public void ResetAllProjectGrids()
		{
			CheckParentCall();

			ClearAllProjectGrids();
			var scenes = GetChildScenes();
			for (var i = 0; i < scenes.Length; i++)
			{
				var childScene = scenes[i];
				if (!childScene.IsInAir())
					SetProjectGrids(childScene.GetPos(), childScene);
			}
		}

		////////////////////////////子场景容器////////////////////////
		public Scene[] GetChildScenes(string id = null, string belong = null)
		{
			if (belong == null && id == null)
				return this.oChildScenes.GetScenes();
			return this.oChildScenes.GetScenes(null, scene =>
			{
				if (belong != null && !scene.GetBelong().Equals(belong))
					return false;
				return id == null || scene.GetId().Equals(id);
			});
		}

		public Scene GetChildScene(string idOrRid, string belong = null)
		{
			if (IdUtil.IsRid(idOrRid)) // rid的情况
			{
				string rid = idOrRid;
				if (!this.oChildScenes.GetSceneDict_ToEdit().ContainsKey(rid))
					return null;
				Scene childScene = this.oChildScenes.GetSceneDict_ToEdit()[rid] as Scene;
				if (belong != null && !childScene.GetBelong().Equals(belong))
					return null;
				return childScene;
			}
			// id的情况
			string id = idOrRid;
			Scene[] childScenes = GetChildScenes(id, belong);
			return childScenes.Length == 0 ? null : childScenes[0];
		}

		//获得场景内所有子场景数量（仅供父级场景调用）
		public int GetChildSceneCount(string belong = null)
		{
			CheckParentCall();
			return GetChildScenes(null, belong).Length;
		}


		//添加子场景到指定坐标（仅供父级场景调用）
		public void AddChildScene(Vector2Int pos, Scene childScene)
		{
			CheckParentCall();

			childScene.SetEnv(this);
			childScene.SetPos(pos);

			this.oChildScenes.GetSceneDict_ToEdit()[childScene.GetRid()] = childScene;

			// 处理子场景障碍投影
			if (!childScene.IsInAir())
				SetProjectGrids(pos, childScene);

			// 触发进入事件
			DoEnter(childScene);
		}

		//移除子场景（仅供父级场景调用）
		public void RemoveChildScene(Scene childScene)
		{
			CheckParentCall();

			bool isContain = oChildScenes.GetSceneDict_ToEdit().ContainsKey(childScene.GetRid());
			oChildScenes.GetSceneDict_ToEdit().Remove(childScene.GetRid());
			if (isContain)
			{
				// 处理子场景障碍投影
				if (!childScene.IsInAir())
					ClearProjectGrids(childScene.GetPos(), childScene);

				// 触发离开事件
				DoLeave(childScene);
				childScene.SetEnv(null);
			}
		}

		//移除子场景（仅供父级场景调用）
		public void RemoveChildScene(string rid)
		{
			Scene childScene = this.oChildScenes.GetScene(rid);
			RemoveChildScene(childScene);
		}


		//将子场景移到指定位置（仅供父级场景调用）
		public void MoveChildScene(Scene scene, Vector2Int toPos, List<Vector2Int> trackList, int type)
		{
			CheckParentCall();

			Vector2Int fromPos = scene.GetPos();
			scene.SetPos(toPos);
			scene.SetTmp("lastMoveTime", DateTimeUtil.NowTicks());
			scene.SetTmp("lastMoveTrackList", trackList);

			// 处理子场景障碍投影
			if (!scene.IsInAir())
			{
				ClearProjectGrids(fromPos, scene);
				SetProjectGrids(toPos, scene);
			}

			// 触发移动事件
			DoMove(scene, fromPos, toPos, trackList, type);
		}

		//获得指定范围的子场景（仅供父级场景调用）
		public Scene[] GetRangeScenes(AStarRange range, string belong)
		{
			CheckParentCall();

			List<Scene> list = new List<Scene>();
			var scenes = GetChildScenes(null, belong);
			for (var i = 0; i < scenes.Length; i++)
			{
				Scene childScene = scenes[i];
				if (range.IsInRange(childScene.GetPos()))
					list.Add(childScene);
			}

			return list.ToArray();
		}

		// 获得视图内所有子场景（仅供父级场景调用）
		public Scene[] GetViewingScenes(Vector2Int pos, string belong)
		{
			return GetViewingScenes(pos, pos, belong);
		}

		// 获得视图内所有子场景（仅供父级场景调用）
		public Scene[] GetViewingScenes(Vector2Int pos1, Vector2Int pos2, string belong)
		{
			CheckParentCall();
			return GetRangeScenes(AStarUtil.GetViewingRange(pos1, pos2), belong);
		}

		//获得指定分组的子场景
		public Scene GetGroupScene(string group, string belong)
		{
			CheckParentCall();
			var scenes = GetChildScenes(null, belong);
			for (var i = 0; i < scenes.Length; i++)
			{
				var childScene = scenes[i];
				if (@group.Equals(childScene.GetGroup()))
					return childScene;
			}

			return null;
		}

	}
}