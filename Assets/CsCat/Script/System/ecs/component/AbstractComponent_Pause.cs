using System;

namespace CsCat
{
  public partial class AbstractComponent
  {
    private bool _is_paused;

    public bool is_paused => _is_paused;

    public void SetIsPaused(bool is_paused)
    {
      if (_is_paused == is_paused)
        return;
      _is_paused = is_paused;
      __SetIsPaused(is_paused);
    }

    protected virtual void __SetIsPaused(bool is_paused)
    {

    }

    void __OnDespawn_Pause()
    {
      _is_paused = false;
    }
  }
}