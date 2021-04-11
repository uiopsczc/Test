using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class Unit
  {
    public UnitMoveComp unitMoveComp;

    public void __MoveTo(Vector3 move_to_target_pos, float duration)
    {
      if (graphicComponent.transform == null)
        return;
      move_to_target_pos = this.unitDefinition.offset_y > 0
        ? (move_to_target_pos + new Vector3(0, this.unitDefinition.offset_y, 0))
        : move_to_target_pos;
      Client.instance.moveManager.MoveTo(graphicComponent.transform, move_to_target_pos, duration);
    }

    public void StopMoveTo()
    {
      if (graphicComponent.transform == null)
        return;
      Client.instance.moveManager.StopMoveTo(graphicComponent.transform);
    }

    public void Move(Vector3 target_pos, float? speed = null)
    {
      if (!this.IsCanMove())
        return;
      this.unitMoveComp.Move(target_pos, speed);
    }

    public void MoveByPath(List<Vector3> path, float? speed = null)
    {
      if (!this.IsCanMove())
        return;
      this.unitMoveComp.MoveByPath(path, speed);
    }

    public void MoveStop(Quaternion? rotation = null, Vector3? pos = null)
    {
      this.unitMoveComp.MoveStop(rotation, pos);
    }

    public void BeThrowed(UnitBeThrowedInfo unitBeThrowedInfo)
    {
      if (this.IsDead() || this.IsImmuneControl())
        return;
      this.unitMoveComp.BeThrowed(unitBeThrowedInfo);
    }

    public void StopBeThrowed(bool is_end)
    {
      this.unitMoveComp.StopBeThrowed(is_end);
    }

    public void SetIsMoveWithMoveAnimation(bool is_move_with_move_animation)
    {
      this.unitMoveComp.SetIsMoveWithMoveAnimation(is_move_with_move_animation);
    }

    public void FaceTo(Quaternion rotation)
    {
      if (graphicComponent.transform == null)
        return;
    }

    public void OnlyFaceTo(Quaternion rotation)
    {
      if (graphicComponent.transform == null)
        return;
    }

    public void FaceToDir(Vector3 dir)
    {
      if (graphicComponent.transform == null)
        return;
    }

    public void OnlyFaceToDir(Vector3 dir)
    {
      if (graphicComponent.transform == null)
        return;
    }

    public void LookAt(Unit unit, string mode)
    {
      this.unitMoveComp.LookAt(unit, mode);
    }

    public void LookAt(Vector3 eulerAngle, string mode)
    {
      this.unitMoveComp.LookAt(eulerAngle, mode);
    }

  }
}