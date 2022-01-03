using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UnitMoveComp
	{
		public void Move(Vector3 target_pos, float? speed, string animation_name = null)
		{
			MoveByPath(new List<Vector3>() { target_pos }, speed, animation_name);
		}

		public void MoveByPath(List<Vector3> path, float? speed, string animation_name = null)
		{
			var unit = this.unit;
			if (!unit.IsCanMove())
				return;
			this.move_type = "move";
			if (!path.IsNullOrEmpty() && !unit.is_in_sight)
				unit.SetPosition(path[0]);
			this.unitMoveInfo.path = path;
			if (speed != null)
				this.unitMoveInfo.speed = speed.Value;
			if (!animation_name.IsNullOrWhiteSpace())
				this.unitMoveInfo.animation_name = animation_name;
			float last_move_animation_speed = this.unitMoveInfo.animation_speed;
			this.unitMoveInfo.animation_speed = this.unitMoveInfo.speed / this.walk_step_length;
			if (this.unitMoveInfo.IsHasAnimationName()
				&& this.is_move_with_move_animation
				&& Math.Abs(this.unitMoveInfo.animation_speed - last_move_animation_speed) > 0.2f)
				unit.PlayAnimation(this.unitMoveInfo.animation_name, 0.2f, this.unitMoveInfo.animation_speed);
			this.__MoveNextTarget(1);
		}


		public void __MoveNextTarget(int? index = null)
		{
			var unit = this.unit;
			if (index != null)
				this.unitMoveInfo.target_index_in_path = index.Value;
			else
				this.unitMoveInfo.target_index_in_path = this.unitMoveInfo.target_index_in_path + 1;
			if (this.unitMoveInfo.path.ContainsIndex(this.unitMoveInfo.target_index_in_path))
			{
				Vector3 origin_pos = this.unitMoveInfo.path[this.unitMoveInfo.target_index_in_path - 1];
				this.unitMoveInfo.target_pos = this.unitMoveInfo.path[this.unitMoveInfo.target_index_in_path];
				float distance = Vector3.Distance(this.unitMoveInfo.target_pos, origin_pos);
				this.unitMoveInfo.remain_duration = distance / this.unitMoveInfo.speed;
				this.unit.__MoveTo(this.unitMoveInfo.target_pos, this.unitMoveInfo.remain_duration);
				this.unitMoveInfo.end_rotation = Quaternion.LookRotation(this.unitMoveInfo.target_pos - this.unit.GetPosition())
				  .GetNotZero(unit.GetRotation());
				this.unitMoveInfo.rotate_remain_duration =
				  Quaternion.Angle(this.unitMoveInfo.end_rotation, unit.GetRotation()) / 1080;
			}
			else
				this.MoveStop();
		}

		public void MoveStop(Quaternion? rotation = null, Vector3? pos = null)
		{
			var unit = this.unit;
			if (pos != null &&
				(!unit.is_in_sight || Vector3.SqrMagnitude(unit.GetPosition() - pos.Value) > this.adjust_dist_sqr))
				unit.SetPosition(pos.Value);
			if (rotation != null)
			{
				this.unitMoveInfo.end_rotation = rotation.Value;
				this.unitMoveInfo.rotate_remain_duration = Quaternion.Angle(rotation.Value, unit.GetRotation()) / 720;
			}

			if (this.move_type.IsNullOrWhiteSpace())
				return;
			if ("move".Equals(this.move_type) && this.is_move_with_move_animation && this.unitMoveInfo.IsHasAnimationName())
			{
				if (unit.animation != null)
					unit.StopAnimation(this.unitMoveInfo.animation_name, 0.2f); //animation动画是层叠的，停掉walk自动就播放idle
				else
					unit.animatorComp.PlayAnimation(AnimationNameConst.idle, true);
			}

			this.move_type = null;
			this.unitMoveInfo.remain_duration = 0;
			this.unitMoveInfo.animation_speed = -1;
			unit.StopMoveTo();
		}

		public void __UpdateMove(float deltaTime)
		{
			var unit = this.unit;
			var deltaTime_remain_duration = deltaTime;
			while ("move".Equals(this.move_type) && deltaTime_remain_duration > 0)
			{
				Vector3 new_pos;
				if (this.unitMoveInfo.remain_duration > deltaTime_remain_duration)
				{
					new_pos = Vector3.Lerp(unit.GetPosition(), this.unitMoveInfo.target_pos,
					  deltaTime_remain_duration / this.unitMoveInfo.remain_duration);
					this.unitMoveInfo.remain_duration = this.unitMoveInfo.remain_duration - deltaTime_remain_duration;
					deltaTime_remain_duration = 0;
				}
				else
				{
					new_pos = this.unitMoveInfo.target_pos;
					deltaTime_remain_duration = deltaTime_remain_duration - this.unitMoveInfo.remain_duration;
				}

				Vector3 look_dir;
				if (unit.unitLockTargetInfo.IsHasLockTarget())
					look_dir = unit.unitLockTargetInfo.GetLockTargetPosition() - unit.GetPosition();
				else
					look_dir = new_pos - unit.GetPosition();
				unit.OnlyFaceToDir(look_dir);
				unit.SetPosition(new_pos);
				if (deltaTime_remain_duration > 0)
					this.__MoveNextTarget();
			}

			if (this.unitMoveInfo.look_at_unit != null)
			{
				if (this.unitMoveInfo.look_at_unit.IsDead())
				{
					this.unitMoveInfo.look_at_unit = null;
					return;
				}

				Vector3 dir = this.unitMoveInfo.look_at_unit.GetPosition() - unit.GetPosition();
				float angle = Vector3.Angle(unit.GetRotation().Forward(), dir);
				if (angle > 5)
				{
					var target_rotation = Quaternion.LookRotation(dir).GetNotZero(unit.GetRotation());
					unit.SetRotation(Quaternion.Slerp(unit.GetRotation(), target_rotation, 0.3f));
				}
			}
			else
			{
				if (this.unitMoveInfo.rotate_remain_duration > 0)
				{
					if (this.unitMoveInfo.rotate_remain_duration <= deltaTime)
					{
						unit.SetRotation(this.unitMoveInfo.end_rotation);
						this.unitMoveInfo.rotate_remain_duration = 0;
					}
					else
					{
						unit.SetRotation(Quaternion.Slerp(unit.GetRotation(), this.unitMoveInfo.end_rotation,
						  deltaTime / this.unitMoveInfo.rotate_remain_duration));
						this.unitMoveInfo.rotate_remain_duration = this.unitMoveInfo.rotate_remain_duration - deltaTime;
					}
				}
			}
		}
	}
}