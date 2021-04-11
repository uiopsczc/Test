using System.Collections;

namespace CsCat
{
  /// <summary>
  /// 状态
  /// </summary>
  public class CoroutineHFSMState : HFSMState
  {
    public virtual IEnumerator IEEnter(params object[] args)
    {
      base.Enter(args);
      yield break;
    }

    public virtual IEnumerator IEExit(params object[] args)
    {
      base.Exit(args);
      yield break;
    }

    public virtual IEnumerator IEExitLoopTo(CoroutineHFSM to_hfsm, params object[] args)
    {
      yield return IEExit();
      var _hfsm = parent_hfsm as CoroutineHFSM;
      while (_hfsm != to_hfsm)
      {
        yield return _hfsm.IEExit(args);
        _hfsm = _hfsm.parent_hfsm as CoroutineHFSM;
      }
    }
  }
}