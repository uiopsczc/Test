using UnityEngine;

namespace CsCat
{
	public class TimelinableSequencePlayerBase
	{
		public Transform transform;
		public TimelinableSequenceBase sequence;
		protected float curTime;

		protected bool isPlaying;
		protected bool isPaused;

		public TimelinableSequencePlayerBase(Transform transform)
		{
			this.transform = transform;
		}


		public virtual void Play()
		{
			Reset();
			if (sequence != null)
				sequence.tracks.Foreach(track => { track.curTimeItemInfoIndex = -1; });
			isPlaying = true;
		}

		public virtual void Stop()
		{
			Reset();
			sequence = null;
		}

		public virtual void Pause()
		{
			isPaused = true;
		}

		public virtual void UnPause()
		{
			isPaused = false;
		}

		public void SetTime(float time)
		{
			UpdateTime(time);
		}

		public virtual void UpdateTime(float time)
		{
			curTime = time;
			if (isPlaying)
				sequence.Tick(time);
			else
				sequence.Retime(time);
		}


		public virtual void Reset()
		{
			if (sequence != null)
				sequence.tracks.Foreach(track => { track.curTimeItemInfoIndex = -1; });
			isPlaying = false;
			isPaused = false;
		}

		public virtual void Dispose()
		{
			Stop();
		}
	}
}