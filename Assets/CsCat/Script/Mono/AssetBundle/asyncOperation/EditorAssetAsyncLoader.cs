namespace CsCat
{
  public class EditorAssetAsyncLoader : BaseAssetAsyncLoader
  {
    public EditorAssetAsyncLoader(AssetCat assetCat)
    {
      this.assetCat = assetCat;
      resultInfo.is_success = true;
    }

    public override void Update()
    {
    }


    protected override float GetProgress()
    {
      return 1.0f;
    }
  }
}