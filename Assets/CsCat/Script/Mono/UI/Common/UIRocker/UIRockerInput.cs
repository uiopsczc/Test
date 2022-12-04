using System;
using UnityEngine;

namespace CsCat
{
	// 左右 键盘的左右键,[-1，1]
	// 下上 键盘的下上键,[-1，1]
	public class UIRockerInput : TreeNode
	{
		private Vector2 _preAxis;
		private Vector2 _curAxis;
		private Vector2 _keyAxis;
		private float _moveCooldownRemainDuration;
		private bool _isHasKeyPressed;
		private bool _isAxisMove;
		private string _name;

		public string eventNameMoveStop => this._name + "MoveStop";
		public string eventNameMovePCT => this._name + "MovePct";

		protected override bool _Update(float deltaTime, float unscaledDeltaTime)
		{
			if (!base._Update(deltaTime, unscaledDeltaTime))
				return false;
			this.UpdateKeyInput(deltaTime, unscaledDeltaTime); // 键盘测试用的
			if (this._moveCooldownRemainDuration > 0)
				this._UpdateMove(deltaTime, unscaledDeltaTime);
			return true;
		}

		public virtual void GetAxisKeyInput(out float axisX, out float axisY)
		{
			axisX = Input.GetAxis("Horizontal"); //键盘的左右,[-1，1]
			axisY = Input.GetAxis("Vertical"); // 键盘的下上,[-1，1]
		}

		public void UpdateKeyInput(float deltaTime, float unscaledDeltaTime)
		{
			GetAxisKeyInput(out float axisX, out float axisY);
			var len = Math.Sqrt(axisX * axisX + axisY * axisY);
			if (len == 0)
			{
				if (!this._isHasKeyPressed)
					return;
				this._isHasKeyPressed = false;
				if (this._isAxisMove)
				{
					this._keyAxis = new Vector2(axisX, axisY);
					this.AxisMove(0, 0);
				}
			}
			else
			{
				this._isHasKeyPressed = true;
				this._keyAxis.x = axisX;
				this._keyAxis.y = axisY;
				this.AxisMove(this._keyAxis.x, this._keyAxis.y);
			}
		}

		public void AxisMove(float x, float y)
		{
			this._curAxis = new Vector2(x, y);
			if (x != 0 || y != 0)
			{
				this.MovePct(x, y);
				this._isAxisMove = true;
			}
			else if (this._isAxisMove)
			{
				this.MoveStop();
				this._isAxisMove = false;
			}
		}

		public void MovePct(float pctX, float pctY)
		{
			Client.instance.uiManager.FireEvent(null, this.eventNameMovePCT, pctX, pctY);
		}

		public void MoveStop()
		{
			this._moveCooldownRemainDuration = 0;
			Client.instance.uiManager.FireEvent(null, this.eventNameMoveStop);
		}

		public void _UpdateMove(float deltaTime, float unscaledDeltaTime)
		{
			this._moveCooldownRemainDuration = this._moveCooldownRemainDuration - deltaTime;
			if (this._moveCooldownRemainDuration <= 0)
				this.MovePct(this._curAxis.x, this._curAxis.y);
		}
	}
}