using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    // 数据对象
    public class Thing : Doer
    {
        public void SetPos(Vector2Int pos)
        {
            SetTmp(StringConst.String_o_pos, pos);
        }

        public Vector2Int GetPos()
        {
            return GetTmp<Vector2Int>(StringConst.String_o_pos);
        }

        //////////////////////////////////////OnXXX/////////////////////////////////////


        //本物件进入场景to_scene事件
        public void OnEnterScene(Scene toScene)
        {
        }

        //本物件离开场景from_scene事件
        public void OnLeaveScene(Scene fromScene)
        {
        }

        //本物件在场景中移动事件
        public void OnMove(Scene scene, Vector2Int fromPos, Vector2Int toPos, List<Vector2Int> trackList, int type)
        {
        }

        //////////////////////////////////////GetXXX/////////////////////////////////////


        //////////////////////////////////////SetXXX/////////////////////////////////////


        /////////////////////////////////////////////////////////

        public bool IsInAround(Vector2Int comparePos, int radius)
        {
            return AStarUtil.IsInAround(this.GetPos(), comparePos, radius);
        }

        public bool IsInSector(Vector2Int sectorCenterPos, Vector2 sectorDir, int sectorRadius,
            float sectorHalfDegrees)
        {
            return AStarUtil.IsInSector(this.GetPos(), sectorCenterPos, sectorDir, sectorRadius, sectorHalfDegrees);
        }
    }
}