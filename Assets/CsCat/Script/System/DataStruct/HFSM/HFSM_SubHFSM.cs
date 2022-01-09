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
		public Dictionary<string, HFSM> subDirectHFSMDict = new Dictionary<string, HFSM>();

		/// <summary>
		/// 默认的状态机，必须是直接sub
		/// </summary>
		public HFSM defaultSubDirectHFSM;

		/// <summary>
		/// 当前状态，必须是直接SubHFSM
		/// </summary>
		public HFSM currentSubDirectHFSM;
		//////////////////////////////////////////////////////////////////////
		// Add
		//////////////////////////////////////////////////////////////////////
		public HFSM AddSubDirectHFSMWithoutInit(string key, Type subDirectHFSMType)
		{
			HFSM subDirectHFSM = base.AddChildWithoutInit(key, subDirectHFSMType) as HFSM;
			subDirectHFSM.parentHFSM = this;
			this.subDirectHFSMDict[subDirectHFSM.key] = subDirectHFSM;
			return subDirectHFSM;
		}

		public T AddSubDirectHFSMWithoutInit<T>(string key) where T : HFSM
		{
			return this.AddSubDirectHFSMWithoutInit(key, typeof(T)) as T;
		}

		public HFSM AddSubDirectHFSM(string key, Type subDirectHFSMType, params object[] initArgs)
		{
			HFSM subDirectHFSM = AddSubDirectHFSMWithoutInit(key, subDirectHFSMType);
			subDirectHFSM.InvokeMethod("Init", true, initArgs);
			subDirectHFSM.PostInit();
			return subDirectHFSM;
		}

		public T AddSubDirectHFSM<T>(string key, params object[] initArgs) where T : HFSM
		{
			return this.AddSubDirectHFSM(key, typeof(T), initArgs) as T;
		}

		//////////////////////////////////////////////////////////////////////
		// Remove
		//////////////////////////////////////////////////////////////////////
		public void RemoveSubDirectHFSM(string key)
		{
			this.RemoveChild(this.key);
			this.subDirectHFSMDict[key].parentHFSM = null;
			this.subDirectHFSMDict.Remove(key);
		}
		//////////////////////////////////////////////////////////////////////
		// Set
		//////////////////////////////////////////////////////////////////////
		public void SetDefaultSubDirectHFSM(string key)
		{
			this.defaultSubDirectHFSM = subDirectHFSMDict[key];
		}
		//////////////////////////////////////////////////////////////////////
		// Get
		//////////////////////////////////////////////////////////////////////
		public HFSM GetSubHFSM(string key, bool isLoopSubHFSMDict)
		{
			if (this.subDirectHFSMDict.ContainsKey(key))
				return this.subDirectHFSMDict[key];
			if (isLoopSubHFSMDict)
			{
				foreach (var subDirectHFSM in this.subDirectHFSMDict.Values)
				{
					HFSM instance = subDirectHFSM.GetSubHFSM(key, true);
					if (instance != null)
						return instance;
				}
			}

			return null;
		}

	}
}