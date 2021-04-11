using System.Collections;

namespace CsCat
{
  //引用计数、不缓存,自己主动管理AB的回收unload(true)
  public partial class AssetBundleManager : TickObject
  {
    // 从服务器下载网页内容，需提供完整url，非AB（不计引用计数、不缓存），Creater使用后记得回收
    public ResourceWebRequester DownloadFileAsyncNoCache(string url)
    {
      var resourceWebRequester = PoolCatManagerUtil.Spawn<ResourceWebRequester>();
      resourceWebRequester.Init(url, true);
      resourceWebRequester_all_dict[url]= resourceWebRequester;
      resourceWebRequester_waiting_queue.Enqueue(resourceWebRequester);
      return resourceWebRequester;
    }

    // 从资源服务器下载非Assetbundle资源，非AB（不计引用计数、不缓存），Creater使用后记得回收
    public ResourceWebRequester DownloadFileAsyncNoCache(string download_url, string file_path)
    {
      var resourceWebRequester = PoolCatManagerUtil.Spawn<ResourceWebRequester>();
      var url = download_url + file_path;
      resourceWebRequester.Init(url, true);
      resourceWebRequester_all_dict[url]= resourceWebRequester;
      resourceWebRequester_waiting_queue.Enqueue(resourceWebRequester);
      return resourceWebRequester;
    }

    //max_reload_count 失败的重新load的最大次数
    public IEnumerator DownloadFileAsync(string url,int max_reload_count, int cur_reload_count=0)
    {
      var resourceWebRequester = DownloadFileAsyncNoCache(url);
      cur_reload_count++;
      yield return resourceWebRequester;
      while (cur_reload_count < max_reload_count)
      {
        if (!resourceWebRequester.error.IsNullOrWhiteSpace())
        {
          LogCat.LogError(resourceWebRequester.error);
          resourceWebRequester.Destroy();
          PoolCatManagerUtil.Despawn(resourceWebRequester);
          resourceWebRequester = DownloadFileAsyncNoCache(url);
          yield return resourceWebRequester;
        }
        cur_reload_count++;
      }
    }

    //max_reload_count 失败的重新load的最大次数
    public IEnumerator DownloadFileAsync(string download_url, string file_path, int max_reload_count, int cur_reload_count = 0)
    {
      var resourceWebRequester = DownloadFileAsyncNoCache(download_url, file_path);
      cur_reload_count++;
      yield return resourceWebRequester;
      while (cur_reload_count < max_reload_count)
      {
        if (!resourceWebRequester.error.IsNullOrWhiteSpace())
        {
          LogCat.LogError(resourceWebRequester.error);
          resourceWebRequester.Destroy();
          PoolCatManagerUtil.Despawn(resourceWebRequester);
          resourceWebRequester = DownloadFileAsyncNoCache(download_url, file_path);
          yield return resourceWebRequester;
        }
        cur_reload_count++;
      }
    }
  }
}