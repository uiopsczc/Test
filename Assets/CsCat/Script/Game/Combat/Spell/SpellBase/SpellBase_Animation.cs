using System.Collections;
using UnityEngine;

namespace CsCat
{
	public partial class SpellBase
	{
		private float _animationTimePct;
		private float _animationStartTime;
		public bool isPastBreakTime;

		protected void PlaySpellAnimation(Vector3? faceToPosition = null)
		{
			if (this.cfgSpellData.animationDuration > 0)
			{
				this._animationTimePct = 0;
				this._animationStartTime = CombatUtil.GetTime();
			}

			if (!this.cfgSpellData.animationName.IsNullOrWhiteSpace())
			{
				if (faceToPosition == null && this.targetUnit != null)
					faceToPosition = this.targetUnit.GetPosition();
				// 不转向
				if (this.cfgSpellData.isNotFaceToTarget)
					faceToPosition = null;
				float speed = this.cfgSpellData.type == "普攻" ? this.sourceUnit.GetCalcPropValue("攻击速度") : 1;
				this.sourceUnit.PlayAnimation(this.cfgSpellData.animationName, null, speed, faceToPosition,
				  this.cfgSpellData.isCanMoveWhileCast);
			}
		}

		protected void StopSpellAnimation()
		{
			if (!this.cfgSpellData.animationName.IsNullOrWhiteSpace())
				this.sourceUnit.StopAnimation(this.cfgSpellData.animationName);
		}

		//注意：只能在start时调用，不能在事件中调用
		protected void RegisterAnimationEvent(float? timePct, string invokeMethodName, Hashtable argDict = null)
		{
			if (this.cfgSpellData.animationDuration == 0 || timePct == null || timePct.Value <= 0)
			{
				this.InvokeMethod(invokeMethodName, false, argDict);
				return;
			}

			var newEvent = new Hashtable();
			newEvent["timePct"] = timePct.Value;
			newEvent["eventName"] = invokeMethodName;
			newEvent["argDict"] = argDict;

			for (int i = 0; i < this.animationEventList.Count; i++)
			{
				var animationEvent = this.animationEventList[i];
				if (animationEvent.Get<float>("timePct") > timePct)
				{
					this.animationEventList.Insert(i, newEvent);
					return;
				}
			}

			this.animationEventList.Add(newEvent);
		}

		private void ProcessAnimationEvent(float deltaTime)
		{
			if (this._animationTimePct == 0)
				return;
			this._animationTimePct = this._animationTimePct + deltaTime /
										(this.cfgSpellData.animationDuration /
										 (1 + this.sourceUnit.GetCalcPropValue("攻击速度")));
			while (true)
			{
				//没有animation_event了
				if (animationEventList.IsNullOrEmpty())
					return;
				var animationEvent = this.animationEventList[0];
				// 还没触发
				if (animationEvent.Get<float>("timePct") > this._animationTimePct)
					return;
				// 时间到，可以进行触发
				this.animationEventList.RemoveFirst();
				this.InvokeMethod(animationEvent.Get<string>("invokeMethodName"), false,
				  animationEvent.Get<Hashtable>("argDict"));
			}
		}

		public void PassBreakTime()
		{
			this.isPastBreakTime = true;
			this.sourceUnit.UpdateMixedStates();
		}
	}
}
