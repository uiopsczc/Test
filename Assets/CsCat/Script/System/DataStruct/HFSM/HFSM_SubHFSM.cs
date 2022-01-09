using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class HFSM
	{
		/// <summary>
		/// 所有的直接子状态机
		/// </summary>
		public Dictionary<string, HFSM> sub_direct_hfsm_dict = new Dictionary<string, HFSM>();

		/// <summary>
		/// 默认的状态机，必须是直接sub
		/// </summary>
		public HFSM default_sub_direct_hfsm;

		/// <summary>
		/// 当前状态，必须是直接SubHFSM
		/// </summary>
		public HFSM current_sub_direct_hfsm;
		//////////////////////////////////////////////////////////////////////
		// Add
		//////////////////////////////////////////////////////////////////////
		public HFSM AddSubDirectHFSMWithoutInit(string key, Type sub_direct_hfsm_type)
		{
			HFSM sub_direct_hfsm = base.AddChildWithoutInit(key, sub_direct_hfsm_type) as HFSM;
			sub_direct_hfsm.parent_hfsm = this;
			this.sub_direct_hfsm_dict[sub_direct_hfsm.key] = sub_direct_hfsm;
			return sub_direct_hfsm;
		}

		public T AddSubDirectHFSMWithoutInit<T>(string key) where T : HFSM
		{
			return this.AddSubDirectHFSMWithoutInit(key, typeof(T)) as T;
		}

		public HFSM AddSubDirectHFSM(string key, Type sub_direct_hfsm_type, params object[] init_args)
		{
			HFSM sub_direct_hfsm = AddSubDirectHFSMWithoutInit(key, sub_direct_hfsm_type);
			sub_direct_hfsm.InvokeMethod("Init", true, init_args);
			sub_direct_hfsm.PostInit();
			return sub_direct_hfsm;
		}

		public T AddSubDirectHFSM<T>(string key, params object[] init_args) where T : HFSM
		{
			return this.AddSubDirectHFSM(key, typeof(T), init_args) as T;
		}

		//////////////////////////////////////////////////////////////////////
		// Remove
		//////////////////////////////////////////////////////////////////////
		public void RemoveSubDirectHFSM(string key)
		{
			this.RemoveChild(this.key);
			this.sub_direct_hfsm_dict[key].parent_hfsm = null;
			this.sub_direct_hfsm_dict.Remove(key);
		}
		//////////////////////////////////////////////////////////////////////
		// Set
		//////////////////////////////////////////////////////////////////////
		public void SetDefaultSubDirectHFSM(string key)
		{
			this.default_sub_direct_hfsm = sub_direct_hfsm_dict[key];
		}
		//////////////////////////////////////////////////////////////////////
		// Get
		//////////////////////////////////////////////////////////////////////
		public HFSM GetSubHFSM(string key, bool is_loop_sub_hfsm_dict)
		{
			if (this.sub_direct_hfsm_dict.ContainsKey(key))
				return this.sub_direct_hfsm_dict[key];
			if (is_loop_sub_hfsm_dict)
			{
				foreach (var _sub_direct_hfsm in this.sub_direct_hfsm_dict.Values)
				{
					HFSM instance = _sub_direct_hfsm.GetSubHFSM(key, true);
					if (instance != null)
						return instance;
				}
			}

			return null;
		}

	}
}