using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AssetBundleUpdater : TickObject
	{
		private static readonly int _Max_DownLoad_Num = 5;
		private readonly List<ResourceWebRequester> _downloadingRequestList = new List<ResourceWebRequester>();

		private BuildInfo _clientBuildInfo;

		//is_finish,total_bytes,downloded_bytes,
		public Dictionary<string, Hashtable> needDownloadDict = new Dictionary<string, Hashtable>();
		private long _totalNeedDownloadBytes;

		public bool isUpdateFinish;

		private bool _isUpdatingRes;
		private List<string> _needDownloadList = new List<string>();
		private BuildInfo _serverBuildInfo;

		public override void Init()
		{
			base.Init();
			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Done,
				_OnResourceWebRequesterDone);
		}

		public IEnumerator CheckUpdate()
		{
			if (Application.isEditor && EditorModeConst.IsEditorMode)
			{
				isUpdateFinish = true;
				yield break;
			}

			_downloadingRequestList.Clear();
			_clientBuildInfo = new BuildInfo(FilePathConst.PersistentAssetBundleRoot);
			_serverBuildInfo = new BuildInfo(URLSetting.Server_Resource_URL);
			//    LogCat.LogWarning("fffffffffffffff1:"+ FilePathConst.GetPersistentAssetBundleRoot());
			//    LogCat.LogWarning("fffffffffffffff2:"+ URLSetting.SERVER_RESOURCE_URL);

			//Update  AssetPathRefContentJson
			yield return _clientBuildInfo.LoadAssetPathRefContentJson();
			yield return _serverBuildInfo.LoadAssetPathRefContentJson();
			if (!ObjectUtil.Equals(_clientBuildInfo.assetPathRefContentJson,
				_serverBuildInfo.assetPathRefContentJson))
				StdioUtil.WriteTextFile(_clientBuildInfo.WithRootPathOfUrlRoot(AssetPathRefConst.SaveFileName).Trim(),
					_serverBuildInfo.assetPathRefContentJson);
			AssetPathRefManager.instance.Load(_serverBuildInfo.assetPathRefContentJson);


			//Update ResVersion
			yield return _clientBuildInfo.LoadResVersion();
			yield return _serverBuildInfo.LoadResVersion();
			if (!BuildUtil.CheckResVersionIsNew(_clientBuildInfo.resVersion, _serverBuildInfo.resVersion))
			{
				_UpdateResFinish();
				yield break;
			}

			//Update Mainfest
			yield return _clientBuildInfo.LoadManifest();
			yield return _serverBuildInfo.LoadManifest();

			yield return _serverBuildInfo.LoadAssetBundleMap();

			_needDownloadList.Clear();
			_needDownloadList =
				BuildUtil.GetManifestDiffAssetBundleList(_clientBuildInfo.manifest, _serverBuildInfo.manifest);
			if (!_needDownloadList.IsNullOrEmpty())
			{
				for (var i = 0; i < _needDownloadList.Count; i++)
				{
					var assetBundleName = _needDownloadList[i];
					needDownloadDict[assetBundleName] = new Hashtable();
					needDownloadDict[assetBundleName]["is_finished"] = false;
					needDownloadDict[assetBundleName]["total_bytes"] =
						_serverBuildInfo.assetBundleMap.dict[assetBundleName];
					needDownloadDict[assetBundleName]["downloded_bytes"] = (long) 0;
					_totalNeedDownloadBytes += _serverBuildInfo.assetBundleMap.dict[assetBundleName];
				}

				yield return _serverBuildInfo.LoadAssetPathMap();

				yield return _UpdateRes();
				_serverBuildInfo.manifest.SaveToDisk();
				_serverBuildInfo.assetPathMap.SaveToDisk();
				_serverBuildInfo.assetBundleMap.SaveToDisk();
			}

			_UpdateResFinish();
		}


		private IEnumerator _UpdateRes()
		{
			_isUpdatingRes = true;
			yield return new WaitUntil(() => _isUpdatingRes == false);
		}

		private void _UpdateResFinish()
		{
			StdioUtil.WriteTextFile(BuildConst.ResVersionFileName.WithRootPath(FilePathConst.PersistentAssetBundleRoot),
				_serverBuildInfo.resVersion);
			_clientBuildInfo.Dispose();
			_serverBuildInfo.Dispose();
			isUpdateFinish = true;
			Debug.LogWarning("Update Resource Finish");
		}


		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base.Update(deltaTime, unscaledDeltaTime);
			//LogCat.LogWarning(isUpdatingRes);
			if (!_isUpdatingRes)
				return;

			while (_downloadingRequestList.Count < _Max_DownLoad_Num && _needDownloadList.Count > 0)
			{
				var filePath = _needDownloadList[_needDownloadList.Count - 1];
				_needDownloadList.RemoveAt(_needDownloadList.Count - 1);
				var resourceWebRequester =
					Client.instance.assetBundleManager.DownloadFileAsyncNoCache(URLSetting.Server_Resource_URL,
						filePath);
				resourceWebRequester._cache["file_path"] = filePath;
				_downloadingRequestList.Add(resourceWebRequester);
			}

			for (var i = 0; i < _downloadingRequestList.Count; i++)
			{
				var downloadingRequest = _downloadingRequestList[i];
				needDownloadDict[downloadingRequest._cache.Get<string>("file_path")]["downloded_bytes"] =
					downloadingRequest.GetDownloadedBytes();
			}

			long currentDownloadedBytes = 0;
			foreach (var keyValue in needDownloadDict)
			{
				var download = keyValue.Value;
				currentDownloadedBytes += download.GetOrGetDefault2<long>("downloded_bytes", () => (long) 0);
			}

			Client.instance.uiManager.uiLoadingPanel.SetDesc(string.Format("{0}/{1}",
				NumberUnitUtil.GetString(currentDownloadedBytes, 2, 1000),
				NumberUnitUtil.GetString(_totalNeedDownloadBytes, 1, 1000)));
		}

		private void _OnResourceWebRequesterDone(ResourceWebRequester resourceWebRequester)
		{
			//    LogCat.LogError("kkkkkkkkkkkkkkk:"+resourceWebRequester.url);
			if (!_downloadingRequestList.Contains(resourceWebRequester))
				return;

			if (!resourceWebRequester.error.IsNullOrWhiteSpace())
			{
				LogCat.LogError("Error when downloading file : " + resourceWebRequester._cache.Get<string>("file_path") +
				                "\n from url : " +
				                resourceWebRequester._url + "\n err : " + resourceWebRequester.error);
				_needDownloadList.Add(resourceWebRequester._cache.Get<string>("file_path"));
			}
			else
			{
				_downloadingRequestList.Remove(resourceWebRequester);
				needDownloadDict[resourceWebRequester._cache.Get<string>("file_path")]["is_finished"] = true;
				needDownloadDict[resourceWebRequester._cache.Get<string>("file_path")]["downloded_bytes"] =
					needDownloadDict[resourceWebRequester._cache.Get<string>("file_path")]["total_bytes"];
				var filePath = resourceWebRequester._cache.Get<string>("file_path")
					.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
				StdioUtil.WriteFile(filePath, resourceWebRequester.bytes);
			}

			resourceWebRequester.Destroy();
			PoolCatManagerUtil.Despawn(resourceWebRequester);

			//    LogCat.LogError("ffffffffffffaaaaaaa:"+downloadingRequest.Count);
			//    LogCat.LogError("ffffffffffffbbbbbbb:" + needDownloadList.Count);
			if (_downloadingRequestList.Count == 0 && _needDownloadList.Count == 0)
				_isUpdatingRes = false;
		}
	}
}