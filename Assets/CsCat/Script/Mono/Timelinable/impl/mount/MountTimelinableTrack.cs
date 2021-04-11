
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class MountTimelinableTrack : TimelinableTrackBase
  {
    [NonSerialized]
    public Transform transform;
    [SerializeField] private MountTimelinableItemInfo[] _itemInfoes = new MountTimelinableItemInfo[0];
    [SerializeField] private MountTimelinableItemInfoLibrary _itemInfoLibrary;

    public override TimelinableItemInfoBase[] itemInfoes
    {
      get { return _itemInfoes; }
      set { _itemInfoes = value as MountTimelinableItemInfo[]; }
    }

    public override TimelinableItemInfoLibraryBase itemInfoLibrary
    {
      get { return _itemInfoLibrary; }
      set { _itemInfoLibrary = value as MountTimelinableItemInfoLibrary; }
    }


    public override void Retime(float time, params object[] args)
    {
      base.Retime(time, args.ToList().AddFirst(this.transform).ToArray());
    }

    public override void Tick(float time, params object[] args)
    {
      base.Tick(time, args.ToList().AddFirst(this.transform).ToArray());
    }


    
  }
}



