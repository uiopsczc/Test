using System;

namespace CsCat
{
  public partial class AbstractComponent
  {
    public Action reset_callback;


    public void Reset()
    {
      _Reset();
      __PostReset();
    }
    protected virtual void _Reset()
    {
    }
    protected virtual void __PostReset()
    {
      reset_callback?.Invoke();
      this.reset_callback = null;
    }


    void __OnDespawn_Reset()
    {
      reset_callback = null;
    }
  }
}