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
  public class ResLoad
  {
    private readonly Dictionary<string, ResLoadDataInfo> resLoadDataInfo_dict = new Dictionary<string, ResLoadDataInfo>();
    public bool is_not_check_destroy;
    public ResLoad(bool is_not_check_destroy = false)
    {
      this.is_not_check_destroy = is_not_check_destroy;
    }
    public bool IsAllLoadDone()
    {
      foreach (var resLoadDataInfo in resLoadDataInfo_dict.Values)
        if (!resLoadDataInfo.resLoadData.assetCat.IsLoadDone())
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
      Action<AssetCat> on_load_done_callback = null,object callback_cause=null)
    {
      if (!Application.isPlaying)
      {
#if UNITY_EDITOR
        Object obj = AssetDatabase.LoadAssetAtPath<Object>(asset_path);
        if (!resLoadDataInfo_dict.ContainsKey(asset_path.GetMainAssetPath()))
        {
          AssetCat _assetCat = new AssetCat(asset_path);
          resLoadDataInfo_dict[asset_path.GetMainAssetPath()] = new ResLoadDataInfo(new ResLoadData(_assetCat), is_not_check_destroy);
        }

        var resLoadDataInfo = resLoadDataInfo_dict[asset_path.GetMainAssetPath()];
        if (!resLoadDataInfo.resLoadData.assetCat.asset_dict.ContainsKey(obj.name))
          resLoadDataInfo.resLoadData.assetCat.asset_dict[obj.name] = new Dictionary<Type, Object>();
        if (!resLoadDataInfo.resLoadData.assetCat.asset_dict[obj.name].ContainsKey(obj.GetType()))
          resLoadDataInfo.resLoadData.assetCat.asset_dict[obj.name][obj.GetType()] = obj;
        on_load_success_callback?.Invoke(resLoadDataInfo.resLoadData.assetCat);
        on_load_done_callback?.Invoke(resLoadDataInfo.resLoadData.assetCat);
        return resLoadDataInfo.resLoadData.assetCat;
#endif
      }
      callback_cause = callback_cause ?? this;
      var assetCat =
        Client.instance.assetBundleManager.GetOrLoadAssetCat(asset_path.GetMainAssetPath(), on_load_success_callback,
          on_load_fail_callback, on_load_done_callback, callback_cause);
      if(!resLoadDataInfo_dict.ContainsKey(asset_path.GetMainAssetPath()))
        resLoadDataInfo_dict[asset_path.GetMainAssetPath()] = new ResLoadDataInfo(new ResLoadData(assetCat), is_not_check_destroy);
      resLoadDataInfo_dict[asset_path.GetMainAssetPath()].AddCallbackCause(callback_cause);
      return assetCat;
    }


    public void CancelLoadCallback(AssetCat assetCat,object callback_cause=null)
    {
      string to_remove_key = null;
      foreach (var key in resLoadDataInfo_dict.Keys)
      {
        var resLoadDataInfo = resLoadDataInfo_dict[key];
        if (resLoadDataInfo.resLoadData.assetCat == assetCat)
        {
          resLoadDataInfo.RemoveCallbackCause(callback_cause);
          if(resLoadDataInfo.callback_cause_dict.Count==0&&!is_not_check_destroy)//is_not_check_destroy的时候不删除，因为要在destroy的时候作为删除的asset的依据
            to_remove_key = key;
          break;
        }
      }

      if (to_remove_key != null)
        resLoadDataInfo_dict.Remove(to_remove_key);
    }

    public void CancelLoadAllCallbacks(AssetCat assetCat)
    {
      string to_remove_key = null;
      foreach (var key in resLoadDataInfo_dict.Keys)
      {
        var resLoadDataInfo = resLoadDataInfo_dict[key];
        if (resLoadDataInfo.resLoadData.assetCat == assetCat)
        {
          resLoadDataInfo.RemoveAllCallbackCauses();
          if (resLoadDataInfo.callback_cause_dict.Count == 0&& !is_not_check_destroy)//is_not_check_destroy的时候不删除，因为要在destroy的时候作为删除的asset的依据
            to_remove_key = key;
          break;
        }
      }
      if (to_remove_key != null)
        resLoadDataInfo_dict.Remove(to_remove_key);
    }


    public void Reset()
    {
      foreach (var resLoadDataInfo in resLoadDataInfo_dict.Values)
        resLoadDataInfo.Destroy();

      resLoadDataInfo_dict.Clear();
    }

    public void Destroy()
    {
      Reset();
    }
  }
}