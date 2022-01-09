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
		public HFSM parentHFSM;


		public HFSMState()
		{
		}


		public T GetOwner<T>() where T : GameEntity
		{
			return this.GetRootHFSM().owner as T;
		}

		#region virtual method

		public virtual bool IsCanChangeToState(HFSMState toState, params object[] args)
		{
			return true;
		}

		public virtual void Enter(params object[] args)
		{
			this.SetIsEnabled(true, false);
			this.parentHFSM.currentSubDirectState = this;
		}


		public virtual void Exit(params object[] args)
		{
			this.SetIsEnabled(false, false);
			this.parentHFSM.currentSubDirectState = null;
		}

		public virtual void ExitLoopTo(HFSM toHFSM, params object[] args)
		{
			this.Exit();
			var hfsm = parentHFSM;
			while (hfsm != toHFSM)
			{
				hfsm.Exit(args);
				hfsm = hfsm.parentHFSM;
			}
		}

		#endregion


		public override bool IsCanUpdate()
		{
			return isEnabled && base.IsCanUpdate();
		}

		public List<HFSM> GetParentHFSMList()
		{
			List<HFSM> list = new List<HFSM>();
			var hfsm = parentHFSM;
			list.Add(hfsm);
			list.AddRange(hfsm.GetParentHFSMList());
			return list;
		}

		//两个state最靠近的父hfsm
		public HFSM GetNearestSameParentHFSM(HFSMState state2)
		{
			if (state2 == null)
				return this.GetRootHFSM();
			List<HFSM> hfsmList1 = GetParentHFSMList();
			List<HFSM> hfsmList2 = state2.GetParentHFSMList();

			List<HFSM> hfsmDeeperList;
			Dictionary<HFSM, bool> hfsmDict = new Dictionary<HFSM, bool>();
			hfsmDeeperList = hfsmList1.Count > hfsmList2.Count ? hfsmList1 : hfsmList2;
			var hfsmLowerList = hfsmDeeperList == hfsmList1 ? hfsmList2 : hfsmList1;
			foreach (var hfsm in hfsmLowerList)
				hfsmDict[hfsm] = true;

			for (var i = 0; i < hfsmDeeperList.Count; i++)
			{
				var hfsm = hfsmDeeperList[i];
				if (hfsmDict.ContainsKey(hfsm))
					return hfsm;
			}

			return null;
		}


		public HFSM GetRootHFSM()
		{
			return this.parentHFSM.GetRootHFSM();
		}

		public void ChangeToState(string key, bool isForce = false, params object[] args)
		{
			this.GetRootHFSM().ChangeToState(key, isForce, args);
		}

		public void ChangeToState(HFSMState toState, bool isForce = false, params object[] args)
		{
			this.GetRootHFSM().ChangeToState(toState, isForce, args);
		}

		public void ChangeToHFSM(string key, bool isForce = false, params object[] args)
		{
			this.GetRootHFSM().ChangeToHFSM(key, isForce, args);
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