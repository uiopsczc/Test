using UnityEngine;

namespace CsCat
{
  public partial class Unit
  {
    public Animation animation;
    private string cur_animation_name;
    private ActionManager actionManager;
    public AnimatorComp animatorComp;
    private AnimationCullingType? animationCullingType;

    private void SetAnimationCullingType(AnimationCullingType animationCullingType)
    {
      if (this.animation == null)
      {
        this.animationCullingType = animationCullingType;
        return;
      }

      this.animation.cullingType = animationCullingType;
    }

    public void PlayAnimation(string animation_name, float? blend_time = null, float? speed = null,
      Vector3? face_to_position = null, bool is_not_move_stop = false)
    {
      float _blend_time = blend_time.GetValueOrDefault(0.1f);
      float _speed = speed.GetValueOrDefault(1);
      if (this.animation != null)
      {
        if (AnimationNameConst.die.Equals(this.cur_animation_name))
          return;
        if (this.actionManager != null)
        {
          if (AnimationNameConst.walk.Equals(animation_name) && !this.cur_animation_name.IsNullOrWhiteSpace())
          {
            this.actionManager.Stop(this.cur_animation_name);
            this.cur_animation_name = null;
          }

          if (AnimationNameConst.idle.Equals(animation_name) && !this.cur_animation_name.IsNullOrWhiteSpace())
            this.actionManager.Play(animation_name, _speed, -1, false);
          else if (AnimationNameConst.walk.Equals(animation_name))
            this.actionManager.Play(animation_name, _speed, 0, false);
          else
          {
            this.actionManager.Play(animation_name, _speed, 0, true);
            this.cur_animation_name = animation_name;
            if (AnimationNameConst.die.Equals(animation_name))
              this.actionManager.Stop(AnimationNameConst.idle);
          }
        }
        else
        {
          if (AnimationNameConst.walk.Equals(animation_name) && !this.cur_animation_name.IsNullOrWhiteSpace())
          {
            this.animation.Blend(this.cur_animation_name, 0, _blend_time);
            this.cur_animation_name = null;
          }

          var animationState = this.animation[animation_name];
          if (animationState != null)
            LogCat.LogErrorFormat("animation is no exist: {0} , {1}", animation_name, this.unit_id);
          var speed_threshold = 0.5f;
          if (AnimationNameConst.walk.Equals(animation_name) && _speed < speed_threshold)
          {
            animationState.speed = speed_threshold;
            this.animation.CrossFade(animation_name, _blend_time);
            this.animation.Blend(animation_name, _speed / speed_threshold, _blend_time);
          }
          else
          {
            animationState.speed = _speed;
            this.animation.CrossFade(animation_name, _blend_time);
          }

          if (!(AnimationNameConst.idle.Equals(animation_name) || AnimationNameConst.walk.Equals(animation_name)))
          {
            if (this.cur_animation_name.Equals(animation_name))
              this.animation[animation_name].time = 0;
            this.cur_animation_name = animation_name;
          }
        }
      }
      else
        this.animatorComp.PlayAnimation(animation_name, true, _speed);

      if (face_to_position != null)
      {
        var rotation = Quaternion.LookRotation(face_to_position.Value - this.GetPosition());
        if (!rotation.IsZero() && !is_not_move_stop)
          this.MoveStop(rotation);
      }
    }

    public void StopAnimation(string animation_name = null, float? blend_time = null)
    {
      float _blend_time = blend_time.GetValueOrDefault(0.1f);
      if (this.animation != null)
      {
        if (this.actionManager != null)
          this.actionManager.Stop(animation_name);
        else
        {
          animation_name = animation_name ?? this.cur_animation_name;
          this.animation.Blend(animation_name, 0, _blend_time);
        }
      }
    }
  }
}