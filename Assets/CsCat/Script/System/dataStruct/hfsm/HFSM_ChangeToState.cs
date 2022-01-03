using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class HFSM
	{

		public virtual void ChangeToState(string key, bool is_force = false, params object[] args)
		{
			HFSMState to_state = this.GetRootHFSM().GetSubState(key, true);
			ChangeToState(to_state, is_force, args);
		}


		public virtual void ChangeToState(HFSMState to_state, bool is_force = false, params object[] args)
		{
			HFSM root_hfsm = this.GetRootHFSM();
			HFSMState from_state = root_hfsm.GetCurrentState();

			if (from_state == to_state)
				return;

			if (!is_force && from_state != null && !from_state.IsCanChangeToState(to_state, args))
				return;

			HFSM nearest_same_parent_hfsm = to_state.GetNearestSameParentHFSM(from_state);
			if (from_state != null)
			{
				this.Broadcast(root_hfsm.eventDispatchers, HFSMEventNameConst.Pre_State_Exit, from_state);
				from_state.ExitLoopTo(nearest_same_parent_hfsm);
				this.Broadcast(root_hfsm.eventDispatchers, HFSMEventNameConst.Post_State_Exit, from_state);
			}

			this.Broadcast(root_hfsm.eventDispatchers, HFSMEventNameConst.Pre_State_Enter, to_state);
			nearest_same_parent_hfsm.EnterLoopTo(to_state, args);
			this.Broadcast(root_hfsm.eventDispatchers, HFSMEventNameConst.Post_State_Enter, to_state);

			previous_state = from_state;
			this.Broadcast(root_hfsm.eventDispatchers, HFSMEventNameConst.State_Change_Finish, from_state, to_state);
		}

		public virtual void ChangeToHFSM(string key, bool is_force = false, params object[] args)
		{
			HFSM to_hfsm = this.GetRootHFSM().GetSubHFSM(key, true);
			while (to_hfsm.default_sub_direct_hfsm != null)
				to_hfsm = to_hfsm.default_sub_direct_hfsm;
			HFSMState to_state = to_hfsm.default_sub_direct_state;
			this.ChangeToState(to_state, is_force, args);
		}

	}
}