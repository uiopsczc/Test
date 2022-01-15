using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	///   功能：Assetbundle加载器，给逻辑层使用（预加载），支持协程操作
	/// </summary>
	public class AssetBundleAsyncLoader : BaseAssetBundleAsyncLoader
	{
		protected int totalWaitingAssetBundleCatCount;

		protected Dictionary<string, AssetBundleCat> waitingAssetBundleCatDict =
			new Dictionary<string, AssetBundleCat>();

		protected Dictionary<string, long> assetBundleDownloadedBytesDict = new Dictionary<string, long>();
		protected long needDownloadBytes;


		public void Init(AssetBundleCat assetBundleCat)
		{
			this.assetBundleName = assetBundleName;
			this.assetBundleCat = assetBundleCat;
			// 只添加没有被加载过的
			if (!assetBundleCat.IsLoadDone())
			{
				waitingAssetBundleCatDict[assetBundleCat.assetBundleName] = assetBundleCat;
				assetBundleDownloadedBytesDict[assetBundleCat.assetBundleName] = 0;
			}

			for (var i = 0; i < assetBundleCat.dependenceAssetBundleCatList.Count; i++)
			{
				var dependenceAssetBundleCat = assetBundleCat.dependenceAssetBundleCatList[i];
				if (dependenceAssetBundleCat.IsLoadDone())
					continue;
				waitingAssetBundleCatDict[dependenceAssetBundleCat.assetBundleName] = dependenceAssetBundleCat;
				assetBundleDownloadedBytesDict[dependenceAssetBundleCat.assetBundleName] = 0;
			}

			totalWaitingAssetBundleCatCount = waitingAssetBundleCatDict.Count;


			needDownloadBytes = 0;
			foreach (var keyValue in assetBundleDownloadedBytesDict)
			{
				var curAssetBundleName = keyValue.Key;
				needDownloadBytes += Client.instance.assetBundleManager.assetBundleMap.dict[curAssetBundleName];
			}

			if (totalWaitingAssetBundleCatCount == 0)
			{
				resultInfo.isSuccess = true;
				return;
			}

			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Fail,
				OnResourceWebRequesterFail);
			AddListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Success,
				OnResourceWebRequesterSuccess);
		}


		protected override float GetProgress()
		{
			if (resultInfo.isDone)
				return 1.0f;

			var progressValue = (float) GetDownloadedBytes() / GetNeedDownloadBytes();
			return progressValue;
		}

		public override List<string> GetAssetBundlePathList()
		{
			List<string> result = new List<string>();
			foreach (var keyValue in assetBundleDownloadedBytesDict)
			{
				var assetBundleName = keyValue.Key;
				result.Add(assetBundleName);
			}
			return result;
		}


		public override long GetNeedDownloadBytes()
		{
			return needDownloadBytes;
		}

		public override long GetDownloadedBytes()
		{
			if (resultInfo.isDone)
				return GetNeedDownloadBytes();
			long downloadedBytes = 0;
			foreach (var keyValue in assetBundleDownloadedBytesDict)
			{
				var assetBundleName = keyValue.Key;
				bool isLoadDone = !waitingAssetBundleCatDict.ContainsKey(assetBundleName);
				if (isLoadDone) //已经下载完的assetBundle_name
					assetBundleDownloadedBytesDict[assetBundleName] =
						Client.instance.assetBundleManager.assetBundleMap.GetAssetBundleBytes(assetBundleName);
				else
				{
					var resourceWebRequester = this.waitingAssetBundleCatDict[assetBundleName].resourceWebRequester;
					assetBundleDownloadedBytesDict[assetBundleName] =
						resourceWebRequester?.GetDownloadedBytes() ??
						Client.instance.assetBundleManager.assetBundleMap.GetAssetBundleBytes(assetBundleName);
				}

				downloadedBytes += assetBundleDownloadedBytesDict[assetBundleName];
			}

			return downloadedBytes;
		}

		public override void Update()
		{
		}

		private void OnResourceWebRequesterSuccess(ResourceWebRequester resourceWebRequester)
		{
			if (resultInfo.isDone)
				return;
			if (!waitingAssetBundleCatDict.ContainsValue(resourceWebRequester.assetBundleCat) ||
			    resourceWebRequester.isNotCache) return;
			waitingAssetBundleCatDict.Remove(resourceWebRequester.assetBundleCat.assetBundleName);
			if (waitingAssetBundleCatDict.Count == 0)
				resultInfo.isSuccess = true;
		}

		private void OnResourceWebRequesterFail(ResourceWebRequester resourceWebRequester)
		{
			if (resultInfo.isDone)
				return;
			if (!waitingAssetBundleCatDict.ContainsValue(resourceWebRequester.assetBundleCat) ||
			    resourceWebRequester.isNotCache) return;
			resultInfo.isFail = true;
			RemoveListener<ResourceWebRequester>(null, AssetBundleEventNameConst.On_ResourceWebRequester_Fail,
				OnResourceWebRequesterFail);
		}


		protected override void OnSuccess()
		{
			base.OnSuccess();
			Broadcast(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Success, this);
		}

		protected override void OnFail()
		{
			base.OnFail();
			Broadcast(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Fail, this);
		}

		protected override void OnDone()
		{
			base.OnDone();
			Broadcast(null, AssetBundleEventNameConst.On_AssetBundleAsyncLoader_Done, this);
			this.Destroy();
			PoolCatManagerUtil.Despawn(this);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			totalWaitingAssetBundleCatCount = 0;
			waitingAssetBundleCatDict.Clear();
			assetBundleDownloadedBytesDict.Clear();
			needDownloadBytes = 0;
		}
	}
}