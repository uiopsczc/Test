using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  /// HFSMState
  /// </summary>
  public class HFSMState : TickObject
  {
    public HFSM parent_hfsm;

    #region ctor

    public HFSMState()
    {
    }

    #endregion

    public T GetOwner<T>() where T : GameEntity
    {
      return this.GetRootHFSM().owner as T;
    }
    #region virtual method

    public virtual bool IsCanChangeToState(HFSMState to_state, params object[] args)
    {
      return true;
    }

    public virtual void Enter(params object[] args)
    {
      this.SetIsEnabled(true, false);
      this.parent_hfsm.current_sub_direct_state = this;
    }


    public virtual void Exit(params object[] args)
    {
      this.SetIsEnabled(false, false);
      this.parent_hfsm.current_sub_direct_state = null;
    }

    public virtual void ExitLoopTo(HFSM to_hfsm, params object[] args)
    {
      this.Exit();
      var _hfsm = parent_hfsm;
      while (_hfsm != to_hfsm)
      {
        _hfsm.Exit(args);
        _hfsm = _hfsm.parent_hfsm;
      }
    }
    #endregion



    public override bool IsCanUpdate()
    {
      return is_enabled && base.IsCanUpdate();
    }

    public List<HFSM> GetParentHFSMList()
    {
      List<HFSM> list = new List<HFSM>();
      var _hfsm = parent_hfsm;
      list.Add(_hfsm);
      list.AddRange(_hfsm.GetParentHFSMList());
      return list;
    }

    //两个state最靠近的父hfsm
    public HFSM GetNearestSameParentHFSM(HFSMState state2)
    {
      if (state2 == null)
        return this.GetRootHFSM();
      List<HFSM> hfsm_list1 = GetParentHFSMList();
      List<HFSM> hfsm_list2 = state2.GetParentHFSMList();

      List<HFSM> hfsm_deeper_list;
      Dictionary<HFSM, bool> hfsm_dict = new Dictionary<HFSM, bool>();
      if (hfsm_list1.Count > hfsm_list2.Count)
        hfsm_deeper_list = hfsm_list1;
      else
        hfsm_deeper_list = hfsm_list2;
      var hfsm_lower_list = hfsm_deeper_list == hfsm_list1 ? hfsm_list2 : hfsm_list1;
      foreach (var hfsm in hfsm_lower_list)
        hfsm_dict[hfsm] = true;

      foreach (var hfsm in hfsm_deeper_list)
      {
        if (hfsm_dict.ContainsKey(hfsm))
          return hfsm;
      }
      return null;
    }



    public HFSM GetRootHFSM()
    {
      return this.parent_hfsm.GetRootHFSM();
    }

    public void ChangeToState(string key, bool is_force = false, params object[] args)
    {
      this.GetRootHFSM().ChangeToState(key, is_force, args);
    }

    public void ChangeToState(HFSMState to_state, bool is_force = false, params object[] args)
    {
      this.GetRootHFSM().ChangeToState(to_state, is_force, args);
    }

    public void ChangeToHFSM(string key, bool is_force = false, params object[] args)
    {
      this.GetRootHFSM().ChangeToHFSM(key, is_force, args);
    }

    /// <summary>
    /// 回到上一个状态
    /// </summary>
    public void RevertToPreviousState()
    {
      this.GetRootHFSM().RevertToPreviousState();
    }
  }
}
