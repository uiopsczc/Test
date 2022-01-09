using System.Collections.Generic;

namespace CsCat
{
	public abstract class BaseAssetBundleAsyncLoader : ResourceAsyncOperation
	{
		public string assetBundle_name { get; protected set; }

		public AssetBundleCat assetBundleCat { get; set; }

		public virtual List<string> GetAssetBundlePathList()
		{
			return null;
		}

		protected override void _Destroy()
		{
			base._Destroy();
			assetBundle_name = null;
			assetBundleCat = null;
		}
	}
}