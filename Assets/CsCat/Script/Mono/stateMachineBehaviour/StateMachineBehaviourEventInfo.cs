using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public class StateMachineBehaviourEventInfo
	{
		public List<ValueParse> arg_list = new List<ValueParse>();
		public StateMachineBehaviourEventName eventName;
		[HideInInspector] public bool is_triggered;
		public bool is_trigger_on_exit;
		[Range(0f, 1f)] public float normalized_time;
	}
}