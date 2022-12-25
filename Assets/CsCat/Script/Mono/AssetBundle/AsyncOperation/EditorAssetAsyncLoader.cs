namespace CsCat
{
	public class EditorAssetAsyncLoader : BaseAssetAsyncLoader
	{
		public EditorAssetAsyncLoader(AssetCat assetCat)
		{
			this._assetCat = assetCat;
			resultInfo.isSuccess = true;
		}

		public override void Update()
		{
		}


		protected override float _GetProgress()
		{
			return 1.0f;
		}
	}
}