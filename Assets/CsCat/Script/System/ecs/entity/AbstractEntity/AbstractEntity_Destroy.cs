using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    private bool __is_destroyed;
    public Action destroy_callback;

    public bool IsDestroyed()
    {
      return __is_destroyed;
    }

    public void Destroy()
    {
      if (IsDestroyed())
        return;
      RemoveAllChildren();
      SetIsEnabled(false, false);
      SetIsPaused(false, false);
      RemoveAllComponents();
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

    public virtual void OnDespawn()
    {
      __OnDespawn_();
      __OnDespawn_Child();
      __OnDespawn_Component();
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