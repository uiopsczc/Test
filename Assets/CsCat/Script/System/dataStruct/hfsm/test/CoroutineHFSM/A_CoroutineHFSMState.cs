using System.Collections;

namespace CsCat
{
  public class A_CoroutineHFSMState : CoroutineHFSMState
  {

    public override IEnumerator IEEnter(object[] args)
    {
      yield return base.IEEnter(args);
      LogCat.log("Enter A_CoroutineHFSMState", this.key);
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      LogCat.log("Execute A_CoroutineHFSMState", this.key);
    }

    public override IEnumerator IEExit(object[] args)
    {
      yield return base.IEExit(args);
      LogCat.log("Exit A_CoroutineHFSMState", this.key);
    }
  }
}

