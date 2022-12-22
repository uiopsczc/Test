using UnityEngine;

namespace CsCat
{
	public class TimelinableSequencePlayerBase
	{
		public Transform transform;
		public TimelinableSequenceBase sequence;
		protected float _curTime;

		protected bool _isPlaying;
		protected bool _isPaused;

		public TimelinableSequencePlayerBase(Transform transform)
		{
			this.transform = transform;
		}


		public virtual void Play()
		{
			Reset();
			if (sequence != null)
				sequence.tracks.Foreach(track => { track.curTimeItemInfoIndex = -1; });
			_isPlaying = true;
		}

		public virtual void Stop()
		{
			Reset();
			sequence = null;
		}

		public virtual void Pause()
		{
			_isPaused = true;
		}

		public virtual void UnPause()
		{
			_isPaused = false;
		}

		public void SetTime(float time)
		{
			UpdateTime(time);
		}

		public virtual void UpdateTime(float time)
		{
			_curTime = time;
			if (_isPlaying)
				sequence.Tick(time);
			else
				sequence.Retime(time);
		}


		public virtual void Reset()
		{
			if (sequence != null)
				sequence.tracks.Foreach(track => { track.curTimeItemInfoIndex = -1; });
			_isPlaying = false;
			_isPaused = false;
		}

		public virtual void Dispose()
		{
			Stop();
		}
	}
}