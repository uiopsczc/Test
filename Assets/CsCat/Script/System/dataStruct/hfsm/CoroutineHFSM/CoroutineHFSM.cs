using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class CoroutineHFSM : HFSM
  {
    public CoroutineHFSM(GameEntity owner) : base(owner)
    {
    }
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

    public virtual IEnumerator IEEnterLoopTo(CoroutineHFSMState to_state, params object[] args)
    {
      var hfsm_list = new List<CoroutineHFSM>();//倒序
      var _hfsm = to_state.parent_hfsm as CoroutineHFSM;
      while (_hfsm != this)
      {
        hfsm_list.Add(_hfsm);
        _hfsm = _hfsm.parent_hfsm as CoroutineHFSM;
      }

      for (int i = hfsm_list.Count - 1; i >= 0; i--)
        yield return hfsm_list[i].IEEnter(args);

      yield return to_state.IEEnter(args);
    }


    public override void ChangeToState(HFSMState to_state, bool is_force = false, params object[] args)
    {
      StartCoroutine(IEChangeToState(to_state as CoroutineHFSMState, is_force, args));
    }

    public IEnumerator IEChangeToState(CoroutineHFSMState to_state, bool is_force = false, params object[] args)
    {
      CoroutineHFSM root_hfsm = this.GetRootHFSM() as CoroutineHFSM;
      CoroutineHFSMState from_state = root_hfsm.GetCurrentState() as CoroutineHFSMState;

      if (from_state == to_state)
        yield break;

      if (!is_force && from_state != null && !from_state.IsCanChangeToState(to_state, args))
        yield break;

      CoroutineHFSM nearest_same_parent_hfsm = to_state.GetNearestSameParentHFSM(from_state) as CoroutineHFSM;
      if (from_state != null)
      {
        this.Broadcast(root_hfsm.eventDispatchers, CoroutineHFSMEventNameConst.Pre_State_Exit, from_state);
        yield return from_state.IEExitLoopTo(nearest_same_parent_hfsm);
        this.Broadcast(root_hfsm.eventDispatchers, CoroutineHFSMEventNameConst.Post_State_Exit, from_state);
      }

      this.Broadcast(root_hfsm.eventDispatchers,CoroutineHFSMEventNameConst.Pre_State_Enter, to_state);
      yield return nearest_same_parent_hfsm.IEEnterLoopTo(to_state, args);
      this.Broadcast(root_hfsm.eventDispatchers,CoroutineHFSMEventNameConst.Post_State_Enter, to_state);

      previous_state = from_state;
      this.Broadcast(root_hfsm.eventDispatchers, CoroutineHFSMEventNameConst.State_Change_Finish, from_state, to_state);
    }


  }
}

