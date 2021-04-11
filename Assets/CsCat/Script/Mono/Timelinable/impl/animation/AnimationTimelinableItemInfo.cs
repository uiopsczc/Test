using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class AnimationTimelinableItemInfo : TimelinableItemInfoBase
  {
    [SerializeField]
    private AnimationClip _animationClip;

    public AnimationClip animationClip
    {
      get => _animationClip;
      set
      {
        var pre_animationClip = _animationClip;
        _animationClip = value;
        if (pre_animationClip != value)
          duration = _animationClip.length;
      }
    }
    public float cross_fade_duration=0.1f;


    public float speed
    {
      get
      {
        if (animationClip != null)
        {
          if (animationClip.length > 0)
            return animationClip.length / duration;
          return 1;
        }
        return 1;
      }
    }

    public AnimationTimelinableItemInfo()
    {
    }

    public AnimationTimelinableItemInfo(AnimationTimelinableItemInfo other)
    {
      CopyFrom(other);
    }


    public override void CopyTo(object dest)
    {
      var _dest = dest as AnimationTimelinableItemInfo;
      _dest.animationClip = animationClip;
      base.CopyTo(dest);
    }

    public override void CopyFrom(object source)
    {
      var _source = source as AnimationTimelinableItemInfo;
      animationClip = _source.animationClip;
      base.CopyFrom(source);
    }

    public override void Play(params object[] args)
    {
      var track = args[args.Length-1] as AnimationTimelinableTrack;
      var animator = args[0] as Animator;
      if (animator != null)
      {
        animator.speed = speed;
        animator.CrossFade(animationClip.name, cross_fade_duration);
      }
      base.Play(args);
      //      LogCat.log("Play");
    }

    public override void Stop(params object[] args)
    {
      base.Stop(args);
      //      LogCat.log("Stop");
    }


  }
}