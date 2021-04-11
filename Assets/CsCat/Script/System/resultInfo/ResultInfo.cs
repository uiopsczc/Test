using System;

namespace CsCat
{
  public class ResultInfo : ISpawnable
  {
    private bool _is_success;
    private bool _is_fail;
    private bool _is_done;

    public Action on_success_callback;
    public Action on_fail_callback;
    public Action on_done_callback;

    public ResultInfo()
    {
    }

    public ResultInfo(Action on_success_callback = null, Action on_fail_callback = null, Action on_done_callback = null)
    {
      Init(on_success_callback, on_fail_callback, on_done_callback);
    }

    public void Init(Action on_success_callback = null, Action on_fail_callback = null, Action on_done_callback = null)
    {
      this.on_success_callback = on_success_callback;
      this.on_fail_callback = on_fail_callback;
      this.on_done_callback = on_done_callback;
    }

    public bool is_success
    {
      get => _is_success;
      set
      {
        if (_is_success == value)
          return;
        _is_success = value;
        if (value)
        {
          OnSuccess();
          is_done = true;
        }
      }
    }

    public bool is_fail
    {
      get => _is_fail;
      set
      {
        if (_is_fail == value)
          return;
        _is_fail = value;
        if (value)
        {
          OnFail();
          is_done = true;
        }
      }
    }


    public bool is_done
    {
      get => _is_done;
      set
      {
        if (_is_done == value)
          return;
        _is_done = value;
        if (value)
          OnDone();
      }
    }

    void OnSuccess()
    {
      this.on_success_callback?.Invoke();
    }


    void OnFail()
    {
      this.on_fail_callback?.Invoke();
    }


    void OnDone()
    {
      this.on_done_callback?.Invoke();
    }

    public void Reset()
    {
      this._is_success = false;
      this._is_fail = false;
      this._is_done = false;

      on_success_callback = null;
      on_fail_callback = null;
      on_done_callback = null;
    }


    public void OnDespawn()
    {
      Reset();
    }
  }
}