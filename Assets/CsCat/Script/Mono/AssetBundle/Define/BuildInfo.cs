using System.Collections;

namespace CsCat
{
  public class BuildInfo
  {
    public AssetPathMap assetPathMap = new AssetPathMap();
    public AssetBundleMap assetBundleMap = new AssetBundleMap();
    public Manifest manifest = new Manifest();
    public ResourceWebRequester manifest_request;
    public string res_version;
    public string assetPathRef_content_json;
    private readonly string url_root;


    public BuildInfo(string url_root)
    {
      this.url_root = url_root;
    }

    public string WithRootPathOfUrlRoot(string cotent)
    {
      return cotent.WithRootPath(url_root);
    }

    public IEnumerator LoadResVersion()
    {
      var res_version_request_url = BuildConst.Res_Version_File_Name.WithRootPath(url_root);
      var res_version_request = Client.instance.assetBundleManager.DownloadFileAsyncNoCache(res_version_request_url);

      yield return res_version_request;
      if (res_version_request.error.IsNullOrWhiteSpace())
        res_version = res_version_request.text;
      res_version_request.Destroy();
      PoolCatManagerUtil.Despawn(res_version_request);
    }

    public IEnumerator LoadAssetPathMap()
    {
      var assetPathMap_request_url = BuildConst.AssetPathMap_File_Name.WithRootPath(url_root);
      var assetPathMap_request = Client.instance.assetBundleManager.DownloadFileAsyncNoCache(assetPathMap_request_url);
      yield return assetPathMap_request;
      if (assetPathMap_request.error.IsNullOrWhiteSpace())
        assetPathMap.Initialize(assetPathMap_request.text);
      assetPathMap_request.Destroy();
      PoolCatManagerUtil.Despawn(assetPathMap_request);
    }


    public IEnumerator LoadAssetBundleMap()
    {
      var assetBundleMap_request_url = BuildConst.AssetBundleMap_File_Name.WithRootPath(url_root);
      var assetBundleMap_request =
        Client.instance.assetBundleManager.DownloadFileAsyncNoCache(assetBundleMap_request_url);
      yield return assetBundleMap_request;
      if (assetBundleMap_request.error.IsNullOrWhiteSpace())
        assetBundleMap.Initialize(assetBundleMap_request.text);
      assetBundleMap_request.Destroy();
      PoolCatManagerUtil.Despawn(assetBundleMap_request);
    }


    public IEnumerator LoadMainfest()
    {
      var manifest_request_url = BuildConst.ManifestBundle_Path.WithRootPath(url_root);
      manifest_request = Client.instance.assetBundleManager.DownloadFileAsyncNoCache(manifest_request_url);
      yield return manifest_request;
      if (manifest_request.error.IsNullOrWhiteSpace())
      {
        manifest.LoadFromAssetbundle(manifest_request.assetBundle);
        manifest.SaveBytes(manifest_request.bytes);
        manifest_request.assetBundle.Unload(false);
      }

      manifest_request.Destroy();
      PoolCatManagerUtil.Despawn(manifest_request);
    }

    public IEnumerator LoadAssetPathRefContentJson()
    {
      var assetPathRef_content_json_request_url = AssetPathRefConst.Save_File_Name.WithRootPath(url_root);
      var assetPathRef_content_json_request_url_request =
        Client.instance.assetBundleManager.DownloadFileAsyncNoCache(assetPathRef_content_json_request_url);
      yield return assetPathRef_content_json_request_url_request;
      if (assetPathRef_content_json_request_url_request.error.IsNullOrWhiteSpace())
        assetPathRef_content_json = assetPathRef_content_json_request_url_request.text;
      assetPathRef_content_json_request_url_request.Destroy();
      PoolCatManagerUtil.Despawn(assetPathRef_content_json_request_url_request);
    }

    public void Dispose()
    {
    }
  }
}