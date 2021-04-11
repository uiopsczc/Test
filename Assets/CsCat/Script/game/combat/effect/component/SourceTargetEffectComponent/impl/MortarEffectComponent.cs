using UnityEngine;

namespace CsCat
{
  //迫击炮弹道
  public class MortarEffectComponent : SourceTargetEffectComponent
  {
    private float all_duration;
    private float start_angle;
    private Vector3 direction;
    private Vector3 gravity;
    private float height;
    private float remain_duration;
    private Vector3 start_position;
    private Vector3 velocity;
    private float vertical;

    public void Init(IPosition source_iposition,
      IPosition target_iposition
      , Vector3 gravity, float start_angle)
    {
      base.Init();
      this.source_iposition = source_iposition;
      this.target_iposition = target_iposition;
      SetSocket();
      this.gravity = gravity;
      this.start_angle = start_angle;

      __InitFields();

      Calculate(0);
      this.effectEntity.ApplyToTransformComponent(this.current_position, this.current_eulerAngles);
    }

    void __InitFields()
    {
      this.source_position = source_iposition.GetPosition();
      this.target_position = target_iposition.GetPosition();
      this.current_eulerAngles = Quaternion.LookRotation(target_position - source_position, Vector3.up).eulerAngles;
      var target_position_xz = target_position.SetZeroY();
      var source_position_xz = source_position.SetZeroY();
      var distance = Vector3.Distance(target_position_xz, source_position_xz);
      var rad = Mathf.Atan2(start_angle, distance);
      var dir_horizon = (target_position_xz - source_position_xz).normalized / Mathf.Tan(rad);
      var dir = dir_horizon + new Vector3(0, 1, 0);
      var gravity_y = Mathf.Abs(this.gravity.y);
      var height = source_position.y - target_position.y;
      var rate = Mathf.Tan(rad) * gravity_y * distance /
                 Mathf.Sqrt(2 * gravity_y * (height + distance * Mathf.Tan(rad)));

      this.velocity = dir * rate;
      this.remain_duration = distance / (dir_horizon.magnitude * rate);
      this.all_duration = remain_duration;
      this.start_position = source_position;
      this.vertical = rate;
      this.direction = velocity;
      this.height = start_position.y;

      this.current_position = start_position;
    }


    protected override void Calculate(float deltaTime)
    {
      remain_duration = remain_duration - deltaTime;
      if (remain_duration <= 0)
      {
        OnEffectReach();
        return;
      }
      direction = direction + gravity * deltaTime;
      this.current_eulerAngles = Quaternion.LookRotation(direction).eulerAngles;
      var pass_duration = all_duration - remain_duration;
      var interp = remain_duration / all_duration;
      var position_new = start_position * interp + target_position * (1 - interp);
      var height = this.height + vertical * pass_duration + gravity.y * pass_duration * pass_duration * 0.5f;
      position_new.y = height;
      this.current_position = position_new;
    }
  }
}