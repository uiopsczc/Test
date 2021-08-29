using System;
using System.Collections.Generic;

namespace CsCat
{
  public class EventDispatchers
  {
    private const string EventDispatcher_Name = "EventDispatcher";
    private const string Listeners_Name = "Listeners";

    

    public readonly IEventSource source;

    private Cache cache = new Cache();

    public EventDispatchers(IEventSource source)
    {
      this.source = source;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    private EventDispatcher GetEventDispatcher()
    {
      var args = EventDispatcher_Name;
      if (cache.ContainsKey(EventDispatcher_Name))
      {
        var result = cache.Get<EventDispatcher>(EventDispatcher_Name);
        return result;
      }
      else
      {
        var result = new EventDispatcher();
        cache[args] = result;
        return result;
      }
    }

    private EventDispatcher<P0> GetEventDispatcher<P0>()
    {
      var args = (EventDispatcher_Name, typeof(P0));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<EventDispatcher<P0>>(args);
        return result;
      }
      else
      {
        var result = new EventDispatcher<P0>();
        cache[args] = result;
        return result;
      }
    }

    private EventDispatcher<P0, P1> GetEventDispatcher<P0, P1>()
    {
      var args = (EventDispatcher_Name, typeof(P0),typeof(P1));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<EventDispatcher<P0, P1>>(args);
        return result;
      }
      else
      {
        var result = new EventDispatcher<P0, P1>();
        cache[args] = result;
        return result;
      }
    }

