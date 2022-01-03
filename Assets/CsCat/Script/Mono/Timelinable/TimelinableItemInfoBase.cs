using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class TimelinableItemInfoBase : IComparable, ICopyable
	{
		public string name;
		public float time;//修改这个值，通常需要ArrayUtil.Sort(track.ItemInfoes);track.Retime(play_time,xxx);
		public float duration = 1;

		[NonSerialized] public bool _is_playing;
		[NonSerialized] public bool _is_paused;

		public bool is_playing
		{
			get { return _is_playing; }
		}
		public bool is_paused
		{
			get { return _is_paused; }
		}




		public int CompareTo(object other)
		{
			TimelinableItemInfoBase other_timelinableItemInfo = other as TimelinableItemInfoBase;
			return time.CompareTo(other_timelinableItemInfo.time);
		}

		public bool IsTimeInside(float compare_time)
		{
			return compare_time >= time && compare_time < time + duration;
		}

		public virtual void Play(params object[] args)
		{
			_is_playing = true;
			LogCat.log(this.name + " set_to_play");
		}

		public virtual void Stop(params object[] args)
		{
			_is_playing = false;
			LogCat.log(this.name + " set_to_stop");
		}

		public virtual void SetIsPaused(bool is_paused)
		{
			this._is_paused = is_paused;
		}


		public virtual void CopyTo(object dest)
		{
			var _dest = dest as TimelinableItemInfoBase;
			_dest.name = name;
			_dest.time = time;
			_dest.duration = duration;
		}

		public virtual void CopyFrom(object source)
		{
			var _source = source as TimelinableItemInfoBase;
			name = _source.name;
			time = _source.time;
			duration = _source.duration;
		}
	}
}