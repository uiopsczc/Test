using System;
using UnityEngine;

namespace CsCat
{
  public partial class Act : IDespawn
  {
    protected MonoBehaviour _owner;
    public string id;
    public bool is_break;
    public ActSequence parent;
    public Status status;

    protected MonoBehaviour owner
    {
      get
      {
        if (_owner != null)
          return _owner;
        var current = parent;
        while (current.owner == null)
          current = current.parent;
        return current.owner;
      }
    }

    public Action<Action> on_exit_callback; //参数是用于跳到下一个act    (参数next)
    public Action on_pre_exit_callback;
    public Action<Act> on_start_callback;
    public Action on_pre_start_callback;
    public Action<Act> on_update_callback;


    public Act()
    {
    }

    public Act(ActSequence parent)
    {
      Init(parent);
    }

    public Act(ActSequence parent, string id) : this(parent)
    {
      Init(parent, id);
    }

    public void Init(ActSequence parent, string id = null)
    {
      this.parent = parent;
      this.id = id;
      status = Status.Ready;
    }


    public Act OnPreStart(Action on_pre_start_callback, bool is_append = true)
    {
      if (is_append)
        this.on_pre_start_callback += on_pre_start_callback;
      else
        this.on_pre_start_callback.InsertFirst(on_pre_start_callback);
      return this;
    }

    public Act OnStart(Action<Act> on_start_callback, bool is_append = true)
    {
      if (is_append)
        this.on_start_callback += on_start_callback;
      else
        this.on_start_callback.InsertFirst(on_start_callback);
      return this;
    }

    public Act OnUpdate(Action<Act> on_update_callback, bool is_append = true)
    {
      if (is_append)
        this.on_update_callback += on_update_callback;
      else
        this.on_update_callback.InsertFirst(on_update_callback);
      return this;
    }

    public Act OnPreExit(Action on_pre_exit_callback, bool is_append = true)
    {
      if (is_append)
        this.on_pre_exit_callback += on_pre_exit_callback;
      else
        this.on_pre_exit_callback.InsertFirst(on_pre_exit_callback);
      return this;
    }

    public Act OnExit(Action<Action> on_exit_callback, bool is_append = true)
    {
      if (is_append)
        this.on_exit_callback += on_exit_callback;
      else
        this.on_exit_callback.InsertFirst(on_exit_callback);
      return this;
    }


    public virtual void Start()
    {
      status = Status.Starting;
      on_pre_start_callback?.Invoke();
      on_start_callback?.Invoke(this);
    }

    public virtual void Update()
    {
      if (status == Status.Started) on_update_callback?.Invoke(this);
    }

    public virtual void Exit()
    {
      status = Status.Exiting;
      on_pre_exit_callback?.Invoke();

      Action parent_next_action = () => { };
      if (parent != null)
        parent_next_action = parent.Next;

      if (is_break)
        parent_next_action = () => { };


      if (on_exit_callback != null)
        on_exit_callback(parent_next_action);
      else
        parent_next_action();
      status = Status.Exited;

      Reset();
    }

    protected virtual void Reset()
    {
      status = Status.Ready;
      is_break = false;
    }

    public virtual void Break()
    {
      is_break = true;
      Exit();
    }


    public virtual void OnDespawn()
    {
      _owner = null;
      id = null;
      is_break = false;
      parent = null;
      status = Status.Ready;

      on_exit_callback = null;
      on_pre_exit_callback = null;
      on_start_callback = null;
      on_pre_start_callback = null;
      on_update_callback = null;
    }
  }
}