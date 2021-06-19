using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class Scene : Thing
  {
    private Scenes o_child_scenes;
    private SceneItems o_sceneItems;

    public override void Init()
    {
      base.Init();
      this.o_child_scenes = new Scenes(this, "o_child_scenes");
      this.o_sceneItems = new SceneItems(this, "o_sceneItems");
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
    void DoMove(Thing thing, Vector2Int from_pos, Vector2Int to_pos, List<Vector2Int> track_list, int type)
    {
      try
      {
        OnMoveThing(thing, from_pos, to_pos, track_list, type);
      }
      catch (Exception exception)
      {
        LogCat.LogError(string.Format("to.OnMoveThing error! [{0},{1}->({2}->{3}]:{4}", this, thing, from_pos, to_pos,
          exception));
      }

      try
      {
        thing.OnMove(this, from_pos, to_pos, track_list, type);
      }
      catch (Exception exception)
      {
        LogCat.LogError(string.Format("thing.OnMove error! [{0},{1}->({2}->{3}]:{4}", this, thing, from_pos, to_pos,
          exception));
      }
    }

    //物件thing本场景转移到另一场景事件
    void DoShift(Thing thing, Vector2Int from_pos, Scene child_scene, Vector2Int to_pos, int type)
    {
      try
      {
        OnShift(thing, from_pos, child_scene, to_pos, type);
      }
      catch (Exception exception)
      {
        LogCat.LogError(string.Format("env.OnShift error! [{0},{1}->({2}->{3}]:{4}", this, thing, from_pos, to_pos,
          exception));
      }
    }

    //////////////////////OnXXX/////////////////////////////////////
    public override void OnInit()
    {
      base.OnInit();
    }

    public override void OnSave(Hashtable dict, Hashtable dict_tmp)
    {
      base.OnSave(dict, dict_tmp);
    }

    public override void OnRestore(Hashtable dict, Hashtable dict_tmp)
    {
      base.OnRestore(dict, dict_tmp);
    }

    public void OnEnter(Thing thing)
    {
    }

    public void OnLeave(Thing thing)
    {
    }

    //物件thing在本场景中移动事件
    public void OnMoveThing(Thing thing, Vector2Int from_pos, Vector2Int to_pos, List<Vector2Int> track_list, int type)
    {
    }

    //物件thing本场景转移到另一场景事件
    public void OnShift(Thing thing, Vector2Int from_pos, Scene child_scene, Vector2Int to_pos, int type)
    {
    }


    ////////////////////////////////////////////Util///////////////////////////////////////////////////
    //是否子场景
    public bool IsChildScene(Scene to_top_parent_scene = null)
    {
      return GetEnv() != null;
    }

    public bool IsChildSceneOf(Scene to_top_parent_scene = null)
    {
      if (to_top_parent_scene != null)
      {
        var parent_scene = GetEnv();
        while (parent_scene != null)
        {
          if (to_top_parent_scene == parent_scene)
            return true;
          parent_scene = parent_scene.GetEnv();
        }

        return false;
      }

      return GetEnv() == null ? true : false;
    }


    public void SetIsInAir(bool is_in_air)
    {
      SetTmp("o_is_in_air", is_in_air);
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
      Scene parent_scene = this.GetEnv<Scene>();
      if (parent_scene != null)
      {
        parent_scene.ClearProjectGrids(GetPos(), this);
        parent_scene.SetProjectGrids(GetPos(), this);
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
      if (sceneMapInfo != null)
        return sceneMapInfo.grids;
      return null;
    }

    // 自身投影数据 project_grids[x][y]
    public int[][] GetProjectGrids()
    {
      SceneMapInfo sceneMapInfo = GetSceneMapInfo();
      if (sceneMapInfo != null)
        return sceneMapInfo.project_grids;
      return null;
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
        return sceneMapInfo.offset_pos;
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
    public int ToParentX(Vector2Int base_on_parent_pos, Vector2Int offset_pos, int x)
    {
      return base_on_parent_pos.x - offset_pos.x + x;
    }

    //将自身y坐标转换为父级场景y坐标
    public int ToParentY(Vector2Int base_on_parent_pos, Vector2Int offset_pos, int y)
    {
      return base_on_parent_pos.y - offset_pos.y + y;
    }

    public Vector2Int ToParentPos(Vector2Int base_on_parent_pos, Vector2Int offset_pos, Vector2Int pos)
    {
      return new Vector2Int(ToParentX(base_on_parent_pos, offset_pos, pos.x),
        ToParentY(base_on_parent_pos, offset_pos, pos.y));
    }

    //将自身坐标转换为父级场景坐标
    public Vector2Int ToParentPos(Vector2Int pos, Scene to_top_parent_scene)
    {
      bool is_child_scene = IsChildScene();
      if (is_child_scene)
      {
        Vector2Int base_on_parent_pos = GetPos();
        Vector2Int offset_pos = GetOffsetPos();
        pos = ToParentPos(base_on_parent_pos, offset_pos, pos);
      }

      if (to_top_parent_scene != this.GetEnv())
      {
        if (is_child_scene)
          return this.GetEnv<Scene>().ToParentPos(pos, to_top_parent_scene);
        throw new Exception(string.Format("没有目标的scene:{0}", to_top_parent_scene));
      }
      else
        return pos;
    }

    //将自身坐标转换为父级场景坐标
    public List<Vector2Int> ToParentPosList(List<Vector2Int> pos_list, Scene to_top_parent_scene)
    {
      List<Vector2Int> result = new List<Vector2Int>();
      foreach (var pos in pos_list)
        result.Add(ToParentPos(pos, to_top_parent_scene));
      return result;
    }

    //将自身坐标转换为父级场景坐标
    public AStarRange ToParentRange(AStarRange range, Scene to_top_parent_scene)
    {
      Vector2Int left_bottom =
        ToParentPos(new Vector2Int(range.left_bottom_x, range.left_bottom_y), to_top_parent_scene);
      Vector2Int right_top = ToParentPos(new Vector2Int(range.right_top_x, range.right_top_y), to_top_parent_scene);
      return new AStarRange(left_bottom, right_top);
    }

    //将父级场景x坐标转换为自身x坐标
    public int FromParentX(Vector2Int base_on_parent_pos, Vector2Int offset_pos, int x)
    {
      return x - base_on_parent_pos.x + offset_pos.x;
    }

    //将父级场景y坐标转换为自身y坐标
    public int FromParentY(Vector2Int base_on_parent_pos, Vector2Int offset_pos, int y)
    {
      return y - base_on_parent_pos.y + offset_pos.y;
    }

    public Vector2Int FromParentPos(Vector2Int base_on_parent_pos, Vector2Int offset_pos, Vector2Int pos)
    {
      return new Vector2Int(FromParentX(base_on_parent_pos, offset_pos, pos.x),
        FromParentY(base_on_parent_pos, offset_pos, pos.y));
    }

    //将父级场景坐标转换为自身坐标
    public Vector2Int FromParentPos(Vector2Int pos)
    {
      if (IsChildScene())
      {
        Vector2Int base_on_parent_pos = GetPos();
        Vector2Int offset_pos = GetOffsetPos();
        return FromParentPos(base_on_parent_pos, offset_pos, pos);
      }
      else
        return pos;
    }

    //将父级场景坐标转换为自身坐标
    public List<Vector2Int> FromParentPosList(List<Vector2Int> pos_list)
    {
      List<Vector2Int> result = new List<Vector2Int>();
      foreach (var pos in pos_list)
        result.Add(FromParentPos(pos));
      return result;
    }

    //将父级场景坐标转换为自身坐标
    public AStarRange FromParentRange(AStarRange range)
    {
      if (IsChildScene())
      {
        Vector2Int left_bottom = FromParentPos(new Vector2Int(range.left_bottom_x, range.left_bottom_y));
        Vector2Int right_top = FromParentPos(new Vector2Int(range.right_top_x, range.right_top_y));
        return new AStarRange(left_bottom, right_top);
      }
      else
        return range;
    }

    //检测指定点是否属于该地图内
    public bool IsInMapRange(Vector2Int pos)
    {
      int[][] project_grids = GetProjectGrids();
      if (project_grids == null || !AStarUtil.IsInRange(project_grids, pos))
        return false;
      return project_grids[pos.x][pos.y] != 0; // 投影层不为空的范围就是图内
    }


    // 随机获取地图上一点
    public Vector2Int GetRandomPos(int[] can_pass_obstacle_types, int[] can_pass_terrain_types,
      RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      int width = GetWidth();
      int height = GetHeight();
      int x = randomManager.RandomInt(0, width);
      int y = randomManager.RandomInt(0, height);

      Vector2Int result = new Vector2Int(x, y);
      if (can_pass_obstacle_types != null || can_pass_terrain_types != null)
      {
        if (can_pass_obstacle_types == null)
          can_pass_obstacle_types = AStarMapPathConst.Air_Can_Pass_Obstacle_Types;
        if (can_pass_terrain_types == null)
          can_pass_terrain_types = AStarMapPathConst.Air_Can_Pass_Terrain_Types;
        Vector2Int? free_point = AStarUtil.FindAroundFreePoint(GetMapPath(), result, null, can_pass_obstacle_types,
          can_pass_terrain_types, randomManager);
        if (free_point != null)
          result = free_point.Value;
      }

      return result;
    }
  }
}