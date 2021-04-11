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
  /// <summary>
  ///   功能：assetBundle管理类，为外部提供统一的资源加载界面、协调Assetbundle各个子系统的运行
  ///   注意：
  ///   1、抛弃Resources目录的使用，官方建议：https://unity3d.com/cn/learn/tutorials/temas/best-practices/resources-folder?playlist=30089
  ///   2、提供Editor和Simulate模式，前者不适用Assetbundle，直接加载资源，快速开发；后者使用Assetbundle，用本地服务器模拟资源更新
  ///   3、场景不进行打包，场景资源打包为预设
  ///   4、只提供异步接口，所有加载按异步进行
  ///   5、采用LZMA压缩方式，性能瓶颈在Assetbundle加载上，ab加载异步，asset加载同步，ab加载后导出全部asset并卸载ab
  ///   7、随意卸载公共ab包可能导致内存资源重复，最好在切换场景时再手动清理不需要的公共ab包
  ///   8、常驻包（公共ab包）引用计数不为0时手动清理无效，正在等待加载的所有ab包不能强行终止---一旦发起创建就一定要等操作结束，异步过程进行中清理无效
  ///   9、切换场景时最好预加载所有可能使用到的资源，所有加载器用完以后记得Dispose回收，清理GC时注意先释放所有Asset缓存
  ///   10、逻辑层所有Asset路径带文件类型后缀，且是AssetBundleConfig.ResourcesFolderName下的相对路径，注意：路径区分大小写
  /// </summary>
  public partial class AssetBundleManager : TickObject
  {
    // 最大同时进行的ab创建数量
    private const int Max_AssetBundle_Create_Num = 20;

    private readonly List<AssetCat>
      assetCat_of_no_ref_list = new List<AssetCat>(); //这里使用延迟删除处理，和GameObject.Destory类似,检查因为没有ref而需要删除的asset（如果有ref则不会删除）

    // 逻辑层正在等待的asset加载异步句柄
    public readonly List<AssetAsyncLoader> assetAsyncloader_prosessing_list = new List<AssetAsyncLoader>();


    // 逻辑层正在等待的ab加载异步句柄
    public readonly List<AssetBundleAsyncLoader> assetBundleAsyncLoader_prosessing_list =
      new List<AssetBundleAsyncLoader>();

    // 加载数据请求：正在prosessing或者等待prosessing的资源请求
    public readonly Dictionary<string, ResourceWebRequester> resourceWebRequester_all_dict =
      new Dictionary<string, ResourceWebRequester>();

    // 正在处理的资源请求
    private readonly List<ResourceWebRequester> resourceWebRequester_prosessing_list = new List<ResourceWebRequester>();


    // 等待处理的资源请求
    private readonly Queue<ResourceWebRequester> resourceWebRequester_waiting_queue = new Queue<ResourceWebRequester>();

    // 常驻内存的(游戏中一直存在的)asset
    public Dictionary<string, bool> asset_resident_dict = new Dictionary<string, bool>();

    // AssetBundle缓存包
    public Dictionary<string, AssetBundleCat> assetBundleCat_dict = new Dictionary<string, AssetBundleCat>();

    // asset缓存
    public Dictionary<string, AssetCat> assetCat_dict = new Dictionary<string, AssetCat>();

    // 资源路径相关的映射表
    public AssetPathMap assetPathMap;
    public AssetBundleMap assetBundleMap;

    // manifest：提供依赖关系查找以及hash值比对
    public Manifest manifest;


    public string download_url => URLSetting.Server_Resource_Url;

    public override void Init()
    {
      base.Init();
      
      AddListener<ResourceWebRequester>(AssetBundleEventNameConst.On_ResourceWebRequester_Done,
        OnResourceWebRequesterDone);

      AddListener<AssetBundleAsyncLoader>(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail,
        OnAssetBundleAsyncLoaderFail);
      AddListener<AssetBundleAsyncLoader>(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Done,
        OnAssetBundleAsyncLoaderDone);

      AddListener<AssetAsyncLoader>(AssetBundleEventNameConst.On_AssetAsyncLoader_Fail,
        OnAssetAsyncLoaderFail);
      AddListener<AssetAsyncLoader>(AssetBundleEventNameConst.On_AssetAsyncLoader_Done,
        OnAssetAsyncLoaderDone);
    }


    public IEnumerator Initialize()
    {
      if (Application.isEditor && EditorModeConst.Is_Editor_Mode)
        yield break;

      manifest = new Manifest();
      assetPathMap = new AssetPathMap();
      assetBundleMap = new AssetBundleMap();

      // 说明：同时请求资源可以提高加载速度
      var manifest_request =
        DownloadFileAsyncNoCache(manifest.assetBundle_name.WithRootPath(FilePathConst.PersistentAssetBundleRoot));
      var assetPathMap_request =
        DownloadFileAsyncNoCache(
          BuildConst.AssetPathMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot));
      var assetBundleMap_request =
        DownloadFileAsyncNoCache(
          BuildConst.AssetBundleMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot));

      yield return manifest_request;
      if (manifest_request.error.IsNullOrWhiteSpace())
      {
        var assetBundle = manifest_request.assetBundle;
        manifest.LoadFromAssetbundle(assetBundle);
        assetBundle.Unload(false);
        manifest_request.Destroy();
        PoolCatManagerUtil.Despawn(manifest_request);
      }


      yield return assetPathMap_request;
      if (assetPathMap_request.error.IsNullOrWhiteSpace())
      {
        assetPathMap.Initialize(assetPathMap_request.text);
        assetPathMap_request.Destroy();
        PoolCatManagerUtil.Despawn(assetPathMap_request);
      }

      yield return assetBundleMap_request;
      if (assetBundleMap_request.error.IsNullOrWhiteSpace())
      {
        assetBundleMap.Initialize(assetBundleMap_request.text);
        assetBundleMap_request.Destroy();
        PoolCatManagerUtil.Despawn(assetBundleMap_request);
      }

      LogCat.Log("AssetMananger init finished");
    }
    


    

    

    

    public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.Update(deltaTime, unscaledDeltaTime);
      OnProsessingResourceWebRequester();
      OnProsessingAssetBundleAsyncLoader();
      OnProsessingAssetAsyncLoader();
      CheckAssetOfNoRefList();
    }

    private void OnProsessingResourceWebRequester()
    {
      var slot_count = resourceWebRequester_prosessing_list.Count;
      while (slot_count < Max_AssetBundle_Create_Num && resourceWebRequester_waiting_queue.Count > 0)
      {
        var resourceWebRequester = resourceWebRequester_waiting_queue.Dequeue();
        resourceWebRequester.Start();
        resourceWebRequester_prosessing_list.Add(resourceWebRequester);
        slot_count++;
      }
      for (var i = resourceWebRequester_prosessing_list.Count - 1; i >= 0; i--)
        resourceWebRequester_prosessing_list[i].Update();
    }

    private void OnProsessingAssetBundleAsyncLoader()
    {
    }

    private void OnProsessingAssetAsyncLoader()
    {
    }


    private void OnResourceWebRequesterDone(ResourceWebRequester resourceWebRequester)
    {
      if (!resourceWebRequester_prosessing_list.Contains(resourceWebRequester))
        return;
      resourceWebRequester_prosessing_list.Remove(resourceWebRequester);
      resourceWebRequester_all_dict.RemoveByFunc((k,v)=>v== resourceWebRequester);
      // 无缓存，不计引用计数、Creater使用后由上层回收，所以这里不需要做任何处理
      if (!resourceWebRequester.is_not_cache)
      {
        var assetBundleCat = resourceWebRequester.assetBundleCat;
        // 解除webRequester对AB持有的引用
        assetBundleCat?.SubRefCount();
        resourceWebRequester.Destroy();
        PoolCatManagerUtil.Despawn(resourceWebRequester);
      }
    }
























    //  public AssetCat GetAssetCat(Object asset)
    //  {
    //    foreach (var asset_path in assetCatDict.Keys)
    //      if (assetCatDict[asset_path].Get() == asset ||
    //          assetCatDict[asset_path].subAssetDict.Values.values.Contains(asset))
    //        return assetCatDict[asset_path];
    //    return null;
    //  }





  }
}