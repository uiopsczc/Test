using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
  [Serializable]
  public class TransformTweenClip : PlayableAsset, ITimelineClipAsset
  {
    [HideInInspector] public TimelineClip clip;
    public TransformTweenBehaviour template = new TransformTweenBehaviour();

    public ClipCaps clipCaps => ClipCaps.None;


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      template.clip = clip;
      var playable = ScriptPlayable<TransformTweenBehaviour>.Create(graph, template);
      return playable;
    }
  }
}