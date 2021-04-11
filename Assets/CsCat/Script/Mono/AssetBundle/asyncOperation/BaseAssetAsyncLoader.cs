using System.Collections.Generic;

namespace CsCat
{
  public abstract class BaseAssetAsyncLoader : ResourceAsyncOperation
  {
    public AssetCat assetCat { get; protected set; }

    public virtual List<string> GetAssetBundlePathList()
    {
      return new List<string>();
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      assetCat = null;
    }
  }
}