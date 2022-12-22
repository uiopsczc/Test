using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		public Animation animation;
		private string curAnimationName;
		private ActionManager actionManager;
		public AnimatorComp animatorComp;
		private AnimationCullingType? animationCullingType;

		private void SetAnimationCullingType(AnimationCullingType animationCullingType)
		{
			if (this.animation == null)
			{
				this.animationCullingType = animationCullingType;
				return;
			}

			this.animation.cullingType = animationCullingType;
		}

		public void PlayAnimation(string animationName, float? blendTime = null, float? speed = null,
		  Vector3? faceToPosition = null, bool isNotMoveStop = false)
		{
			float blendTimeValue = blendTime.GetValueOrDefault(0.1f);
			float speedValue = speed.GetValueOrDefault(1);
			if (this.animation != null)
			{
				if (AnimationNameConst.Die.Equals(this.curAnimationName))
					return;
				if (this.actionManager != null)
				{
					if (AnimationNameConst.Walk.Equals(animationName) && !this.curAnimationName.IsNullOrWhiteSpace())
					{
						this.actionManager.Stop(this.curAnimationName);
						this.curAnimationName = null;
					}

					if (AnimationNameConst.Idle.Equals(animationName) && !this.curAnimationName.IsNullOrWhiteSpace())
						this.actionManager.Play(animationName, speedValue, -1, false);
					else if (AnimationNameConst.Walk.Equals(animationName))
						this.actionManager.Play(animationName, speedValue, 0, false);
					else
					{
						this.actionManager.Play(animationName, speedValue, 0, true);
						this.curAnimationName = animationName;
						if (AnimationNameConst.Die.Equals(animationName))
							this.actionManager.Stop(AnimationNameConst.Idle);
					}
				}
				else
				{
					if (AnimationNameConst.Walk.Equals(animationName) && !this.curAnimationName.IsNullOrWhiteSpace())
					{
						this.animation.Blend(this.curAnimationName, 0, blendTimeValue);
						this.curAnimationName = null;
					}

					var animationState = this.animation[animationName];
					if (animationState != null)
						LogCat.LogErrorFormat("animation is no exist: {0} , {1}", animationName, this.unitId);
					var speed_threshold = 0.5f;
					if (AnimationNameConst.Walk.Equals(animationName) && speedValue < speed_threshold)
					{
						animationState.speed = speed_threshold;
						this.animation.CrossFade(animationName, blendTimeValue);
						this.animation.Blend(animationName, speedValue / speed_threshold, blendTimeValue);
					}
					else
					{
						animationState.speed = speedValue;
						this.animation.CrossFade(animationName, blendTimeValue);
					}

					if (!(AnimationNameConst.Idle.Equals(animationName) || AnimationNameConst.Walk.Equals(animationName)))
					{
						if (this.curAnimationName.Equals(animationName))
							this.animation[animationName].time = 0;
						this.curAnimationName = animationName;
					}
				}
			}
			else
				this.animatorComp.PlayAnimation(animationName, true, speedValue);

			if (faceToPosition != null)
			{
				var rotation = Quaternion.LookRotation(faceToPosition.Value - this.GetPosition());
				if (!rotation.IsZero() && !isNotMoveStop)
					this.MoveStop(rotation);
			}
		}

		public void StopAnimation(string animationName = null, float? blendTime = null)
		{
			float blendTimeValue = blendTime.GetValueOrDefault(0.1f);
			if (this.animation != null)
			{
				if (this.actionManager != null)
					this.actionManager.Stop(animationName);
				else
				{
					animationName = animationName ?? this.curAnimationName;
					this.animation.Blend(animationName, 0, blendTimeValue);
				}
			}
		}
	}
}