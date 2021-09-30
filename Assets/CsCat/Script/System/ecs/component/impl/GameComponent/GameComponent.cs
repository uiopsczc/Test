using System;

namespace CsCat
{
  public partial class GameComponent : AbstractComponent
  {
    protected override void __Reset()
    {
      base.__Reset();
      StopAllCoroutines();
      StopAllPausableCoroutines();
      RemoveAllDOTweens();
      RemoveAllTimers();

	    RemoveAllListeners();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      StopAllCoroutines();
      StopAllPausableCoroutines();
      RemoveAllDOTweens();
      RemoveAllTimers();

      RemoveAllListeners();
    }
  }
}