using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public partial class HFSM
  {
    public override bool IsCanUpdate()
    {
      return isEnabled && base.IsCanUpdate();
    }

    public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate()) return;
      _Update(deltaTime, unscaledDeltaTime);
      foreach (var component in ForeachComponent())
        component.Update(deltaTime, unscaledDeltaTime);
      current_sub_direct_state?.Update(deltaTime, unscaledDeltaTime);
      current_sub_direct_hfsm?.Update(deltaTime, unscaledDeltaTime);
    }

    public override void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate()) return;
      _FixedUpdate(deltaTime, unscaledDeltaTime);
      foreach (var component in ForeachComponent())
        component.FixedUpdate(deltaTime, unscaledDeltaTime);
      current_sub_direct_state?.FixedUpdate(deltaTime, unscaledDeltaTime);
      current_sub_direct_hfsm?.FixedUpdate(deltaTime, unscaledDeltaTime);
    }


    public override void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate()) return;
      _LateUpdate(deltaTime, unscaledDeltaTime);
      foreach (var component in ForeachComponent())
        component.LateUpdate(deltaTime, unscaledDeltaTime);
      current_sub_direct_state?.LateUpdate(deltaTime, unscaledDeltaTime);
      current_sub_direct_hfsm?.LateUpdate(deltaTime, unscaledDeltaTime);
    }
  }
}