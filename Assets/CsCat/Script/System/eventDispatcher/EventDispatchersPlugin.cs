using System;
using System.Collections.Generic;

namespace CsCat
{
  public class EventDispatchersPlugin
  {
    private Dictionary<EventListenerInfoBase, KeyValuePairCat<Action, int>> listener_dict =
      new Dictionary<EventListenerInfoBase, KeyValuePairCat<Action, int>>(); //callback是移除的callback, int表示需要删除的次数

    private EventDispatchers eventDispatchers;

    public EventDispatchersPlugin(EventDispatchers eventDispatchers)
    {
      this.eventDispatchers = eventDispatchers;
    }

    public EventListenerInfo AddListener(string eventName, Action handler)
    {
      return AddListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public EventListenerInfo AddListener(EventName eventName, Action handler)
    {
      var eventListenerInfo = eventDispatchers.AddListener(eventName, handler);
      listener_dict.GetOrAddDefault(eventListenerInfo,
        () => PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, int>>().Init(() => RemoveListener(eventName, handler), 0));
      listener_dict[eventListenerInfo].value += 1;//个数加1
      return eventListenerInfo;
    }

    public bool RemoveListener(string eventName, Action handler)
    {
      return RemoveListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public bool RemoveListener(EventName eventName, Action handler)
    {
      return RemoveListener(PoolCatManagerUtil.Spawn<EventListenerInfo>().Init(eventName, handler));
    }

    public bool RemoveListener(EventListenerInfo eventListenerInfo)
    {
      if (!this.listener_dict.ContainsKey(eventListenerInfo))
        return false;
      var kv = listener_dict[eventListenerInfo];
      if (kv.value <= 0)
        return false;
      kv.value--;
      if (kv.value == 0)
      {
        var to_despawn = listener_dict.FindKey(eventListenerInfo);
        this.listener_dict.Remove(eventListenerInfo);
        to_despawn.Despawn();
      }

      return eventDispatchers.RemoveListener(eventListenerInfo);
    }

    public bool RemoveListener(Action handler)
    {
      EventListenerInfo listener = null;
      foreach (var key in this.listener_dict.Keys)
      {
        if (key.GetType() == typeof(EventListenerInfo) && key._handler.Equals(handler))
        {
          listener = PoolCatManagerUtil.Spawn<EventListenerInfo>().Init(key.eventName, handler);
          break;
        }
      }

      if (listener == null)
        return false;
      return RemoveListener(listener);
    }

    public void Broadcast(string eventName)
    {
      Broadcast(eventName.ToEventName(eventDispatchers.source));
    }

    public void Broadcast(EventName eventName)
    {
      eventDispatchers.Broadcast(eventName);
    }



    public EventListenerInfo<P0> AddListener<P0>(string eventName, Action<P0> handler)
    {
      return AddListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public EventListenerInfo<P0> AddListener<P0>(EventName eventName, Action<P0> handler)
    {
      var eventListenerInfo = eventDispatchers.AddListener(eventName, handler);
      listener_dict.GetOrAddDefault(eventListenerInfo,
        () => PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, int>>().Init(() => RemoveListener(eventName, handler), 0));
      listener_dict[eventListenerInfo].value += 1;//个数加1
      return eventListenerInfo;
    }

    public bool RemoveListener<P0>(string eventName, Action<P0> handler)
    {
      return RemoveListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public bool RemoveListener<P0>(EventName eventName, Action<P0> handler)
    {
      return RemoveListener(PoolCatManagerUtil.Spawn<EventListenerInfo<P0>>().Init(eventName, handler));
    }

    public bool RemoveListener<P0>(EventListenerInfo<P0> eventListenerInfo)
    {
      if (!this.listener_dict.ContainsKey(eventListenerInfo))
        return false;
      var kv = listener_dict[eventListenerInfo];
      if (kv.value <= 0)
        return false;
      kv.value--;
      if (kv.value == 0)
      {
        var to_despawn = listener_dict.FindKey(eventListenerInfo);
        this.listener_dict.Remove(eventListenerInfo);
        to_despawn.Despawn();
      }

      return eventDispatchers.RemoveListener(eventListenerInfo);
    }

    public bool RemoveListener<P0>(Action<P0> handler)
    {
      EventListenerInfo<P0> listener = null;
      foreach (var key in this.listener_dict.Keys)
      {
        if (key.GetType() == typeof(EventListenerInfo<P0>) && key._handler.Equals(handler))
        {
          listener = PoolCatManagerUtil.Spawn<EventListenerInfo<P0>>().Init(key.eventName, handler);
          break;
        }
      }

      if (listener == null)
        return false;
      return RemoveListener(listener);
    }

    public void Broadcast<P0>(string eventName, P0 p0)
    {
      Broadcast(eventName.ToEventName(eventDispatchers.source), p0);
    }

    public void Broadcast<P0>(EventName eventName, P0 p0)
    {
      eventDispatchers.Broadcast(eventName, p0);
    }



    public EventListenerInfo<P0, P1> AddListener<P0, P1>(string eventName, Action<P0, P1> handler)
    {
      return AddListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public EventListenerInfo<P0, P1> AddListener<P0, P1>(EventName eventName, Action<P0, P1> handler)
    {
      var eventListenerInfo = eventDispatchers.AddListener(eventName, handler);
      listener_dict.GetOrAddDefault(eventListenerInfo,
        () => PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, int>>().Init(() => RemoveListener(eventName, handler), 0));
      listener_dict[eventListenerInfo].value += 1;//个数加1
      return eventListenerInfo;
    }

    public bool RemoveListener<P0, P1>(string eventName, Action<P0, P1> handler)
    {
      return RemoveListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public bool RemoveListener<P0, P1>(EventName eventName, Action<P0, P1> handler)
    {
      return RemoveListener(PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1>>().Init(eventName, handler));
    }

    public bool RemoveListener<P0, P1>(EventListenerInfo<P0, P1> eventListenerInfo)
    {
      if (!this.listener_dict.ContainsKey(eventListenerInfo))
        return false;
      var kv = listener_dict[eventListenerInfo];
      if (kv.value <= 0)
        return false;
      kv.value--;
      if (kv.value == 0)
      {
        var to_despawn = listener_dict.FindKey(eventListenerInfo);
        this.listener_dict.Remove(eventListenerInfo);
        to_despawn.Despawn();
      }

      eventDispatchers.RemoveListener(eventListenerInfo);
      return true;
    }

    public bool RemoveListener<P0, P1>(Action<P0, P1> handler)
    {
      EventListenerInfo<P0, P1> listener = null;
      foreach (var key in this.listener_dict.Keys)
      {
        if (key.GetType() == typeof(EventListenerInfo<P0, P1>) && key._handler.Equals(handler))
        {
          listener = PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1>>().Init(key.eventName, handler);
          break;
        }
      }

      if (listener == null)
        return false;
      return RemoveListener(listener);
    }

    public void Broadcast<P0, P1>(string eventName, P0 p0, P1 p1)
    {
      Broadcast(eventName.ToEventName(eventDispatchers.source), p0, p1);
    }

    public void Broadcast<P0, P1>(EventName eventName, P0 p0, P1 p1)
    {
      eventDispatchers.Broadcast(eventName, p0, p1);
    }



    public EventListenerInfo<P0, P1, P2> AddListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
    {
      return AddListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public EventListenerInfo<P0, P1, P2> AddListener<P0, P1, P2>(EventName eventName, Action<P0, P1, P2> handler)
    {
      var eventListenerInfo = eventDispatchers.AddListener(eventName, handler);
      listener_dict.GetOrAddDefault(eventListenerInfo,
        () => PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, int>>().Init(() => RemoveListener(eventName, handler), 0));
      listener_dict[eventListenerInfo].value += 1;//个数加1
      return eventListenerInfo;
    }

    public bool RemoveListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
    {
      return RemoveListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public bool RemoveListener<P0, P1, P2>(EventName eventName, Action<P0, P1, P2> handler)
    {
      return RemoveListener(PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2>>().Init(eventName, handler));
    }

    public bool RemoveListener<P0, P1, P2>(EventListenerInfo<P0, P1, P2> eventListenerInfo)
    {
      if (!this.listener_dict.ContainsKey(eventListenerInfo))
        return false;
      var kv = listener_dict[eventListenerInfo];
      if (kv.value <= 0)
        return false;
      kv.value--;
      if (kv.value == 0)
      {
        var to_despawn = listener_dict.FindKey(eventListenerInfo);
        this.listener_dict.Remove(eventListenerInfo);
        to_despawn.Despawn();
      }

      return eventDispatchers.RemoveListener(eventListenerInfo);
    }

    public bool RemoveListener<P0, P1, P2>(Action<P0, P1, P2> handler)
    {
      EventListenerInfo<P0, P1, P2> listener = null;
      foreach (var key in this.listener_dict.Keys)
      {
        if (key.GetType() == typeof(EventListenerInfo<P0, P1, P2>) && key._handler.Equals(handler))
        {
          listener = PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2>>().Init(key.eventName, handler);
          break;
        }
      }

      if (listener == null)
        return false;
      return RemoveListener(listener);
    }

    public void Broadcast<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
    {
      Broadcast(eventName.ToEventName(eventDispatchers.source), p0, p1, p2);
    }

    public void Broadcast<P0, P1, P2>(EventName eventName, P0 p0, P1 p1, P2 p2)
    {
      eventDispatchers.Broadcast(eventName, p0, p1, p2);
    }



    public EventListenerInfo<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(string eventName,
      Action<P0, P1, P2, P3> handler)
    {
      return AddListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public EventListenerInfo<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(EventName eventName,
      Action<P0, P1, P2, P3> handler)
    {
      var eventListenerInfo = eventDispatchers.AddListener(eventName, handler);
      listener_dict.GetOrAddDefault(eventListenerInfo,
        () => PoolCatManagerUtil.Spawn<KeyValuePairCat<Action, int>>().Init(() => RemoveListener(eventName, handler), 0));
      listener_dict[eventListenerInfo].value += 1;//个数加1
      return eventListenerInfo;
    }

    public bool RemoveListener<P0, P1, P2, P3>(string eventName,
      Action<P0, P1, P2, P3> handler)
    {
      return RemoveListener(eventName.ToEventName(eventDispatchers.source), handler);
    }

    public bool RemoveListener<P0, P1, P2, P3>(EventName eventName,
      Action<P0, P1, P2, P3> handler)
    {
      return RemoveListener(PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2, P3>>().Init(eventName, handler));
    }

    public bool RemoveListener<P0, P1, P2, P3>(
      EventListenerInfo<P0, P1, P2, P3> eventListenerInfo)
    {
      if (!this.listener_dict.ContainsKey(eventListenerInfo))
        return false;
      var kv = listener_dict[eventListenerInfo];
      if (kv.value <= 0)
        return false;
      kv.value--;
      if (kv.value == 0)
      {
        var to_despawn = listener_dict.FindKey(eventListenerInfo);
        this.listener_dict.Remove(eventListenerInfo);
        to_despawn.Despawn();
      }

      return eventDispatchers.RemoveListener(eventListenerInfo);
    }

    public bool RemoveListener<P0, P1, P2, P3>(Action<P0, P1, P2, P3> handler)
    {
      EventListenerInfo<P0, P1, P2, P3> listener = null;
      foreach (var key in this.listener_dict.Keys)
      {
        if (key.GetType() == typeof(EventListenerInfo<P0, P1, P2, P3>) && key._handler.Equals(handler))
        {
          listener = PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2, P3>>().Init(key.eventName, handler);
          break;
        }
      }

      if (listener == null)
        return false;
      return RemoveListener(listener);
    }

    public void Broadcast<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
    {
      Broadcast(eventName.ToEventName(eventDispatchers.source), p0, p1, p2, p3);
    }

    public void Broadcast<P0, P1, P2, P3>(EventName eventName, P0 p0, P1 p1, P2 p2, P3 p3)
    {
      eventDispatchers.Broadcast(eventName, p0, p1, p2, p3);
    }

    public void RemoveAllListeners()
    {
      List<KeyValuePairCat<Action, int>> list = PoolCatManagerUtil.Spawn<List<KeyValuePairCat<Action, int>>>();
      list.AddRange(listener_dict.Values);
      foreach (var kv in list)
      {
        Action remove_callback = kv.key;
        int remove_count = kv.value;
        for (int i = 0; i < remove_count; i++)
          remove_callback();
        kv.Despawn();
      }
      list.Despawn();
      listener_dict.Clear();
    }


    public void Destroy()
    {
      RemoveAllListeners();
    }
  }
}