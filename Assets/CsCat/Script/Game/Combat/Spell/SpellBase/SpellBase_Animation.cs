using System.Collections;
using UnityEngine;

namespace CsCat
{
	public partial class SpellBase
	{
		private float __animationTimePct;
		private float __animationStartTime;
		public bool isPastBreakTime;

		protected void PlaySpellAnimation(Vector3? faceToPosition = null)
		{
			if (this.cfgSpellData.animation_duration > 0)
			{
				this.__animationTimePct = 0;
				this.__animationStartTime = CombatUtil.GetTime();
			}

			if (!this.cfgSpellData.animation_name.IsNullOrWhiteSpace())
			{
				if (faceToPosition == null && this.targetUnit != null)
					faceToPosition = this.targetUnit.GetPosition();
				// 不转向
				if (this.cfgSpellData.is_not_face_to_target)
					faceToPosition = null;
				float speed = this.cfgSpellData.type == "普攻" ? this.sourceUnit.GetCalcPropValue("攻击速度") : 1;
				this.sourceUnit.PlayAnimation(this.cfgSpellData.animation_name, null, speed, faceToPosition,
				  this.cfgSpellData.is_can_move_while_cast);
			}
		}

		protected void StopSpellAnimation()
		{
			if (!this.cfgSpellData.animation_name.IsNullOrWhiteSpace())
				this.sourceUnit.StopAnimation(this.cfgSpellData.animation_name);
		}

		//注意：只能在start时调用，不能在事件中调用
		protected void RegisterAnimationEvent(float? timePct, string invokeMethodName, Hashtable argDict = null)
		{
			if (this.cfgSpellData.animation_duration == 0 || timePct == null || timePct.Value <= 0)
			{
				this.InvokeMethod(invokeMethodName, false, argDict);
				return;
			}

			var newEvent = new Hashtable();
			newEvent["time_pct"] = timePct.Value;
			newEvent["event_name"] = invokeMethodName;
			newEvent["arg_dict"] = argDict;

			for (int i = 0; i < this.animationEventList.Count; i++)
			{
				var animationEvent = this.animationEventList[i];
				if (animationEvent.Get<float>("time_pct") > timePct)
				{
					this.animationEventList.Insert(i, newEvent);
					return;
				}
			}

			this.animationEventList.Add(newEvent);
		}

		private void ProcessAnimationEvent(float deltaTime)
		{
			if (this.__animationTimePct == 0)
				return;
			this.__animationTimePct = this.__animationTimePct + deltaTime /
										(this.cfgSpellData.animation_duration /
										 (1 + this.sourceUnit.GetCalcPropValue("攻击速度")));
			while (true)
			{
				//没有animation_event了
				if (animationEventList.IsNullOrEmpty())
					return;
				var animationEvent = this.animationEventList[0];
				// 还没触发
				if (animationEvent.Get<float>("time_pct") > this.__animationTimePct)
					return;
				// 时间到，可以进行触发
				this.animationEventList.RemoveFirst();
				this.InvokeMethod(animationEvent.Get<string>("invoke_method_name"), false,
				  animationEvent.Get<Hashtable>("arg_dict"));
			}
		}

		public void PassBreakTime()
		{
			this.isPastBreakTime = true;
			this.sourceUnit.UpdateMixedStates();
		}
	}
}
