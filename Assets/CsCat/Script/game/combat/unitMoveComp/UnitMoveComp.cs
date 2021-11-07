using System;

namespace CsCat
{
  public partial class UnitMoveComp : TickObject
  {
    private Unit unit;
    private float walk_step_length;
    public string move_type; // move, be_throwed
    private bool is_move_with_move_animation = true;
    private float adjust_dist_sqr = 3 * 3;
    public bool is_get_caught;
    private UnitMoveInfo unitMoveInfo = new UnitMoveInfo();
    private UnitLookAtInfo unitLookAtInfo = new UnitLookAtInfo();

    public void Init(Unit unit)
    {
      base.Init();
      this.unit = unit;
      this.walk_step_length = this.unit.cfgUnitData.walk_step_length;
      this.unitMoveInfo.speed = this.unit.GetSpeed();
      this.unitMoveInfo.target_pos = this.unit.GetPosition();
      this.unitMoveInfo.end_rotation = this.unit.GetRotation();
    }

    protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base._Update(deltaTime, unscaledDeltaTime);
      this.__UpdateMove(deltaTime);

      //    if (this.unitLookAtInfo.HasLookAt())
      //      this.__UpdateLookAt(deltaTime);
      if (this.unitBeThrowedInfo != null)
        this.__UpdateBeThrowed(deltaTime);
    }

    public void Destroy()
    {
      this.unit = null;
    }

    public void OnBuild()
    {
    }

    public void OnBuildOk()
    {
      Unit unit = this.unit;
      if ("move".Equals(this.move_type))
      {
        if (this.unitMoveInfo.IsHasAnimationName() && this.is_move_with_move_animation)
          unit.PlayAnimation(this.unitMoveInfo.animation_name, 0, this.unitMoveInfo.animation_speed);
        unit.__MoveTo(this.unitMoveInfo.target_pos, this.unitMoveInfo.remain_duration);
      }
    }

    public void OnSpeedChange(float old_value, float new_value)
    {
      var unit = this.unit;
      float factor = new_value / old_value;
      this.unitMoveInfo.speed = this.unitMoveInfo.speed * factor;
      if (this.move_type.Equals("move"))
      {
        this.unitMoveInfo.remain_duration = this.unitMoveInfo.remain_duration / factor;
        var old_move_animation_speed = this.unitMoveInfo.animation_speed;
        this.unitMoveInfo.animation_speed = this.unitMoveInfo.animation_speed * factor;
        if (unit.graphicComponent.transform != null)
        {
          unit.__MoveTo(this.unitMoveInfo.target_pos, this.unitMoveInfo.remain_duration);
          if (this.unitMoveInfo.IsHasAnimationName() && this.is_move_with_move_animation &&
              Math.Abs(this.unitMoveInfo.animation_speed - old_move_animation_speed) > 0.2f)
            unit.PlayAnimation(this.unitMoveInfo.animation_name, 0.2f, this.unitMoveInfo.animation_speed);
        }
      }
    }

    public void SetIsMoveWithMoveAnimation(bool is_move_with_move_animation)
    {
      var unit = this.unit;
      this.is_move_with_move_animation = is_move_with_move_animation;
      if (this.move_type.Equals("move"))
      {
        if (is_move_with_move_animation)
        {
          if (this.unitMoveInfo.IsHasAnimationName())
            unit.PlayAnimation(this.unitMoveInfo.animation_name, null, this.unitMoveInfo.animation_speed);
        }
        else
          unit.StopAnimation(this.unitMoveInfo.animation_name, 0.2f);
      }
    }
  }
}