using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class ResLoadComponentPlugin
  {
    private ResLoadComponent resLoadComponent;

    public Dictionary<AssetCat, Dictionary<object, bool>> assetCat_dict =
      new Dictionary<AssetCat, Dictionary<object, bool>>();

    public ResLoadComponentPlugin(ResLoadComponent resLoadComponent)
    {
      this.resLoadComponent = resLoadComponent;
    }

    public bool IsAllLoadDone()
    {
      foreach (var assetCat in assetCat_dict.Keys)
      {
        if (!assetCat.IsLoadDone())
          return false;
      }

      return true;
    }

    public IEnumerator IEIsAllLoadDone(Action on_all_load_done_callback = null)
    {
      yield return new WaitUntil(() => IsAllLoadDone());
      on_all_load_done_callback();
    }

    public void CheckIsAllLoadDone(Action on_all_load_done_callback = null)
    {
      resLoadComponent.StartCoroutine(IEIsAllLoadDone(on_all_load_done_callback));
    }


    // 加载某个资源
    public AssetCat GetOrLoadAsset(string asset_path, Action<AssetCat> on_load_success_callback = null,
      Action<AssetCat> on_load_fail_callback = null, Action<AssetCat> on_load_done_callback = null,
      object callback_cause = null)
    {
      var assetCat = resLoadComponent.GetOrLoadAsset(asset_path, on_load_success_callback, on_load_fail_callback,
        on_load_done_callback, callback_cause);
      __AddToAssetCatDict(assetCat, callback_cause);
      return assetCat;
    }


    public void CancelLoadCallback(AssetCat assetCat, object callback_cause = null)
    {
      this.resLoadComponent.CancelLoadCallback(assetCat, callback_cause);
      __RemoveFromAssetCatDict(assetCat, callback_cause);
    }

    public void CancelLoadAllCallback(AssetCat assetCat)
    {
      if (!assetCat_dict.ContainsKey(assetCat))
        return;
      foreach (var callback_cuase_dict in assetCat_dict.Values)
      foreach (var callback_cuase in callback_cuase_dict.Keys)
        resLoadComponent.CancelLoadCallback(assetCat, callback_cuase.GetNullableKey());
        
      assetCat_dict.Remove(assetCat);
    }


    void __AddToAssetCatDict(AssetCat assetCat, object callbak_cause)
    {
      assetCat_dict.GetOrAddDefault(assetCat, () => new Dictionary<object, bool>())[callbak_cause.GetNotNullKey()] =
        true;
    }

    void __RemoveFromAssetCatDict(AssetCat assetCat, object callbak_cause)
    {
      if (!assetCat_dict.ContainsKey(assetCat))
        return;
      assetCat_dict[assetCat].Remove(callbak_cause.GetNotNullKey());
    }

    public void Destroy()
    {
      foreach (var assetCat in assetCat_dict.Keys)
      foreach (var callbak_cause in assetCat_dict[assetCat].Keys)
        resLoadComponent.CancelLoadCallback(assetCat, callbak_cause.GetNullableKey());
      assetCat_dict.Clear();
      resLoadComponent = null;
    }
  }
}