using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class GameEntity
  {
    protected override void __SetIsPaused(bool is_paused)
    {
      base.__SetIsPaused(is_paused);
      //Corotinues无法Pause
      //SetPause_Coroutines(is_pause);
    }



  }
}