using System;
using UnityEngine;

namespace CsCat
{
	public partial class UnitMoveComp
	{
		public UnitBeThrowedInfo unitBeThrowedInfo;

		public void BeThrowed(UnitBeThrowedInfo unitBeThrowedInfo)
		{
			Vector3 endPos = unitBeThrowedInfo.endPos;
			float duration = unitBeThrowedInfo.duration;
			float height = unitBeThrowedInfo.height;
			var endRotation = unitBeThrowedInfo.endRotation;
			var rotateDuration = unitBeThrowedInfo.rotateDuration;
			if ("be_throwed".Equals(this.moveType))
				return;
			var unit = this.unit;
			this.moveType = "be_throwed";
			if (unitBeThrowedInfo.IsHasAnimationName() && unit.animation != null)
				this.unit.PlayAnimation(unitBeThrowedInfo.animationName);
			this.unitBeThrowedInfo = unitBeThrowedInfo;
			this.unit.UpdateMixedStates();
			this.unitBeThrowedInfo.orgHeight = unit.GetPosition().y;
			this.unitBeThrowedInfo.startPos = unit.GetPosition();
			this.unitBeThrowedInfo.remainDuration = duration;
			float deltaHeight = endPos.y - unit.GetPosition().y;
			// 起点和落点，取最高的，加上height，为真正的最高高度
			var maxHeight = deltaHeight > 0 ? Math.Max((deltaHeight + height), 0) : height;
			this.unitBeThrowedInfo.maxHeight = maxHeight;
			if (maxHeight == 0)
			{
				this.unitBeThrowedInfo.heightAccelerate = deltaHeight * 2 / (duration * duration);
				this.unitBeThrowedInfo.height_speed = 0;
			}
			else
			{
				float hTime = duration / ((float)Math.Sqrt(1 - deltaHeight / maxHeight) + 1);
				this.unitBeThrowedInfo.heightAccelerate = -2 * maxHeight / (hTime * hTime);
				this.unitBeThrowedInfo.height_speed = -this.unitBeThrowedInfo.heightAccelerate * hTime;
			}

			if (endRotation != null && rotateDuration != null)
			{
				this.unitBeThrowedInfo.rotateRemainDuration = rotateDuration.Value;
				this.unitBeThrowedInfo.startRotation = unit.GetRotation();
			}
		}


		public void StopBeThrowed(bool isEnd = false)
		{
			if (isEnd)
			{
				if (this.unitBeThrowedInfo != null)
				{
					this.unitBeThrowedInfo.remainDuration = 0.02f;
					this.__UpdateBeThrowed(0.02f);
				}

				return;
			}

			if (this.unitBeThrowedInfo != null && !this.unitBeThrowedInfo.isNotStopAnimation &&
				this.unitBeThrowedInfo.IsHasAnimationName())
				this.unit.StopAnimation(this.unitBeThrowedInfo.animationName, 0.2f);

			var isBackToGround = unitBeThrowedInfo?.isBackToGround ?? false;
			this.unitBeThrowedInfo = null;
			this.moveType = null;
			this.unit.UpdateMixedStates();

			if (isBackToGround)
			{
				var unitBeThrowedInfo = new UnitBeThrowedInfo();
				unitBeThrowedInfo.endPos = Client.instance.combat.pathManager.GetGroundPos(this.unit.GetPosition());
				unitBeThrowedInfo.duration = 0.1f;
				unitBeThrowedInfo.height = 0f;
				unitBeThrowedInfo.isBackToGround = false;
				this.BeThrowed(unitBeThrowedInfo);
			}
		}

		public void __UpdateBeThrowed(float deltaTime)
		{
			var unit = this.unit;
			this.unitBeThrowedInfo.remainDuration = this.unitBeThrowedInfo.remainDuration - deltaTime;
			if (this.unitBeThrowedInfo.remainDuration <= 0)
			{
				this.StopBeThrowed();
				return;
			}

			float passedDuration = this.unitBeThrowedInfo.duration - this.unitBeThrowedInfo.remainDuration; // 已经运行的时间
																											  //计算高度
			float curHeight;
			if (this.unitBeThrowedInfo.calcHeightFunc != null)
				curHeight = this.unitBeThrowedInfo.orgHeight +
							 this.unitBeThrowedInfo.calcHeightFunc(this.unitBeThrowedInfo);
			else
				curHeight = this.unitBeThrowedInfo.orgHeight + this.unitBeThrowedInfo.height_speed * passedDuration +
							 this.unitBeThrowedInfo.heightAccelerate * passedDuration * passedDuration * 0.5f;
			//计算水平位置
			float interp = (float)Math.Pow((1 - passedDuration / this.unitBeThrowedInfo.duration),
			  this.unitBeThrowedInfo.interp);
			Vector3 newPos = this.unitBeThrowedInfo.startPos * interp + this.unitBeThrowedInfo.endPos * (1 - interp);

			newPos.y = curHeight;
			unit.SetPosition(newPos);

			if (this.unitBeThrowedInfo.rotateDuration != null && this.unitBeThrowedInfo.rotateRemainDuration != null)
			{
				this.unitBeThrowedInfo.rotateRemainDuration = this.unitBeThrowedInfo.rotateRemainDuration - deltaTime;
				if (this.unitBeThrowedInfo.rotateRemainDuration <= 0)
				{
					this.unitBeThrowedInfo.rotateDuration = null;
					this.unitBeThrowedInfo.rotateRemainDuration = null;
					unit.SetRotation(this.unitBeThrowedInfo.endRotation.Value);
				}
				else
					unit.SetRotation(Quaternion.Slerp(this.unitBeThrowedInfo.startRotation,
					  this.unitBeThrowedInfo.endRotation.Value,
					  this.unitBeThrowedInfo.rotateRemainDuration.Value / this.unitBeThrowedInfo.rotateDuration.Value));
			}
		}
	}
}