using System;
using UnityEngine;

namespace CsCat
{
	// 左右 键盘的左右键,[-1，1]
	// 下上 键盘的下上键,[-1，1]
	public class UIRockerInput : TickObject
	{
		private Vector2 pre_axis;
		private Vector2 cur_axis;
		private Vector2 key_axis;
		private float move_cooldown_remain_duration;
		private bool has_key_pressed;
		private bool is_axis_move;
		private string name;

		public string event_name_move_stop => this.name + "MoveStop";
		public string event_name_move_pct => this.name + "MovePct";

		protected override void _Update(float deltaTime, float unscaledDeltaTime)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.__UpdateKeyInput(deltaTime, unscaledDeltaTime); // 键盘测试用的
			if (this.move_cooldown_remain_duration > 0)
				this.__UpdateMove(deltaTime, unscaledDeltaTime);
		}

		public virtual void __GetAxisKeyInput(out float axis_x, out float axis_y)
		{
			axis_x = Input.GetAxis("Horizontal"); //键盘的左右,[-1，1]
			axis_y = Input.GetAxis("Vertical"); // 键盘的下上,[-1，1]
		}

		public void __UpdateKeyInput(float deltaTime, float unscaledDeltaTime)
		{
			__GetAxisKeyInput(out float axis_x, out float axis_y);
			var len = Math.Sqrt(axis_x * axis_x + axis_y * axis_y);
			if (len == 0)
			{
				if (!this.has_key_pressed)
					return;
				this.has_key_pressed = false;
				if (this.is_axis_move)
				{
					this.key_axis = new Vector2(axis_x, axis_y);
					this.AxisMove(0, 0);
				}
			}
			else
			{
				this.has_key_pressed = true;
				this.key_axis.x = axis_x;
				this.key_axis.y = axis_y;
				this.AxisMove(this.key_axis.x, this.key_axis.y);
			}
		}

		public void AxisMove(float x, float y)
		{
			this.cur_axis = new Vector2(x, y);
			if (x != 0 || y != 0)
			{
				this.MovePct(x, y);
				this.is_axis_move = true;
			}
			else if (this.is_axis_move)
			{
				this.MoveStop();
				this.is_axis_move = false;
			}
		}

		public void MovePct(float pct_x, float pct_y)
		{
			this.Broadcast(null, this.event_name_move_pct, pct_x, pct_y);
		}

		public void MoveStop()
		{
			this.move_cooldown_remain_duration = 0;
			this.Broadcast(null, this.event_name_move_stop);
		}

		public void __UpdateMove(float deltaTime, float unscaledDeltaTime)
		{
			this.move_cooldown_remain_duration = this.move_cooldown_remain_duration - deltaTime;
			if (this.move_cooldown_remain_duration <= 0)
				this.MovePct(this.cur_axis.x, this.cur_axis.y);
		}
	}
}