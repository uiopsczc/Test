using System;
using System.Collections;

namespace CsCat
{
  public abstract class ResourceAsyncOperation : GameEntity, IEnumerator
  {
    public ResultInfo _resultInfo;


    public ResultInfo resultInfo
    {
      get
      {
        if (_resultInfo != null) return _resultInfo;
        _resultInfo = PoolCatManagerUtil.Spawn<ResultInfo>();
        _resultInfo.Init(OnSuccess, OnFail, OnDone);
        return _resultInfo;
      }
    }
    public float progress => GetProgress();
    public object Current => null;


    public Action on_success_callback;
    public Action on_fail_callback;
    public Action on_done_callback;




    public bool MoveNext()
    {
      return !resultInfo.is_done;
    }

    public abstract void Update();

    protected virtual void OnSuccess()
    {
      this.on_success_callback?.Invoke();
      this.on_success_callback = null;
    }

    protected virtual void OnFail()
    {
      this.on_fail_callback?.Invoke();
      this.on_fail_callback = null;
    }

    protected virtual void OnDone()
    {
      this.on_done_callback?.Invoke();
      this.on_done_callback = null;
    }


    protected abstract float GetProgress();

    public virtual long GetNeedDownloadBytes()
    {
      return 1;
    }

    public virtual long GetDownloadedBytes()
    {
      return 1;
    }

    public void Reset()
    {
      on_success_callback = null;
      on_fail_callback = null;
      on_done_callback = null;
      if (_resultInfo != null)
        PoolCatManagerUtil.Despawn(_resultInfo);
      _resultInfo = null;
    }

    public override void OnDespawn()
    {
      base.OnDespawn();
      Reset();
    }
  }
}