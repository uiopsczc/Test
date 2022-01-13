using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class StateMachineBehaviourEvents : StateMachineBehaviour
	{
		[SerializeField]
		public List<StateMachineBehaviourEventInfo> eventInfoList = new List<StateMachineBehaviourEventInfo>();

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			for (var i = 0; i < eventInfoList.Count; i++)
			{
				var eventInfo = eventInfoList[i];
				eventInfo.isTriggered = false;
			}
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (Application.isPlaying)
				for (var i = 0; i < eventInfoList.Count; i++)
				{
					var eventInfo = eventInfoList[i];
					if (!eventInfo.isTriggerOnExit && !eventInfo.isTriggered &&
					    stateInfo.normalizedTime >= eventInfo.normalizedTime)
					{
						eventInfo.isTriggered = true;
						//            Client.instance.eventDispatchers.Broadcast<StateMachineBehaviourEventName, List<ValueParse>>(
						//              animator.GetEventName(),
						//              eventInfo.eventName, eventInfo.arg_list);
					}
				}
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (Application.isPlaying)
				for (var i = 0; i < eventInfoList.Count; i++)
				{
					var eventInfo = eventInfoList[i];
					if (eventInfo.isTriggerOnExit)
					{
						eventInfo.isTriggered = true;
						//            Client.instance.eventDispatchers.Broadcast<StateMachineBehaviourEventName, List<ValueParse>>(
						//              animator.GetEventName(),
						//              eventInfo.eventName, eventInfo.arg_list);
					}
				}
		}
	}
}