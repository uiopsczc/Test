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
			var unit = this.unit;
			if (!unit.IsCanMove())
				return;
			this.moveType = "move";
			if (!path.IsNullOrEmpty() && !unit.isInSight)
				unit.SetPosition(path[0]);
			this.unitMoveInfo.path = path;
			if (speed != null)
				this.unitMoveInfo.speed = speed.Value;
			if (!animationName.IsNullOrWhiteSpace())
				this.unitMoveInfo.animationName = animationName;
			float lastMoveAnimationSpeed = this.unitMoveInfo.animationSpeed;
			this.unitMoveInfo.animationSpeed = this.unitMoveInfo.speed / this.walkStepLength;
			if (this.unitMoveInfo.IsHasAnimationName()
				&& this.isMoveWithMoveAnimation
				&& Math.Abs(this.unitMoveInfo.animationSpeed - lastMoveAnimationSpeed) > 0.2f)
				unit.PlayAnimation(this.unitMoveInfo.animationName, 0.2f, this.unitMoveInfo.animationSpeed);
			this.__MoveNextTarget(1);
		}


		public void __MoveNextTarget(int? index = null)
		{
			var unit = this.unit;
			if (index != null)
				this.unitMoveInfo.targetIndexInPath = index.Value;
			else
				this.unitMoveInfo.targetIndexInPath = this.unitMoveInfo.targetIndexInPath + 1;
			if (this.unitMoveInfo.path.ContainsIndex(this.unitMoveInfo.targetIndexInPath))
			{
				Vector3 originPos = this.unitMoveInfo.path[this.unitMoveInfo.targetIndexInPath - 1];
				this.unitMoveInfo.targetPos = this.unitMoveInfo.path[this.unitMoveInfo.targetIndexInPath];
				float distance = Vector3.Distance(this.unitMoveInfo.targetPos, originPos);
				this.unitMoveInfo.remainDuration = distance / this.unitMoveInfo.speed;
				this.unit.__MoveTo(this.unitMoveInfo.targetPos, this.unitMoveInfo.remainDuration);
				this.unitMoveInfo.endRotation = Quaternion.LookRotation(this.unitMoveInfo.targetPos - this.unit.GetPosition())
				  .GetNotZero(unit.GetRotation());
				this.unitMoveInfo.rotateRemainDuration =
				  Quaternion.Angle(this.unitMoveInfo.endRotation, unit.GetRotation()) / 1080;
			}
			else
				this.MoveStop();
		}

		public void MoveStop(Quaternion? rotation = null, Vector3? pos = null)
		{
			var unit = this.unit;
			if (pos != null &&
				(!unit.isInSight || Vector3.SqrMagnitude(unit.GetPosition() - pos.Value) > this.adjustDistSqr))
				unit.SetPosition(pos.Value);
			if (rotation != null)
			{
				this.unitMoveInfo.endRotation = rotation.Value;
				this.unitMoveInfo.rotateRemainDuration = Quaternion.Angle(rotation.Value, unit.GetRotation()) / 720;
			}

			if (this.moveType.IsNullOrWhiteSpace())
				return;
			if ("move".Equals(this.moveType) && this.isMoveWithMoveAnimation && this.unitMoveInfo.IsHasAnimationName())
			{
				if (unit.animation != null)
					unit.StopAnimation(this.unitMoveInfo.animationName, 0.2f); //animation动画是层叠的，停掉walk自动就播放idle
				else
					unit.animatorComp.PlayAnimation(AnimationNameConst.idle, true);
			}

			this.moveType = null;
			this.unitMoveInfo.remainDuration = 0;
			this.unitMoveInfo.animationSpeed = -1;
			unit.StopMoveTo();
		}

		public void __UpdateMove(float deltaTime)
		{
			var unit = this.unit;
			var deltaTimeRemainDuration = deltaTime;
			while ("move".Equals(this.moveType) && deltaTimeRemainDuration > 0)
			{
				Vector3 newPos;
				if (this.unitMoveInfo.remainDuration > deltaTimeRemainDuration)
				{
					newPos = Vector3.Lerp(unit.GetPosition(), this.unitMoveInfo.targetPos,
					  deltaTimeRemainDuration / this.unitMoveInfo.remainDuration);
					this.unitMoveInfo.remainDuration = this.unitMoveInfo.remainDuration - deltaTimeRemainDuration;
					deltaTimeRemainDuration = 0;
				}
				else
				{
					newPos = this.unitMoveInfo.targetPos;
					deltaTimeRemainDuration = deltaTimeRemainDuration - this.unitMoveInfo.remainDuration;
				}

				Vector3 lookDir;
				if (unit.unitLockTargetInfo.IsHasLockTarget())
					lookDir = unit.unitLockTargetInfo.GetLockTargetPosition() - unit.GetPosition();
				else
					lookDir = newPos - unit.GetPosition();
				unit.OnlyFaceToDir(lookDir);
				unit.SetPosition(newPos);
				if (deltaTimeRemainDuration > 0)
					this.__MoveNextTarget();
			}

			if (this.unitMoveInfo.lookAtUnit != null)
			{
				if (this.unitMoveInfo.lookAtUnit.IsDead())
				{
					this.unitMoveInfo.lookAtUnit = null;
					return;
				}

				Vector3 dir = this.unitMoveInfo.lookAtUnit.GetPosition() - unit.GetPosition();
				float angle = Vector3.Angle(unit.GetRotation().Forward(), dir);
				if (angle > 5)
				{
					var targetRotation = Quaternion.LookRotation(dir).GetNotZero(unit.GetRotation());
					unit.SetRotation(Quaternion.Slerp(unit.GetRotation(), targetRotation, 0.3f));
				}
			}
			else
			{
				if (this.unitMoveInfo.rotateRemainDuration > 0)
				{
					if (this.unitMoveInfo.rotateRemainDuration <= deltaTime)
					{
						unit.SetRotation(this.unitMoveInfo.endRotation);
						this.unitMoveInfo.rotateRemainDuration = 0;
					}
					else
					{
						unit.SetRotation(Quaternion.Slerp(unit.GetRotation(), this.unitMoveInfo.endRotation,
						  deltaTime / this.unitMoveInfo.rotateRemainDuration));
						this.unitMoveInfo.rotateRemainDuration = this.unitMoveInfo.rotateRemainDuration - deltaTime;
					}
				}
			}
		}
	}
}