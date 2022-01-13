using UnityEngine;

namespace CsCat
{
	public partial class TimelinableSequenceBase : ScriptableObject
	{
		public virtual TimelinableTrackBase[] tracks
		{
			get => null;
			set { }
		}

		//相对于Tick，效率低一些
		public void Retime(float time, params object[] args)
		{
			tracks.Foreach(track => track.Retime(time, args));
		}

		public void Tick(float time, params object[] args)
		{
			tracks.Foreach(track => track.Tick(time, args));
		}

		public void AddTrack(TimelinableTrackBase track)
		{
			tracks = ArrayUtil.AddLast(tracks, track) as TimelinableTrackBase[];
		}

		public void RemoveTrack(TimelinableTrackBase track)
		{
			var t = ArrayUtil.Remove(tracks, track);
			tracks = t as TimelinableTrackBase[];
		}

		public void RemoveTrackAt(int index)
		{
			var t = ArrayUtil.RemoveAt(tracks, index);
			LogCat.log(t);
			tracks = t as TimelinableTrackBase[];
		}

		public void StopAllPlayingItemInfo(params object[] args)
		{
			for (var i = 0; i < tracks.Length; i++)
			{
				var track = tracks[i];
				track.StopAllPlayingItemInfo(args);
			}
		}

		public virtual void OnSequenceDisable(params object[] args)
		{
			StopAllPlayingItemInfo(args);
		}
	}
}