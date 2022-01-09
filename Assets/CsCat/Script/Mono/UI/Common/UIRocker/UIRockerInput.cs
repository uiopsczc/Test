using System;
using UnityEngine;

namespace CsCat
{
	// 左右 键盘的左右键,[-1，1]
	// 下上 键盘的下上键,[-1，1]
	public class UIRockerInput : TickObject
	{
		private Vector2 preAxis;
		private Vector2 curAxis;
		private Vector2 keyAxis;
		private float moveCooldownRemainDuration;
		private bool hasKeyPressed;
		private bool isAxisMove;
		private string name;

		public string eventNameMoveStop => this.name + "MoveStop";
		public string eventNameMovePCT => this.name + "MovePct";

		protected override void _Update(float deltaTime, float unscaledDeltaTime)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.__UpdateKeyInput(deltaTime, unscaledDeltaTime); // 键盘测试用的
			if (this.moveCooldownRemainDuration > 0)
				this.__UpdateMove(deltaTime, unscaledDeltaTime);
		}

		public virtual void __GetAxisKeyInput(out float axisX, out float axisY)
		{
			axisX = Input.GetAxis("Horizontal"); //键盘的左右,[-1，1]
			axisY = Input.GetAxis("Vertical"); // 键盘的下上,[-1，1]
		}

		public void __UpdateKeyInput(float deltaTime, float unscaledDeltaTime)
		{
			__GetAxisKeyInput(out float axisX, out float axisY);
			var len = Math.Sqrt(axisX * axisX + axisY * axisY);
			if (len == 0)
			{
				if (!this.hasKeyPressed)
					return;
				this.hasKeyPressed = false;
				if (this.isAxisMove)
				{
					this.keyAxis = new Vector2(axisX, axisY);
					this.AxisMove(0, 0);
				}
			}
			else
			{
				this.hasKeyPressed = true;
				this.keyAxis.x = axisX;
				this.keyAxis.y = axisY;
				this.AxisMove(this.keyAxis.x, this.keyAxis.y);
			}
		}

		public void AxisMove(float x, float y)
		{
			this.curAxis = new Vector2(x, y);
			if (x != 0 || y != 0)
			{
				this.MovePct(x, y);
				this.isAxisMove = true;
			}
			else if (this.isAxisMove)
			{
				this.MoveStop();
				this.isAxisMove = false;
			}
		}

		public void MovePct(float pctX, float pctY)
		{
			this.Broadcast(null, this.eventNameMovePCT, pctX, pctY);
		}

		public void MoveStop()
		{
			this.moveCooldownRemainDuration = 0;
			this.Broadcast(null, this.eventNameMoveStop);
		}

		public void __UpdateMove(float deltaTime, float unscaledDeltaTime)
		{
			this.moveCooldownRemainDuration = this.moveCooldownRemainDuration - deltaTime;
			if (this.moveCooldownRemainDuration <= 0)
				this.MovePct(this.curAxis.x, this.curAxis.y);
		}
	}
}