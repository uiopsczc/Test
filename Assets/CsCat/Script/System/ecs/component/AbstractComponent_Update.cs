using System;

namespace CsCat
{
  public partial class AbstractComponent
  {
    
    public virtual bool IsCanUpdate()
    {
      return is_enabled&&!is_paused&&!IsDestroyed();
    }
    
    public virtual void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (this.IsCanUpdate())
        __Update(deltaTime, unscaledDeltaTime);
    }

    public virtual void FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (this.IsCanUpdate())
        __FixedUpdate(deltaTime, unscaledDeltaTime);
    }


    public virtual void LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (this.IsCanUpdate())
        __LateUpdate(deltaTime, unscaledDeltaTime);
    }


    protected virtual void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
    }

    protected virtual void __FixedUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
    }

    protected virtual void __LateUpdate(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
    }
  }
}