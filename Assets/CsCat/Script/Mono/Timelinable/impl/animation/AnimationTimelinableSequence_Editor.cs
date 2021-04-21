#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class AnimationTimelinableSequence : TimelinableSequenceBase
  {
    public void SyncAnimationWindow()
    {
      int i = 0;
      foreach (var track in tracks)
        (track as AnimationTimelinableTrack).SyncAnimationWindow();
    }






  }
}
#endif