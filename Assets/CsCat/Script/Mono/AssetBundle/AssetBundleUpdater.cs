using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AssetBundleUpdater : TickObject
	{
		private static readonly int Max_DownLoad_Num = 5;
		private readonly List<ResourceWebRequester> downloading_request_list = new List<ResourceWebRequester>();

		private BuildInfo client_buildInfo;

		//is_finish,total_bytes,downloded_bytes,
		public Dictionary<string, Hashtable> need_download_dict = new Dictionary<string, Hashtable>();
		private long total_need_download_bytes;

		public bool is_update_finish;

		private bool is_updating_res;
		private List<string> need_download_list = new List<string>();
		private BuildInfo server_buildInfo;

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
				is_update_finish = true;
				yield break;
			}

			downloading_request_list.Clear();
			client_buildInfo = new BuildInfo(FilePathConst.PersistentAssetBundleRoot);
			server_buildInfo = new BuildInfo(URLSetting.Server_Resource_URL);
			//    LogCat.LogWarning("fffffffffffffff1:"+ FilePathConst.GetPersistentAssetBundleRoot());
			//    LogCat.LogWarning("fffffffffffffff2:"+ URLSetting.SERVER_RESOURCE_URL);

			//Update  AssetPathRefContentJson
			yield return client_buildInfo.LoadAssetPathRefContentJson();
			yield return server_buildInfo.LoadAssetPathRefContentJson();
			if (!ObjectUtil.Equals(client_buildInfo.assetPathRef_content_json, server_buildInfo.assetPathRef_content_json))
				StdioUtil.WriteTextFile(client_buildInfo.WithRootPathOfUrlRoot(AssetPathRefConst.SaveFileName).Trim(),
				  server_buildInfo.assetPathRef_content_json);
			AssetPathRefManager.instance.Load(server_buildInfo.assetPathRef_content_json);


			//Update ResVersion
			yield return client_buildInfo.LoadResVersion();
			yield return server_buildInfo.LoadResVersion();
			if (!BuildUtil.CheckResVersionIsNew(client_buildInfo.res_version, server_buildInfo.res_version))
			{
				UpdateResFinish();
				yield break;
			}

			//Update Mainfest
			yield return client_buildInfo.LoadMainfest();
			yield return server_buildInfo.LoadMainfest();

			yield return server_buildInfo.LoadAssetBundleMap();

			need_download_list.Clear();
			need_download_list =
			  BuildUtil.GetManifestDiffAssetBundleList(client_buildInfo.manifest, server_buildInfo.manifest);
			if (!need_download_list.IsNullOrEmpty())
			{
				foreach (var assetBundle_name in need_download_list)
				{
					need_download_dict[assetBundle_name] = new Hashtable();
					need_download_dict[assetBundle_name]["is_finished"] = false;
					need_download_dict[assetBundle_name]["total_bytes"] = server_buildInfo.assetBundleMap.dict[assetBundle_name];
					need_download_dict[assetBundle_name]["downloded_bytes"] = (long)0;
					total_need_download_bytes += server_buildInfo.assetBundleMap.dict[assetBundle_name];
				}

				yield return server_buildInfo.LoadAssetPathMap();

				yield return UpdateRes();
				server_buildInfo.manifest.SaveToDisk();
				server_buildInfo.assetPathMap.SaveToDisk();
				server_buildInfo.assetBundleMap.SaveToDisk();
			}

			UpdateResFinish();
		}


		private IEnumerator UpdateRes()
		{
			is_updating_res = true;
			yield return new WaitUntil(() => is_updating_res == false);
		}

		private void UpdateResFinish()
		{
			StdioUtil.WriteTextFile(BuildConst.ResVersionFileName.WithRootPath(FilePathConst.PersistentAssetBundleRoot),
			  server_buildInfo.res_version);
			client_buildInfo.Dispose();
			server_buildInfo.Dispose();
			is_update_finish = true;
			Debug.LogWarning("Update Resource Finish");
		}


		public override void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base.Update(deltaTime, unscaledDeltaTime);
			//LogCat.LogWarning(isUpdatingRes);
			if (!is_updating_res)
				return;

			while (downloading_request_list.Count < Max_DownLoad_Num && need_download_list.Count > 0)
			{
				var file_path = need_download_list[need_download_list.Count - 1];
				need_download_list.RemoveAt(need_download_list.Count - 1);
				var resourceWebRequester =
				  Client.instance.assetBundleManager.DownloadFileAsyncNoCache(URLSetting.Server_Resource_URL, file_path);
				resourceWebRequester.cache["file_path"] = file_path;
				downloading_request_list.Add(resourceWebRequester);
			}

			foreach (var downloading_request in downloading_request_list)
				need_download_dict[downloading_request.cache.Get<string>("file_path")]["downloded_bytes"] = downloading_request.GetDownloadedBytes();

			long current_downloaded_bytes = 0;
			foreach (var download in need_download_dict.Values)
				current_downloaded_bytes += download.GetOrGetDefault2<long>("downloded_bytes", () => (long)0);
			Client.instance.uiManager.uiLoadingPanel.SetDesc(string.Format("{0}/{1}",
			  NumberUnitUtil.GetString(current_downloaded_bytes, 2, 1000),
			  NumberUnitUtil.GetString(total_need_download_bytes, 1, 1000)));
		}

		private void OnResourceWebRequesterDone(ResourceWebRequester resourceWebRequester)
		{
			//    LogCat.LogError("kkkkkkkkkkkkkkk:"+resourceWebRequester.url);
			if (!downloading_request_list.Contains(resourceWebRequester))
				return;

			if (!resourceWebRequester.error.IsNullOrWhiteSpace())
			{
				LogCat.LogError("Error when downloading file : " + resourceWebRequester.cache.Get<string>("file_path") + "\n from url : " +
								resourceWebRequester.url + "\n err : " + resourceWebRequester.error);
				need_download_list.Add(resourceWebRequester.cache.Get<string>("file_path"));
			}
			else
			{
				downloading_request_list.Remove(resourceWebRequester);
				need_download_dict[resourceWebRequester.cache.Get<string>("file_path")]["is_finished"] = true;
				need_download_dict[resourceWebRequester.cache.Get<string>("file_path")]["downloded_bytes"] =
				  need_download_dict[resourceWebRequester.cache.Get<string>("file_path")]["total_bytes"];
				var filePath = resourceWebRequester.cache.Get<string>("file_path").WithRootPath(FilePathConst.PersistentAssetBundleRoot);
				StdioUtil.WriteFile(filePath, resourceWebRequester.bytes);
			}

			resourceWebRequester.Destroy();
			PoolCatManagerUtil.Despawn(resourceWebRequester);

			//    LogCat.LogError("ffffffffffffaaaaaaa:"+downloadingRequest.Count);
			//    LogCat.LogError("ffffffffffffbbbbbbb:" + needDownloadList.Count);
			if (downloading_request_list.Count == 0 && need_download_list.Count == 0)
				is_updating_res = false;
		}
	}
}