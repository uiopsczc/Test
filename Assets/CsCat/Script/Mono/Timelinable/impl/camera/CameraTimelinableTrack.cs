
using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class CameraTimelinableTrack : TimelinableTrackBase
  {
    [NonSerialized] public Animator animator;
    [NonSerialized] public float animator_speed_when_paused;

    [SerializeField] private CameraTimelinableItemInfo[] _itemInfoes = new CameraTimelinableItemInfo[0];

    public override TimelinableItemInfoBase[] itemInfoes
    {
      get { return _itemInfoes; }
      set { _itemInfoes = value as CameraTimelinableItemInfo[]; }
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
      base.Retime(time, args);
#if UNITY_EDITOR
      SyncAnimationWindow();
#endif
    }
  }
}



