using System.Collections;

namespace CsCat
{
	public class BuildInfo
	{
		public AssetPathMap assetPathMap = new AssetPathMap();
		public AssetBundleMap assetBundleMap = new AssetBundleMap();
		public Manifest manifest = new Manifest();
		public ResourceWebRequester manifestRequest;
		public string resVersion;
		public string assetPathRefContentJson;
		private readonly string urlRoot;


		public BuildInfo(string urlRoot)
		{
			this.urlRoot = urlRoot;
		}

		public string WithRootPathOfUrlRoot(string content)
		{
			return content.WithRootPath(urlRoot);
		}

		public IEnumerator LoadResVersion()
		{
			var resVersionRequestUrl = BuildConst.ResVersionFileName.WithRootPath(urlRoot);
			var resVersionRequest = Client.instance.assetBundleManager.DownloadFileAsyncNoCache(resVersionRequestUrl);

			yield return resVersionRequest;
			if (resVersionRequest.error.IsNullOrWhiteSpace())
				resVersion = resVersionRequest.text;
			resVersionRequest.Destroy();
			PoolCatManagerUtil.Despawn(resVersionRequest);
		}

		public IEnumerator LoadAssetPathMap()
		{
			var assetPathMapRequestUrl = BuildConst.AssetPathMap_File_Name.WithRootPath(urlRoot);
			var assetPathMapRequest = Client.instance.assetBundleManager.DownloadFileAsyncNoCache(assetPathMapRequestUrl);
			yield return assetPathMapRequest;
			if (assetPathMapRequest.error.IsNullOrWhiteSpace())
				assetPathMap.Initialize(assetPathMapRequest.text);
			assetPathMapRequest.Destroy();
			PoolCatManagerUtil.Despawn(assetPathMapRequest);
		}


		public IEnumerator LoadAssetBundleMap()
		{
			var assetBundleMapRequestURL = BuildConst.AssetBundleMap_File_Name.WithRootPath(urlRoot);
			var assetBundleMapRequest =
			  Client.instance.assetBundleManager.DownloadFileAsyncNoCache(assetBundleMapRequestURL);
			yield return assetBundleMapRequest;
			if (assetBundleMapRequest.error.IsNullOrWhiteSpace())
				assetBundleMap.Initialize(assetBundleMapRequest.text);
			assetBundleMapRequest.Destroy();
			PoolCatManagerUtil.Despawn(assetBundleMapRequest);
		}


		public IEnumerator LoadManifest()
		{
			var manifestRequestURL = BuildConst.ManifestBundle_Path.WithRootPath(urlRoot);
			manifestRequest = Client.instance.assetBundleManager.DownloadFileAsyncNoCache(manifestRequestURL);
			yield return manifestRequest;
			if (manifestRequest.error.IsNullOrWhiteSpace())
			{
				manifest.LoadFromAssetBundle(manifestRequest.assetBundle);
				manifest.SaveBytes(manifestRequest.bytes);
				manifestRequest.assetBundle.Unload(false);
			}

			manifestRequest.Destroy();
			PoolCatManagerUtil.Despawn(manifestRequest);
		}

		public IEnumerator LoadAssetPathRefContentJson()
		{
			var assetPathRefContentJsonRequestURL = AssetPathRefConst.SaveFileName.WithRootPath(urlRoot);
			var assetPathRefContentJsonRequestURLRequest =
			  Client.instance.assetBundleManager.DownloadFileAsyncNoCache(assetPathRefContentJsonRequestURL);
			yield return assetPathRefContentJsonRequestURLRequest;
			if (assetPathRefContentJsonRequestURLRequest.error.IsNullOrWhiteSpace())
				assetPathRefContentJson = assetPathRefContentJsonRequestURLRequest.text;
			assetPathRefContentJsonRequestURLRequest.Destroy();
			PoolCatManagerUtil.Despawn(assetPathRefContentJsonRequestURLRequest);
		}

		public void Dispose()
		{
		}
	}
}