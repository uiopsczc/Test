using System;

namespace CsCat
{
  public class EditorAssetBundleAsyncLoader : BaseAssetBundleAsyncLoader
  {
    public EditorAssetBundleAsyncLoader(string assetBundle_name)
    {
      this.assetBundle_name = assetBundle_name;
      resultInfo.isSuccess = true;
    }




    protected override float GetProgress()
    {
      return 1.0f;
    }

    public override void Update()
    {
    }

  }
}