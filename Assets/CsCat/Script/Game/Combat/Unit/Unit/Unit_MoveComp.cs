using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		public UnitMoveComp unitMoveComp;

		public void MoveTo(Vector3 moveToTargetPos, float duration)
		{
			if (graphicComponent.transform == null)
				return;
			moveToTargetPos = this.cfgUnitData.offsetYy > 0
			  ? (moveToTargetPos + new Vector3(0, this.cfgUnitData.offsetYy, 0))
			  : moveToTargetPos;
			Client.instance.moveManager.MoveTo(graphicComponent.transform, moveToTargetPos, duration);
		}

		public void StopMoveTo()
		{
			if (graphicComponent.transform == null)
				return;
			Client.instance.moveManager.StopMoveTo(graphicComponent.transform);
		}

		public void Move(Vector3 targetPos, float? speed = null)
		{
			if (!this.IsCanMove())
				return;
			this.unitMoveComp.Move(targetPos, speed);
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

		public void StopBeThrowed(bool isEnd)
		{
			this.unitMoveComp.StopBeThrowed(isEnd);
		}

		public void SetIsMoveWithMoveAnimation(bool isMoveWithMoveAnimation)
		{
			this.unitMoveComp.SetIsMoveWithMoveAnimation(isMoveWithMoveAnimation);
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