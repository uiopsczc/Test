using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class HFSM
	{
		public virtual void ChangeToState(string key, bool isForce = false, params object[] args)
		{
			HFSMState toState = this.GetRootHFSM().GetSubState(key, true);
			ChangeToState(toState, isForce, args);
		}


		public virtual void ChangeToState(HFSMState toState, bool isForce = false, params object[] args)
		{
			HFSM rootHFSM = this.GetRootHFSM();
			HFSMState fromState = rootHFSM.GetCurrentState();

			if (fromState == toState)
				return;

			if (!isForce && fromState != null && !fromState.IsCanChangeToState(toState, args))
				return;

			HFSM nearestSameParentHFSM = toState.GetNearestSameParentHFSM(fromState);
			if (fromState != null)
			{
				this.Broadcast(rootHFSM.eventDispatchers, HFSMEventNameConst.Pre_State_Exit, fromState);
				fromState.ExitLoopTo(nearestSameParentHFSM);
				this.Broadcast(rootHFSM.eventDispatchers, HFSMEventNameConst.Post_State_Exit, fromState);
			}

			this.Broadcast(rootHFSM.eventDispatchers, HFSMEventNameConst.Pre_State_Enter, toState);
			nearestSameParentHFSM.EnterLoopTo(toState, args);
			this.Broadcast(rootHFSM.eventDispatchers, HFSMEventNameConst.Post_State_Enter, toState);

			previousState = fromState;
			this.Broadcast(rootHFSM.eventDispatchers, HFSMEventNameConst.State_Change_Finish, fromState, toState);
		}

		public virtual void ChangeToHFSM(string key, bool isForce = false, params object[] args)
		{
			HFSM toHFSM = this.GetRootHFSM().GetSubHFSM(key, true);
			while (toHFSM.defaultSubDirectHFSM != null)
				toHFSM = toHFSM.defaultSubDirectHFSM;
			HFSMState toState = toHFSM.defaultSubDirectState;
			this.ChangeToState(toState, isForce, args);
		}
	}
}