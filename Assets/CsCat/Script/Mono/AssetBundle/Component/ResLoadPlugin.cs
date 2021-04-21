using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class ResLoadPlugin
  {
    public Dictionary<AssetCat, Dictionary<object, bool>> assetCat_dict =
      new Dictionary<AssetCat, Dictionary<object, bool>>();

    private ResLoad resLoad;

    public ResLoadPlugin(ResLoad resLoad)
    {
      this.resLoad = resLoad;
    }

    public bool IsAllLoadDone()
    {
      foreach (var assetCat in assetCat_dict.Keys)
        if (!assetCat.IsLoadDone())
          return false;
      return true;
    }

    public IEnumerator IEIsAllLoadDone(Action on_all_load_done_callback = null)
    {
      yield return new WaitUntil(() => { return IsAllLoadDone(); });
      on_all_load_done_callback?.Invoke();
    }


    // 加载某个资源
    public AssetCat GetOrLoadAsset(string asset_path, Action<AssetCat> on_load_success_callback = null,
      Action<AssetCat> on_load_fail_callback = null,
      Action<AssetCat> on_load_done_callback = null, object callback_cause = null)
    {
      var assetCat = GetOrLoadAsset(asset_path, on_load_success_callback, on_load_fail_callback, on_load_done_callback,
        callback_cause);
      if (!this.assetCat_dict.ContainsKey(assetCat))
        this.assetCat_dict[assetCat] = new Dictionary<object, bool>();
      this.assetCat_dict[assetCat][callback_cause.GetNotNullKey()] = true;
      return assetCat;
    }


    public void CancelLoadCallback(AssetCat assetCat, object callback_cause = null)
    {
      this.assetCat_dict[assetCat].Remove(callback_cause.GetNotNullKey());
      if (this.assetCat_dict[assetCat].Count == 0)
        this.assetCat_dict.Remove(assetCat);
      this.resLoad.CancelLoadCallback(assetCat, callback_cause);
    }

    public void CancelLoadAllCallback(AssetCat assetCat)
    {
      foreach (var callback_cause in assetCat_dict[assetCat].Values)
      {
        if (callback_cause.Equals(NullUtil.GetDefaultString()))
          this.resLoad.CancelLoadCallback(assetCat, null);
        else
          this.resLoad.CancelLoadCallback(assetCat, callback_cause);
      }
      assetCat_dict.Remove(assetCat);
    }


    public void Reset()
    {
      foreach (var assetCat in assetCat_dict.Keys)
        foreach (var callback_cause in assetCat_dict[assetCat].Values)
        {
          if (callback_cause.Equals(NullUtil.GetDefaultString()))
            this.resLoad.CancelLoadCallback(assetCat, null);
          else
            this.resLoad.CancelLoadCallback(assetCat, callback_cause);
        }

      assetCat_dict.Clear();
    }

    public void Destroy()
    {
      Reset();
    }
  }
}