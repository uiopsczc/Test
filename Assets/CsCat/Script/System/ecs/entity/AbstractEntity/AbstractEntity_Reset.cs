using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    public Action reset_callback;
    

    public void Reset(bool is_loop_children = false)
    {
      if(is_loop_children)
        ResetAllChildren();
      ResetAllComponents();
      __Reset();
      __PostReset();
    }
    protected virtual void __Reset()
    {
      
    }
    protected virtual void __PostReset()
    {
      reset_callback?.Invoke();
      reset_callback = null;
    }

    public void ResetAllChildren()
    {
      foreach (var child in ForeachChild())
        child.Reset(true);
    }

    public void ResetAllComponents()
    {
      foreach (var component in ForeachComponent())
        component.Reset();
    }

    void __OnDespawn_Reset()
    {
      reset_callback = null;
    }
  }
}