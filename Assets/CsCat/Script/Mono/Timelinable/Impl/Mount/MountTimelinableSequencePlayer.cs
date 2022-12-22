using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class MountTimelinableSequencePlayer : TimelinableSequencePlayerBase
	{
		public MountTimelinableSequencePlayer(Transform transform) : base(transform)
		{
		}

		public override void Play()
		{
			base.Play();
		}

		public override void Stop()
		{
			base.Stop();
		}

		public override void Reset()
		{
			base.Reset();
			if (sequence != null)
			{
				for (var m = 0; m < sequence.tracks.Length; m++)
				{
					var track = sequence.tracks[m];
					for (int n = track.playingItemInfoList.Count - 1; n >= 0; n--)
						track.Stop(track.playingItemInfoList[n], transform,
							!Application.isPlaying
								? null
								: (Func<GameObject, Transform, GameObject>) TimelinableUtil.SpawnGameObject,
							!Application.isPlaying
								? null
								: (Action<GameObject, Transform>) TimelinableUtil.DespawnGameObject);
				}
			}
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override void Pause()
		{
			base.Pause();
		}

		public override void UnPause()
		{
			base.UnPause();
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