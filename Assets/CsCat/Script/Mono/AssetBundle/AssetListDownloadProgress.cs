using System;
using System.Collections.Generic;

namespace CsCat
{
  /// <summary>
  /// 用来获取assetList的下载进度【包括需要下载多少bytes，已下载多少bytes，进度[0,1]】
  /// </summary>
  public class AssetListDownloadProgress
  {
    public List<string> asset_path_list;
    public Dictionary<string, long> assetBundle_downloaded_bytes_dict = new Dictionary<string, long>();
    private long need_download_bytes;

    public AssetListDownloadProgress(List<string> asset_path_list)
    {
      asset_path_list.Unique();
      List<string> assetBundle_name_list = new List<string>();
      foreach (var asset_path in asset_path_list)
      {
        var assetAsyncloader = Client.instance.assetBundleManager.assetAsyncloader_prosessing_list.Find(
          _assetAsyncloader =>
            _assetAsyncloader.assetCat.asset_path.Equals(asset_path));
        if (assetAsyncloader != null && !assetAsyncloader.resultInfo.is_done)
        {
          if (!assetAsyncloader.GetAssetBundlePathList().IsNullOrEmpty())
            assetBundle_name_list.AddRange(assetAsyncloader.GetAssetBundlePathList());
        }
      }

      assetBundle_name_list.Unique();
      foreach (var assetBundle_name in assetBundle_name_list)
      {
        assetBundle_downloaded_bytes_dict[assetBundle_name] = 0;
        need_download_bytes += Client.instance.assetBundleManager.assetBundleMap.dict[assetBundle_name];
      }
    }

    public long GetNeedDownloadBytes()
    {
      return this.need_download_bytes;
    }

    public long GetDownloadedBytes()
    {
      long downloaded_bytes = 0;
      foreach (var assetBundle_name in assetBundle_downloaded_bytes_dict.Keys)
      {
        var webRequesting = Client.instance.assetBundleManager.GetAssetBundleAsyncWebRequester(assetBundle_name);
        assetBundle_downloaded_bytes_dict[assetBundle_name] =
          webRequesting?.GetDownloadedBytes() ??
          Client.instance.assetBundleManager.assetBundleMap.dict[assetBundle_name];
        downloaded_bytes += assetBundle_downloaded_bytes_dict[assetBundle_name];
      }
      return downloaded_bytes;
    }

    public float GetProgress()
    {
      if (GetNeedDownloadBytes() == 0)
        return 1;
      return (float)GetDownloadedBytes() / GetNeedDownloadBytes();
    }
  }
}