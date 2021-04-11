using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class AnimationTimelinableSequencePlayer : TimelinableSequencePlayerBase
  {
    public Animator animator;
    private Vector3 localPosition;
    private Vector3 localEulerAngles;
    private Vector3 localScale;

    private float speed_when_paused;//停止时的播放速度
    public float speed = 1;

    public AnimationTimelinableSequencePlayer(Transform transform, float speed = 1) : base(transform)
    {
      this.speed = speed;
      animator = transform.GetComponent<Animator>();
      this.localPosition = transform.localPosition;
      this.localEulerAngles = transform.localEulerAngles;
      this.localScale = transform.localScale;
    }

    public override void Play()
    {
      base.Play();
      if (animator != null)
      {
//        if (sequence != null)
//          animator.runtimeAnimatorController = (sequence as AnimationTimelinableSequence).runtimeAnimatorController;

        animator.enabled = true;
        animator.speed = 1;
      }

    }

    public override void Stop()
    {
      base.Stop();
    }

    public override void Reset()
    {
      base.Reset();
      transform.localPosition = localPosition;
      transform.localEulerAngles = localEulerAngles;
      transform.localScale = localScale;
      if (animator != null)
        animator.enabled = false;
    }

    public override void Dispose()
    {
      if (animator != null)
        animator.runtimeAnimatorController = null;
      base.Dispose();
    }

    public override void Pause()
    {
      base.Pause();
      if (animator != null)
      {
        speed_when_paused = animator.speed;
        animator.speed = 0;
      }
    }

    public override void UnPause()
    {
      base.UnPause();
      if (animator != null)
        animator.speed = speed_when_paused;
    }

    public override void UpdateTime(float time)
    {
      cur_time = time;
      if (is_playing)
        sequence.Tick(time, this);
      else
        sequence.Retime(time, this);
    }

  }
}