    private EventDispatcher<P0, P1, P2> GetEventDispatcher<P0, P1, P2>()
    {
      var args = (EventDispatcher_Name, typeof(P0), typeof(P1), typeof(P2));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<EventDispatcher<P0, P1, P2>>(args);
        return result;
      }
      else
      {
        var result = new EventDispatcher<P0, P1, P2>();
        cache[args] = result;
        return result;
      }
    }

    private EventDispatcher<P0, P1, P2, P3> GetEventDispatcher<P0, P1, P2, P3>()
    {
      var args = (EventDispatcher_Name, typeof(P0), typeof(P1), typeof(P2), typeof(P3));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<EventDispatcher<P0, P1, P2, P3>>(args);
        return result;
      }
      else
      {
        var result = new EventDispatcher<P0, P1, P2, P3>();
        cache[args] = result;
        return result;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    private List<EventListenerInfo> GetListeners()
    {
      var args = Listeners_Name;
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<List<EventListenerInfo>>(args);
        args.Despawn();
        return result;
      }
      else
      {
        var result = new List<EventListenerInfo>();
        cache[args] = result;
        return result;
      }
    }

    private List<EventListenerInfo<P0>> GetListeners<P0>()
    {
      var args = (Listeners_Name, typeof(P0));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<List<EventListenerInfo<P0>>>(args);
        args.Despawn();
        return result;
      }
      else
      {
        var result = new List<EventListenerInfo<P0>>();
        cache[args] = result;
        return result;
      }
    }

    private List<EventListenerInfo<P0, P1>> GetListeners<P0, P1>()
    {
      var args = (Listeners_Name, typeof(P0), typeof(P1));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<List<EventListenerInfo<P0, P1>>>(args);
        args.Despawn();
        return result;
      }
      else
      {
        var result = new List<EventListenerInfo<P0, P1>>();
        cache[args] = result;
        return result;
      }
    }

    private List<EventListenerInfo<P0, P1, P2>> GetListeners<P0, P1, P2>()
    {
      var args = (Listeners_Name, typeof(P0), typeof(P1),typeof(P2));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<List<EventListenerInfo<P0, P1, P2>>>(args);
        args.Despawn();
        return result;
      }
      else
      {
        var result = new List<EventListenerInfo<P0, P1, P2>>();
        cache[args] = result;
        return result;
      }
    }

    private List<EventListenerInfo<P0, P1, P2, P3>> GetListeners<P0, P1, P2, P3>()
    {
      var args = (Listeners_Name, typeof(P0), typeof(P1), typeof(P2), typeof(P3));
      if (cache.ContainsKey(args))
      {
        var result = cache.Get<List<EventListenerInfo<P0, P1, P2, P3>>>(args);
        args.Despawn();
        return result;
      }
      else
      {
        var result = new List<EventListenerInfo<P0, P1, P2, P3>>();
        cache[args] = result;
        return result;
      }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    public EventListenerInfo AddListener(string eventName, Action handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result =  AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo AddListener(EventName eventName, Action handler)
    {
      var eventListenerInfo = GetEventDispatcher().AddListener(eventName, handler);
      GetListeners().Add(eventListenerInfo);
      return eventListenerInfo;
    }

    public bool RemoveListener(string eventName, Action handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result =  RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener(EventName eventName, Action handler)
    {
      var _eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo>().Init(eventName.Clone(), handler);
      var result =  RemoveListener(_eventListenerInfo);
      _eventListenerInfo.Despawn();
      return result;
    }

    public bool RemoveListener(EventListenerInfo eventListenerInfo)
    {
      var listeners = GetListeners();
      int index = listeners.IndexOf(eventListenerInfo);
      if (index != -1)
      {
        var to_despawn = listeners[index];
        listeners.RemoveAt(index);
        try
        {
          if (GetEventDispatcher().RemoveListener(eventListenerInfo))
            return true;
        }
        finally
        {
          to_despawn.Despawn();
        }
        
      }

      return false;
    }

    public bool RemoveListener(Action handler)
    {
      var listeners = GetListeners();
      EventListenerInfo eventListenerInfo = null;
      foreach (var element in listeners)
      {
        if (element.handler.Equals(handler))
        {
          eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo>().Init(element.eventName.Clone(), element.handler);
          break;
        }
      }

      if (eventListenerInfo == null)
        return false;
      var result =  RemoveListener(eventListenerInfo);
      eventListenerInfo.Despawn();
      return result;
    }


    public void Broadcast(string eventName)
    {
      var _eventName = eventName.ToEventName(source);
      Broadcast(_eventName);
      _eventName.Despawn();
    }

    public void Broadcast(EventName eventName)
    {
      GetEventDispatcher().Broadcast(eventName);
    }



    public EventListenerInfo<P0> AddListener<P0>(string eventName, Action<P0> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo<P0> AddListener<P0>(EventName eventName, Action<P0> handler)
    {
      var eventListenerInfo = GetEventDispatcher<P0>().AddListener(eventName, handler);
      GetListeners<P0>().Add(eventListenerInfo);
      return eventListenerInfo;
    }

    public bool RemoveListener<P0>(string eventName, Action<P0> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener<P0>(EventName eventName, Action<P0> handler)
    {
      var _eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0>>().Init(eventName.Clone(), handler);
      var result = RemoveListener(_eventListenerInfo);
      _eventListenerInfo.Despawn();
      return result;
    }

    public bool RemoveListener<P0>(EventListenerInfo<P0> eventListenerInfo)
    {
      var listeners = GetListeners<P0>();
      int index = listeners.IndexOf(eventListenerInfo);
      if (index != -1)
      {
        var to_despawn = listeners[index];
        listeners.RemoveAt(index);
        try
        {
          if (GetEventDispatcher<P0>().RemoveListener(eventListenerInfo))
            return true;
        }
        finally
        {
          to_despawn.Despawn();
        }
      }

      return false;
    }

    public bool RemoveListener<P0>(Action<P0> handler)
    {
      var listeners = GetListeners<P0>();
      EventListenerInfo<P0> eventListenerInfo = null;
      foreach (var element in listeners)
      {
        if (element.handler.Equals(handler))
        {
          eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0>>().Init(element.eventName.Clone(), element.handler);
          break;
        }
      }

      if (eventListenerInfo == null)
        return false;
      var result = RemoveListener(eventListenerInfo);
      eventListenerInfo.Despawn();
      return result;
    }


    public void Broadcast<P0>(string eventName, P0 p0)
    {
      var _eventName = eventName.ToEventName(source);
      Broadcast(_eventName,p0);
      _eventName.Despawn();
    }

    public void Broadcast<P0>(EventName eventName, P0 p0)
    {
      GetEventDispatcher<P0>().Broadcast(eventName, p0);
    }



    public EventListenerInfo<P0, P1> AddListener<P0, P1>(string eventName, Action<P0, P1> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo<P0, P1> AddListener<P0, P1>(EventName eventName, Action<P0, P1> handler)
    {
      var eventListenerInfo = GetEventDispatcher<P0,P1>().AddListener(eventName, handler);
      GetListeners<P0,P1>().Add(eventListenerInfo);
      return eventListenerInfo;
    }

    public bool RemoveListener<P0, P1>(string eventName, Action<P0, P1> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener<P0, P1>(EventName eventName, Action<P0, P1> handler)
    {
      var _eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0,P1>>().Init(eventName.Clone(), handler);
      var result = RemoveListener(_eventListenerInfo);
      _eventListenerInfo.Despawn();
      return result;
    }

    public bool RemoveListener<P0, P1>(EventListenerInfo<P0, P1> eventListenerInfo)
    {
      var listeners = GetListeners<P0,P1>();
      int index = listeners.IndexOf(eventListenerInfo);
      if (index != -1)
      {
        var to_despawn = listeners[index];
        listeners.RemoveAt(index);
        try
        {
          if (GetEventDispatcher<P0,P1>().RemoveListener(eventListenerInfo))
            return true;
        }
        finally
        {
          to_despawn.Despawn();
        }
      }

      return false;
    }

    public bool RemoveListener<P0, P1>(Action<P0, P1> handler)
    {
      var listeners = GetListeners<P0,P1>();
      EventListenerInfo<P0,P1> eventListenerInfo = null;
      foreach (var element in listeners)
      {
        if (element.handler.Equals(handler))
        {
          eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0,P1>>().Init(element.eventName.Clone(), element.handler);
          break;
        }
      }

      if (eventListenerInfo == null)
        return false;
      var result = RemoveListener(eventListenerInfo);
      eventListenerInfo.Despawn();
      return result;
    }

    public void Broadcast<P0, P1>(string eventName, P0 p0, P1 p1)
    {
      var _eventName = eventName.ToEventName(source);
      Broadcast(_eventName,p0,p1);
      _eventName.Despawn();
    }

    public void Broadcast<P0, P1>(EventName eventName, P0 p0, P1 p1)
    {
      GetEventDispatcher<P0, P1>().Broadcast(eventName, p0, p1);
    }



    public EventListenerInfo<P0, P1, P2> AddListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo<P0, P1, P2> AddListener<P0, P1, P2>(EventName eventName, Action<P0, P1, P2> handler)
    {
      var eventListenerInfo = GetEventDispatcher<P0,P1,P2>().AddListener(eventName, handler);
      GetListeners<P0,P1,P2>().Add(eventListenerInfo);
      return eventListenerInfo;
    }

    public bool RemoveListener<P0, P1, P2>(string eventName, Action<P0, P1, P2> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener<P0, P1, P2>(EventName eventName, Action<P0, P1, P2> handler)
    {
      var _eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0,P1,P2>>().Init(eventName.Clone(), handler);
      var result = RemoveListener(_eventListenerInfo);
      _eventListenerInfo.Despawn();
      return result;
    }

    public bool RemoveListener<P0, P1, P2>(EventListenerInfo<P0, P1, P2> eventListenerInfo)
    {
      var listeners = GetListeners<P0, P1, P2>();
      int index = listeners.IndexOf(eventListenerInfo);
      if (index != -1)
      {
        var to_despawn = listeners[index];
        listeners.RemoveAt(index);
        try
        {
          if (GetEventDispatcher<P0,P1,P2>().RemoveListener(eventListenerInfo))
            return true;
        }
        finally
        {
          to_despawn.Despawn();
        }
      }

      return false;
    }

    public bool RemoveListener<P0, P1, P2>(Action<P0, P1, P2> handler)
    {
      var listeners = GetListeners<P0, P1, P2>();
      EventListenerInfo<P0, P1, P2> eventListenerInfo = null;
      foreach (var element in listeners)
      {
        if (element.handler.Equals(handler))
        {
          eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2>>().Init(element.eventName.Clone(), element.handler);
          break;
        }
      }

      if (eventListenerInfo == null)
        return false;
      var result = RemoveListener(eventListenerInfo);
      eventListenerInfo.Despawn();
      return result;
    }



    public void Broadcast<P0, P1, P2>(string eventName, P0 p0, P1 p1, P2 p2)
    {
      var _eventName = eventName.ToEventName(source);
      Broadcast(_eventName,p0,p1,p2);
      _eventName.Despawn();
    }

    public void Broadcast<P0, P1, P2>(EventName eventName, P0 p0, P1 p1, P2 p2)
    {
      GetEventDispatcher<P0, P1, P2>().Broadcast(eventName, p0, p1, p2);
    }



    public EventListenerInfo<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(string eventName,
      Action<P0, P1, P2, P3> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = AddListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public EventListenerInfo<P0, P1, P2, P3> AddListener<P0, P1, P2, P3>(EventName eventName,
      Action<P0, P1, P2, P3> handler)
    {
      var eventListenerInfo = GetEventDispatcher<P0, P1, P2, P3>().AddListener(eventName, handler);
      GetListeners<P0, P1, P2, P3>().Add(eventListenerInfo);
      return eventListenerInfo;
    }

    public bool RemoveListener<P0, P1, P2, P3>(string eventName,
      Action<P0, P1, P2, P3> handler)
    {
      var _eventName = eventName.ToEventName(source);
      var result = RemoveListener(_eventName, handler);
      _eventName.Despawn();
      return result;
    }

    public bool RemoveListener<P0, P1, P2, P3>(EventName eventName,
      Action<P0, P1, P2, P3> handler)
    {
      var _eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2, P3>>().Init(eventName.Clone(), handler);
      var result = RemoveListener(_eventListenerInfo);
      _eventListenerInfo.Despawn();
      return result;
    }

    public bool RemoveListener<P0, P1, P2, P3>(
      EventListenerInfo<P0, P1, P2, P3> eventListenerInfo)
    {
      var listeners = GetListeners<P0, P1, P2, P3>();
      int index = listeners.IndexOf(eventListenerInfo);
      if (index != -1)
      {
        var to_despawn = listeners[index];
        listeners.RemoveAt(index);
        try
        {
          if (GetEventDispatcher<P0,P1,P2,P3>().RemoveListener(eventListenerInfo))
            return true;
        }
        finally
        {
          to_despawn.Despawn();
        }
      }

      return false;
    }

    public bool RemoveListener<P0, P1, P2, P3>(Action<P0, P1, P2, P3> handler)
    {
      var listeners = GetListeners<P0, P1, P2, P3>();
      EventListenerInfo<P0, P1, P2, P3> eventListenerInfo = null;
      foreach (var element in listeners)
      {
        if (element.handler.Equals(handler))
        {
          eventListenerInfo = PoolCatManagerUtil.Spawn<EventListenerInfo<P0, P1, P2, P3>>().Init(element.eventName.Clone(), element.handler);
          break;
        }
      }

      if (eventListenerInfo == null)
        return false;
      var result = RemoveListener(eventListenerInfo);
      eventListenerInfo.Despawn();
      return result;
    }

    public void Broadcast<P0, P1, P2, P3>(string eventName, P0 p0, P1 p1, P2 p2, P3 p3)
    {
      var _eventName = eventName.ToEventName(source);
      Broadcast(_eventName,p0,p1,p2,p3);
      _eventName.Despawn();
    }

    public void Broadcast<P0, P1, P2, P3>(EventName eventName, P0 p0, P1 p1, P2 p2, P3 p3)
    {
      GetEventDispatcher<P0, P1, P2, P3>().Broadcast(eventName, p0, p1, p2, p3);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    public void RemoveAllListeners()
    {
      foreach (var key in this.cache.dict.Keys)
      {
        if (cache.dict[key] is IEventDispatcher)
        {
          var eventDispatcher = (IEventDispatcher)cache.dict[key];
          eventDispatcher.RemoveAllListeners();
        }
      }

      cache.Clear();
    }


    public void Destroy()
    {
      RemoveAllListeners();
    }
  }
}