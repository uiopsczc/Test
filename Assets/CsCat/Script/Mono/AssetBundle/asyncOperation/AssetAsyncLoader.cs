

using System.Collections.Generic;

namespace CsCat
{
  public class AssetAsyncLoader : BaseAssetAsyncLoader
  {
    protected BaseAssetBundleAsyncLoader assetBundleLoader;



    //通过AssetBundleLoader获取
    public void Init(AssetCat assetCat, BaseAssetBundleAsyncLoader assetBundleLoader)
    {
      this.assetCat = assetCat;
      this.assetBundleLoader = assetBundleLoader;

      AddListener<AssetBundleAsyncLoader>(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail,
        OnAssetBundleAsyncLoaderFail);
      AddListener<AssetBundleAsyncLoader>(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success,
        OnAssetBundleAsyncLoaderSuccess);
    }


    protected override float GetProgress()
    {
      return resultInfo.is_done ? 1.0f : assetBundleLoader.progress;
    }

    public override long GetNeedDownloadBytes()
    {
      return assetBundleLoader.GetNeedDownloadBytes();
    }
    public override long GetDownloadedBytes()
    {
      return assetBundleLoader.GetDownloadedBytes();
    }

    public override List<string> GetAssetBundlePathList()
    {
      return assetBundleLoader.GetAssetBundlePathList();
    }


    public override void Update()
    {
    }

    private void OnAssetBundleAsyncLoaderSuccess(AssetBundleAsyncLoader assetBundleAsyncLoader)
    {
      if (assetBundleLoader != assetBundleAsyncLoader)
        return;
      resultInfo.is_success = true;
      RemoveListener<AssetBundleAsyncLoader>(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success,
        OnAssetBundleAsyncLoaderSuccess);
    }

    private void OnAssetBundleAsyncLoaderFail(AssetBundleAsyncLoader assetBundleAsyncLoader)
    {
      if (assetBundleLoader != assetBundleAsyncLoader)
        return;
      resultInfo.is_fail = true;
      RemoveListener<AssetBundleAsyncLoader>(AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail,
        OnAssetBundleAsyncLoaderFail);
    }

    protected override void OnSuccess()
    {
      base.OnSuccess();

      var assetBundleCat = this.assetBundleLoader.assetBundleCat;
      var assetBundle = assetBundleCat.Get();
      var assets = assetBundle.LoadAssetWithSubAssets(assetCat.asset_path);
      assetCat.SetAssets(assets);

      Broadcast(AssetBundleEventNameConst.On_AssetAsyncLoader_Success, this);
    }

    protected override void OnFail()
    {
      base.OnFail();
      Broadcast(AssetBundleEventNameConst.On_AssetAsyncLoader_Fail, this);
    }

    protected override void OnDone()
    {
      base.OnDone();
      Broadcast(AssetBundleEventNameConst.On_AssetAsyncLoader_Done, this);
    }

    //需要手动释放
    protected override void __Destroy()
    {
      base.__Destroy();
      assetBundleLoader = null;
    }
  }
}