using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public partial class GameEntity
  {
    public bool is_all_assets_load_done;
    public Action all_assets_load_done_callback;

    // 通过resLoadComponent操作reload的东西
    public virtual void PreLoadAssets()
    {
      //resLoadComponent.LoadAssetAsync("resPath");
    }

    public void InvokeAfterAllAssetsLoadDone(Action callback)
    {
      if (is_all_assets_load_done)
        callback();
      else
        all_assets_load_done_callback += callback;
    }



    protected void CheckIsAllAssetsLoadDone()
    {
      this.StartCoroutine(IECheckIsAllAssetsLoadDone());
    }

    protected virtual IEnumerator IECheckIsAllAssetsLoadDone()
    {
      yield return this.resLoadComponent.IEIsAllLoadDone();
      if (!graphicComponent.prefab_path.IsNullOrEmpty())
        yield return new WaitUntil(() => graphicComponent.IsLoadDone());
      OnAllAssetsLoadDone();
    }

    public virtual void OnAllAssetsLoadDone()
    {
      this.Broadcast(ECSEventNameConst.OnAllAssetsLoadDone.ToEventName(this));
      is_all_assets_load_done = true;
      all_assets_load_done_callback?.Invoke();
      all_assets_load_done_callback = null;
    }

  }
}