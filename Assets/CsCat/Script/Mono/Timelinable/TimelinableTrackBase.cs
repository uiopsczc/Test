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
			get { return null; }
			set { }
		}

		[NonSerialized] public float curTime = 0;
		[NonSerialized] public int cur_time_itemInfo_index = -1;

		[NonSerialized] private bool _is_paused;
		public bool is_paused { get => _is_paused; }


		public TimelinableItemInfoBase cur_time_itemInfo
		{
			get
			{
				return cur_time_itemInfo_index < 0
				  ? null
				  : itemInfoes[cur_time_itemInfo_index];
			}
		}


		[NonSerialized] public List<TimelinableItemInfoBase> playing_itemInfo_list = new List<TimelinableItemInfoBase>();
		[NonSerialized] public List<int> to_play_itemInfo_index_list = new List<int>();
		[NonSerialized] protected List<int> to_stop_itemInfo_index_list = new List<int>();

		public virtual TimelinableItemInfoLibraryBase itemInfoLibrary
		{
			get { return null; }
			set { }
		}

		public virtual void CopyTo(object dest)
		{
			var _dest = dest as TimelinableTrackBase;
			_dest.name = name;
			_dest.itemInfoLibrary = itemInfoLibrary;
#if UNITY_EDITOR
			_dest.is_itemInfoLibrary_sorted = is_itemInfoLibrary_sorted;
			_dest.itemInfoLibrary_toggleTween = itemInfoLibrary_toggleTween;
			_dest.toggleTween = toggleTween;
#endif

			for (int i = 0; i < itemInfoes.Length; i++)
			{
				var _dest_itemInfo = itemInfoes[i].GetType().CreateInstance<TimelinableItemInfoBase>();
				itemInfoes[i].CopyTo(_dest_itemInfo);
				_dest.AddItemInfo(_dest_itemInfo);
			}


		}

		public virtual void CopyFrom(object source)
		{
			var _source = source as TimelinableTrackBase;
			name = _source.name;
			itemInfoLibrary = _source.itemInfoLibrary;
#if UNITY_EDITOR
			is_itemInfoLibrary_sorted = _source.is_itemInfoLibrary_sorted;
#endif
			for (int i = 0; i < _source.itemInfoes.Length; i++)
			{
				var _itemInfo = _source.itemInfoes[i].GetType().CreateInstance<TimelinableItemInfoBase>();
				_itemInfo.CopyFrom(_source.itemInfoes[i]);
				AddItemInfo(_itemInfo);
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
			float elasped_duration = 0;
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
				elasped_duration = time - itemInfo.time;
				if (elasped_duration >= itemInfo.duration)
					index = -1;
			}

			return (index, elasped_duration);
		}

		public void SetIsPaused(bool is_paused)
		{
			bool pre_state = this.is_paused;
			this._is_paused = is_paused;
			if (pre_state != is_paused)
				OnPauseStateChange();
		}

		protected virtual void OnPauseStateChange()
		{
			foreach (var playing_itemInfo in playing_itemInfo_list)
				playing_itemInfo.SetIsPaused(this.is_paused);
		}

		//相对于Tick，效率低一些
		public virtual void Retime(float time, params object[] args)
		{
			curTime = time;
			int start_index = 0;
			//从0开始检查
			Handle_Time(start_index, args);

		}

		public virtual void Tick(float time, params object[] args)
		{
			curTime = time;
			//从itemInfo_index开始检查
			int start_index = cur_time_itemInfo_index;
			Handle_Time(start_index, args);
		}

		protected virtual void Handle_Time(int start_index, params object[] args)
		{
			for (int i = start_index; i < itemInfoes.Length; i++)
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
					cur_time_itemInfo_index = i;
				}
				else
				{
					AddToToStopItemInfoIndexList(i);
					break;
				}
			}

			Handle_To_Play_And_Stop_ItemInfo_Index_list(args);
		}

		protected virtual void Handle_To_Play_And_Stop_ItemInfo_Index_list(params object[] args)
		{
			for (int i = 0; i < to_stop_itemInfo_index_list.Count; i++)
				Stop(itemInfoes[to_stop_itemInfo_index_list[i]], args);
			StopNotPlayingItemInfo(curTime, args);
			for (int i = 0; i < to_play_itemInfo_index_list.Count; i++)
				Play(itemInfoes[to_play_itemInfo_index_list[i]], args);
			to_stop_itemInfo_index_list.Clear();
			to_play_itemInfo_index_list.Clear();
		}

		protected virtual void AddToToStopItemInfoIndexList(int itemInfo_index)
		{
			if (itemInfoes[itemInfo_index].is_playing && !to_stop_itemInfo_index_list.Contains(itemInfo_index))
				to_stop_itemInfo_index_list.Add(itemInfo_index);
		}

		protected virtual void AddToToPlayItemInfoIndexList(int itemInfo_index)
		{
			if (!itemInfoes[itemInfo_index].is_playing && !to_play_itemInfo_index_list.Contains(itemInfo_index))
				to_play_itemInfo_index_list.Add(itemInfo_index);
		}

		protected virtual void Play(TimelinableItemInfoBase itemInfo, params object[] args)
		{
			itemInfo.Play(args.ToList().AddLast(this).ToArray());
			if (!playing_itemInfo_list.Contains(itemInfo))
				playing_itemInfo_list.Add(itemInfo);
		}

		public virtual void Stop(TimelinableItemInfoBase itemInfo, params object[] args)
		{
			itemInfo.Stop(args.ToList().AddLast(this).ToArray());
			playing_itemInfo_list.Remove(itemInfo);
		}


		void StopNotPlayingItemInfo(float time, params object[] args)
		{
			for (int i = playing_itemInfo_list.Count - 1; i >= 0; i--)
			{
				var playing_itemInfo = playing_itemInfo_list[i];
				if (itemInfoes.IndexOf(playing_itemInfo) == -1 || !playing_itemInfo.IsTimeInside(time))
					Stop(playing_itemInfo, args);
			}
		}

		public void StopAllPlayingItemInfo(params object[] args)
		{
			for (int i = playing_itemInfo_list.Count - 1; i >= 0; i--)
			{
				var playing_itemInfo = playing_itemInfo_list[i];
				Stop(playing_itemInfo, args);
			}
		}

	}
}