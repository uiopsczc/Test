using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AssetBundleUpdater : TickObject
	{
		private static readonly int Max_DownLoad_Num = 5;
		private readonly List<ResourceWebRequester> downloadingRequestList = new List<ResourceWebRequester>();

		private BuildInfo clientBuildInfo;

		//is_finish,total_bytes,downloded_bytes,
		public Dictionary<string, Hashtable> needDownloadDict = new Dictionary<string, Hashtable>();
		private long totalNeedDownloadBytes;

		public bool isUpdateFinish;

		private bool isUpdatingRes;
		private List<string> needDownloadList = new List<string>();
		private BuildInfo serverBuildInfo;

		public override void Init()
		{
			base.Init();
			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Done,
				OnResourceWebRequesterDone);
		}

		public IEnumerator CheckUpdate()
		{
			if (Application.isEditor && EditorModeConst.IsEditorMode)
			{
				isUpdateFinish = true;
				yield break;
			}

			downloadingRequestList.Clear();
			clientBuildInfo = new BuildInfo(FilePathConst.PersistentAssetBundleRoot);
			serverBuildInfo = new BuildInfo(URLSetting.Server_Resource_URL);
			//    LogCat.LogWarning("fffffffffffffff1:"+ FilePathConst.GetPersistentAssetBundleRoot());
			//    LogCat.LogWarning("fffffffffffffff2:"+ URLSetting.SERVER_RESOURCE_URL);

			//Update  AssetPathRefContentJson
			yield return clientBuildInfo.LoadAssetPathRefContentJson();
			yield return serverBuildInfo.LoadAssetPathRefContentJson();
			if (!ObjectUtil.Equals(clientBuildInfo.assetPathRefContentJson,
				serverBuildInfo.assetPathRefContentJson))
				StdioUtil.WriteTextFile(clientBuildInfo.WithRootPathOfUrlRoot(AssetPathRefConst.SaveFileName).Trim(),
					serverBuildInfo.assetPathRefContentJson);
			AssetPathRefManager.instance.Load(serverBuildInfo.assetPathRefContentJson);


			//Update ResVersion
			yield return clientBuildInfo.LoadResVersion();
			yield return serverBuildInfo.LoadResVersion();
			if (!BuildUtil.CheckResVersionIsNew(clientBuildInfo.resVersion, serverBuildInfo.resVersion))
			{
				UpdateResFinish();
				yield break;
			}

			//Update Mainfest
			yield return clientBuildInfo.LoadManifest();
			yield return serverBuildInfo.LoadManifest();

			yield return serverBuildInfo.LoadAssetBundleMap();

			needDownloadList.Clear();
			needDownloadList =
				BuildUtil.GetManifestDiffAssetBundleList(clientBuildInfo.manifest, serverBuildInfo.manifest);
			if (!needDownloadList.IsNullOrEmpty())
			{
				for (var i = 0; i < needDownloadList.Count; i++)
				{
					var assetBundleName = needDownloadList[i];
					needDownloadDict[assetBundleName] = new Hashtable();
					needDownloadDict[assetBundleName]["is_finished"] = false;
					needDownloadDict[assetBundleName]["total_bytes"] =
						serverBuildInfo.assetBundleMap.dict[assetBundleName];
					needDownloadDict[assetBundleName]["downloded_bytes"] = (long) 0;
					totalNeedDownloadBytes += serverBuildInfo.assetBundleMap.dict[assetBundleName];
				}

				yield return serverBuildInfo.LoadAssetPathMap();

				yield return UpdateRes();
				serverBuildInfo.manifest.SaveToDisk();
				serverBuildInfo.assetPathMap.SaveToDisk();
				serverBuildInfo.assetBundleMap.SaveToDisk();
			}

			UpdateResFinish();
		}


		private IEnumerator UpdateRes()
		{
			isUpdatingRes = true;
			yield return new WaitUntil(() => isUpdatingRes == false);
		}

		private void UpdateResFinish()
		{
			StdioUtil.WriteTextFile(BuildConst.ResVersionFileName.WithRootPath(FilePathConst.PersistentAssetBundleRoot),
				serverBuildInfo.resVersion);
			clientBuildInfo.Dispose();
			serverBuildInfo.Dispose();
			isUpdateFinish = true;
			Debug.LogWarning("Update Resource Finish");
		}


		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base.Update(deltaTime, unscaledDeltaTime);
			//LogCat.LogWarning(isUpdatingRes);
			if (!isUpdatingRes)
				return;

			while (downloadingRequestList.Count < Max_DownLoad_Num && needDownloadList.Count > 0)
			{
				var filePath = needDownloadList[needDownloadList.Count - 1];
				needDownloadList.RemoveAt(needDownloadList.Count - 1);
				var resourceWebRequester =
					Client.instance.assetBundleManager.DownloadFileAsyncNoCache(URLSetting.Server_Resource_URL,
						filePath);
				resourceWebRequester._cache["file_path"] = filePath;
				downloadingRequestList.Add(resourceWebRequester);
			}

			for (var i = 0; i < downloadingRequestList.Count; i++)
			{
				var downloadingRequest = downloadingRequestList[i];
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
				NumberUnitUtil.GetString(totalNeedDownloadBytes, 1, 1000)));
		}

		private void OnResourceWebRequesterDone(ResourceWebRequester resourceWebRequester)
		{
			//    LogCat.LogError("kkkkkkkkkkkkkkk:"+resourceWebRequester.url);
			if (!downloadingRequestList.Contains(resourceWebRequester))
				return;

			if (!resourceWebRequester.error.IsNullOrWhiteSpace())
			{
				LogCat.LogError("Error when downloading file : " + resourceWebRequester._cache.Get<string>("file_path") +
				                "\n from url : " +
				                resourceWebRequester.url + "\n err : " + resourceWebRequester.error);
				needDownloadList.Add(resourceWebRequester._cache.Get<string>("file_path"));
			}
			else
			{
				downloadingRequestList.Remove(resourceWebRequester);
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
			if (downloadingRequestList.Count == 0 && needDownloadList.Count == 0)
				isUpdatingRes = false;
		}
	}
}