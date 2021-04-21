using System.Collections.Generic;

namespace CsCat
{
  public class HFSMComponent<T> : AbstractComponent where T : HFSM
  {
    public T hfsm;
    public void Init(T hfsm)
    {
      base.Init();
      this.hfsm = hfsm;
    }

    protected override void __SetIsPaused(bool is_paused)
    {
      base.__SetIsPaused(is_paused);
      this.hfsm.SetIsPaused(true, true);
    }

    protected override void __SetIsEnabled(bool is_enabled)
    {
      base.__SetIsEnabled(is_enabled);
      this.hfsm.SetIsEnabled(true, true);
    }

    protected override void __Reset()
    {
      base.__Reset();
      this.hfsm.Reset(true);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      this.hfsm.Destroy();
    }

    public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.Update(deltaTime, unscaledDeltaTime);
      this.hfsm.CheckDestroyed();
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      this.hfsm.Update(deltaTime, unscaledDeltaTime);
    }

    protected override void __FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__FixedUpdate(deltaTime, unscaledDeltaTime);
      this.hfsm.FixedUpdate(deltaTime, unscaledDeltaTime);
    }

    protected override void __LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__LateUpdate(deltaTime, unscaledDeltaTime);
      this.hfsm.LateUpdate(deltaTime, unscaledDeltaTime);
    }
  }
}