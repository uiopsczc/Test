
using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class CameraTimelinableTrack : TimelinableTrackBase
	{
		[NonSerialized] public Animator animator;
		[NonSerialized] public float animatorSpeedWhenPaused;

		[SerializeField] private CameraTimelinableItemInfo[] _itemInfoes = new CameraTimelinableItemInfo[0];

		public override TimelinableItemInfoBase[] itemInfoes
		{
			get => _itemInfoes;
			set => _itemInfoes = value as CameraTimelinableItemInfo[];
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
			base.Retime(time, args);
#if UNITY_EDITOR
			SyncAnimationWindow();
#endif
		}
	}
}



