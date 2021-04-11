using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  // 数据对象
  public class Thing : Doer
  {
    public void SetPos(Vector2Int pos)
    {
      SetTmp("o_pos", pos);
    }

    public Vector2Int GetPos()
    {
      return GetTmp<Vector2Int>("o_pos");
    }

    //////////////////////////////////////OnXXX/////////////////////////////////////


    //本物件进入场景to_scene事件
    public void OnEnterScene(Scene to_scene)
    {
    }

    //本物件离开场景from_scene事件
    public void OnLeaveScene(Scene from_scene)
    {
    }

    //本物件在场景中移动事件
    public void OnMove(Scene scene, Vector2Int from_pos, Vector2Int to_pos, List<Vector2Int> track_list, int type)
    {
    }

    //////////////////////////////////////GetXXX/////////////////////////////////////


    //////////////////////////////////////SetXXX/////////////////////////////////////


    /////////////////////////////////////////////////////////

    public bool IsInAround(Vector2Int compare_pos, int radius)
    {
      if (AStarUtil.IsInAround(this.GetPos(), compare_pos, radius))
        return true;
      return false;
    }

    public bool IsInSector(Vector2Int sector_center_pos, Vector2 sector_dir, int sector_radius,
      float sector_half_degrees)
    {
      if (AStarUtil.IsInSector(this.GetPos(), sector_center_pos, sector_dir, sector_radius, sector_half_degrees))
        return true;
      return false;
    }
  }
}
