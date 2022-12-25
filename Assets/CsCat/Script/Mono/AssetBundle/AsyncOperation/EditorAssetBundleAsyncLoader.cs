using System;

namespace CsCat
{
	public class EditorAssetBundleAsyncLoader : BaseAssetBundleAsyncLoader
	{
		public EditorAssetBundleAsyncLoader(string assetBundleName)
		{
			this._assetBundleName = assetBundleName;
			resultInfo.isSuccess = true;
		}




		protected override float _GetProgress()
		{
			return 1.0f;
		}

		public override void Update()
		{
		}

	}
}