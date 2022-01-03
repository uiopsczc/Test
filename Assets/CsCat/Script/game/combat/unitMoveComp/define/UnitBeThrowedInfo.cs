using System;
using UnityEngine;

namespace CsCat
{
	public class UnitBeThrowedInfo
	{
		public Vector3 start_pos; //开始位置
		public Vector3 end_pos; //结束位置
		public float height;
		public float max_height;
		public string animation_name = AnimationNameConst.be_throwed;
		public float org_height;
		public float duration;
		public float remain_duration;
		public float interp = 1;
		public float height_speed;
		public float height_accelerate;
		public float? rotate_duration;
		public float? rotate_remain_duration;
		public Quaternion start_rotation;
		public Quaternion? end_rotation;
		public Func<UnitBeThrowedInfo, float> calc_height_func;
		public bool is_not_stop_animation;
		public bool is_back_to_ground = true;

		public bool IsHasAnimationName()
		{
			return !this.animation_name.IsNullOrWhiteSpace();
		}
	}
}