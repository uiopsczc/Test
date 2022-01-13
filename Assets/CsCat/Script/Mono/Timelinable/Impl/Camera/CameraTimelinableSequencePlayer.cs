using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class CameraTimelinableSequencePlayer : TimelinableSequencePlayerBase
	{
		private Animator animator;
		private Vector3 localPosition;
		private Vector3 localEulerAngles;
		private Vector3 localScale;

		private float speed_when_paused;//停止时的播放速度
		private float speed = 1;

		public CameraTimelinableSequencePlayer(Transform transform, float speed = 1) : base(transform)
		{
			this.speed = speed;
			animator = transform.GetComponent<Animator>();
			this.localPosition = transform.localPosition;
			this.localEulerAngles = transform.localEulerAngles;
			this.localScale = transform.localScale;
		}

		public override void Play()
		{
			base.Play();
			if (animator != null)
			{
				if (sequence != null)
					animator.runtimeAnimatorController = (sequence as CameraTimelinableSequence).runtimeAnimatorController;

				animator.enabled = true;
				animator.speed = 1;
			}

		}

		public override void Stop()
		{
			base.Stop();
		}

		public override void Reset()
		{
			base.Reset();
			transform.localPosition = localPosition;
			transform.localEulerAngles = localEulerAngles;
			transform.localScale = localScale;
			if (animator != null)
				animator.enabled = false;
		}

		public override void Dispose()
		{
			if (animator != null)
				animator.runtimeAnimatorController = null;
			base.Dispose();
		}

		public override void Pause()
		{
			base.Pause();
			if (animator != null)
			{
				speed_when_paused = animator.speed;
				animator.speed = 0;
			}
		}

		public override void UnPause()
		{
			base.UnPause();
			if (animator != null)
				animator.speed = speed_when_paused;
		}

		public override void UpdateTime(float time)
		{
			curTime = time;
			if (isPlaying)
				sequence.Tick(time, animator, speed);
			else
				sequence.Retime(time, animator, speed);
		}

	}
}



