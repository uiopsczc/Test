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

		public virtual IEnumerator IEEnterLoopTo(CoroutineHFSMState toState, params object[] args)
		{
			var hfsmList = new List<CoroutineHFSM>();//倒序
			var hfsm = toState.parentHFSM as CoroutineHFSM;
			while (hfsm != this)
			{
				hfsmList.Add(hfsm);
				hfsm = hfsm.parentHFSM as CoroutineHFSM;
			}

			for (int i = hfsmList.Count - 1; i >= 0; i--)
				yield return hfsmList[i].IEEnter(args);

			yield return toState.IEEnter(args);
		}


		public override void ChangeToState(HFSMState toState, bool isForce = false, params object[] args)
		{
			StartCoroutine(IEChangeToState(toState as CoroutineHFSMState, isForce, args));
		}

		public IEnumerator IEChangeToState(CoroutineHFSMState toState, bool isForce = false, params object[] args)
		{
			CoroutineHFSM rootHFSM = this.GetRootHFSM() as CoroutineHFSM;
			CoroutineHFSMState fromState = rootHFSM.GetCurrentState() as CoroutineHFSMState;

			if (fromState == toState)
				yield break;

			if (!isForce && fromState != null && !fromState.IsCanChangeToState(toState, args))
				yield break;

			CoroutineHFSM nearestSameParentHFSM = toState.GetNearestSameParentHFSM(fromState) as CoroutineHFSM;
			if (fromState != null)
			{
				this.Broadcast(rootHFSM.eventDispatchers, CoroutineHFSMEventNameConst.Pre_State_Exit, fromState);
				yield return fromState.IEExitLoopTo(nearestSameParentHFSM);
				this.Broadcast(rootHFSM.eventDispatchers, CoroutineHFSMEventNameConst.Post_State_Exit, fromState);
			}

			this.Broadcast(rootHFSM.eventDispatchers, CoroutineHFSMEventNameConst.Pre_State_Enter, toState);
			yield return nearestSameParentHFSM.IEEnterLoopTo(toState, args);
			this.Broadcast(rootHFSM.eventDispatchers, CoroutineHFSMEventNameConst.Post_State_Enter, toState);

			previousState = fromState;
			this.Broadcast(rootHFSM.eventDispatchers, CoroutineHFSMEventNameConst.State_Change_Finish, fromState, toState);
		}


	}
}

