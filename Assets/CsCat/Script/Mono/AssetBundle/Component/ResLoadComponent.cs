using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace CsCat
{
  public class ResLoadComponent : GameComponent
  {
    public ResLoad resLoad;
    public void Init(ResLoad resLoad)
    {
      base.Init();
      this.resLoad = resLoad;
    }

    public bool IsAllLoadDone()
    {
      return this.resLoad.IsAllLoadDone();
    }

    public IEnumerator IEIsAllLoadDone(Action on_all_load_done_callback = null)
    {
      yield return this.resLoad.IEIsAllLoadDone(on_all_load_done_callback);
    }

    public void CheckIsAllLoadDone(Action on_all_load_done_callback = null)
    {
      this.StartCoroutine(IEIsAllLoadDone(on_all_load_done_callback));
    }



    // 加载某个资源
    public AssetCat GetOrLoadAsset(string asset_path, Action<AssetCat> on_load_success_callback = null, Action<AssetCat> on_load_fail_callback = null, Action<AssetCat> on_load_done_callback = null, object callback_cause = null)
    {
      return this.resLoad.GetOrLoadAsset(asset_path, on_load_success_callback, on_load_fail_callback, on_load_done_callback, callback_cause);
    }


    public void CancelLoadCallback(AssetCat assetCat, object callback_cause = null)
    {
      this.resLoad.CancelLoadCallback(assetCat, callback_cause);
    }

    public void CancelLoadAllCallbacks(AssetCat assetCat)
    {
      this.resLoad.CancelLoadAllCallbacks(assetCat);
    }

    protected override void __Reset()
    {
      base.__Reset();
      this.resLoad.Reset();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      this.resLoad.Destroy();
    }


  }
}