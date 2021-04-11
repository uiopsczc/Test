using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    protected bool _is_paused;
    public bool is_paused=> _is_paused;

    public virtual void SetIsPaused(bool is_paused, bool is_loop_children = false)
    {
      if (_is_paused == is_paused)
        return;
      this._is_paused = is_paused;
      if(is_loop_children)
        SetAllChildrenIsPaused(is_paused);
      SetAllComponentsIsPaused(is_paused);
      __SetIsPaused(is_paused);
    }

    protected virtual void __SetIsPaused(bool is_paused)
    {
    }

    public void SetAllChildrenIsPaused(bool is_paused)
    {
      foreach (var child in ForeachChild())
        child.SetIsPaused(is_paused, true);
    }
    public void SetAllComponentsIsPaused(bool is_paused)
    {
      foreach (var component in ForeachComponent())
        component.SetIsPaused(is_paused);
    }

    void __OnDespawn_Pause()
    {
      _is_paused = false;
    }
  }
}