using UnityEngine;

namespace CsCat
{
  public class LineEffectComponent : SourceTargetEffectComponent
  {
    private float speed;
    private float acc_speed;
    private float stay_source_duration;


    public void Init(IPosition source_iposition,
      IPosition target_iposition, float stay_source_duration = 0, float speed = 0,
      float acc_speed = 0)
    {
      base.Init();
      this.source_iposition = source_iposition;
      this.target_iposition = target_iposition;
      SetSocket();
      this.speed = speed;
      this.acc_speed = acc_speed;
      this.stay_source_duration = stay_source_duration;

      Calculate(0);
      this.effectEntity.ApplyToTransformComponent(this.current_position, this.current_eulerAngles);
    }



    protected override void Calculate(float deltaTime)
    {
      this.stay_source_duration = this.stay_source_duration - deltaTime;

      if (this.stay_source_duration >= 0)
      {
        this.source_position = this.source_iposition.GetPosition();
        this.target_position = this.target_iposition.GetPosition();
        this.current_position = this.source_position;
        CalculateEulerAngles();
        return;
      }

      this.speed += this.acc_speed;
      float remain_duration = Vector3.Distance(this.current_position, this.target_position) / this.speed;
      float pct = Mathf.Clamp01(deltaTime / remain_duration);
      this.current_position = Vector3.Lerp(this.current_position, this.target_position, pct);

      this.CalculateEulerAngles();
      if (pct == 1)
        OnEffectReach();
    }

    
  }
}



