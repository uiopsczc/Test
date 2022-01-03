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
				foreach (var track in sequence.tracks)
				{
					for (int i = track.playing_itemInfo_list.Count - 1; i >= 0; i--)
						track.Stop(track.playing_itemInfo_list[i], transform,
						  !Application.isPlaying ? null : (Func<GameObject, Transform, GameObject>)TimelinableUtil.SpawnGameObject,
						  !Application.isPlaying ? null : (Action<GameObject, Transform>)TimelinableUtil.DespawnGameObject);
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
			cur_time = time;
			if (is_playing)
				sequence.Tick(time, this);
			else
				sequence.Retime(time, this);
		}
	}
}