using UnityEngine;

namespace CsCat
{
  public class AutoAssetDestory : MonoBehaviour
  {
    private AssetCat assetCat;

    /// <summary>
    ///   只能通过这个方法添加
    /// </summary>
    /// <param name="go"></param>
    /// <param name="assetCat"></param>
    public static void Add(GameObject go, AssetCat assetCat)
    {
      assetCat.AddRefCount();
      var autoAssetDestory = go.AddComponent<AutoAssetDestory>();
      autoAssetDestory.assetCat = assetCat;
    }

    private void OnDestroy()
    {
      if (assetCat != null)
        assetCat.SubRefCount(1, true);
      else
        LogCat.LogErrorFormat("{0} destory but ont find assetCat", name);
    }
  }
}