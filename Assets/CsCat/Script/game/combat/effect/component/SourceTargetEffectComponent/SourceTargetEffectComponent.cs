using System;
using UnityEngine;

namespace CsCat
{
  //弹道
  public class SourceTargetEffectComponent : EffectComponent
  {
    protected string source_socket_name;
    protected string target_socket_name;
    public Vector3 source_position;
    public Vector3 target_position;
    public IPosition source_iposition;
    public IPosition target_iposition;
    public Vector3 current_position;
    public Vector3 current_eulerAngles;


    public Action on_reach_callback;



    public void SetSocket()
    {
      this.source_socket_name = this.effectEntity.cfgEffectData.socket_name_1 ?? "missile";
      this.target_socket_name = this.effectEntity.cfgEffectData.socket_name_2 ?? "chest";

      source_iposition?.SetSocketName(this.source_socket_name);
      target_iposition?.SetSocketName(this.target_socket_name);
    }




    // 计算sourcePosition,targetPosition,eulerAngles
    protected virtual void Calculate(float deltaTime)
    {
      this.source_position = this.source_iposition.GetPosition();
      this.target_position = this.target_iposition.GetPosition();
      this.current_position = this.source_position;
      CalculateEulerAngles();
    }

    public void CalculateEulerAngles()
    {
      Vector3 diff = this.target_position - this.current_position;
      if (diff.Equals(Vector3.zero))
        this.current_eulerAngles = Vector3.zero;
      else
        this.current_eulerAngles = Quaternion.LookRotation(diff, Vector3.up).eulerAngles;
    }

    public virtual void OnEffectReach()
    {
      on_reach_callback?.Invoke();
      this.effectEntity.OnEffectReach();
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      Calculate(deltaTime);
      this.effectEntity.ApplyToTransformComponent(this.current_position, this.current_eulerAngles);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      this.on_reach_callback = null;
    }
  }
}


