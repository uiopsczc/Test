using System;
using System.Collections.Generic;

namespace CsCat
{
	[Serializable]
	public partial class TimelinableTrackBase : ICopyable
	{
		public string name;

		public virtual TimelinableItemInfoBase[] itemInfoes
		{
			get => null;
			set { }
		}

		[NonSerialized] public float curTime = 0;
		[NonSerialized] public int curTimeItemInfoIndex = -1;

		[NonSerialized] private bool _isPaused;
		public bool isPaused => _isPaused;


		public TimelinableItemInfoBase curTimeItemInfo => curTimeItemInfoIndex < 0
			? null
			: itemInfoes[curTimeItemInfoIndex];


		[NonSerialized] public List<TimelinableItemInfoBase> playingItemInfoList = new List<TimelinableItemInfoBase>();
		[NonSerialized] public List<int> toPlayItemInfoIndexList = new List<int>();
		[NonSerialized] protected List<int> toStopItemInfoIndexList = new List<int>();

		public virtual TimelinableItemInfoLibraryBase itemInfoLibrary
		{
			get => null;
			set { }
		}

		public virtual void CopyTo(object dest)
		{
			var destTimelinableTrack = dest as TimelinableTrackBase;
			destTimelinableTrack.name = name;
			destTimelinableTrack.itemInfoLibrary = itemInfoLibrary;
#if UNITY_EDITOR
			destTimelinableTrack.isItemInfoLibrarySorted = isItemInfoLibrarySorted;
			destTimelinableTrack.itemInfoLibraryToggleTween = itemInfoLibraryToggleTween;
			destTimelinableTrack.toggleTween = toggleTween;
#endif

			for (int i = 0; i < itemInfoes.Length; i++)
			{
				var destItemInfo = itemInfoes[i].GetType().CreateInstance<TimelinableItemInfoBase>();
				itemInfoes[i].CopyTo(destItemInfo);
				destTimelinableTrack.AddItemInfo(destItemInfo);
			}
		}

		public virtual void CopyFrom(object source)
		{
			var sourceTimelinableTrack = source as TimelinableTrackBase;
			name = sourceTimelinableTrack.name;
			itemInfoLibrary = sourceTimelinableTrack.itemInfoLibrary;
#if UNITY_EDITOR
			isItemInfoLibrarySorted = sourceTimelinableTrack.isItemInfoLibrarySorted;
#endif
			for (int i = 0; i < sourceTimelinableTrack.itemInfoes.Length; i++)
			{
				var itemInfo = sourceTimelinableTrack.itemInfoes[i].GetType().CreateInstance<TimelinableItemInfoBase>();
				itemInfo.CopyFrom(sourceTimelinableTrack.itemInfoes[i]);
				AddItemInfo(itemInfo);
			}
		}


		public void AddItemInfo(TimelinableItemInfoBase timelinableItemInfo)
		{
			itemInfoes = ArrayUtil.AddLast(itemInfoes, timelinableItemInfo) as TimelinableItemInfoBase[];
			Array.Sort(itemInfoes);
		}

		public void RemoveItemInfo(TimelinableItemInfoBase timelinableItemInfo)
		{
			itemInfoes = ArrayUtil.Remove(itemInfoes, timelinableItemInfo) as TimelinableItemInfoBase[];
		}

		public void RemoveItemInfoAt(int index)
		{
			itemInfoes = ArrayUtil.RemoveAt(itemInfoes, index) as TimelinableItemInfoBase[];
		}

		/// <param name="time"></param>
		/// <returns>返回time在timelinableInfo_list的index，和在timelinableInfo_list[index]的相对时间</returns>
		public (int, float) GetInfoIndexAndElaspedDuration(float time)
		{
			int index = -1;
			float elaspedDuration = 0;
			int count = itemInfoes.Length;
			for (int i = 0; i < count; i++)
			{
				var itemInfo = itemInfoes[i];
				if (time >= itemInfo.time)
					index = i;
				else
					break;
			}

			if (index >= 0 && index < count)
			{
				var itemInfo = itemInfoes[index];
				elaspedDuration = time - itemInfo.time;
				if (elaspedDuration >= itemInfo.duration)
					index = -1;
			}

			return (index, elaspedDuration);
		}

