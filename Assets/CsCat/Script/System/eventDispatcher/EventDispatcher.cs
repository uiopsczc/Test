using System;
using System.Collections.Generic;

namespace CsCat
{
  public class EventDispatcher : IEventDispatcher
  {
    public ValueListDictionary<EventName, KeyValuePairCat<Action, bool>> listener_dict =
      new ValueListDictionary<EventName, KeyValuePairCat<Action, bool>>();


    public EventListenerInfo AddListener(string eventName, Action handler)
    {
      var _eventName = eventName.ToEventName();
      var result = AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo AddListener(EventName eventName, Action handler)
    {
      var handler_info = PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, bool>>().Init(handler, true);
      listener_dict.Add(eventName.Clone(), handler_info);
      return PoolCatManagerUtil.Spawn<EventListenerInfo>().Init(eventName.Clone(), handler);
    }

    public bool RemoveListener(string eventName, Action handler)
    {
      var _eventName = eventName.ToEventName();
      var result = RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener(EventListenerInfo eventListenerInfo)
    {
      return RemoveListener(eventListenerInfo.eventName, eventListenerInfo.handler);
    }

    public bool RemoveListener(EventName eventName, Action handler)
    {
      if (!listener_dict.ContainsKey(eventName))
        return false;
      foreach (var handler_info in listener_dict[eventName])
      {
        if (handler_info.value && handler_info.key.Equals(handler))
        {
          handler_info.value = false;
          return true;
        }
      }
      return false;
    }

    public bool RemoveListener(Action handler)
    {
      foreach (var eventName in this.listener_dict.Keys)
      {
        foreach (var handler_info in listener_dict[eventName])
        {
          if (handler_info.value && handler_info.key.Equals(handler))
          {
            handler_info.value = false;
            return true;
          }
        }
      }

      return false;
    }

    public void RemoveAllListeners()
    {
      foreach (var eventName in listener_dict.Keys)
      {
        var value = listener_dict[eventName];
        value.Despawn();
        eventName.Despawn();
      }

      listener_dict.Clear();
    }

    public void Broadcast(string eventName)
    {
      Broadcast(eventName.ToEventName());
    }

    public void Broadcast(EventName eventName)
    {
      if (listener_dict.ContainsKey(eventName))
      {
        var handler_info_list = PoolCatManagerUtil.Spawn<List<KeyValuePairCat<Action, bool>>>();
        handler_info_list.AddRange(listener_dict[eventName]);
        foreach (var handler_info in handler_info_list)
          try
          {
            if (handler_info.value)
              handler_info.key();
          }
          catch (Exception ex)
          {
            LogCat.LogError(ex);
          }

        handler_info_list.Clear();
        handler_info_list.Despawn();
      }

      // check remove
      CheckRemoved();
      CheckEmpty();
    }

    void CheckRemoved()
    {
      foreach (var handler_info_list in listener_dict.Values)
      {
        for (int i = handler_info_list.Count - 1; i >= 0; i--)
        {
          var handler_info = handler_info_list[i];
          if (handler_info.value == false)
          {
            handler_info_list.RemoveAt(i);
            handler_info.Despawn();
          }
        }
      }
    }

    void CheckEmpty()
    {
      var to_remove_eventName_list = PoolCatManagerUtil.Spawn<List<EventName>>(PoolNameConst.EventName_List);
      foreach (var eventName in listener_dict.Keys)
      {
        if (listener_dict[eventName].Count == 0)
          to_remove_eventName_list.Add(eventName);
      }

      foreach (var to_remove_eventName in to_remove_eventName_list)
      {
        listener_dict.Remove(to_remove_eventName);
        to_remove_eventName.Despawn();
      }

      to_remove_eventName_list.Clear();
      to_remove_eventName_list.Despawn();
    }
  }
}