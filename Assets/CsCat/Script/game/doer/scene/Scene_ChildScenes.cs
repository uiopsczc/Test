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
    public void ClearProjectGrids(Vector2Int base_on_parent_pos, Scene child_scene)
    {
      CheckParentCall();

      AStarMapPath mapPath = GetMapPath();
      int[][] project_grids;
      if (child_scene.GetMapType() == 1)
        project_grids = child_scene.GetGrids();
      else
        project_grids = child_scene.GetProjectGrids();
      if (project_grids != null)
      {
        Vector2Int offset_pos = child_scene.GetOffsetPos();
        for (int x = 0; x < project_grids.Length; x++)
        {
          for (int y = 0; y < project_grids[x].Length; y++)
          {
            int v = project_grids[x][y];
            if (v == 0 || !AStarUtil.IsValidObstacleType(v)) // 子场景无效区域不投影
              continue;
            int px = ToParentX(base_on_parent_pos, offset_pos, x);
            int py = ToParentY(base_on_parent_pos, offset_pos, y);
            if (mapPath.IsValidPoint(px, py)) // 父场景无效区域不投影
              mapPath.project_grids[px][py] = 0;
          }
        }
      }
    }

    // 设置子场景投影障碍到主场景（仅供父级场景调用）
    public void SetProjectGrids(Vector2Int base_on_parent_pos, Scene child_scene)
    {
      CheckParentCall();

      AStarMapPath mapPath = GetMapPath();
      if (mapPath == null || mapPath.grids == null)
        return;
      int[][] project_grids;
      if (child_scene.GetMapType() == 1)
        project_grids = child_scene.GetGrids();
      else
        project_grids = child_scene.GetProjectGrids();
      if (project_grids != null)
      {
        Vector2Int offset_pos = child_scene.GetOffsetPos();
        for (int x = 0; x < project_grids.Length; x++)
        {
          for (int y = 0; y < project_grids[x].Length; y++)
          {
            int v = project_grids[x][y];
            if (v == 0 || !AStarUtil.IsValidObstacleType(v)) // 子场景无效区域不投影
              continue;
            int px = ToParentX(base_on_parent_pos, offset_pos, x);
            int py = ToParentY(base_on_parent_pos, offset_pos, y);
            if (mapPath.IsValidPoint(px, py)) // 父场景无效区域不投影
              mapPath.project_grids[px][py] = v;
          }
        }
      }
    }


    //清空所有动态障碍（仅供父级场景调用）
    public void ClearAllProjectGrids()
    {
      CheckParentCall();

      AStarMapPath mapPath = GetMapPath();
      if (mapPath == null || mapPath.project_grids == null)
        return;
      for (int x = 0; x < mapPath.project_grids.Length; x++)
      {
        for (int y = 0; y < mapPath.project_grids[x].Length; y++)
        {
          mapPath.project_grids[x][y] = 0;
        }
      }
    }

    //重置所有动态障碍（仅供父级场景调用）
    public void ResetAllProjectGrids()
    {
      CheckParentCall();

      ClearAllProjectGrids();
      foreach (var child_scene in GetChildScenes())
      {
        if (!child_scene.IsInAir())
          SetProjectGrids(child_scene.GetPos(), child_scene);
      }
    }

    ////////////////////////////子场景容器////////////////////////
    public Scene[] GetChildScenes(string id = null, string belong = null)
    {
      if (belong == null && id == null)
        return this.o_child_scenes.GetScenes();
      else
        return this.o_child_scenes.GetScenes(null, scene =>
        {
          if (belong != null && !scene.GetBelong().Equals(belong))
            return false;
          if (id != null && !scene.GetId().Equals(id))
            return false;
          return true;
        });
    }

    public Scene GetChildScene(string id_or_rid, string belong = null)
    {
      if (IdUtil.IsRid(id_or_rid)) // rid的情况
      {
        string rid = id_or_rid;
        if (!this.o_child_scenes.GetSceneDict_ToEdit().ContainsKey(rid))
          return null;
        Scene child_scene = this.o_child_scenes.GetSceneDict_ToEdit()[rid] as Scene;
        if (belong != null && !child_scene.GetBelong().Equals(belong))
          return null;
        return child_scene;
      }
      else // id的情况
      {
        string id = id_or_rid;
        Scene[] child_scenes = GetChildScenes(id, belong);
        return child_scenes.Length == 0 ? null : child_scenes[0];
      }
    }

    //获得场景内所有子场景数量（仅供父级场景调用）
    public int GetChildSceneCount(string belong = null)
    {
      CheckParentCall();
      return GetChildScenes(null, belong).Length;
    }


    //添加子场景到指定坐标（仅供父级场景调用）
    public void AddChildScene(Vector2Int pos, Scene child_scene)
    {
      CheckParentCall();

      child_scene.SetEnv(this);
      child_scene.SetPos(pos);

      this.o_child_scenes.GetSceneDict_ToEdit()[child_scene.GetRid()] = child_scene;

      // 处理子场景障碍投影
      if (!child_scene.IsInAir())
        SetProjectGrids(pos, child_scene);

      // 触发进入事件
      DoEnter(child_scene);
    }

    //移除子场景（仅供父级场景调用）
    public void RemoveChildScene(Scene child_scene)
    {
      CheckParentCall();

      bool is_contain = o_child_scenes.GetSceneDict_ToEdit().ContainsKey(child_scene.GetRid());
      o_child_scenes.GetSceneDict_ToEdit().Remove(child_scene.GetRid());
      if (is_contain)
      {
        // 处理子场景障碍投影
        if (!child_scene.IsInAir())
          ClearProjectGrids(child_scene.GetPos(), child_scene);

        // 触发离开事件
        DoLeave(child_scene);
        child_scene.SetEnv(null);
      }
    }

    //移除子场景（仅供父级场景调用）
    public void RemoveChildScene(string rid)
    {
      Scene child_scene = this.o_child_scenes.GetScene(rid);
      RemoveChildScene(child_scene);
    }


    //将子场景移到指定位置（仅供父级场景调用）
    public void MoveChildScene(Scene scene, Vector2Int to_pos, List<Vector2Int> track_list, int type)
    {
      CheckParentCall();

      Vector2Int from_pos = scene.GetPos();
      scene.SetPos(to_pos);
      scene.SetTmp("last_move_time", DateTimeUtil.NowTicks());
      scene.SetTmp("last_move_track_list", track_list);

      // 处理子场景障碍投影
      if (!scene.IsInAir())
      {
        ClearProjectGrids(from_pos, scene);
        SetProjectGrids(to_pos, scene);
      }

      // 触发移动事件
      DoMove(scene, from_pos, to_pos, track_list, type);
    }

    //获得指定范围的子场景（仅供父级场景调用）
    public Scene[] GetRangeScenes(AStarRange range, string belong)
    {
      CheckParentCall();

      List<Scene> list = new List<Scene>();
      foreach (Scene child_scene in GetChildScenes(null, belong))
      {
        if (range.IsInRange(child_scene.GetPos()))
          list.Add(child_scene);
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
      foreach (var child_scene in GetChildScenes(null, belong))
      {
        if (group.Equals(child_scene.GetGroup()))
          return child_scene;
      }

      return null;
    }

  }
}