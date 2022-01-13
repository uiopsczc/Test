using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public class StateMachineBehaviourEventInfo
	{
		public List<ValueParse> argList = new List<ValueParse>();
		public StateMachineBehaviourEventName eventName;
		[HideInInspector] public bool isTriggered;
		public bool isTriggerOnExit;
		[Range(0f, 1f)] public float normalizedTime;
	}
}