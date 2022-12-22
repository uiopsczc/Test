using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class AnimationTimelinableTrack : TimelinableTrackBase
	{
		public RuntimeAnimatorController runtimeAnimatorController;
		[NonSerialized] public Animator animator;
		[NonSerialized] public float animatorSpeedWhenPaused;

		[SerializeField] private AnimationTimelinableItemInfo[] _itemInfoes = new AnimationTimelinableItemInfo[0];

		public override TimelinableItemInfoBase[] itemInfoes
		{
			get => _itemInfoes;
			set => _itemInfoes = value as AnimationTimelinableItemInfo[];
		}

		public override void CopyTo(object dest)
		{
			base.CopyTo(dest);
			var destAnimationTimelinableTrack = dest as AnimationTimelinableTrack;
			destAnimationTimelinableTrack.animator = animator;
			destAnimationTimelinableTrack.runtimeAnimatorController = runtimeAnimatorController;
		}

		public override void CopyFrom(object source)
		{
			base.CopyFrom(source);
			var sourceAnimationTimelinableTrack = source as AnimationTimelinableTrack;
			animator = sourceAnimationTimelinableTrack.animator;
			runtimeAnimatorController = sourceAnimationTimelinableTrack.runtimeAnimatorController;
		}

		protected override void _OnPauseStateChange()
		{
			base._OnPauseStateChange();
			if (animator != null)
			{
				if (isPaused)
					animatorSpeedWhenPaused = animator.speed;
				else
					animator.speed = animatorSpeedWhenPaused;
			}
		}


		public override void Retime(float time, params object[] args)
		{
			base.Retime(time, args.ToList().AddFirst(this.animator).ToArray());
#if UNITY_EDITOR
			SyncAnimationWindow();
#endif
		}

		public override void Tick(float time, params object[] args)
		{
			base.Tick(time, args.ToList().AddFirst(this.animator).ToArray());
		}

	}
}