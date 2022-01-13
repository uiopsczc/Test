using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class TimelinableItemInfoBase : IComparable, ICopyable
	{
		public string name;
		public float time; //修改这个值，通常需要ArrayUtil.Sort(track.ItemInfoes);track.Retime(play_time,xxx);
		public float duration = 1;

		[NonSerialized] public bool _isPlaying;
		[NonSerialized] public bool _isPaused;

		public bool isPlaying => _isPlaying;

		public bool isPaused => _isPaused;


		public int CompareTo(object other)
		{
			TimelinableItemInfoBase otherTimelinableItemInfo = other as TimelinableItemInfoBase;
			return time.CompareTo(otherTimelinableItemInfo.time);
		}

		public bool IsTimeInside(float compareTime)
		{
			return compareTime >= time && compareTime < time + duration;
		}

		public virtual void Play(params object[] args)
		{
			_isPlaying = true;
			LogCat.log(this.name + " set_to_play");
		}

		public virtual void Stop(params object[] args)
		{
			_isPlaying = false;
			LogCat.log(this.name + " set_to_stop");
		}

		public virtual void SetIsPaused(bool isPaused)
		{
			this._isPaused = isPaused;
		}


		public virtual void CopyTo(object dest)
		{
			var destTimelinableItemInfo = dest as TimelinableItemInfoBase;
			destTimelinableItemInfo.name = name;
			destTimelinableItemInfo.time = time;
			destTimelinableItemInfo.duration = duration;
		}

		public virtual void CopyFrom(object source)
		{
			var sourceTimelinableItemInfo = source as TimelinableItemInfoBase;
			name = sourceTimelinableItemInfo.name;
			time = sourceTimelinableItemInfo.time;
			duration = sourceTimelinableItemInfo.duration;
		}
	}
}