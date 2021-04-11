using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class AssetBundleManager
  {
    private bool CreateAssetBundleAsync(string assetBundle_name)
    {
      if (__IsAssetBundleLoadSuccess(assetBundle_name) || resourceWebRequester_all_dict.ContainsKey(assetBundle_name))
        return false;


      // webRequester持有的引用,webRequester结束后会删除该次引用(在AssetBunldeMananger中的OnProsessingWebRequester的进行删除本次引用操作)
      var assetBundleCat = new AssetBundleCat(assetBundle_name);
      assetBundleCat.AddRefCount();
      AddAssetBundleCat(assetBundleCat);

      var resourceWebRequester = PoolCatManagerUtil.Spawn<ResourceWebRequester>();
      var url = assetBundle_name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
      resourceWebRequester.Init(assetBundleCat, url);
      resourceWebRequester_all_dict[assetBundle_name]= resourceWebRequester;
      resourceWebRequester_waiting_queue.Enqueue(resourceWebRequester);
      
      return true;
    }

    // 异步请求Assetbundle资源
    private BaseAssetBundleAsyncLoader __LoadAssetBundleAsync(string assetBundle_name)
    {
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode)
        return new EditorAssetBundleAsyncLoader(assetBundle_name);

      var assetBundleAsyncLoader = PoolCatManagerUtil.Spawn<AssetBundleAsyncLoader>();
      assetBundleAsyncLoader_prosessing_list.Add(assetBundleAsyncLoader);
      var dependance_names = manifest.GetAllDependencies(assetBundle_name);
      List<AssetBundleCat> dependance_assetBundleCat_list = new List<AssetBundleCat>();
      foreach (var dependance_name in dependance_names)
      {
        CreateAssetBundleAsync(dependance_name);
        var dependance_assetBundleCat = __GetAssetBundleCat(dependance_name);
        // A依赖于B，A对B持有引用
        dependance_assetBundleCat.AddRefCount();
        dependance_assetBundleCat_list.Add(dependance_assetBundleCat);
      }

      CreateAssetBundleAsync(assetBundle_name);
      var assetBundleCat = __GetAssetBundleCat(assetBundle_name);
      foreach (var dependance_assetBundleCat in dependance_assetBundleCat_list)
        assetBundleCat.AddDependenceAssetBundleCat(dependance_assetBundleCat);      
      assetBundleAsyncLoader.Init(assetBundleCat);
      // 添加assetBundleAsyncLoader加载器对AB持有的引用,assetBundleAsyncLoader结束后会删除该次引用(在AssetBunldeMananger中的OnProsessingAssetBundleAsyncLoader的进行删除本次引用操作)
      assetBundleCat.AddRefCount();
      return assetBundleAsyncLoader;
    }
    

    private void OnAssetBundleAsyncLoaderFail(AssetBundleAsyncLoader assetBundleAsyncLoader)
    {
      if (!assetBundleAsyncLoader_prosessing_list.Contains(assetBundleAsyncLoader))
        return;

      //assetCat的OnFail中反过来回调减引用
    }

    private void OnAssetBundleAsyncLoaderDone(AssetBundleAsyncLoader assetBundleAsyncLoader)
    {
      if (!assetBundleAsyncLoader_prosessing_list.Contains(assetBundleAsyncLoader))
        return;

      // 解除assetBundleAsyncLoader加载器对AB持有的引用
      assetBundleAsyncLoader.assetBundleCat.SubRefCount();
      assetBundleAsyncLoader_prosessing_list.Remove(assetBundleAsyncLoader);
    }

    private bool __IsAssetBundleLoadSuccess(string assetBundle_name)
    {
      return assetBundleCat_dict.ContainsKey(assetBundle_name) && __GetAssetBundleCat(assetBundle_name).IsLoadSuccess();
    }

    private AssetBundleCat __GetAssetBundleCat(string assetBundle_name)
    {
      assetBundleCat_dict.TryGetValue(assetBundle_name, out var target);
      return target;
    }

    public void RemoveAssetBundleCat(string assetBundle_name)
    {
      RemoveAssetBundleCat(this.__GetAssetBundleCat(assetBundle_name));
    }

    public void RemoveAssetBundleCat(AssetBundleCat assetBundleCat)
    {
      if (assetBundleCat == null)
        return;
      LogCat.log(assetBundleCat.assetBundle_name + " removed");
      assetBundleCat.Get()?.Unload(true);//最关键的一步
      assetBundleCat_dict.RemoveByFunc((key, value) => value == assetBundleCat);
    }


    protected void AddAssetBundleCat(AssetBundleCat assetBundle)
    {
      assetBundleCat_dict[assetBundle.assetBundle_name] = assetBundle;
    }

    public ResourceWebRequester GetAssetBundleAsyncWebRequester(string assetBundle_name)
    {
      resourceWebRequester_all_dict.TryGetValue(assetBundle_name, out var webRequester);
      return webRequester;
    }
  }
}