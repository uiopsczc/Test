
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class MountTimelinableSequence : TimelinableSequenceBase
  {
    [SerializeField] private MountTimelinableTrack[] _tracks = new MountTimelinableTrack[0];

    public override TimelinableTrackBase[] tracks
    {
      get { return _tracks; }
      set { _tracks = value as MountTimelinableTrack[]; }
    }
  }
}



