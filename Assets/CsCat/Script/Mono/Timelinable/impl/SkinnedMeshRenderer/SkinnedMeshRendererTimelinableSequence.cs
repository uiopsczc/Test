
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CsCat
{
  public class SkinnedMeshRendererTimelinableSequence : TimelinableSequenceBase
  {
    [SerializeField] private SkinnedMeshRendererTimelinableTrack[] _tracks = new SkinnedMeshRendererTimelinableTrack[0];

    public override TimelinableTrackBase[] tracks
    {
      get { return _tracks; }
      set { _tracks = value as SkinnedMeshRendererTimelinableTrack[]; }
    }
  }
}



