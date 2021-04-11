using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class AnimationTimelinableTrack : TimelinableTrackBase
  {
    public RuntimeAnimatorController runtimeAnimatorController;
    [NonSerialized] public Animator animator;
    [NonSerialized] public float animator_speed_when_paused;

    [SerializeField] private AnimationTimelinableItemInfo[] _itemInfoes = new AnimationTimelinableItemInfo[0];

    public override TimelinableItemInfoBase[] itemInfoes
    {
      get { return _itemInfoes; }
      set { _itemInfoes = value as AnimationTimelinableItemInfo[]; }
    }

    public override void CopyTo(object dest)
    {
      base.CopyTo(dest);
      var _dest = dest as AnimationTimelinableTrack;
      _dest.animator = animator;
      _dest.runtimeAnimatorController = runtimeAnimatorController;
    }

    public override void CopyFrom(object source)
    {
      base.CopyFrom(source);
      var _source = source as AnimationTimelinableTrack;
      animator = _source.animator;
      runtimeAnimatorController = _source.runtimeAnimatorController;
    }

    protected override void OnPauseStateChange()
    {
      base.OnPauseStateChange();
      if (animator != null)
      {
        if (is_paused)
          animator_speed_when_paused = animator.speed;
        else
          animator.speed = animator_speed_when_paused;
      }
    }
    

    public override void Retime(float time, params object[] args)
    {
      base.Retime(time, args.ToList().AddFirst(this.animator).ToArray());
#if UNITY_EDITOR
      SyncAnimationWindow();
#endif
    }

    public override void Tick(float time, params object[] args)
    {
      base.Tick(time, args.ToList().AddFirst(this.animator).ToArray());
    }
    
  }
}