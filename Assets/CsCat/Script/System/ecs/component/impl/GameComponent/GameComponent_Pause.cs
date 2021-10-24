using System;

namespace CsCat
{
  public partial class GameComponent
  {
    protected override void _SetIsPaused(bool isPaused)
    {
      base._SetIsPaused(isPaused);
      SetIsPaused_Timers(isPaused);
      SetIsPaused_DOTweens(isPaused);
      //Corotinues无法Pause
      //SetPause_Coroutines(is_pause);
      SetIsPaused_PausableCoroutines(isPaused);
    }
  }
}