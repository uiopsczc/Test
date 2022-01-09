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
		public Dictionary<string, HFSMState> subDirectStateDict = new Dictionary<string, HFSMState>();

		/// <summary>
		/// 默认的状态，必须是直接subStates
		/// </summary>
		public HFSMState defaultSubDirectState;

		/// <summary>
		/// 当前状态，必须是直接subStates
		/// </summary>
		public HFSMState currentSubDirectState;

		//////////////////////////////////////////////////////////////////////
		// Add
		//////////////////////////////////////////////////////////////////////
		public HFSMState AddSubDirectStateWithoutInit(string key, Type subDirectStateType)
		{
			HFSMState subDirectState = base.AddChildWithoutInit(key, subDirectStateType) as HFSMState;
			subDirectState.parentHFSM = this;
			this.subDirectStateDict[subDirectState.key] = subDirectState;
			return subDirectState;
		}

		public T AddSubDirectStateWithoutInit<T>(string key) where T : HFSMState
		{
			return AddSubDirectStateWithoutInit(key, typeof(T)) as T;
		}

		public HFSMState AddSubDirectState(string key, Type subDirectStateType, params object[] init_args)
		{
			HFSMState subDirectState = AddSubDirectStateWithoutInit(key, subDirectStateType);
			subDirectState.InvokeMethod("Init", true, init_args);
			subDirectState.PostInit();
			return subDirectState;
		}

		public T AddSubDirectState<T>(string key, params object[] initArgs) where T : HFSMState
		{
			return AddSubDirectState(key, typeof(T), initArgs) as T;
		}

		//////////////////////////////////////////////////////////////////////
		// Remove
		//////////////////////////////////////////////////////////////////////
		public void RemoveSubDirectState(string key)
		{
			this.RemoveChild(key);
			this.subDirectStateDict[key].parentHFSM = null;
			this.subDirectStateDict.Remove(key);
		}

		//////////////////////////////////////////////////////////////////////
		// Set
		//////////////////////////////////////////////////////////////////////
		public void SetDefaultSubDirectState(string key)
		{
			this.defaultSubDirectState = subDirectStateDict[key];
		}

		//////////////////////////////////////////////////////////////////////
		// Get
		//////////////////////////////////////////////////////////////////////
		public HFSMState GetSubState(string key, bool isLoopSubHFSMDict)
		{
			if (this.subDirectStateDict.ContainsKey(key))
				return this.subDirectStateDict[key];
			if (isLoopSubHFSMDict)
			{
				foreach (var subDirectHFSM in this.subDirectHFSMDict.Values)
				{
					HFSMState instance = subDirectHFSM.GetSubState(key, true);
					if (instance != null)
						return instance;
				}
			}

			return null;
		}
	}
}