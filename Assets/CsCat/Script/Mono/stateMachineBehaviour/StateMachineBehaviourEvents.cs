using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class StateMachineBehaviourEvents : StateMachineBehaviour
  {
    [SerializeField]
    public List<StateMachineBehaviourEventInfo> eventInfo_list = new List<StateMachineBehaviourEventInfo>();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      foreach (var eventInfo in eventInfo_list)
        eventInfo.is_triggered = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if (Application.isPlaying)
        foreach (var eventInfo in eventInfo_list)
          if (!eventInfo.is_trigger_on_exit && !eventInfo.is_triggered &&
              stateInfo.normalizedTime >= eventInfo.normalized_time)
          {
            eventInfo.is_triggered = true;
            Client.instance.eventDispatchers.Broadcast<StateMachineBehaviourEventName, List<ValueParse>>(
              animator.GetEventName(),
              eventInfo.eventName, eventInfo.arg_list);
          }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if (Application.isPlaying)
        foreach (var eventInfo in eventInfo_list)
          if (eventInfo.is_trigger_on_exit)
          {
            eventInfo.is_triggered = true;
            Client.instance.eventDispatchers.Broadcast<StateMachineBehaviourEventName, List<ValueParse>>(
              animator.GetEventName(),
              eventInfo.eventName, eventInfo.arg_list);
          }
    }
  }
}