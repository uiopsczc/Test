using System;

namespace CsCat
{
  public partial class AbstractComponent
  {
    private bool _is_enabled;

    public bool is_enabled => _is_enabled;


    public void SetIsEnabled(bool is_enabled)
    {
      if (_is_enabled == is_enabled)
        return;
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