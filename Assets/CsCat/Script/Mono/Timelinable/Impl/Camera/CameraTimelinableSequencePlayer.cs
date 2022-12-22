using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class CameraTimelinableSequencePlayer : TimelinableSequencePlayerBase
	{
		private readonly Animator _animator;
		private readonly Vector3 _localPosition;
		private readonly Vector3 _localEulerAngles;
		private readonly Vector3 _localScale;

		private float speedWhenPaused;//停止时的播放速度
		private readonly float _speed = 1;

		public CameraTimelinableSequencePlayer(Transform transform, float speed = 1) : base(transform)
		{
			this._speed = speed;
			_animator = transform.GetComponent<Animator>();
			this._localPosition = transform.localPosition;
			this._localEulerAngles = transform.localEulerAngles;
			this._localScale = transform.localScale;
		}

		public override void Play()
		{
			base.Play();
			if (_animator != null)
			{
				if (sequence != null)
					_animator.runtimeAnimatorController = (sequence as CameraTimelinableSequence).runtimeAnimatorController;

				_animator.enabled = true;
				_animator.speed = 1;
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
			if (_animator != null)
				_animator.enabled = false;
		}

		public override void Dispose()
		{
			if (_animator != null)
				_animator.runtimeAnimatorController = null;
			base.Dispose();
		}

		public override void Pause()
		{
			base.Pause();
			if (_animator != null)
			{
				speedWhenPaused = _animator.speed;
				_animator.speed = 0;
			}
		}

		public override void UnPause()
		{
			base.UnPause();
			if (_animator != null)
				_animator.speed = speedWhenPaused;
		}

		public override void UpdateTime(float time)
		{
			_curTime = time;
			if (_isPlaying)
				sequence.Tick(time, _animator, _speed);
			else
				sequence.Retime(time, _animator, _speed);
		}

	}
}