		public void SetIsPaused(bool isPaused)
		{
			bool preState = this.isPaused;
			this._isPaused = isPaused;
			if (preState != isPaused)
				OnPauseStateChange();
		}

		protected virtual void OnPauseStateChange()
		{
			for (var i = 0; i < playingItemInfoList.Count; i++)
			{
				var playingItemInfo = playingItemInfoList[i];
				playingItemInfo.SetIsPaused(this.isPaused);
			}
		}

		//相对于Tick，效率低一些
		public virtual void Retime(float time, params object[] args)
		{
			curTime = time;
			int startIndex = 0;
			//从0开始检查
			HandleTime(startIndex, args);
		}

		public virtual void Tick(float time, params object[] args)
		{
			curTime = time;
			//从itemInfo_index开始检查
			int startIndex = curTimeItemInfoIndex;
			HandleTime(startIndex, args);
		}

		protected virtual void HandleTime(int startIndex, params object[] args)
		{
			for (int i = startIndex; i < itemInfoes.Length; i++)
			{
				if (i < 0)
					continue;

				var itemInfo = itemInfoes[i];
				if (curTime >= itemInfo.time)
				{
					if (curTime < itemInfo.time + itemInfo.duration)
						AddToToPlayItemInfoIndexList(i);
					else
						AddToToStopItemInfoIndexList(i);
					curTimeItemInfoIndex = i;
				}
				else
				{
					AddToToStopItemInfoIndexList(i);
					break;
				}
			}

			HandleToPlayAndStopItemInfoIndexList(args);
		}

		protected virtual void HandleToPlayAndStopItemInfoIndexList(params object[] args)
		{
			for (int i = 0; i < toStopItemInfoIndexList.Count; i++)
				Stop(itemInfoes[toStopItemInfoIndexList[i]], args);
			StopNotPlayingItemInfo(curTime, args);
			for (int i = 0; i < toPlayItemInfoIndexList.Count; i++)
				Play(itemInfoes[toPlayItemInfoIndexList[i]], args);
			toStopItemInfoIndexList.Clear();
			toPlayItemInfoIndexList.Clear();
		}

		protected virtual void AddToToStopItemInfoIndexList(int itemInfoIndex)
		{
			if (itemInfoes[itemInfoIndex].isPlaying && !toStopItemInfoIndexList.Contains(itemInfoIndex))
				toStopItemInfoIndexList.Add(itemInfoIndex);
		}

		protected virtual void AddToToPlayItemInfoIndexList(int itemInfoIndex)
		{
			if (!itemInfoes[itemInfoIndex].isPlaying && !toPlayItemInfoIndexList.Contains(itemInfoIndex))
				toPlayItemInfoIndexList.Add(itemInfoIndex);
		}

		protected virtual void Play(TimelinableItemInfoBase itemInfo, params object[] args)
		{
			itemInfo.Play(args.ToList().AddLast(this).ToArray());
			if (!playingItemInfoList.Contains(itemInfo))
				playingItemInfoList.Add(itemInfo);
		}

		public virtual void Stop(TimelinableItemInfoBase itemInfo, params object[] args)
		{
			itemInfo.Stop(args.ToList().AddLast(this).ToArray());
			playingItemInfoList.Remove(itemInfo);
		}


		void StopNotPlayingItemInfo(float time, params object[] args)
		{
			for (int i = playingItemInfoList.Count - 1; i >= 0; i--)
			{
				var playingItemInfo = playingItemInfoList[i];
				if (itemInfoes.IndexOf(playingItemInfo) == -1 || !playingItemInfo.IsTimeInside(time))
					Stop(playingItemInfo, args);
			}
		}

		public void StopAllPlayingItemInfo(params object[] args)
		{
			for (int i = playingItemInfoList.Count - 1; i >= 0; i--)
			{
				var playingItemInfo = playingItemInfoList[i];
				Stop(playingItemInfo, args);
			}
		}
	}
}