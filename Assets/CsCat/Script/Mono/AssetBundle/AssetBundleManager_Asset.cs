using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace CsCat
{
  public partial class AssetBundleManager
  {
    #region 常驻内存的

    //常驻内存的(游戏中一直存在的)asset
    public void SetResidentAssets(bool is_resident, params string[] asset_paths)
    {
      foreach (var asset_path in asset_paths)
      {
        if (is_resident)
          asset_resident_dict[asset_path] = true;
        else
        {
          asset_resident_dict.Remove(asset_path);
          AddAssetCatOfNoRef(this.GetAssetCat(asset_path));
        }
      }
    }

    //常驻内存的(游戏中一直存在的)asset
    public void SetResidentAssets(List<string> asset_path_list, bool is_resident = true)
    {
      SetResidentAssets(is_resident, asset_path_list.ToArray());
    }

    #endregion

    private AssetCat __GetOrAddAssetCat(string asset_path)
    {
      assetCat_dict.TryGetValue(asset_path, out var target_assetCat);

      //编辑器模式下的或者是resouce模式下的,没有找到则创建一个AssetCat
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode
          || asset_path.Contains(FilePathConst.ResourcesFlag)
      )
      {
        Object[] assets = null;
        if (asset_path.Contains(FilePathConst.ResourcesFlag))
        {
          var resource_path = asset_path.Substring(asset_path.IndexEndOf(FilePathConst.ResourcesFlag) + 1)
            .GetMainAssetPath().WithoutSuffix();
          assets = Resources.LoadAll(resource_path);
        }
        else
        {
#if UNITY_EDITOR
          assets = AssetDatabase.LoadAllAssetsAtPath(asset_path);
#endif
        }

        if (!assets.IsNullOrEmpty()) //有该资源
        {
          target_assetCat = new AssetCat(asset_path);
          target_assetCat.SetAssets(assets);
          assetCat_dict[asset_path] = target_assetCat;
        }
        else
          return null;
      }

      return target_assetCat;
    }

    private bool __UnLoadUnUseAsset(string asset_path)
    {
      return __UnLoadUnUseAsset(GetAssetCat(asset_path));
    }

    private bool __UnLoadUnUseAsset(AssetCat assetCat)
    {
      if (assetCat == null)
        return false;
      if (!IsAssetLoadSuccess(assetCat.asset_path))
        return false;

      if (assetCat.ref_count == 0)
      {
        __RemoveAssetCat(assetCat);
        return true;
      }

      return false;
    }

    public AssetCat GetOrLoadAssetCat(string asset_path, Action<AssetCat> on_load_success_callback = null,
      Action<AssetCat> on_load_fail_callback = null, Action<AssetCat> on_load_done_callback = null,
      object callback_cause = null)
    {
      //编辑器模式下的或者是resouce模式下的
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode
          || asset_path.Contains(FilePathConst.ResourcesFlag)
      )
      {
        var assetCat = __GetOrAddAssetCat(asset_path);
        on_load_success_callback?.Invoke(assetCat);
        on_load_done_callback?.Invoke(assetCat);
        return assetCat;
      }

      if (IsAssetLoadSuccess(asset_path))
      {
        var assetCat = GetAssetCat(asset_path);
        on_load_success_callback?.Invoke(assetCat);
        on_load_done_callback?.Invoke(assetCat);
        return assetCat;
      }

      LoadAssetAsync(asset_path, on_load_success_callback, on_load_fail_callback, on_load_done_callback,
        callback_cause);
      var _assetCat = GetAssetCat(asset_path);
      return _assetCat;
    }

    public IEnumerator LoadAssetAsync(List<string> asset_path_list,
      Action<AssetCat> on_load_success_callback = null, Action<AssetCat> on_load_fail_callback = null,
      Action<AssetCat> on_load_done_callback = null, object callback_cause = null)
    {
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode)
      {
        foreach (var asset_path in asset_path_list)
        {
          on_load_success_callback?.Invoke(__GetOrAddAssetCat(asset_path));
          on_load_done_callback?.Invoke(__GetOrAddAssetCat(asset_path));
        }

        yield break;
      }

      foreach (var asset_path in asset_path_list)
        LoadAssetAsync(asset_path, on_load_success_callback, on_load_fail_callback, on_load_done_callback,
          callback_cause);

      yield return new WaitUntil(() =>
      {
        foreach (var asset_path in asset_path_list)
        {
          var assetCat = __GetOrAddAssetCat(asset_path);
          if (!assetCat.IsLoadDone())
            return false;
        }

        return true;
      });
    }

    public BaseAssetAsyncLoader LoadAssetAsync(string asset_path, Action<AssetCat> on_load_success_callback = null,
      Action<AssetCat> on_load_fail_callback = null, Action<AssetCat> on_load_done_callback = null,
      object callback_cause = null)
    {
      AssetCat assetCat;
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode
          || asset_path.Contains(FilePathConst.ResourcesFlag)
      )
      {
        assetCat = __GetOrAddAssetCat(asset_path);
        on_load_success_callback?.Invoke(assetCat);
        on_load_done_callback?.Invoke(assetCat);
        return new EditorAssetAsyncLoader(assetCat);
      }

      var _asset_path = asset_path;
      var assetBundle_name = assetPathMap.GetAssetBundleName(_asset_path);
      if (assetBundle_name == null)
        LogCat.error(string.Format("{0}没有设置成ab包", _asset_path));

      if (assetCat_dict.ContainsKey(_asset_path))
      {
        assetCat = GetAssetCat(_asset_path);
        //已经加载成功
        if (assetCat.IsLoadSuccess())
        {
          on_load_success_callback?.Invoke(assetCat);
          on_load_done_callback?.Invoke(assetCat);
          return null;
        }

        //加载中
        assetCat.AddOnLoadSuccessCallback(on_load_success_callback, callback_cause);
        assetCat.AddOnLoadFailCallback(on_load_fail_callback, callback_cause);
        assetCat.AddOnLoadDoneCallback(on_load_done_callback, callback_cause);
        return assetAsyncloader_prosessing_list.Find(assetAsyncloader =>
          assetAsyncloader.assetCat.asset_path.Equals(_asset_path));
      }

      //没有加载
      var assetAsyncLoader = PoolCatManagerUtil.Spawn<AssetAsyncLoader>();
      assetCat = new AssetCat(_asset_path);
      __AddAssetCat(_asset_path, assetCat);
      //添加对assetAsyncLoader的引用
      assetCat.AddRefCount();
      assetCat.AddOnLoadSuccessCallback(on_load_success_callback, callback_cause);
      assetCat.AddOnLoadFailCallback(on_load_fail_callback, callback_cause);
      assetCat.AddOnLoadDoneCallback(on_load_done_callback, callback_cause);

      var assetBundleLoader = __LoadAssetBundleAsync(assetBundle_name);
      assetAsyncLoader.Init(assetCat, assetBundleLoader);
      //asset拥有对assetBundle的引用
      var assetBundleCat = assetBundleLoader.assetBundleCat;
      assetBundleCat.AddRefCount();

      assetCat.assetBundleCat = assetBundleCat;

      assetAsyncloader_prosessing_list.Add(assetAsyncLoader);
      return assetAsyncLoader;
    }

    private void CheckAssetOfNoRefList()
    {
      for (var i = assetCat_of_no_ref_list.Count - 1; i >= 0; i--)
      {
        var assetCat = assetCat_of_no_ref_list[i];
        if (assetCat.ref_count <= 0)
        {
          assetCat_of_no_ref_list.Remove(assetCat);
          __RemoveAssetCat(assetCat);
        }
      }
    }

    private void OnAssetAsyncLoaderFail(AssetAsyncLoader assetAsyncLoader)
    {
      if (!assetAsyncloader_prosessing_list.Contains(assetAsyncLoader))
        return;

      //失败的时候解除对assetCat的引用
      assetAsyncLoader.assetCat.SubRefCount();
    }

    private void OnAssetAsyncLoaderDone(AssetAsyncLoader assetAsyncLoader)
    {
      if (!assetAsyncloader_prosessing_list.Contains(assetAsyncLoader))
        return;

      //解除对assetAsyncLoader的引用
      assetAsyncLoader.assetCat.SubRefCount();

      assetAsyncloader_prosessing_list.Remove(assetAsyncLoader);
      assetAsyncLoader.Destroy();
      PoolCatManagerUtil.Despawn(assetAsyncLoader);
    }

    // 添加没有被引用的assetCat,延迟到下一帧删除
    public void AddAssetCatOfNoRef(AssetCat assetCat)
    {
      if (assetCat == null)
        return;
      if (!assetCat_of_no_ref_list.Contains(assetCat) && !asset_resident_dict.ContainsKey(assetCat.asset_path))
        assetCat_of_no_ref_list.Add(assetCat);
    }

    public bool IsAssetLoadSuccess(string asset_path)
    {
      return assetCat_dict.ContainsKey(asset_path) && __GetOrAddAssetCat(asset_path).IsLoadSuccess();
    }

    public AssetCat GetAssetCat(string asset_path)
    {
      assetCat_dict.TryGetValue(asset_path, out var target);
      return target;
    }


    public void __RemoveAssetCat(string asset_path)
    {
      __RemoveAssetCat(this.GetAssetCat(asset_path));
    }

    private void __RemoveAssetCat(AssetCat assetCat)
    {
      if (assetCat == null)
        return;
      assetCat_dict.RemoveByFunc((key, value) => value == assetCat);
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode
          || assetCat.asset_path.Contains(FilePathConst.ResourcesFlag)
      )
        return;
      var assetBundleCat = assetCat.assetBundleCat;
      //assetBundle_name的dependencies的引用-1
      assetBundleCat?.SubRefCountOfDependencies();
      //减少一个assetBundle对asset的引用
      assetBundleCat?.SubRefCount();
    }

    private void __AddAssetCat(string asset_path, AssetCat asset)
    {
      assetCat_dict[asset_path] = asset;
    }

    public string GetAssetBundlePath(string asset_path)
    {
      return assetPathMap.GetAssetBundleName(asset_path);
    }
  }
}