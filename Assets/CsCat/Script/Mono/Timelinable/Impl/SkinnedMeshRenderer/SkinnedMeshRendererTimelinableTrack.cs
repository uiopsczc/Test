using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class SkinnedMeshRendererTimelinableTrack : TimelinableTrackBase
	{
		[SerializeField]
		private SkinnedMeshRendererTimelinableItemInfo[] _itemInfoes = new SkinnedMeshRendererTimelinableItemInfo[0];

		[SerializeField] private SkinnedMeshRendererTimelinableItemInfoLibrary _itemInfoLibrary;
		[NonSerialized] private List<SkinnedMeshRenderer> _skinnedMeshRendererList = new List<SkinnedMeshRenderer>();

		public override TimelinableItemInfoBase[] itemInfoes
		{
			get => _itemInfoes;
			set => _itemInfoes = value as SkinnedMeshRendererTimelinableItemInfo[];
		}

		public override TimelinableItemInfoLibraryBase itemInfoLibrary
		{
			get => _itemInfoLibrary;
			set => _itemInfoLibrary = value as SkinnedMeshRendererTimelinableItemInfoLibrary;
		}


		protected override void _HandleTime(int startIndex, params object[] args)
		{
			StopAllPlayingItemInfo();
			for (int i = startIndex; i < _itemInfoes.Length; i++)
			{
				if (i < 0)
					continue;

				var itemInfo = _itemInfoes[i];
				if (curTime >= itemInfo.time)
				{
					if (curTime < itemInfo.time + itemInfo.duration)
						_AddToToPlayItemInfoIndexList(i);
					curTimeItemInfoIndex = i;
				}
				else
					break;
			}

			//加上最后一个cur_time_itemInfo_index
			if (curTimeItemInfoIndex >= 0)
				_AddToToPlayItemInfoIndexList(curTimeItemInfoIndex);
			_HandleToPlayAndStopItemInfoIndexList(args);
		}

		protected override void _AddToToPlayItemInfoIndexList(int itemInfoIndex)
		{
			if (!this.toPlayItemInfoIndexList.Contains(itemInfoIndex))
				this.toPlayItemInfoIndexList.Add(itemInfoIndex);
		}

		public override void CopyTo(object dest)
		{
			var destSkinnedMeshRendererTimelinableTrack = dest as SkinnedMeshRendererTimelinableTrack;
#if UNITY_EDITOR
			skinnedMeshRendererReorderableListInfo.CopyTo(destSkinnedMeshRendererTimelinableTrack.skinnedMeshRendererReorderableListInfo);
#endif
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var sourceSkinnedMeshRendererTimelinableTrack = source as SkinnedMeshRendererTimelinableTrack;
#if UNITY_EDITOR
			skinnedMeshRendererReorderableListInfo.CopyFrom(sourceSkinnedMeshRendererTimelinableTrack.skinnedMeshRendererReorderableListInfo);
#endif
			base.CopyFrom(source);
		}

		//////////////////////////////////////////////////////////////////////////////////////
		public void SetSkinnedMeshRendererListOfItemInfoes()
		{
			for (var i = 0; i < _itemInfoes.Length; i++)
			{
				var itemInfo = _itemInfoes[i];
				itemInfo.skinnedMeshRendererList = _skinnedMeshRendererList;
			}
		}
	}
}