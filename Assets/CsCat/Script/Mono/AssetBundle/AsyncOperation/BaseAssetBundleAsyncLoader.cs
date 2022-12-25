using System.Collections.Generic;

namespace CsCat
{
	public abstract class BaseAssetBundleAsyncLoader : ResourceAsyncOperation
	{
		protected string _assetBundleName;

		protected AssetBundleCat _assetBundleCat;

		public virtual List<string> GetAssetBundlePathList()
		{
			return null;
		}

		public string GetAssetBundleName()
		{
			return this._assetBundleName;
		}

		public AssetBundleCat GetAssetBundleCat()
		{
			return this._assetBundleCat;
		}


		public override void Reset()
		{
			_assetBundleName = null;
			_assetBundleCat = null;
			base.Reset();
		}
	}
}