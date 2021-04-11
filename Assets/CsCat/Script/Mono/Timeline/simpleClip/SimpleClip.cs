using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
  [Serializable]
  public class SimpleClip : PlayableAsset, ITimelineClipAsset
  {
    public SimpleBehaviour template = new SimpleBehaviour();

    //Defines clip characteristics such as blending, extrapolation, looping, etc.
    public ClipCaps clipCaps => ClipCaps.None;

    //Necessary function to pair the Clip with the Behaviour
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      var playable = ScriptPlayable<SimpleBehaviour>.Create(graph, template);
      return playable;
    }
  }
}