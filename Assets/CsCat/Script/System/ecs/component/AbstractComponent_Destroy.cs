using System;

namespace CsCat
{
  public partial class AbstractComponent
  {
    private bool __is_destroyed;
    public Action destroy_callback;

    public bool IsDestroyed()
    {
      return this.__is_destroyed;
    }
    

    public void Destroy()
    {
      if (IsDestroyed())
        return;
      SetIsEnabled(false);
      SetIsPaused(false);
      __Destroy();
      __is_destroyed = true;
      __PostDestroy();
      cache.Clear();
    }

    protected virtual void __Destroy()
    {
    }

    protected virtual void __PostDestroy()
    {
      destroy_callback?.Invoke();
      destroy_callback = null;
    }

    
    public void OnDespawn()
    {
      __OnDespawn_();
      __OnDespawn_Destroy();
      __OnDespawn_Enable();
      __OnDespawn_Pause();
      __OnDespawn_Reset();
    }

    void __OnDespawn_Destroy()
    {
      __is_destroyed = false;
      destroy_callback = null;
    }
  }
}