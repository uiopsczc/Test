using System.Collections.Generic;

namespace CsCat
{
	public abstract class BaseAssetAsyncLoader : ResourceAsyncOperation
	{
		protected AssetCat _assetCat;

		public virtual List<string> GetAssetBundlePathList()
		{
			return new List<string>();
		}

		public AssetCat GetAssetCat()
		{
			return this._assetCat;
		}


		public override void Reset()
		{
			_assetCat = null;
			base.Reset();
		}
	}
}