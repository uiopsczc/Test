using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  ///   功能：Assetbundle加载器，给逻辑层使用（预加载），支持协程操作
  /// </summary>
  public class AssetBundleAsyncLoader : BaseAssetBundleAsyncLoader
  {
    protected int total_waiting_assetBundleCat_count;
    protected Dictionary<string, AssetBundleCat> waiting_assetBundleCat_dict = new Dictionary<string, AssetBundleCat>();
    protected Dictionary<string, long> assetBundle_downloaded_bytes_dict = new Dictionary<string, long>();
    protected long need_download_bytes;
    
    

    public void Init(AssetBundleCat assetBundleCat)
    {
      this.assetBundle_name = assetBundle_name;
      this.assetBundleCat = assetBundleCat;
      // 只添加没有被加载过的
      if (!assetBundleCat.IsLoadDone())
      {
        waiting_assetBundleCat_dict[assetBundleCat.assetBundle_name]= assetBundleCat;
        assetBundle_downloaded_bytes_dict[assetBundleCat.assetBundle_name] = 0;
      }
      
        foreach (var dependence_assetBundleCat in assetBundleCat.dependence_assetBundleCat_list)
        {
          if (dependence_assetBundleCat.IsLoadDone())
            continue;
          waiting_assetBundleCat_dict[dependence_assetBundleCat.assetBundle_name] = dependence_assetBundleCat;
          assetBundle_downloaded_bytes_dict[dependence_assetBundleCat.assetBundle_name] = 0;
        }

      total_waiting_assetBundleCat_count = waiting_assetBundleCat_dict.Count;

      
      need_download_bytes = 0;
      foreach (var _assetBundle_name in assetBundle_downloaded_bytes_dict.Keys)
        need_download_bytes += Client.instance.assetBundleManager.assetBundleMap.dict[_assetBundle_name];

      if (total_waiting_assetBundleCat_count == 0)
      {
        resultInfo.is_success = true;
        return;
      }

      AddListener<ResourceWebRequester>(AssetBundleEventNameConst.On_ResourceWebRequester_Fail,
        OnResourceWebRequesterFail);
      AddListener<ResourceWebRequester>(AssetBundleEventNameConst.On_ResourceWebRequester_Success,
        OnResourceWebRequesterSuccess);
    }
    

    protected override float GetProgress()
    {
      if (resultInfo.is_done)
        return 1.0f;

      var progress_value = (float) GetDownloadedBytes() / GetNeedDownloadBytes();
      return progress_value;
    }

    public override List<string> GetAssetBundlePathList()
    {
      List<string> result = new List<string>();
      foreach (var assetBundle_name in assetBundle_downloaded_bytes_dict.Keys)
        result.Add(assetBundle_name);
      return result;
    }


    public override long GetNeedDownloadBytes()
    {
      return need_download_bytes;
    }

    public override long GetDownloadedBytes()
    {
      if (resultInfo.is_done)
        return GetNeedDownloadBytes();
      long downloaded_bytes = 0;
      foreach (var assetBundle_name in assetBundle_downloaded_bytes_dict.Keys)
      {
        bool is_load_done = !waiting_assetBundleCat_dict.ContainsKey(assetBundle_name);
        if (is_load_done) //已经下载完的assetBundle_name
          assetBundle_downloaded_bytes_dict[assetBundle_name] =
            Client.instance.assetBundleManager.assetBundleMap.GetAssetBundleBytes(assetBundle_name);
        else
        {
          var resourceWebRequester = this.waiting_assetBundleCat_dict[assetBundle_name].resourceWebRequester;
          assetBundle_downloaded_bytes_dict[assetBundle_name] =
            resourceWebRequester?.GetDownloadedBytes() ??
            Client.instance.assetBundleManager.assetBundleMap.GetAssetBundleBytes(assetBundle_name);
        }
        downloaded_bytes += assetBundle_downloaded_bytes_dict[assetBundle_name];
      }
      return downloaded_bytes;
    }

    public override void Update()
    {
    }

    private void OnResourceWebRequesterSuccess(ResourceWebRequester resourceWebRequester)
    {
      if (resultInfo.is_done)
        return;
      if (!waiting_assetBundleCat_dict.ContainsValue(resourceWebRequester.assetBundleCat) ||
          resourceWebRequester.is_not_cache) return;
      waiting_assetBundleCat_dict.Remove(resourceWebRequester.assetBundleCat.assetBundle_name);
      if (waiting_assetBundleCat_dict.Count == 0)
        resultInfo.is_success = true;
    }

    private void OnResourceWebRequesterFail(ResourceWebRequester resourceWebRequester)
    {
      if (resultInfo.is_done)
        return;
      if (!waiting_assetBundleCat_dict.ContainsValue(resourceWebRequester.assetBundleCat) ||
          resourceWebRequester.is_not_cache) return;
      resultInfo.is_fail = true;
      RemoveListener<ResourceWebRequester>(AssetBundleEventNameConst.On_ResourceWebRequester_Fail,
        OnResourceWebRequesterFail);
    }
    

    protected override void OnSuccess()
    {
      base.OnSuccess();
      Broadcast(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success, this);
    }
    protected override void OnFail()
    {
      base.OnFail();
      Broadcast(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail, this);
    }
    
    protected override void OnDone()
    {
      base.OnDone();
      Broadcast(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Done, this);
      this.Destroy();
      PoolCatManagerUtil.Despawn(this);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      total_waiting_assetBundleCat_count = 0;
      waiting_assetBundleCat_dict.Clear();
      assetBundle_downloaded_bytes_dict.Clear();
      need_download_bytes = 0;
    }
  }
}