using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UnitMoveComp
	{
		public void Move(Vector3 targetPos, float? speed, string animationName = null)
		{
			MoveByPath(new List<Vector3>() { targetPos }, speed, animationName);
		}

		public void MoveByPath(List<Vector3> path, float? speed, string animationName = null)
		{
			var unit = this._unit;
			if (!unit.IsCanMove())
				return;
			this.moveType = "move";
			if (!path.IsNullOrEmpty() && !unit.isInSight)
				unit.SetPosition(path[0]);
			this._unitMoveInfo.path = path;
			if (speed != null)
				this._unitMoveInfo.speed = speed.Value;
			if (!animationName.IsNullOrWhiteSpace())
				this._unitMoveInfo.animationName = animationName;
			float lastMoveAnimationSpeed = this._unitMoveInfo.animationSpeed;
			this._unitMoveInfo.animationSpeed = this._unitMoveInfo.speed / this._walkStepLength;
			if (this._unitMoveInfo.IsHasAnimationName()
				&& this._isMoveWithMoveAnimation
				&& Math.Abs(this._unitMoveInfo.animationSpeed - lastMoveAnimationSpeed) > 0.2f)
				unit.PlayAnimation(this._unitMoveInfo.animationName, 0.2f, this._unitMoveInfo.animationSpeed);
			this._MoveNextTarget(1);
		}


		public void _MoveNextTarget(int? index = null)
		{
			var unit = this._unit;
			if (index != null)
				this._unitMoveInfo.targetIndexInPath = index.Value;
			else
				this._unitMoveInfo.targetIndexInPath = this._unitMoveInfo.targetIndexInPath + 1;
			if (this._unitMoveInfo.path.ContainsIndex(this._unitMoveInfo.targetIndexInPath))
			{
				Vector3 originPos = this._unitMoveInfo.path[this._unitMoveInfo.targetIndexInPath - 1];
				this._unitMoveInfo.targetPos = this._unitMoveInfo.path[this._unitMoveInfo.targetIndexInPath];
				float distance = Vector3.Distance(this._unitMoveInfo.targetPos, originPos);
				this._unitMoveInfo.remainDuration = distance / this._unitMoveInfo.speed;
				this._unit.MoveTo(this._unitMoveInfo.targetPos, this._unitMoveInfo.remainDuration);
				this._unitMoveInfo.endRotation = Quaternion.LookRotation(this._unitMoveInfo.targetPos - this._unit.GetPosition())
				  .GetNotZero(unit.GetRotation());
				this._unitMoveInfo.rotateRemainDuration =
				  Quaternion.Angle(this._unitMoveInfo.endRotation, unit.GetRotation()) / 1080;
			}
			else
				this.MoveStop();
		}

		public void MoveStop(Quaternion? rotation = null, Vector3? pos = null)
		{
			var unit = this._unit;
			if (pos != null &&
				(!unit.isInSight || Vector3.SqrMagnitude(unit.GetPosition() - pos.Value) > this.adjustDistSqr))
				unit.SetPosition(pos.Value);
			if (rotation != null)
			{
				this._unitMoveInfo.endRotation = rotation.Value;
				this._unitMoveInfo.rotateRemainDuration = Quaternion.Angle(rotation.Value, unit.GetRotation()) / 720;
			}

			if (this.moveType.IsNullOrWhiteSpace())
				return;
			if ("move".Equals(this.moveType) && this._isMoveWithMoveAnimation && this._unitMoveInfo.IsHasAnimationName())
			{
				if (unit.animation != null)
					unit.StopAnimation(this._unitMoveInfo.animationName, 0.2f); //animation动画是层叠的，停掉walk自动就播放idle
				else
					unit.animatorComp.PlayAnimation(AnimationNameConst.idle, true);
			}

			this.moveType = null;
			this._unitMoveInfo.remainDuration = 0;
			this._unitMoveInfo.animationSpeed = -1;
			unit.StopMoveTo();
		}

		public void _UpdateMove(float deltaTime)
		{
			var unit = this._unit;
			var deltaTimeRemainDuration = deltaTime;
			while ("move".Equals(this.moveType) && deltaTimeRemainDuration > 0)
			{
				Vector3 newPos;
				if (this._unitMoveInfo.remainDuration > deltaTimeRemainDuration)
				{
					newPos = Vector3.Lerp(unit.GetPosition(), this._unitMoveInfo.targetPos,
					  deltaTimeRemainDuration / this._unitMoveInfo.remainDuration);
					this._unitMoveInfo.remainDuration = this._unitMoveInfo.remainDuration - deltaTimeRemainDuration;
					deltaTimeRemainDuration = 0;
				}
				else
				{
					newPos = this._unitMoveInfo.targetPos;
					deltaTimeRemainDuration = deltaTimeRemainDuration - this._unitMoveInfo.remainDuration;
				}

				Vector3 lookDir;
				if (unit.unitLockTargetInfo.IsHasLockTarget())
					lookDir = unit.unitLockTargetInfo.GetLockTargetPosition() - unit.GetPosition();
				else
					lookDir = newPos - unit.GetPosition();
				unit.OnlyFaceToDir(lookDir);
				unit.SetPosition(newPos);
				if (deltaTimeRemainDuration > 0)
					this._MoveNextTarget();
			}

			if (this._unitMoveInfo.lookAtUnit != null)
			{
				if (this._unitMoveInfo.lookAtUnit.IsDead())
				{
					this._unitMoveInfo.lookAtUnit = null;
					return;
				}

				Vector3 dir = this._unitMoveInfo.lookAtUnit.GetPosition() - unit.GetPosition();
				float angle = Vector3.Angle(unit.GetRotation().Forward(), dir);
				if (angle > 5)
				{
					var targetRotation = Quaternion.LookRotation(dir).GetNotZero(unit.GetRotation());
					unit.SetRotation(Quaternion.Slerp(unit.GetRotation(), targetRotation, 0.3f));
				}
			}
			else
			{
				if (this._unitMoveInfo.rotateRemainDuration > 0)
				{
					if (this._unitMoveInfo.rotateRemainDuration <= deltaTime)
					{
						unit.SetRotation(this._unitMoveInfo.endRotation);
						this._unitMoveInfo.rotateRemainDuration = 0;
					}
					else
					{
						unit.SetRotation(Quaternion.Slerp(unit.GetRotation(), this._unitMoveInfo.endRotation,
						  deltaTime / this._unitMoveInfo.rotateRemainDuration));
						this._unitMoveInfo.rotateRemainDuration = this._unitMoveInfo.rotateRemainDuration - deltaTime;
					}
				}
			}
		}
	}
}