using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class AssetCat
  {
    public Dictionary<string, Dictionary<Type, Object>> asset_dict = new Dictionary<string, Dictionary<Type, Object>>();
    public AssetBundleCat assetBundleCat;
    public string asset_path;
    public ResultInfo _resultInfo;


    public ResultInfo resultInfo => _resultInfo ?? (_resultInfo = new ResultInfo(
                                      () =>
                                      {
                                        on_load_success_callback?.Invoke(this);
                                        RemoveAllOnLoadSuccessCallback();
                                      }, () =>
                                      {
                                        on_load_fail_callback?.Invoke(this);
                                        RemoveAllOnLoadFailCallback();
                                      }, () =>
                                      {
                                        on_load_done_callback?.Invoke(this);
                                        RemoveAllOnLoadDoneCallback();
                                      }));

    public int ref_count { get; private set; }


    private Action<AssetCat> on_load_success_callback;
    private Action<AssetCat> on_load_fail_callback;
    private Action<AssetCat> on_load_done_callback;

    //用来追溯来源，就是取消下载的时候，把那些关于它自己的部分的callback删除
    private ValueListDictionary<object, Action<AssetCat>> on_load_success_callback_list_dict =
      new ValueListDictionary<object, Action<AssetCat>>();

    private ValueListDictionary<object, Action<AssetCat>> on_load_fail_callback_list_dict =
      new ValueListDictionary<object, Action<AssetCat>>();

    private ValueListDictionary<object, Action<AssetCat>> on_load_done_callback_list_dict =
      new ValueListDictionary<object, Action<AssetCat>>();

    public AssetCat(string asset_path)
    {
      this.asset_path = asset_path;
    }

    public void AddOnLoadSuccessCallback(Action<AssetCat> on_load_success_callback, object callback_cause = null)
    {
      if (on_load_success_callback == null)
        return;

      this.on_load_success_callback += on_load_success_callback;
      if (callback_cause == null)
        callback_cause = this;
      on_load_success_callback_list_dict.Add(callback_cause, on_load_success_callback);
    }

    public void AddOnLoadFailCallback(Action<AssetCat> on_load_fail_callback, object callback_cause = null)
    {
      if (on_load_fail_callback == null)
        return;

      this.on_load_fail_callback += on_load_fail_callback;
      if (callback_cause == null)
        callback_cause = this;
      on_load_fail_callback_list_dict.Add(callback_cause, on_load_fail_callback);
    }

    public void AddOnLoadDoneCallback(Action<AssetCat> on_load_done_callback, object callback_cause = null)
    {
      if (on_load_done_callback == null)
        return;
      this.on_load_done_callback += on_load_done_callback;
      if (callback_cause == null)
        callback_cause = this;
      on_load_done_callback_list_dict.Add(callback_cause, on_load_done_callback);
    }


    public void RemoveOnLoadSuccessCallback(Action<AssetCat> on_load_success_callback, object callback_cause = null)
    {
      if (on_load_success_callback == null)
        return;

      this.on_load_success_callback -= on_load_success_callback;
      if (callback_cause == null)
        callback_cause = this;
      on_load_success_callback_list_dict.Remove(callback_cause, on_load_success_callback);
    }

    public void RemoveOnLoadFailCallback(Action<AssetCat> on_load_fail_callback, object callback_cause = null)
    {
      if (on_load_fail_callback == null)
        return;

      this.on_load_fail_callback -= on_load_fail_callback;
      if (callback_cause == null)
        callback_cause = this;
      on_load_fail_callback_list_dict.Remove(callback_cause, on_load_fail_callback);
    }

    public void RemoveOnLoadDoneCallback(Action<AssetCat> on_load_done_callback, object callback_cause = null)
    {
      if (on_load_done_callback == null)
        return;

      this.on_load_done_callback -= on_load_done_callback;
      if (callback_cause == null)
        callback_cause = this;
      on_load_done_callback_list_dict.Remove(callback_cause, on_load_done_callback);
    }

    public void RemoveOnLoadSuccessCallback(object callback_cause = null)
    {
      if (callback_cause == null)
        callback_cause = this;
      on_load_success_callback_list_dict.Foreach(callback_cause,
        callback => { this.on_load_success_callback -= callback; });
      on_load_success_callback_list_dict.Remove(callback_cause);
    }

    public void RemoveOnLoadFailCallback(object callback_cause = null)
    {
      if (callback_cause == null)
        callback_cause = this;
      on_load_fail_callback_list_dict.Foreach(callback_cause, callback => { this.on_load_fail_callback -= callback; });
      on_load_fail_callback_list_dict.Remove(callback_cause);
    }

    public void RemoveOnLoadDoneCallback(object callback_cause = null)
    {
      if (callback_cause == null)
        callback_cause = this;
      on_load_done_callback_list_dict.Foreach(callback_cause, callback => { this.on_load_done_callback -= callback; });
      on_load_done_callback_list_dict.Remove(callback_cause);
    }

    public void RemoveAllOnLoadSuccessCallback()
    {
      on_load_success_callback_list_dict.Clear();
      this.on_load_success_callback = null;
    }

    public void RemoveAllOnLoadFailCallback()
    {
      on_load_fail_callback_list_dict.Clear();
      this.on_load_fail_callback = null;
    }

    public void RemoveAllOnLoadDoneCallback()
    {
      on_load_done_callback_list_dict.Clear();
      this.on_load_done_callback = null;
    }

    public void RemoveCallback(object callback_cause = null)
    {
      RemoveOnLoadSuccessCallback(callback_cause);
      RemoveOnLoadFailCallback(callback_cause);
      RemoveOnLoadSuccessCallback(callback_cause);
    }

    public void RemoveAllCallback()
    {
      RemoveAllOnLoadSuccessCallback();
      RemoveAllOnLoadFailCallback();
      RemoveAllOnLoadSuccessCallback();
    }


    public Object Get(string sub_asset_path = null, Type type = null)
    {
//    LogCat.logWarning(sub_asset_path, asset_dict);
      if (asset_dict.Count == 0)
        return null;
      if (sub_asset_path.IsNullOrWhiteSpace())
        return asset_dict.Values.ToArray()[0].Values.ToArray()[0];

      if (type != null)
        return asset_dict[sub_asset_path][type];


      return asset_dict[sub_asset_path].Values.ToArray()[0];
    }

    public T Get<T>(string sub_asset_path = null) where T : Object
    {
      return (T) Get(sub_asset_path, typeof(T));
    }

    public bool IsLoadSuccess()
    {
      return this.resultInfo.is_success;
    }

    public bool IsLoadFail()
    {
      return this.resultInfo.is_fail;
    }

    public bool IsLoadDone()
    {
      return this.resultInfo.is_done;
    }


    public void AddRefCount()
    {
      ref_count++;
      //Debug.LogError(asset_path+"++: "+refCount);
    }

    public int SubRefCount(int sub_value = 1, bool is_remove_asset_if_no_ref = true)
    {
      sub_value = Math.Abs(sub_value);
      ref_count = ref_count - sub_value;

      //Debug.LogError(asset_path + "--: " + refCount);
      if (ref_count < 0)
        ref_count = 0;
      if (is_remove_asset_if_no_ref && ref_count == 0)
      {
        if (IsLoadDone())
        {
          if (Application.isPlaying)
            Client.instance.assetBundleManager.AddAssetCatOfNoRef(this); //延迟删除
        }
        else
          on_load_success_callback += asset_cat =>
          {
            Client.instance.assetBundleManager.AddAssetCatOfNoRef(this);
          }; //延迟删除;
      }

      //AssetBundleManager.Instance.RemoveAssetCat(asset_path);
      return ref_count;
    }

    public void CheckNoRef()
    {
      SubRefCount(0, true);
    }

    public void SetAssets(Object[] assets)
    {
      foreach (var asset in assets)
      {
        if (!asset_dict.ContainsKey(asset.name))
          asset_dict[asset.name] = new Dictionary<Type, Object>();
        asset_dict[asset.name][asset.GetType()] = asset;
      }

      OnSetAssets();
    }

    private void OnSetAssets()
    {
#if UNITY_EDITOR
      // 说明：在Editor模拟时，Shader要重新指定
      HandleAssetShader();
#endif
      this.resultInfo.is_success = true;
    }
#if UNITY_EDITOR
    private void HandleAssetShader()
    {
      var mat = Get() as Material;
      if (mat != null)
        HandleAssetShader_Mat(ref mat);
      var go = Get() as GameObject;
      if (go != null)
        HandleAssetShader_GameObject(go);
    }

    private void HandleAssetShader_Mat(ref Material mat)
    {
      var shader = mat.shader;
      if (shader != null)
      {
        var shader_name = shader.name;
        mat.shader = ShaderUtilCat.FindShader(shader_name);
      }
    }

    private void HandleAssetShader_GameObject(GameObject go)
    {
      var renderers = go.GetComponentsInChildren<Renderer>();
      for (var j = 0; j < renderers.Length; j++)
      {
        var mat = renderers[j].sharedMaterial;
        if (mat == null)
          continue;
        HandleAssetShader_Mat(ref mat);
      }
    }

#endif
  }
}