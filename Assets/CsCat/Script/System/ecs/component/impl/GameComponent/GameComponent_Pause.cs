using System;

namespace CsCat
{
  public partial class GameComponent
  {
    protected override void __SetIsPaused(bool is_paused)
    {
      base.__SetIsPaused(is_paused);
      SetIsPaused_Timers(is_paused);
      SetIsPaused_DOTweens(is_paused);
      //Corotinues无法Pause
      //SetPause_Coroutines(is_pause);
      SetIsPaused_PausableCoroutines(is_paused);
    }
  }
}