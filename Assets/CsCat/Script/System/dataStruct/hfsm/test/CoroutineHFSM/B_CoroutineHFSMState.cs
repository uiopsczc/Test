using System.Collections;

namespace CsCat
{
  public class B_CoroutineHFSMState : CoroutineHFSMState
  {
    public override IEnumerator IEEnter(object[] args)
    {
      yield return base.IEEnter(args);
      LogCat.log("Enter B_CoroutineHFSMState", this.key);
    }

//    public override void DoUpdateLogic(float deltaTime = 0, float unscaledDeltaTime = 0)
//    {
//      base.DoUpdateLogic(deltaTime, unscaledDeltaTime);
//      LogCat.log("Execute B_CoroutineHFSMState", this.key);
//    }

    public override IEnumerator IEExit(object[] args)
    {
      yield return base.IEExit(args);
      LogCat.log("Exit B_CoroutineHFSMState", this.key);
    }
  }
}
