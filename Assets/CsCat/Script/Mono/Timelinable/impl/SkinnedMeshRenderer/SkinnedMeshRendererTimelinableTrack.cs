using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class SkinnedMeshRendererTimelinableTrack : TimelinableTrackBase
  {
    [SerializeField] private SkinnedMeshRendererTimelinableItemInfo[] _itemInfoes = new SkinnedMeshRendererTimelinableItemInfo[0];
    [SerializeField] private SkinnedMeshRendererTimelinableItemInfoLibrary _itemInfoLibrary;
    [NonSerialized] private List<SkinnedMeshRenderer> skinnedMeshRenderer_list = new List<SkinnedMeshRenderer>();

    public override TimelinableItemInfoBase[] itemInfoes
    {
      get { return _itemInfoes; }
      set { _itemInfoes = value as SkinnedMeshRendererTimelinableItemInfo[]; }
    }

    public override TimelinableItemInfoLibraryBase itemInfoLibrary
    {
      get { return _itemInfoLibrary; }
      set { _itemInfoLibrary = value as SkinnedMeshRendererTimelinableItemInfoLibrary; }
    }


    protected override void Handle_Time(int start_index, params object[] args)
    {
      StopAllPlayingItemInfo();
      for (int i = start_index; i < _itemInfoes.Length; i++)
      {
        if (i < 0)
          continue;

        var itemInfo = _itemInfoes[i];
        if (cur_time >= itemInfo.time)
        {
          if (cur_time < itemInfo.time + itemInfo.duration)
            AddToToPlayItemInfoIndexList(i);
          cur_time_itemInfo_index = i;
        }
        else
          break;
      }
      //加上最后一个cur_time_itemInfo_index
      if (cur_time_itemInfo_index >= 0)
        AddToToPlayItemInfoIndexList(cur_time_itemInfo_index);
      Handle_To_Play_And_Stop_ItemInfo_Index_list(args);
    }

    protected override void AddToToPlayItemInfoIndexList(int itemInfo_index)
    {
      if (!this.to_play_itemInfo_index_list.Contains(itemInfo_index))
        this.to_play_itemInfo_index_list.Add(itemInfo_index);
    }

    public override void CopyTo(object dest)
    {
      var _dest = dest as SkinnedMeshRendererTimelinableTrack;
#if UNITY_EDITOR
      skinnedMeshRenderer_reorderableListInfo.CopyTo(_dest.skinnedMeshRenderer_reorderableListInfo);
#endif
      base.CopyTo(dest);
    }

    public override void CopyFrom(object source)
    {
      var _source = source as SkinnedMeshRendererTimelinableTrack;
#if UNITY_EDITOR
      skinnedMeshRenderer_reorderableListInfo.CopyFrom(_source.skinnedMeshRenderer_reorderableListInfo);
#endif
      base.CopyFrom(source);

    }

    //////////////////////////////////////////////////////////////////////////////////////
    public void SetSkinnedMeshRendererList_Of_ItemInfoes()
    {
      foreach (var itemInfo in _itemInfoes)
        itemInfo.skinnedMeshRenderer_list = skinnedMeshRenderer_list;
    }
  }
}