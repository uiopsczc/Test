using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	/// 分层状态机   
	/// </summary>
	public partial class HFSM : TickObject
	{
		/// <summary>
		/// 父节点
		/// </summary>
		public HFSM parentHFSM;

		protected HFSMState previousState;

		public GameEntity owner;

		public override TimerManager timerManager => GetOwner<GameEntity>().timerManager;


		public HFSM()
		{
		}

		public HFSM(GameEntity owner)
		{
			this.owner = owner;
		}

		public override void Init()
		{
			base.Init();
			this.InitStates();
		}

		public virtual void InitStates()
		{
		}



		public override void Start()
		{
			base.Start();
			if (defaultSubDirectState != null)
				ChangeToState(defaultSubDirectState);
			if (defaultSubDirectHFSM != null)
				ChangeToHFSM(defaultSubDirectHFSM.key);
		}

		public T GetOwner<T>() where T : GameEntity
		{
			return this.GetRootHFSM().owner as T;
		}

		public virtual void Enter(params object[] args)
		{
			this.SetIsEnabled(true, false);
			if (parentHFSM != null)
				parentHFSM.currentSubDirectHFSM = this;

		}

		public virtual void Exit(params object[] args)
		{
			this.SetIsEnabled(false, false);
			if (parentHFSM != null)
				parentHFSM.currentSubDirectHFSM = null;
		}

		public virtual void EnterLoopTo(HFSMState toState, params object[] args)
		{
			var hfsmList = new List<HFSM>();//倒序
			var hfsm = toState.parentHFSM;
			while (hfsm != this)
			{
				hfsmList.Add(hfsm);
				hfsm = hfsm.parentHFSM;
			}

			for (int i = hfsmList.Count - 1; i >= 0; i--)
				hfsmList[i].Enter(args);
			toState.Enter(args);
		}

		//////////////////////////////////////////////////////////////////////
		// 
		//////////////////////////////////////////////////////////////////////

		public HFSMState GetCurrentState()
		{
			if (this.currentSubDirectState != null)
				return this.currentSubDirectState;
			else if (this.currentSubDirectHFSM != null)
				return this.currentSubDirectHFSM.GetCurrentState();
			return null;
		}

		/// <summary>
		/// 获取上一次状态
		/// </summary>
		/// <returns>状态</returns>
		public HFSMState GetPreviousState()
		{
			return this.previousState;
		}

		/// <summary>
		/// 回到上一个状态
		/// </summary>
		public void RevertToPreviousState()
		{
			if (this.GetPreviousState() != null)
				this.ChangeToState(this.GetPreviousState());
		}

		public HFSM GetRootHFSM()
		{
			HFSM rootHFSM = this._cache.GetOrAddDefault("root_hfsm", () =>
			{
				HFSM hfsm = this;
				while (hfsm.parentHFSM != null)
					hfsm = hfsm.parentHFSM;
				return hfsm;
			});
			return rootHFSM;
		}

		public List<HFSM> GetParentHFSMList()
		{
			List<HFSM> list = new List<HFSM>();
			var hfsm = parentHFSM;
			while (hfsm != null)
			{
				list.Add(hfsm);
				hfsm = hfsm.parentHFSM;
			}

			return list;
		}

	}
}