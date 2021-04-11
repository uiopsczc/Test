using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class AbstractEntity
  {
    private bool _is_enabled;

    public bool is_enabled => _is_enabled;

    public void SetIsEnabled(bool is_enabled, bool is_loop_children = false)
    {
      if (_is_enabled == is_enabled)
        return;
      if (is_loop_children)
      {
        foreach (var child in ForeachChild())
          child.SetIsEnabled(is_enabled);
      }

      foreach (var component in ForeachComponent())
        component.SetIsEnabled(is_enabled);
      _is_enabled = is_enabled;
      __SetIsEnabled(is_enabled);
      if (is_enabled)
        OnEnable();
      else
        OnDisable();
    }

    protected virtual void __SetIsEnabled(bool is_enabled)
    {

    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    void __OnDespawn_Enable()
    {
      _is_enabled = false;
    }
  }
}