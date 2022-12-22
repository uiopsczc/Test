using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AnimationTimelinableSequencePlayer : TimelinableSequencePlayerBase
	{
		public Animator animator;
		private readonly Vector3 _localPosition;
		private readonly Vector3 _localEulerAngles;
		private readonly Vector3 _localScale;

		private float _speedWhenPaused; //停止时的播放速度
		public float speed = 1;

		public AnimationTimelinableSequencePlayer(Transform transform, float speed = 1) : base(transform)
		{
			this.speed = speed;
			animator = transform.GetComponent<Animator>();
			this._localPosition = transform.localPosition;
			this._localEulerAngles = transform.localEulerAngles;
			this._localScale = transform.localScale;
		}

		public override void Play()
		{
			base.Play();
			if (animator != null)
			{
				//        if (sequence != null)
				//          animator.runtimeAnimatorController = (sequence as AnimationTimelinableSequence).runtimeAnimatorController;

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
			transform.localPosition = _localPosition;
			transform.localEulerAngles = _localEulerAngles;
			transform.localScale = _localScale;
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
				_speedWhenPaused = animator.speed;
				animator.speed = 0;
			}
		}

		public override void UnPause()
		{
			base.UnPause();
			if (animator != null)
				animator.speed = _speedWhenPaused;
		}

		public override void UpdateTime(float time)
		{
			_curTime = time;
			if (_isPlaying)
				sequence.Tick(time, this);
			else
				sequence.Retime(time, this);
		}
	}
}