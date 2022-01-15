using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Scene : Thing
	{
		private Scenes oChildScenes;
		private SceneItems oSceneItems;

		public override void Init()
		{
			base.Init();
			this.oChildScenes = new Scenes(this, "o_child_scenes");
			this.oSceneItems = new SceneItems(this, "o_sceneItems");
		}


		public SceneFactory GetSceneFactory()
		{
			return this.factory as SceneFactory;
		}

		public CfgSceneData GetCfgSceneData()
		{
			return CfgScene.Instance.get_by_id(this.GetId());
		}

		//////////////////////DoXXX/////////////////////////////////////
		void DoEnter(Thing thing)
		{
			try
			{
				OnEnter(thing);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("to.OnEnter error! [{0}->{1}]:{2}", this, thing, exception));
			}

			try
			{
				thing.OnEnterScene(this);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("thing.OnEnterScene error! [{0}->{1}]:{2}", this, thing, exception));
			}
		}

		void DoLeave(Thing thing)
		{
			try
			{
				OnLeave(thing);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("to.OnLeave error! [{0}->{1}]:{2}", this, thing, exception));
			}

			try
			{
				thing.OnLeaveScene(this);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("thing.OnLeaveScene error! [{0}->{1}]:{2}", this, thing, exception));
			}
		}

		//物件thing在本场景中移动事件
		void DoMove(Thing thing, Vector2Int fromPos, Vector2Int toPos, List<Vector2Int> trackList, int type)
		{
			try
			{
				OnMoveThing(thing, fromPos, toPos, trackList, type);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("to.OnMoveThing error! [{0},{1}->({2}->{3}]:{4}", this, thing, fromPos,
					toPos,
					exception));
			}

			try
			{
				thing.OnMove(this, fromPos, toPos, trackList, type);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("thing.OnMove error! [{0},{1}->({2}->{3}]:{4}", this, thing, fromPos,
					toPos,
					exception));
			}
		}

		//物件thing本场景转移到另一场景事件
		void DoShift(Thing thing, Vector2Int fromPos, Scene childScene, Vector2Int toPos, int type)
		{
			try
			{
				OnShift(thing, fromPos, childScene, toPos, type);
			}
			catch (Exception exception)
			{
				LogCat.LogError(string.Format("env.OnShift error! [{0},{1}->({2}->{3}]:{4}", this, thing, fromPos,
					toPos,
					exception));
			}
		}

		//////////////////////OnXXX/////////////////////////////////////
		public override void OnInit()
		{
			base.OnInit();
		}

		public override void OnSave(Hashtable dict, Hashtable dictTmp)
		{
			base.OnSave(dict, dictTmp);
		}

		public override void OnRestore(Hashtable dict, Hashtable dictTmp)
		{
			base.OnRestore(dict, dictTmp);
		}

		public void OnEnter(Thing thing)
		{
		}

		public void OnLeave(Thing thing)
		{
		}

		//物件thing在本场景中移动事件
		public void OnMoveThing(Thing thing, Vector2Int fromPos, Vector2Int toPos, List<Vector2Int> trackList, int type)
		{
		}

		//物件thing本场景转移到另一场景事件
		public void OnShift(Thing thing, Vector2Int fromPos, Scene childScene, Vector2Int toPos, int type)
		{
		}


		////////////////////////////////////////////Util///////////////////////////////////////////////////
		//是否子场景
		public bool IsChildScene(Scene toTopParentScene = null)
		{
			return GetEnv() != null;
		}

		public bool IsChildSceneOf(Scene toTopParentScene = null)
		{
			if (toTopParentScene != null)
			{
				var parentScene = GetEnv();
				while (parentScene != null)
				{
					if (toTopParentScene == parentScene)
						return true;
					parentScene = parentScene.GetEnv();
				}

				return false;
			}

			return GetEnv() == null;
		}


		public void SetIsInAir(bool isInAir)
		{
			SetTmp("o_is_in_air", isInAir);
		}

		//是否在空中
		public bool IsInAir()
		{
			return GetTmp("o_is_in_air", false);
		}

		public void SetGroup(string group)
		{
			SetTmp("group", group);
		}

		public string GetGroup()
		{
			return GetTmp("group", "");
		}

		public void SetMapType(int map_type)
		{
			Scene parentScene = this.GetEnv<Scene>();
			if (parentScene != null)
			{
				parentScene.ClearProjectGrids(GetPos(), this);
				parentScene.SetProjectGrids(GetPos(), this);
			}

			Set("map_type", map_type);
		}

		public int GetMapType()
		{
			return Get<int>("map_type");
		}

		public void SetOrgPos(Vector2Int pos)
		{
			SetTmp("org_pos", pos);
		}

		public Vector2Int GetOrgPos()
		{
			return GetTmp<Vector2Int>("org_pos");
		}


		public SceneMapInfo GetSceneMapInfo()
		{
			SceneMapInfo sceneMapInfo = null;
			if (Get<bool>("is_dynamic_map"))
			{
				string src = Get<string>("src", "");
				if (src.Length > 0)
				{
					var cfgSceneData = CfgScene.Instance.get_by_id(src);
					sceneMapInfo = cfgSceneData.GetSceneMapInfo();
				}
			}
			else
				sceneMapInfo = Get<SceneMapInfo>("sceneMapInfo");

			return sceneMapInfo;
		}

		//自身障碍数据 grids[x][y]
		public int[][] GetGrids()
		{
			SceneMapInfo sceneMapInfo = GetSceneMapInfo();
			return sceneMapInfo?.grids;
		}

		// 自身投影数据 project_grids[x][y]
		public int[][] GetProjectGrids()
		{
			SceneMapInfo sceneMapInfo = GetSceneMapInfo();
			return sceneMapInfo?.projectGrids;
		}


		//获得路径的x轴宽度
		public int GetWidth()
		{
			int[][] grids = GetGrids();
			if (grids != null)
				return grids[0].Length;
			return 0;
		}

		//获得路径的y轴高度
		public int GetHeight()
		{
			int[][] grids = GetGrids();
			if (grids != null)
				return grids.Length;
			return 0;
		}

		//获得基准坐标
		public Vector2Int GetOffsetPos()
		{
			SceneMapInfo sceneMapInfo = GetSceneMapInfo();
			if (sceneMapInfo != null)
				return sceneMapInfo.offsetPos;
			return Vector2Int.zero;
		}

		//获得路径信息
		public AStarMapPath GetMapPath()
		{
			return GetTmp<AStarMapPath>("mapPath");
		}

		//更新路径信息
		public void UpdateMapPath()
		{
			SceneMapInfo sceneMapInfo = GetSceneMapInfo();
			if (sceneMapInfo != null)
			{
				AStarMapPath mapPath = new AStarMapPath(sceneMapInfo.grids);
				SetTmp("mapPath", mapPath);
			}
		}

		//将自身x坐标转换为父级场景x坐标
		public int ToParentX(Vector2Int baseOnParentPos, Vector2Int offsetPos, int x)
		{
			return baseOnParentPos.x - offsetPos.x + x;
		}

		//将自身y坐标转换为父级场景y坐标
		public int ToParentY(Vector2Int baseOnParentPos, Vector2Int offsetPos, int y)
		{
			return baseOnParentPos.y - offsetPos.y + y;
		}

		public Vector2Int ToParentPos(Vector2Int baseOnParentPos, Vector2Int offsetPos, Vector2Int pos)
		{
			return new Vector2Int(ToParentX(baseOnParentPos, offsetPos, pos.x),
				ToParentY(baseOnParentPos, offsetPos, pos.y));
		}

		//将自身坐标转换为父级场景坐标
		public Vector2Int ToParentPos(Vector2Int pos, Scene toTopParentScene)
		{
			bool isChildScene = IsChildScene();
			if (isChildScene)
			{
				Vector2Int baseOnParentPos = GetPos();
				Vector2Int offsetPos = GetOffsetPos();
				pos = ToParentPos(baseOnParentPos, offsetPos, pos);
			}

			if (toTopParentScene != this.GetEnv())
			{
				if (isChildScene)
					return this.GetEnv<Scene>().ToParentPos(pos, toTopParentScene);
				throw new Exception(string.Format("没有目标的scene:{0}", toTopParentScene));
			}

			return pos;
		}

		//将自身坐标转换为父级场景坐标
		public List<Vector2Int> ToParentPosList(List<Vector2Int> posList, Scene toTopParentScene)
		{
			List<Vector2Int> result = new List<Vector2Int>();
			for (var i = 0; i < posList.Count; i++)
			{
				var pos = posList[i];
				result.Add(ToParentPos(pos, toTopParentScene));
			}

			return result;
		}

		//将自身坐标转换为父级场景坐标
		public AStarRange ToParentRange(AStarRange range, Scene toTopParentScene)
		{
			Vector2Int leftBottom =
				ToParentPos(new Vector2Int(range.leftBottomX, range.leftBottomY), toTopParentScene);
			Vector2Int rightTop = ToParentPos(new Vector2Int(range.rightTopX, range.rightTopY), toTopParentScene);
			return new AStarRange(leftBottom, rightTop);
		}

		//将父级场景x坐标转换为自身x坐标
		public int FromParentX(Vector2Int baseOnParentPos, Vector2Int offsetPos, int x)
		{
			return x - baseOnParentPos.x + offsetPos.x;
		}

		//将父级场景y坐标转换为自身y坐标
		public int FromParentY(Vector2Int baseOnParentPos, Vector2Int offsetPos, int y)
		{
			return y - baseOnParentPos.y + offsetPos.y;
		}

		public Vector2Int FromParentPos(Vector2Int baseOnParentPos, Vector2Int offsetPos, Vector2Int pos)
		{
			return new Vector2Int(FromParentX(baseOnParentPos, offsetPos, pos.x),
				FromParentY(baseOnParentPos, offsetPos, pos.y));
		}

		//将父级场景坐标转换为自身坐标
		public Vector2Int FromParentPos(Vector2Int pos)
		{
			if (IsChildScene())
			{
				Vector2Int baseOnParentPos = GetPos();
				Vector2Int offsetPos = GetOffsetPos();
				return FromParentPos(baseOnParentPos, offsetPos, pos);
			}
			else
				return pos;
		}

		//将父级场景坐标转换为自身坐标
		public List<Vector2Int> FromParentPosList(List<Vector2Int> posList)
		{
			List<Vector2Int> result = new List<Vector2Int>();
			for (var i = 0; i < posList.Count; i++)
			{
				var pos = posList[i];
				result.Add(FromParentPos(pos));
			}

			return result;
		}

		//将父级场景坐标转换为自身坐标
		public AStarRange FromParentRange(AStarRange range)
		{
			if (IsChildScene())
			{
				Vector2Int leftBottom = FromParentPos(new Vector2Int(range.leftBottomX, range.leftBottomY));
				Vector2Int rightTop = FromParentPos(new Vector2Int(range.rightTopX, range.rightTopY));
				return new AStarRange(leftBottom, rightTop);
			}
			else
				return range;
		}

		//检测指定点是否属于该地图内
		public bool IsInMapRange(Vector2Int pos)
		{
			int[][] projectGrids = GetProjectGrids();
			if (projectGrids == null || !AStarUtil.IsInRange(projectGrids, pos))
				return false;
			return projectGrids[pos.x][pos.y] != 0; // 投影层不为空的范围就是图内
		}


		// 随机获取地图上一点
		public Vector2Int GetRandomPos(int[] canPassObstacleTypes, int[] canPassTerrainTypes,
			RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			int width = GetWidth();
			int height = GetHeight();
			int x = randomManager.RandomInt(0, width);
			int y = randomManager.RandomInt(0, height);

			Vector2Int result = new Vector2Int(x, y);
			if (canPassObstacleTypes != null || canPassTerrainTypes != null)
			{
				if (canPassObstacleTypes == null)
					canPassObstacleTypes = AStarMapPathConst.Air_Can_Pass_Obstacle_Types;
				if (canPassTerrainTypes == null)
					canPassTerrainTypes = AStarMapPathConst.Air_Can_Pass_Terrain_Types;
				Vector2Int? freePoint = AStarUtil.FindAroundFreePoint(GetMapPath(), result, null, canPassObstacleTypes,
					canPassTerrainTypes, randomManager);
				if (freePoint != null)
					result = freePoint.Value;
			}

			return result;
		}
	}
}