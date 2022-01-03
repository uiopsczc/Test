using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class HFSM
	{
		/// <summary>
		/// 所有的直接子状态
		/// </summary>
		public Dictionary<string, HFSMState> sub_direct_state_dict = new Dictionary<string, HFSMState>();

		/// <summary>
		/// 默认的状态，必须是直接subStates
		/// </summary>
		public HFSMState default_sub_direct_state;

		/// <summary>
		/// 当前状态，必须是直接subStates
		/// </summary>
		public HFSMState current_sub_direct_state;

		//////////////////////////////////////////////////////////////////////
		// Add
		//////////////////////////////////////////////////////////////////////
		public HFSMState AddSubDirectStateWithoutInit(string key, Type sub_direct_state_type)
		{
			HFSMState sub_direct_state = base.AddChildWithoutInit(key, sub_direct_state_type) as HFSMState;
			sub_direct_state.parent_hfsm = this;
			this.sub_direct_state_dict[sub_direct_state.key] = sub_direct_state;
			return sub_direct_state;
		}

		public T AddSubDirectStateWithoutInit<T>(string key) where T : HFSMState
		{
			return AddSubDirectStateWithoutInit(key, typeof(T)) as T;
		}

		public HFSMState AddSubDirectState(string key, Type sub_direct_state_type, params object[] init_args)
		{
			HFSMState sub_direct_state = AddSubDirectStateWithoutInit(key, sub_direct_state_type);
			sub_direct_state.InvokeMethod("Init", true, init_args);
			sub_direct_state.PostInit();
			return sub_direct_state;
		}

		public T AddSubDirectState<T>(string key, params object[] init_args) where T : HFSMState
		{
			return AddSubDirectState(key, typeof(T), init_args) as T;
		}

		//////////////////////////////////////////////////////////////////////
		// Remove
		//////////////////////////////////////////////////////////////////////
		public void RemoveSubDirectState(string key)
		{
			this.RemoveChild(key);
			this.sub_direct_state_dict[key].parent_hfsm = null;
			this.sub_direct_state_dict.Remove(key);
		}
		//////////////////////////////////////////////////////////////////////
		// Set
		//////////////////////////////////////////////////////////////////////
		public void SetDefaultSubDirectState(string key)
		{
			this.default_sub_direct_state = sub_direct_state_dict[key];
		}

		//////////////////////////////////////////////////////////////////////
		// Get
		//////////////////////////////////////////////////////////////////////
		public HFSMState GetSubState(string key, bool is_loop_sub_hfsm_dict)
		{
			if (this.sub_direct_state_dict.ContainsKey(key))
				return this.sub_direct_state_dict[key];
			if (is_loop_sub_hfsm_dict)
			{
				foreach (var _sub_direct_hfsm in this.sub_direct_hfsm_dict.Values)
				{
					HFSMState instance = _sub_direct_hfsm.GetSubState(key, true);
					if (instance != null)
						return instance;
				}
			}

			return null;
		}

	}
}