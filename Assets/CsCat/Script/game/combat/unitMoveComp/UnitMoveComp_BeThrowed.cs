using System;
using UnityEngine;

namespace CsCat
{
	public partial class UnitMoveComp
	{
		public UnitBeThrowedInfo unitBeThrowedInfo;

		public void BeThrowed(UnitBeThrowedInfo unitBeThrowedInfo)
		{
			Vector3 end_pos = unitBeThrowedInfo.end_pos;
			float duration = unitBeThrowedInfo.duration;
			float height = unitBeThrowedInfo.height;
			var end_rotation = unitBeThrowedInfo.end_rotation;
			var rotate_duration = unitBeThrowedInfo.rotate_duration;
			if ("be_throwed".Equals(this.move_type))
				return;
			var unit = this.unit;
			this.move_type = "be_throwed";
			if (unitBeThrowedInfo.IsHasAnimationName() && unit.animation != null)
				this.unit.PlayAnimation(unitBeThrowedInfo.animation_name);
			this.unitBeThrowedInfo = unitBeThrowedInfo;
			this.unit.UpdateMixedStates();
			this.unitBeThrowedInfo.org_height = unit.GetPosition().y;
			this.unitBeThrowedInfo.start_pos = unit.GetPosition();
			this.unitBeThrowedInfo.remain_duration = duration;
			float delta_height = end_pos.y - unit.GetPosition().y;
			// 起点和落点，取最高的，加上height，为真正的最高高度
			float max_height;
			if (delta_height > 0)
				max_height = Math.Max((delta_height + height), 0);
			else
				max_height = height;
			this.unitBeThrowedInfo.max_height = max_height;
			if (max_height == 0)
			{
				this.unitBeThrowedInfo.height_accelerate = delta_height * 2 / (duration * duration);
				this.unitBeThrowedInfo.height_speed = 0;
			}
			else
			{
				float h_time = duration / ((float)Math.Sqrt(1 - delta_height / max_height) + 1);
				this.unitBeThrowedInfo.height_accelerate = -2 * max_height / (h_time * h_time);
				this.unitBeThrowedInfo.height_speed = -this.unitBeThrowedInfo.height_accelerate * h_time;
			}

			if (end_rotation != null && rotate_duration != null)
			{
				this.unitBeThrowedInfo.rotate_remain_duration = rotate_duration.Value;
				this.unitBeThrowedInfo.start_rotation = unit.GetRotation();
			}
		}


		public void StopBeThrowed(bool is_end = false)
		{
			if (is_end)
			{
				if (this.unitBeThrowedInfo != null)
				{
					this.unitBeThrowedInfo.remain_duration = 0.02f;
					this.__UpdateBeThrowed(0.02f);
				}

				return;
			}

			if (this.unitBeThrowedInfo != null && !this.unitBeThrowedInfo.is_not_stop_animation &&
				this.unitBeThrowedInfo.IsHasAnimationName())
				this.unit.StopAnimation(this.unitBeThrowedInfo.animation_name, 0.2f);

			var is_back_to_ground = unitBeThrowedInfo != null ? unitBeThrowedInfo.is_back_to_ground : false;
			this.unitBeThrowedInfo = null;
			this.move_type = null;
			this.unit.UpdateMixedStates();

			if (is_back_to_ground)
			{
				var unitBeThrowedInfo = new UnitBeThrowedInfo();
				unitBeThrowedInfo.end_pos = Client.instance.combat.pathManager.GetGroundPos(this.unit.GetPosition());
				unitBeThrowedInfo.duration = 0.1f;
				unitBeThrowedInfo.height = 0f;
				unitBeThrowedInfo.is_back_to_ground = false;
				this.BeThrowed(unitBeThrowedInfo);
			}
		}

		public void __UpdateBeThrowed(float deltaTime)
		{
			var unit = this.unit;
			this.unitBeThrowedInfo.remain_duration = this.unitBeThrowedInfo.remain_duration - deltaTime;
			if (this.unitBeThrowedInfo.remain_duration <= 0)
			{
				this.StopBeThrowed();
				return;
			}

			float passed_duration = this.unitBeThrowedInfo.duration - this.unitBeThrowedInfo.remain_duration; // 已经运行的时间
																											  //计算高度
			float cur_height;
			if (this.unitBeThrowedInfo.calc_height_func != null)
				cur_height = this.unitBeThrowedInfo.org_height +
							 this.unitBeThrowedInfo.calc_height_func(this.unitBeThrowedInfo);
			else
				cur_height = this.unitBeThrowedInfo.org_height + this.unitBeThrowedInfo.height_speed * passed_duration +
							 this.unitBeThrowedInfo.height_accelerate * passed_duration * passed_duration * 0.5f;
			//计算水平位置
			float interp = (float)Math.Pow((1 - passed_duration / this.unitBeThrowedInfo.duration),
			  this.unitBeThrowedInfo.interp);
			Vector3 new_pos = this.unitBeThrowedInfo.start_pos * interp + this.unitBeThrowedInfo.end_pos * (1 - interp);

			new_pos.y = cur_height;
			unit.SetPosition(new_pos);

			if (this.unitBeThrowedInfo.rotate_duration != null && this.unitBeThrowedInfo.rotate_remain_duration != null)
			{
				this.unitBeThrowedInfo.rotate_remain_duration = this.unitBeThrowedInfo.rotate_remain_duration - deltaTime;
				if (this.unitBeThrowedInfo.rotate_remain_duration <= 0)
				{
					this.unitBeThrowedInfo.rotate_duration = null;
					this.unitBeThrowedInfo.rotate_remain_duration = null;
					unit.SetRotation(this.unitBeThrowedInfo.end_rotation.Value);
				}
				else
					unit.SetRotation(Quaternion.Slerp(this.unitBeThrowedInfo.start_rotation,
					  this.unitBeThrowedInfo.end_rotation.Value,
					  this.unitBeThrowedInfo.rotate_remain_duration.Value / this.unitBeThrowedInfo.rotate_duration.Value));
			}
		}
	}
}