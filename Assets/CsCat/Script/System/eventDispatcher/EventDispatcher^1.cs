using System;
using System.Collections.Generic;

namespace CsCat
{
  public class EventDispatcher<P0> : IEventDispatcher
  {

    public ValueListDictionary<EventName, KeyValuePairCat<Action<P0>, bool>> listener_dict =
      new ValueListDictionary<EventName, KeyValuePairCat<Action<P0>, bool>>();


    public EventListenerInfo<P0> AddListener(string eventName, Action<P0> handler)
    {
      var _eventName = eventName.ToEventName();
      var result = AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo<P0> AddListener(EventName eventName, Action<P0> handler)
    {
      var handler_info = PoolCatManagerUtil.Spawn<KeyValuePairCat<Action<P0>, bool>>().Init(handler, true);
      listener_dict.Add(eventName.Clone(), handler_info);
      return PoolCatManagerUtil.Spawn<EventListenerInfo<P0>>().Init(eventName.Clone(), handler);
    }


    public bool RemoveListener(string eventName, Action<P0> handler)
    {
      var _eventName = eventName.ToEventName();
      var result = RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener(EventListenerInfo<P0> eventListenerInfo)
    {
      return RemoveListener(eventListenerInfo.eventName, eventListenerInfo.handler);
    }

    public bool RemoveListener(EventName eventName, Action<P0> handler)
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

    public bool RemoveListener(Action<P0> handler)
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
        eventName.OnDespawn();
      }
      listener_dict.Clear();
    }


    public void Broadcast(string eventName, P0 p0)
    {
      Broadcast(eventName.ToEventName(), p0);
    }

    public void Broadcast(EventName eventName, P0 p0)
    {
      if (listener_dict.ContainsKey(eventName))
      {
        var handler_info_list = PoolCatManagerUtil.Spawn<List<KeyValuePairCat<Action<P0>, bool>>>();
        handler_info_list.AddRange(listener_dict[eventName]);
        foreach (var handler_info in handler_info_list)
          try
          {
            if (handler_info.value)
              handler_info.key(p0);
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