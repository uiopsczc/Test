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
		public HFSM parent_hfsm;

		protected HFSMState previous_state;

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
			if (default_sub_direct_state != null)
				ChangeToState(default_sub_direct_state);
			if (default_sub_direct_hfsm != null)
				ChangeToHFSM(default_sub_direct_hfsm.key);
		}

		public T GetOwner<T>() where T : GameEntity
		{
			return this.GetRootHFSM().owner as T;
		}

		public virtual void Enter(params object[] args)
		{
			this.SetIsEnabled(true, false);
			if (parent_hfsm != null)
				parent_hfsm.current_sub_direct_hfsm = this;

		}

		public virtual void Exit(params object[] args)
		{
			this.SetIsEnabled(false, false);
			if (parent_hfsm != null)
				parent_hfsm.current_sub_direct_hfsm = null;
		}

		public virtual void EnterLoopTo(HFSMState to_state, params object[] args)
		{
			var hfsm_list = new List<HFSM>();//倒序
			var _hfsm = to_state.parent_hfsm;
			while (_hfsm != this)
			{
				hfsm_list.Add(_hfsm);
				_hfsm = _hfsm.parent_hfsm;
			}

			for (int i = hfsm_list.Count - 1; i >= 0; i--)
				hfsm_list[i].Enter(args);
			to_state.Enter(args);
		}

		//////////////////////////////////////////////////////////////////////
		// 
		//////////////////////////////////////////////////////////////////////

		public HFSMState GetCurrentState()
		{
			if (this.current_sub_direct_state != null)
				return this.current_sub_direct_state;
			else if (this.current_sub_direct_hfsm != null)
				return this.current_sub_direct_hfsm.GetCurrentState();
			return null;
		}

		/// <summary>
		/// 获取上一次状态
		/// </summary>
		/// <returns>状态</returns>
		public HFSMState GetPreviousState()
		{
			return this.previous_state;
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
			HFSM root_hfsm = this.cache.GetOrAddDefault("root_hfsm", () =>
			{
				HFSM _root_hfsm = this;
				while (_root_hfsm.parent_hfsm != null)
					_root_hfsm = _root_hfsm.parent_hfsm;
				return _root_hfsm;
			});
			return root_hfsm;
		}

		public List<HFSM> GetParentHFSMList()
		{
			List<HFSM> list = new List<HFSM>();
			var _hfsm = parent_hfsm;
			while (_hfsm != null)
			{
				list.Add(_hfsm);
				_hfsm = _hfsm.parent_hfsm;
			}

			return list;
		}

	}
}